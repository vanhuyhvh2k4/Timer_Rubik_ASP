using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.Admin.Dto;
using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/account")]
    public class AccountController_AD : Controller
    {
        private readonly IAccountRepository_AD _accountRepository_AD;
        private readonly IAccountRepository _accountRepository;
        private readonly IRuleRepository _ruleRepository;
        private readonly IMapper _mapper;

        public AccountController_AD(IAccountRepository_AD accountRepository_AD, IAccountRepository accountRepository, IRuleRepository ruleRepository, IMapper mapper)
        {
            _accountRepository_AD = accountRepository_AD;
            _accountRepository = accountRepository;
            _ruleRepository = ruleRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateAccount([FromBody] AccountDto_AD createAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var entityAccount = _accountRepository
                                        .GetAccounts()
                                        .Where(ac => ac.Email.Trim().ToUpper() == createAccount.Email.Trim().ToUpper())
                                        .FirstOrDefault();

                if (entityAccount != null)
                {
                    return Conflict("Email Already Exists");
                }

                var accountMap = _mapper.Map<Account>(createAccount);

                _accountRepository_AD.CreateAccount(accountMap);

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
        public IActionResult UpdateAccount([FromRoute] Guid accountId, [FromBody] AccountDto_AD updateAccount)
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

                var oldAccount = _accountRepository.GetAccount(accountId);
                    
                if (!_accountRepository.AccountExists(accountId))
                {
                    return NotFound("Not Found Account");
                }

                if (_accountRepository.GetAccount(updateAccount.Email) != null && oldAccount.Email.Trim().ToUpper() != updateAccount.Email.Trim().ToUpper())
                {
                    return Conflict("Email already exists");
                }

                if (!_ruleRepository.RuleExists(updateAccount.RuleId))
                {
                    return NotFound("Not Found Rule");
                }

                var accountMap = _mapper.Map<Account>(updateAccount);

                _accountRepository_AD.UpdateAccount(accountMap);

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
