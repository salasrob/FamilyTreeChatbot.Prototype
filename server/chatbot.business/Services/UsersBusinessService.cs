
using chatbot.business.Security;
using chatbot.data.Utilities;
using chatbot.data;
using chatbot.entities.Requests;
using chatbot.entities.Security;
using chatbot.entities.Domain;

namespace chatbot.business.Services
{
    public class UsersBusinessService : IUsersBusinessService
    {
        private readonly IUsersDataRepository _usersDataRepository;
        private readonly IAuthenticationBusinessService<int> _authenticationService;
        private readonly ITokenBusinessService _tokenBusinessService;
        private readonly AuthenticationUtil _authenticationUtil;
        public UsersBusinessService(IUsersDataRepository usersDataRepository
                                    , IAuthenticationBusinessService<int> authenticationService
                                    , ITokenBusinessService tokenBusinessService)
        {
            _usersDataRepository = usersDataRepository;
            _authenticationService = authenticationService;
            _tokenBusinessService = tokenBusinessService;
        }

        public async Task<User> TwoFactorLoginAsync(string token)
        {
            DomainSecurityToken authToken = null;
            User? user = null;
            if (!String.IsNullOrEmpty(token))
            {
                //TODO: Get token created in last 30 minutes
                // authToken = await _tokenBusinessService.GetToken(token);
            }

            if (!String.IsNullOrEmpty(token) && authToken != null)
            {
                // user = await _usersDataRepository.GetUserById(authToken.UserId);

                IUserAuth userAuth = new UserBase
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Roles = user.Roles,
                    TenantId = "SawApp-00.1.0"
                };

                await _authenticationService.LogInAsync(userAuth);
            }
            return user;
        }

        public bool TwoFactorAuthenticate(string username, string password)
        {
            IUserAuth user = _usersDataRepository.Authenticate(username, password).Result;
            if (user != null)
            {
                DomainSecurityToken oneTimePassCode = _tokenBusinessService.CreateToken(user, TokenType.OneTimePasscode).Result;

                if (oneTimePassCode is not null)
                {
                    return true;
                    // return _emailerBusinessService.SendEmailWithToken(username, oneTimePassCode.UserToken, EmailType.TwoFactorAuthentication).Result;
                }
            }
            return false;
        }

        public DomainSecurityToken JwtBearerAuthenticate(string username, string password)
        {
            IUserAuth user = _usersDataRepository.Authenticate(username, password).Result;
            DomainSecurityToken? JsonWebToken = null;

            if (user is not null)
            {
                JsonWebToken = _tokenBusinessService.CreateToken(user, TokenType.JsonWebToken).Result;
            }
            return JsonWebToken;
        }

        public async Task LogOutAsync()
        {
            await _authenticationService.LogOutAsync();
        }

        public async Task<int> CreateUser(UserAddRequest userAddRequest)
        {
            int userId = await _usersDataRepository.CreateUser(userAddRequest);

            if (userId > 0)
            {
                Guid token = Guid.NewGuid();
                if (token != Guid.Empty)
                {
                    // await _emailerBusinessService.SendEmailWithToken(userAddRequest.UserName, token.ToString(), EmailType.NewUserConfirmation);
                }
            }
            return userId;
        }
    }
}
