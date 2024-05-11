
using chatbot.business;
using chatbot.entities.Domain;
using chatbot.entities.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chatbot.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersBusinessService _usersBusinessService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, IUsersBusinessService usersBusinessService)
        {
            _logger = logger;
        }

        [HttpPost("2fa")]
        [AllowAnonymous]
        public ActionResult<bool> TwoFactorAuthenticate(UserLoginRequest request)
        {
            try
            {
                // User user = _usersBusinessService.GetUserByUserName(request.Email).Result;
                //if (user == null)
                //{
                //    return NotFound();
                //}

                bool twoFactorEmailSent = _usersBusinessService.TwoFactorAuthenticate(request.Email, request.Password);
                if (twoFactorEmailSent)
                {
                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserId: {request.Email} Login failed: {ex}");
                return Problem($"UserId: {request.Email} Login failed: {ex}");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<User> TwoFactorLogin(string token)
        {
            try
            {
                User user = _usersBusinessService.TwoFactorLoginAsync(token).Result;

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"TwoFactorLogin: {token} TwoFactorLogin failed: {ex}");
                return Problem($"failed: {ex}");
            }
        }
    }
}
