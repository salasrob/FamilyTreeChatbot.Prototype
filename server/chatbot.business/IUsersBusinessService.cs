
using chatbot.entities.Domain;
using chatbot.entities.Requests;
using chatbot.entities.Security;

namespace chatbot.business
{
    public interface IUsersBusinessService
    {
        public DomainSecurityToken JwtBearerAuthenticate(string username, string password);
        public bool TwoFactorAuthenticate(string username, string password);
        Task<User> TwoFactorLoginAsync(string token);
        public Task LogOutAsync();
        public Task<int> CreateUser(UserAddRequest user);
    }
}
