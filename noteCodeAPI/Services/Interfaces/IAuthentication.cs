using noteCodeAPI.DTOs;

namespace noteCodeAPI.Services.Interfaces
{
    public interface IAuthentication
    {
        Task<LoginResponseDTO> LoginAsync(string username, string password);


    }
}
