using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Attributes;
using Timer_Rubik.WebApp.DTO.Admin;
using Timer_Rubik.WebApp.Interfaces.Services.Admin;

namespace Timer_Rubik.WebApp.Controllers.AdminController
{
    [Route("test")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("error")]
        public IActionResult ErrorPage()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO login)
        {
            try
            {
                var response = _accountService.Login(login);

                if (response.Status == 403)
                {
                    ViewBag.Response = response;
                    return View();
                }
                else
                    // Set cookie
                    Response.Cookies.Append("token", response.Data!, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1),
                        HttpOnly = true
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = 500,
                    Message = ex.Message,
                });
            }
            return RedirectToAction("GetAccounts", "Account");
        }

        [AdminToken]
        [HttpGet("account")]
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

        [HttpGet("account/{accountId}")]
        public IActionResult GetAccount([FromRoute] Guid accountId)
        {
            try
            {
                var response = _accountService.GetAccount(accountId);

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

        [HttpPost("account/{accountId}")]
        public IActionResult UpdateAccount([FromRoute] Guid accountId, UpdateAccountDTO updateAccount)
        {
            try
            {
                var response = _accountService.UpdateAccount(accountId, updateAccount);

                if (response.Status == 404)
                {
                    return RedirectToAction("ErrorPage", "Account");
                } else
                {
                    return RedirectToAction("GetAccounts", "Account");
                }
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
