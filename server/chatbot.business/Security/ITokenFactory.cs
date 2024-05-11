
using chatbot.entities.Security;

namespace chatbot.business.Security
{
    public interface ITokenFactory
    {
        public DomainSecurityToken Generate(IUserAuth user, TokenType tokenType = TokenType.JsonWebToken);
    }
}
