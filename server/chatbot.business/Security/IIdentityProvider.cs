
namespace chatbot.business.Security
{
    public interface IIdentityProvider<T>
    {
        T GetCurrentUserId();
    }
}
