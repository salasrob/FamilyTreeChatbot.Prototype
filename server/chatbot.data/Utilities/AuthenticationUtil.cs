
namespace chatbot.data.Utilities
{
    public class AuthenticationUtil
    {
        public bool VerifyPassword(string claimedPassword, string passwordFromDatabase)
        {
            return BCrypt.Net.BCrypt.Verify(claimedPassword, passwordFromDatabase);
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
