using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.DTO.Client;
using Microsoft.AspNetCore.Authorization;
using Timer_Rubik.WebApp.Interfaces.Services;

namespace Timer_Rubik.WebApp.Controllers.ClientController
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IAccountService _accountService;

        public AccountController(IAuthService authService, IAccountService accountService)
        {
            _authService = authService;
            _accountService = accountService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _authService.Login(loginRequest);

                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = 500,
                    Message = ex.Message,
                });
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDTO registerRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _authService.Register(registerRequest);

                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = 500,
                    Message = ex.Message,
                });
            }
        }

        [HttpPatch("forgot")]
        public IActionResult SendMail([FromBody] ForgotPasswordDTO forgotPassword)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _authService.Forgot(forgotPassword);

                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = 500,
                    Message = ex.Message,
                });
            }
        }

        [HttpGet("{accountId}")]
        [Authorize]
        public IActionResult GetAccount([FromRoute] Guid accountId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Guid ownerId = Guid.Parse(HttpContext.User.FindFirst("UserId")!.Value);

                var response = _accountService.GetAccount(ownerId, accountId);

                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = 500,
                    Message = ex.Message,
                });
            }
        }

        [HttpPut("{accountId}")]
        [Authorize]
        public IActionResult UpdateAccount([FromRoute] Guid accountId, [FromBody] UpdateAccountDTO updateAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Guid ownerId = Guid.Parse(HttpContext.User.FindFirst("UserId")!.Value);

                var response = _accountService.UpdateAccount(ownerId, accountId, updateAccount);

                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = 500,
                    Message = ex.Message,
                });
            }
        }
    }
}
