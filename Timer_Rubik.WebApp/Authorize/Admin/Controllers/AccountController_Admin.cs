﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Dto;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/account")]
    public class AccountController_Admin : Controller
    {
        private readonly IAccountRepository_Admin _accountRepository_Admin;
        private readonly IRuleRepository _ruleRepository;
        private readonly IMapper _mapper;

        public AccountController_Admin(IAccountRepository_Admin accountRepository_Admin, IRuleRepository ruleRepository, IMapper mapper)
        {
            _accountRepository_Admin = accountRepository_Admin;
            _ruleRepository = ruleRepository;
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
                var accounts = _accountRepository_Admin
                                .GetAccounts()
                                .Select(rule => new
                                {
                                    id = rule.Id,
                                    ruleId = rule.RuleId,
                                    name = rule.Name,
                                    thumbnail = rule.Thumbnail,
                                    email = rule.Email,
                                })
                                .ToList();

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

                var account = _mapper.Map<AccountDto>(_accountRepository_Admin.GetAccount(accountId));

                var accountRes = new
                {
                    id = account.Id,
                    name = account.Name,
                    thumbnail = account.Thumbnail,
                    email = account.Email,
                };

                if (accountRes == null)
                {
                    return NotFound("Not Found Account");
                }

                return Ok(accountRes);
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

                var entityAccount = _accountRepository_Admin
                                        .GetAccounts()
                                        .Where(ac => ac.Email.Trim().ToUpper() == createAccount.Email.Trim().ToUpper())
                                        .FirstOrDefault();

                if (entityAccount != null)
                {
                    return Conflict("Email Already Exists");
                }

                var accountMap = _mapper.Map<Account>(createAccount);

                _accountRepository_Admin.CreateAccount(accountMap);

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

                var oldAccount = _accountRepository_Admin.GetAccount(accountId);
                    
                if (!_accountRepository_Admin.AccountExists(accountId))
                {
                    return NotFound("Not Found Account");
                }

                if (_accountRepository_Admin.GetAccount(updateAccount.Email) != null && oldAccount.Email.Trim().ToUpper() != updateAccount.Email.Trim().ToUpper())
                {
                    return Conflict("Email already exists");
                }

                if (!_ruleRepository.RuleExists(updateAccount.RuleId))
                {
                    return NotFound("Not Found Rule");
                }

                var accountMap = _mapper.Map<Account>(updateAccount);

                _accountRepository_Admin.UpdateAccount(accountMap);

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
