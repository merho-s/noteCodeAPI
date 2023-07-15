namespace noteCodeAPI.Services.Interfaces
{
    public interface IPasswordHasher
    {
        string GenerateHashPassword(string password, string salt);

        string GenerateSalt();
    }
}
