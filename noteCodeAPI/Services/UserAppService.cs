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
 

        public UserAppService(UserAppRepository userRepos, IHttpContextAccessor httpContextAccessor, IAuthentication login, UnusedActiveTokenRepository unusedTokenRepos, IPasswordHasher passwordHasher)
        {
            _userRepos = userRepos;
            _httpContextAccessor = httpContextAccessor;
            _unusedTokenRepos = unusedTokenRepos;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserApp> GetLoggedUserAsync()
        {
            try
            {
                var userClaims = _httpContextAccessor.HttpContext.User.Claims;
                if (userClaims != null)
                {
                    int userId = int.Parse(userClaims.FirstOrDefault(c => c.Type == "id").Value);
                    return await _userRepos.GetByIdAsync(userId);
                }
                else throw new NotFoundException("User not found.");
            }
            catch (Exception ex)
            {
                return null;
            };
        }

        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            var allUsers = await _userRepos.GetAllAsync();
            if(allUsers != null)
                return allUsers.Select(u => new UserResponseDTO() { Id = u.Id, Username = u.Username, Email = u.Email, Role = u.Role.ToString()}).ToList();
            throw new DatabaseException();
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

        private async Task<bool> BanCurrentTokenAsync()
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
                    return true;
                }
                throw new DatabaseException();
            } throw new NotFoundException("User not found.");              
        }


        public async Task<bool> SignOutAsync()
        {
            if (await BanCurrentTokenAsync())
                return true;
            return false;
        }

        public async Task<UserResponseDTO> RequestAccessAsync(UserRequestDTO userRequest)
        {
            UserApp userFound = await _userRepos.SearchByNameAsync(userRequest.Username);
            if (userFound == null)
            {
                string passwordSalt = _passwordHasher.GenerateSalt();
                UserApp newUser = new()
                {
                    Username = userRequest.Username,
                    Email = userRequest.Email,
                    PasswordSalt = passwordSalt,
                    PasswordHashed = _passwordHasher.GenerateHashPassword(userRequest.Password, passwordSalt),
                    Role = Role.User,
                    IsValid = false
                };
                if (await _userRepos.SaveAsync(newUser))
                {
                    var waitingUserAdded = await _userRepos.SearchByNameAsync(newUser.Username);
                    return new UserResponseDTO()
                    {
                        Id = waitingUserAdded.Id,
                        Username = waitingUserAdded.Username,
                        Email = waitingUserAdded.Email,
                        Role = waitingUserAdded.Role.ToString()
                    };

                }
                else throw new DatabaseException();
            }
            else throw new SameUsernameException();
        }

        public async Task<List<UserResponseDTO>> GetAllWaitingUsersAsync()
        {
            var allWaitingUsers = await _userRepos.GetWaitingUsers();
            if (allWaitingUsers != null)
                return allWaitingUsers.Select(wu => new UserResponseDTO() { Id = wu.Id, Username = wu.Username, Email = wu.Email }).ToList();
            throw new DatabaseException();
        }

        public async Task<bool> GetUserStatusAsync(int userId)
        {
            var user = await _userRepos.GetByIdAsync(userId);
            if(user != null)
            {
                return user.IsValid;
            } throw new DatabaseException();
        }

        public async Task<bool> WhitelistUserAsync(int userId)
        {
            var waitingUser = await _userRepos.GetByIdAsync(userId);
            if(waitingUser != null)
            {
                waitingUser.IsValid = true;
              
                if(await _userRepos.UpdateAsync())
                {
                    return true;
                }
                throw new DatabaseException();
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
                user.Email = userRequest.Email;
                if (await _userRepos.UpdateAsync())
                {
                    return true;
                }
                else throw new DatabaseException();
            }
            else throw new NotFoundException("User not found.");

        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            UserApp userToDelete = await _userRepos.GetByIdAsync(id);
            if (userToDelete != null)
            {
                if (await _userRepos.DeleteAsync(userToDelete))
                {
                    return true;
                }
                else throw new DatabaseException();
            }
            else throw new NotFoundException("User not found.");
        }
        
    }
}
