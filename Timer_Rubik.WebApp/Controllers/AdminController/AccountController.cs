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
        public IActionResult RenderLoginView(LoginDTO login)
        {
            return View(login);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                login.Message = "Empty";
                return RedirectToAction("RenderLoginView");
            }

            var response = _accountService.Login(login);

            login.Message = response.Message;

            if (response.Status == 403)
            {
                return RedirectToAction("RenderLoginView");
            }

            return Ok("Successful");
        }
    }
}
