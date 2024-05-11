
using chatbot.entities.Security;

namespace chatbot.business.Security.Services
{
    public class TokenBusinessService : ITokenBusinessService
    {
        private readonly ITokenFactory _tokenFactory;

        public TokenBusinessService(ITokenFactory tokenFactory)
        {
            _tokenFactory = tokenFactory;
        }

        public async Task<DomainSecurityToken> CreateToken(IUserAuth user, TokenType tokenType)
        {
            DomainSecurityToken? securityToken = null;

            switch (tokenType)
            {
                case TokenType.OneTimePasscode:
                    securityToken = _tokenFactory.Generate(user, TokenType.OneTimePasscode);
                    break;
                case TokenType.JsonWebToken:
                    securityToken = _tokenFactory.Generate(user);
                    break;
                default:
                    break;
            }
            return securityToken;
        }

        public async Task<DomainSecurityToken> GetToken(string token)
        {
            DomainSecurityToken domainToken = new DomainSecurityToken()
            {
                UserToken = "test",
                TokenType = 2,
                UserId = 1

            };
            return domainToken;
            // return await _tokenDataRepository.GetToken(token);
        }
    }
}
