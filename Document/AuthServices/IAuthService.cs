namespace Document.AuthServices
{
    public interface IAuthService
    {
        string GenerateJwtToken(string userId, string username, string secret, string validIssuer, string validAudience);
    }
}
