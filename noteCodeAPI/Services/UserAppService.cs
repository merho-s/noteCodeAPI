using noteCodeAPI.Models;
using noteCodeAPI.Repositories;
using System.Security.Claims;

namespace noteCodeAPI.Services
{
    public class UserAppService
    {
        private UserAppRepository _userRepos;
        private IHttpContextAccessor _httpContextAccessor;

        public UserAppService(UserAppRepository userRepos, IHttpContextAccessor httpContextAccessor)
        {
            _userRepos = userRepos;
            _httpContextAccessor = httpContextAccessor; 
        }

        public UserApp GetLoggedUser()
        {
            string usernameClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            return _userRepos.SearchOne(u => u.Username == usernameClaim);  
        }
    }
}
