namespace noteCodeAPI.Models.Interfaces
{
    public interface IToken
    {
        UserApp User { get; }

        string JwtToken { get; set; }

        DateTime ExpirationDate { get; set; }


    }
}
