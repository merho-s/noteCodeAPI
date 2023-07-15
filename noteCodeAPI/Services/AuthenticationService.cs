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

        public AuthenticationService(UserAppRepository userRepos, IPasswordHasher passwordHasher)
        {
            _userRepos = userRepos;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginResponseDTO> LoginAsync(string username, string password)
        {
            UserApp user = await _userRepos.SearchByNameAsync(username);
            if(user != null)
            {
                string passwordHashed = _passwordHasher.GenerateHashPassword(password, user.PasswordSalt);
                if(user.PasswordHashed == passwordHashed)
                {
                    JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                    SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
                    {
                        Expires = DateTime.Now.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("J'suis la clé, j'suis la clé, j'suis la clé, j'suis la clééééé ! (ref à Dora l'Exploratrice, t'as compris ?)")), SecurityAlgorithms.HmacSha256),
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Role.ToString().ToLower()),
                        new Claim("id", user.Id.ToString())
                        }),
                        Issuer = "sogeti",
                        Audience = "sogeti"

                    };
                    SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
                    LoginResponseDTO loginResponse = new()
                    {
                        Username = user.Username,
                        Token = jwtSecurityTokenHandler.WriteToken(securityToken),
                        ExpirationDate = securityToken.ValidTo
                    };
                    return loginResponse;
                }

            }   
            throw new AuthenticationException();
        }
    }
}
