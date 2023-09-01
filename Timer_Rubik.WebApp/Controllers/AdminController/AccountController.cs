﻿using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Attributes;
using Timer_Rubik.WebApp.DTO.Admin;
using Timer_Rubik.WebApp.Interfaces.Services.Admin;

namespace Timer_Rubik.WebApp.Controllers.AdminController
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AdminToken]
        [HttpGet]
        public IActionResult GetAccounts()
        {
            try
            {
                var token = HttpContext.User.FindFirst("UserId")!.Value;
                var response = _accountService.GetAccounts(Guid.Parse(token));
                return View(response.Data);
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
        public IActionResult GetAccount([FromRoute] Guid accountId)
        {
            try
            {
                var response = _accountService.GetAccount(accountId);

                if (response.Status == 200)
                {
                    return View(response.Data);
                } else
                {
                    return RedirectToAction("Error", "Auth");
                }

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

        [HttpPost("{accountId}")]
        public IActionResult UpdateAccount([FromRoute] Guid accountId, UpdateAccountDTO updateAccount)
        {
            try
            {
                var response = _accountService.UpdateAccount(accountId, updateAccount);

                if (response.Status == 404)
                {
                    return RedirectToAction("Error", "Auth");
                }
                else
                {
                    return RedirectToAction("GetAccounts", "Account");
                }
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

        [HttpDelete("{accountId}")]
        public IActionResult DeleteAccount([FromRoute] Guid accountId)
        {
            try
            {
                var response = _accountService.DeleteAccount(accountId);

                return Ok(response);
            } catch (Exception ex)
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
