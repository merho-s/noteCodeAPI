using noteCodeAPI.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace noteCodeAPI.Services
{
    public sealed class PasswordHasherService : IPasswordHasher
    {
        private int _iteration = 5;
        public string GenerateHashPassword(string password, string salt)
        {
            if (_iteration <= 0) return password;

            using SHA256 sha256 = SHA256.Create();
            string passwordWithSalt = $"{salt}{password}";
            string passwordHashed = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt)));
            _iteration--;
            return GenerateHashPassword(passwordHashed, salt);
        }

        public string GenerateSalt()
        {
            using var rng = RandomNumberGenerator.Create();
            var byteSalt = new byte[16];
            rng.GetBytes(byteSalt);
            var salt = Convert.ToBase64String(byteSalt);
            return salt;

        }
    }
}
