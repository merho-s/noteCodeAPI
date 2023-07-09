using Microsoft.AspNetCore.Authentication;
using noteCodeAPI.DTOs;
using noteCodeAPI.Exceptions;
using noteCodeAPI.Models;
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
        private ILogin _login;
        private IHttpContextAccessor? _httpContextAccessor;
        private UnusedActiveTokenRepository _unusedTokenRepos;

        public UserAppService(UserAppRepository userRepos, IHttpContextAccessor httpContextAccessor, ILogin login, UnusedActiveTokenRepository unusedTokenRepos)
        {
            _userRepos = userRepos;
            _login = login;
            _httpContextAccessor = httpContextAccessor;
            _unusedTokenRepos = unusedTokenRepos;
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

        public async Task<string> BanCurrentTokenAsync()
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
            } throw new NotLoggedUserException();              
        }
        
    }
}
