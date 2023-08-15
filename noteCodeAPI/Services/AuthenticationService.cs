using Microsoft.IdentityModel.Tokens;
using noteCodeAPI.DTOs;
using noteCodeAPI.Exceptions;
using noteCodeAPI.Models;
using noteCodeAPI.Repositories;
using noteCodeAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace noteCodeAPI.Services
{
    public class AuthenticationService : IAuthentication
    {
        private UserAppRepository _userRepos;
        private IPasswordHasher _passwordHasher;
        private IConfiguration _configuration;

        public AuthenticationService(UserAppRepository userRepos, IPasswordHasher passwordHasher, IConfiguration configuration)
        {
            _userRepos = userRepos;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task<LoginResponseDTO> LoginAsync(string username, string password)
        {
            UserApp user = await _userRepos.SearchByNameAsync(username);
            if(user != null)
            {
                if(user.IsValid)
                {
                    string passwordHashed = _passwordHasher.GenerateHashPassword(password, user.PasswordSalt);
                    if (user.PasswordHashed == passwordHashed)
                    {
                        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                        SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
                        {
                            Expires = DateTime.Now.AddHours(1),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["EnvironmentVariables:JWTSecretKey"])), SecurityAlgorithms.HmacSha256),
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim(ClaimTypes.Name, user.Username),
                                new Claim(ClaimTypes.Role, user.Role.ToString().ToLower()),
                                new Claim("id", user.Id.ToString())
                            }),
                            Issuer = "noteCode",
                            Audience = "noteCode"

                        };
                        SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
                        LoginResponseDTO loginResponse = new()
                        {
                            Username = user.Username,
                            Token = jwtSecurityTokenHandler.WriteToken(securityToken),
                            ExpirationDate = securityToken.ValidTo,
                            Role = user.Role.ToString(),
                        };
                        return loginResponse;
                    }
                }
                throw new NotValidUserException();
            }   
            throw new AuthenticationException();
        }
    }
}
