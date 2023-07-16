using Azure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using noteCodeAPI.DTOs;
using noteCodeAPI.Exceptions;
using noteCodeAPI.Models;
using noteCodeAPI.Models.Enums;
using noteCodeAPI.Models.Interfaces;
using noteCodeAPI.Repositories;
using noteCodeAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace noteCodeAPI.Services
{
    public class UserAppService
    {
        private UserAppRepository _userRepos;
        private IHttpContextAccessor? _httpContextAccessor;
        private UnusedActiveTokenRepository _unusedTokenRepos;
        private IPasswordHasher _passwordHasher;
        private WaitingUserRepository _waitingUserRepos;
 

        public UserAppService(UserAppRepository userRepos, IHttpContextAccessor httpContextAccessor, IAuthentication login, UnusedActiveTokenRepository unusedTokenRepos, IPasswordHasher passwordHasher, WaitingUserRepository waitingUserRepos)
        {
            _userRepos = userRepos;
            _httpContextAccessor = httpContextAccessor;
            _unusedTokenRepos = unusedTokenRepos;
            _passwordHasher = passwordHasher;
            _waitingUserRepos = waitingUserRepos;
        }

        public async Task<UserApp> GetLoggedUserAsync()
        {
            try
            {
                var userClaims = _httpContextAccessor.HttpContext.User.Claims;
                int userId = int.Parse(userClaims.FirstOrDefault(c => c.Type == "id").Value);
                return await _userRepos.GetByIdAsync(userId);
            }
            catch (Exception ex)
            {
                return null;
            };
        }

        public async Task<IToken> GetCurrentTokenInfosAsync()
        {
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (token != null && token != "")
            {
                JwtSecurityToken jwtToken = new JwtSecurityToken(token);
                Token tokenResponse = new()
                {
                    JwtToken = token,
                    User = await GetLoggedUserAsync(),
                    ExpirationDate = jwtToken.ValidTo
                };
                return tokenResponse;
            } return null;

        }

        private async Task<string> BanCurrentTokenAsync()
        {
            Token currentToken = (Token)await GetCurrentTokenInfosAsync();
            if (currentToken != null)
            {
                UnusedActiveToken unusedActiveToken = new()
                {
                    JwtToken = currentToken.JwtToken,
                    ExpirationDate = currentToken.ExpirationDate
                };
                if (await _unusedTokenRepos.SaveAsync(unusedActiveToken))
                {
                    return unusedActiveToken.JwtToken;
                }
                throw new DatabaseException();
            } throw new NotFoundUserException();              
        }

        public async Task<bool> SignUpAsync(UserRequestDTO userRequest)
        {
            UserApp userFound = await _userRepos.SearchByNameAsync(userRequest.Username);
            if (userFound == null)
            {
                string passwordSalt = _passwordHasher.GenerateSalt();
                UserApp newUser = new()
                {
                    Username = userRequest.Username,
                    PasswordSalt = passwordSalt,
                    PasswordHashed = _passwordHasher.GenerateHashPassword(userRequest.Password, passwordSalt),
                    Role = Role.User
                };
                if (await _userRepos.SaveAsync(newUser))
                {
                    return true;
                }
                else throw new DatabaseException();
            }
            else throw new SameUsernameException();
        }

        public async Task SignOutAsync()
        {
            await BanCurrentTokenAsync();
            _httpContextAccessor.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());
        }

        public async Task<bool> AddToWaitingUsersAsync(UserRequestDTO userRequest)
        {
            UserApp userFound = await _userRepos.SearchByNameAsync(userRequest.Username);
            if (userFound == null)
            {
                string passwordSalt = _passwordHasher.GenerateSalt();
                WaitingUser newUser = new()
                {
                    Username = userRequest.Username,
                    PasswordSalt = passwordSalt,
                    PasswordHashed = _passwordHasher.GenerateHashPassword(userRequest.Password, passwordSalt),
                };
                if (await _waitingUserRepos.SaveAsync(newUser))
                {
                    return true;
                }
                else throw new DatabaseException();
            }
            else throw new SameUsernameException();
        }

        public async Task<List<WaitingUser>> GetAllWaitingUsersAsync()
        {
            return await _waitingUserRepos.GetAllAsync();
        }
        public async Task<bool> WhitelistUserAsync(int userId)
        {
            var waitingUser = await _waitingUserRepos.GetByIdAsync(userId);
            if(waitingUser != null)
            {
                UserApp newUser = new()
                {
                    Username = waitingUser.Username,
                    PasswordSalt = waitingUser.PasswordSalt,
                    PasswordHashed = waitingUser.PasswordHashed,
                    Role = Role.User
                };
              
                if(await _userRepos.SaveAsync(newUser))
                {
                    await _waitingUserRepos.DeleteAsync(waitingUser);
                    return true;
                }
            }
            throw new DatabaseException();
        }

        public async Task<bool> EditLoggedUserAsync(UserRequestDTO userRequest)
        {
            UserApp user = await GetLoggedUserAsync();

            if (user != null)
            {
                string salt = _passwordHasher.GenerateSalt();
                user.Username = userRequest.Username;
                user.PasswordHashed = _passwordHasher.GenerateHashPassword(userRequest.Password, salt);
                user.PasswordSalt = salt;
                if (await _userRepos.UpdateAsync())
                {
                    return true;
                }
                else throw new DatabaseException();
            }
            else throw new NotFoundUserException();

        }
        
    }
}
