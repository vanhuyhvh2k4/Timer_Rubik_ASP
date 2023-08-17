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

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO login)
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
            return RedirectToAction("GetAccounts", "Account");
        }

        [AdminToken]
        [HttpGet("account")]
        public IActionResult GetAccounts()
        {
            return View();
        }
    }
}
