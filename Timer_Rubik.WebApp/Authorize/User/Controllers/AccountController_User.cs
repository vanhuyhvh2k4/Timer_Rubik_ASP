using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.User.Interfaces;
using Timer_Rubik.WebApp.Dto;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.User.Controllers
{
    [ApiController]
    [Route("api/user/account")]
    public class AccountController_User : Controller
    {
        private readonly IAccountRepository_User _accountRepository_U;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountController_User(IAccountRepository_User accountRepository_U, IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository_U = accountRepository_U;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateAccount([FromBody] AccountDto createAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var entityAccount = _accountRepository
                                        .GetAccounts()
                                        .Where(ac => ac.Email == createAccount.Email)
                                        .FirstOrDefault();

                if (entityAccount != null)
                {
                    return Conflict("Email Already Exists");
                }

                var accountMap = _mapper.Map<Account>(createAccount);

                _accountRepository_U.CreateAccount(accountMap);

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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAccount([FromRoute] Guid accountId, [FromBody] AccountDto updateAccount)
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

                if (!_accountRepository.AccountExists(accountId))
                {
                    return NotFound("Not Found Account");
                }

                var accountMap = _mapper.Map<Account>(updateAccount);

                _accountRepository_U.UpdateAccount(accountMap);

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
    }
}
