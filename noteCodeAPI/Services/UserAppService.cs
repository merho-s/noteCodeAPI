using noteCodeAPI.DTOs;
using noteCodeAPI.Models;
using noteCodeAPI.Repositories;
using noteCodeAPI.Services.Interfaces;
using System.Security.Claims;

namespace noteCodeAPI.Services
{
    public class UserAppService
    {
        private UserAppRepository _userRepos;
        private ILogin _login;
        private IHttpContextAccessor? _httpContextAccessor;

        public UserAppService(UserAppRepository userRepos, IHttpContextAccessor httpContextAccessor, ILogin login)
        {
            _userRepos = userRepos;
            _login = login;
            _httpContextAccessor = httpContextAccessor; 
        }

        public UserApp GetLoggedUser()
        {
            
            try
            {
                string? usernameClaim = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
                return _userRepos.SearchOne(u => u.Username == usernameClaim);                
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            };
        }

        public LoginResponseDTO Login(string username, string password)
        {
            string token = _login.Login(username, password);
            if (token != null)
            {
                LoginResponseDTO loginResponse = new()
                {
                    Token = _login.Login(username, password),
                    UserId = GetLoggedUser().Id
                };
                string expirationDateClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Expiration).Value;
                if (expirationDateClaim != null)
                {
                    loginResponse.ExpirationDate = expirationDateClaim;
                }
                else loginResponse.ExpirationDate = null;
                return loginResponse;
            } throw new Exception("Bad login !");
        
            
        }
    }
}
