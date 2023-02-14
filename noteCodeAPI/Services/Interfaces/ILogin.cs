using noteCodeAPI.DTOs;

namespace noteCodeAPI.Services.Interfaces
{
    public interface ILogin
    {
        public LoginResponseDTO Login(string username, string password);
    }
}
