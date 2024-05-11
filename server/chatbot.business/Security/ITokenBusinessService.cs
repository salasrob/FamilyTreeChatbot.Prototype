
using chatbot.entities.Security;

namespace chatbot.business.Security
{
    public interface ITokenBusinessService
    {
        Task<DomainSecurityToken> CreateToken(IUserAuth user, TokenType tokenType);
    }
}
