
using chatbot.entities.Security;

namespace chatbot.entities.Domain
{
    public class UserBase : IUserAuth
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public object TenantId { get; set; }
    }
}
