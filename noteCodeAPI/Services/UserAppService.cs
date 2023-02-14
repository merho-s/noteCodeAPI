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

        //public LoginResponseDTO Login(string username, string password)
        //{
        //    string token = _login.Login(username, password);
        //    List<string> unusedTokens = new();
        //    _unusedTokenRepos.GetAll().ForEach(t => unusedTokens.Add(t.Token));
          
        //    if (token != null && !unusedTokens.Contains(token))
        //    {
        //        JwtSecurityToken jwtToken = new JwtSecurityToken(token);
        //        string? usernameClaim = jwtToken.Claims?.FirstOrDefault(c => c.Type == "unique_name").Value;
        //        UserApp loggedUser = _userRepos.SearchOne(u => u.Username == usernameClaim);
        //        if (loggedUser != null)
        //        {
        //            LoginResponseDTO loginResponse = new()
        //            {
        //                Token = token,
        //                UserId = loggedUser.Id,
        //                ExpirationDate = jwtToken.ValidTo
        //            };
        //            return loginResponse;
        //        } throw new DatabaseException("Database error: User not found.");
                
        //    } throw new NotLoggedUserException("Your username or password is wrong.");
        //}

        public string BanCurrentToken()
        {
            Token currentToken = (Token)GetCurrentTokenInfos();
            if (currentToken != null)
            {
                UnusedActiveToken unusedActiveToken = new()
                {
                    JwtToken = currentToken.JwtToken,
                    ExpirationDate = currentToken.ExpirationDate
                };
                if (_unusedTokenRepos.Save(unusedActiveToken))
                {
                    return unusedActiveToken.JwtToken;
                }
                throw new DatabaseException();
            } throw new NotLoggedUserException();   
            
        }
        
        public IToken GetCurrentTokenInfos()
        {
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (token != null)
            {
                JwtSecurityToken jwtToken = new JwtSecurityToken(token);
                Token tokenResponse = new()
                {
                    JwtToken = token,
                    User = GetLoggedUser(),
                    ExpirationDate = jwtToken.ValidTo
                };
                return tokenResponse; 
            } return null;
        }
    }
}
