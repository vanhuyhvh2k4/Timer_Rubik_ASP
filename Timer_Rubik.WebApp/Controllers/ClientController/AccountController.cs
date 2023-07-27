using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Interfaces.Utils;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.DTO.Client;
using Timer_Rubik.WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace Timer_Rubik.WebApp.Controllers.ClientController
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailService;
        private readonly IJWTService _jWTService;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public AccountController(IAccountRepository accountRepository, IEmailService emailService, IJWTService jWTService, IPasswordService passwordService, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
            _jWTService = jWTService;
            _passwordService = passwordService;
            _mapper = mapper;
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

                var accountEntity = _accountRepository.GetAccount(loginRequest.Email.Trim());

                if (accountEntity == null)
                {
                    return NotFound("Not Found Account");
                }

                bool isCorrectPassword = _passwordService.VerifyPassword(loginRequest.Password.Trim(), accountEntity.Password.Trim());

                if (!isCorrectPassword)
                {
                    return StatusCode(403, "Password is not correct");
                }

                var accessToken = _jWTService.GenerateAccessToken(accountEntity.Id.ToString(), accountEntity.RuleId.ToString());

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

                if (!_emailService.EmailValid(registerRequest.Email))
                {
                    return BadRequest("Email is invalid");
                }

                if (registerRequest.Password.Length < 6)
                {
                    return BadRequest("Password at least 6 characters");
                }

                if (_accountRepository.GetAccount(registerRequest.Email) != null)
                {
                    return Conflict("Email already exist");
                }

                var accountMap = _mapper.Map<Account>(registerRequest);

                _accountRepository.RegisterAccount(accountMap);

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

        [HttpPatch("forgot")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult SendMail([FromBody] SendEmailDTO emailDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_emailService.EmailValid(emailDTO.Email))
                {
                    return BadRequest("Email is invalid");
                }

                if (_accountRepository.GetAccount(emailDTO.Email) == null)
                {
                    return NotFound("Not Found Email");
                }

                var account = _accountRepository.GetAccount(emailDTO.Email.Trim());

                string randomPassword = _passwordService.GenerateRandomPassword(6);

                _accountRepository.ChangePassword(account.Id, randomPassword);

                _emailService.SendEmail(emailDTO.Email, "Reset Password", $"New Password: {randomPassword}");

                return Ok("Email send");
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
        [Authorize]
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

                var account = _mapper.Map<GetAccountDTO>(_accountRepository.GetAccount(accountId));

                if (account == null)
                {
                    return NotFound("Not Found Account");
                }

                var ownerId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value!);

                if (accountId != ownerId)
                {
                    return BadRequest("Id is not match");
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

        [HttpPut("{accountId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAccount([FromRoute] Guid accountId, [FromBody] UpdateAccountDTO updateAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (updateAccount.Password.Length < 6)
                {
                    return BadRequest("Password at least 6 characters");
                }

                if (!_accountRepository.AccountExists(accountId))
                {
                    return NotFound("Not Found Account");
                }

                var ownerId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value!);

                if (accountId != ownerId)
                {
                    return BadRequest("Id is not match");
                }

                var accountMap = _mapper.Map<Account>(updateAccount);

                _accountRepository.UpdateAccount_User(accountId, accountMap);

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
    }
}
