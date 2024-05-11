
namespace chatbot.entities.Security
{
    public interface IUserAuth
    {
        int Id { get; }
        string UserName { get; }
        IEnumerable<string> Roles { get; }
        object TenantId { get; }
    }
}
