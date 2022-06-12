using DB_Test.EntityContext;

namespace DB_Test.Services
{
    public interface IAuthService
    {
        string GetAuthData(string email, Users user);
        string GetHashedPassword(string pass);
        bool ValidateUserPassword(string hashed, string pass);
    }
}
