using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Attributes;
using Timer_Rubik.WebApp.Authorize.Admin.DTO;
using Timer_Rubik.WebApp.Interfaces.Repository;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/account")]
    public class AccountController_Admin : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountController_Admin(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
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

                var accounts = _mapper.Map<List<GetAccountDTO_Admin>>(_accountRepository.GetAccounts().Where(ac => ac.Id != owerId));

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

                var account = _mapper.Map<GetAccountDTO_Admin>(_accountRepository.GetAccount(accountId));

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

                var oldAccount = _accountRepository.GetAccount(accountId);
                    
                if (!_accountRepository.AccountExists(accountId))
                {
                    return NotFound("Not Found Account");
                }

                var accountMap = _mapper.Map<Account>(updateAccount);

                _accountRepository.UpdateAccount(accountId, accountMap);

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
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_accountRepository.AccountExists(accountId))
                {
                    return NotFound("Not Found Account");
                }

                var owerId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(cl => cl.Type == "UserId")!.Value);

                if (owerId == accountId)
                {
                    return BadRequest("You cannot remove your self");
                }

                var accountEntity = _accountRepository.GetAccount(accountId);

                _accountRepository.DeleteAccount(accountEntity);

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
