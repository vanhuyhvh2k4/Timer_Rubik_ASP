using AutoMapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using Timer_Rubik.WebApp.Authorize.User.DTO;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;
using Timer_Rubik.WebApp.Utils;

namespace Timer_Rubik.WebApp.Authorize.User.Controllers
{
    [ApiController]
    [Route("api/user/account")]
    public class AccountController_User : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AccountController_User(IAccountService accountService, IEmailService emailService, IMapper mapper)
        {
            _accountService = accountService;
            _emailService = emailService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAccounts()
        {
            try
            {
                var accounts = _mapper.Map<List<GetAccountDTO_User>>(_accountService.GetAccounts());

                if (accounts.Count == 0)
                {
                    return NotFound("Not Found Account");
                }

                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    Message = ex.Message,
                });
            }
        }

        [HttpGet("{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAccount([FromRoute] Guid accountId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var account = _mapper.Map<GetAccountDTO_User>(_accountService.GetAccount(accountId));

                if (account == null)
                {
                    return NotFound("Not Found Account");
                }

                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    Message = ex.Message,
                });
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var accountEntity = _accountService.GetAccount(loginRequest.Email.Trim());

                if (accountEntity == null)
                {
                    return NotFound("Not Found Account");
                }

                bool isCorrectPassword = Password.VerifyPassword(loginRequest.Password.Trim(), accountEntity.Password.Trim());

                if (!isCorrectPassword)
                {
                    return StatusCode(403, "Password is not correct");
                }

                var accessToken = JWT.GenerateAccessToken(accountEntity.Id.ToString(), accountEntity.RuleId.ToString());

                var response = new
                {
                    token = accessToken,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    Message = ex.Message,
                });
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (_accountService.GetAccount(registerRequest.Email) != null)
                {
                    return Conflict("Email already exist");
                }

                var accountMap = _mapper.Map<Account>(registerRequest);

                _accountService.RegisterAccount(accountMap);

                return Ok("Created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    Message = ex.Message,
                });
            }
        }

        [HttpPut("{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAccount([FromRoute] Guid accountId, [FromBody] UpdateAccountDTO_User updateAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (accountId != updateAccount.Id)
                {
                    return BadRequest("Id is not match");
                }

                var oldAccount = _accountService.GetAccount(accountId);

                if (!_accountService.AccountExists(accountId))
                {
                    return NotFound("Not Found Account");
                }

                var accountMap = _mapper.Map<Account>(updateAccount);

                _accountService.UpdateAccount_User(accountMap);

                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    Message = ex.Message,
                });
            }
        }

        [HttpPost("sendMail")]
        public IActionResult SendMail()
        {
            _emailService.SendEmail("vanhuyhvh2k4@gmail.com", "Test Email", "this is my test");

            return Ok();
        }
    }
}
