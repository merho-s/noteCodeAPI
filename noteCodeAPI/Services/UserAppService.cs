using Microsoft.AspNetCore.Authentication;
using noteCodeAPI.DTOs;
using noteCodeAPI.Exceptions;
using noteCodeAPI.Models;
using noteCodeAPI.Repositories;
using noteCodeAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace noteCodeAPI.Services
{
    public class UserAppService
    {
        private UserAppRepository _userRepos;
        private ILogin _login;
        private IHttpContextAccessor? _httpContextAccessor;
        private UnusedActiveTokenRepository _unusedActiveTokenRepos;

        public UserAppService(UserAppRepository userRepos, IHttpContextAccessor httpContextAccessor, ILogin login, UnusedActiveTokenRepository unusedActiveTokenRepos)
        {
            _userRepos = userRepos;
            _login = login;
            _httpContextAccessor = httpContextAccessor;
            _unusedActiveTokenRepos = unusedActiveTokenRepos;
        }

        public UserApp GetLoggedUser()
        {
            try
            {
                var userClaims = _httpContextAccessor.HttpContext.User.Claims;
                string? username = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
                return _userRepos.SearchOne(u => u.Username == username);
            }
            catch (Exception ex)
            {
                return null;
            };
        }

        public LoginResponseDTO Login(string username, string password)
        {
            string token = _login.Login(username, password);
            
            if (token != null)
            {
                JwtSecurityToken jwtToken = new JwtSecurityToken(token);
                string? usernameClaim = jwtToken.Claims?.FirstOrDefault(c => c.Type == "unique_name").Value;
                UserApp loggedUser = _userRepos.SearchOne(u => u.Username == usernameClaim);
                if (loggedUser != null)
                {
                    LoginResponseDTO loginResponse = new()
                    {
                        Token = token,
                        UserId = loggedUser.Id,
                        ExpirationDate = jwtToken.ValidTo
                    };
                    return loginResponse;
                } throw new DatabaseException("Database error: User not found.");
                
            } throw new NotLoggedUserException("Your username or password is wrong.");
        }

        public string AddUnusedActiveToken()
        {
            UnusedActiveToken unusedActiveToken = new()
            {
                Token = GetCurrentToken()
            };
            if (_unusedActiveTokenRepos.Save(unusedActiveToken))
            {
                return unusedActiveToken.Token;
            } throw new DatabaseException();
        }

        public string RemoveUnusedActiveToken(int id)
        {
            UnusedActiveToken unusedActiveTokenToRemove = _unusedActiveTokenRepos.GetById(id);
            if (unusedActiveTokenToRemove != null)
            {
                if (_unusedActiveTokenRepos.Delete(unusedActiveTokenToRemove))
                {
                    return unusedActiveTokenToRemove.Token;
                }
                throw new DatabaseException();
            } throw new Exception("Unused active token not found in database.");
            
        }

        public string GetCurrentToken()
        {
            string authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            string token = authorizationHeader.Substring("Bearer ".Length).Trim();
            return token;
        }

        public async Task Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync();
        }
    }
}
