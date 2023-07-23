using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Attributes;
using Timer_Rubik.WebApp.Authorize.Admin.DTO;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Interfaces.Utils;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/account")]
    public class AccountController_Admin : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AccountController_Admin(IAccountService accountService, IEmailService emailService, IMapper mapper)
        {
            _accountService = accountService;
            _emailService = emailService;
            _mapper = mapper;
        }

        [HttpGet]
        [AdminToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAccounts()
        {
            try
            {
                var owerId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(cl => cl.Type == "UserId")!.Value);

                var accounts = _mapper.Map<List<GetAccountDTO_Admin>>(_accountService.GetAccounts().Where(ac => ac.Id != owerId));

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
        [AdminToken]
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

                var account = _mapper.Map<GetAccountDTO_Admin>(_accountService.GetAccount(accountId));

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

        [HttpPut("{accountId}")]
        [AdminToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAccount([FromRoute] Guid accountId, [FromBody] UpdateAccountDTO_Admin updateAccount)
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

                var oldAccount = _accountService.GetAccount(accountId);
                    
                if (!_accountService.AccountExists(accountId))
                {
                    return NotFound("Not Found Account");
                }

                var accountMap = _mapper.Map<Account>(updateAccount);

                _accountService.UpdateAccount(accountId, accountMap);

                return Ok("Updated successfully");
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    Message = ex.Message,
                });
            }
        }

        [HttpDelete("{accountId}")]
        [AdminToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAccount([FromRoute] Guid accountId)
        {
            try
            {
                var owerId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(cl => cl.Type == "UserId")!.Value);

                if (owerId == accountId)
                {
                    return BadRequest("You cannot remove your self");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_accountService.AccountExists(accountId))
                {
                    return NotFound("Not Found Account");
                }

                var accountEntity = _accountService.GetAccount(accountId);

                _accountService.DeleteAccount(accountEntity);

                return Ok("Deleted successfully");
            } catch (Exception ex)
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
