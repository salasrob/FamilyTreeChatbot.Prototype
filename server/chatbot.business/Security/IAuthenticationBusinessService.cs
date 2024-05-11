
using chatbot.entities.Security;

namespace chatbot.business.Security
{
    public interface IAuthenticationBusinessService<T> : IIdentityProvider<T>
    {
        Task LogInAsync(IUserAuth user);
        Task LogOutAsync();
        bool IsLoggedIn();
        IUserAuth GetCurrentUser();
        string ExtractAuthorizationHeader();
    }
}
