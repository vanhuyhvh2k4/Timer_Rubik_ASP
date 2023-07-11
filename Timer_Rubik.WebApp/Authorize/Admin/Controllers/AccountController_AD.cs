using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.Admin.Dto;
using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Dto;
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
        private readonly IMapper _mapper;

        public AccountController_AD(IAccountRepository_AD accountRepository_AD, IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository_AD = accountRepository_AD;
            _accountRepository = accountRepository;
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
                                        .Where(ac => ac.Email == createAccount.Email)
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
    }
}
