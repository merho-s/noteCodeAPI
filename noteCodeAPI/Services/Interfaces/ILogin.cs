using noteCodeAPI.DTOs;

namespace noteCodeAPI.Services.Interfaces
{
    public interface ILogin
    {
        public Task<LoginResponseDTO> LoginAsync(string username, string password);
    }
}
