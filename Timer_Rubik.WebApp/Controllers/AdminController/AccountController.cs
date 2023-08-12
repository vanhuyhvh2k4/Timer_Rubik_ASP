using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("success")]
        public IActionResult RenderSuccessPage()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO login)
        {
            var response = _accountService.Login(login);

            if (response.Status == 403)
            {
                ViewBag.isLogged = 0;
            } else
            {
                return RedirectToAction("RenderSuccessPage");
            }

            return View(login);
        }
    }
}
