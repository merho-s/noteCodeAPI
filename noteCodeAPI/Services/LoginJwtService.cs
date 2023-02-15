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
    public class LoginJwtService : ILogin
    {
        private UserAppRepository _userRepos;

        public LoginJwtService(UserAppRepository userRepos)
        {
            _userRepos = userRepos;
        }
        public LoginResponseDTO Login(string username, string password)
        {
            UserApp user = _userRepos.SearchOne(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                //Créer le token 
                JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
                {
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("J'suis la clé, j'suis la clé, j'suis la clé, j'suis la clééééé ! (ref à Dora l'Exploratrice, t'as compris ?)")), SecurityAlgorithms.HmacSha256),
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Role.ToString().ToLower())
                    }),
                    Issuer = "sogeti",
                    Audience = "sogeti"

                };
                SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
                LoginResponseDTO loginResponse = new()
                {
                    UserId = user.Id,
                    Token = jwtSecurityTokenHandler.WriteToken(securityToken),
                    ExpirationDate = securityToken.ValidTo
                };
                return loginResponse;
            }
            throw new AuthenticationException();
        }
    }
}
