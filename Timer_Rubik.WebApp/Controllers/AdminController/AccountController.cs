using Microsoft.AspNetCore.Mvc;

namespace Timer_Rubik.WebApp.Controllers.AdminController
{
    [Route("admin")]
    public class AccountController : Controller
    {
        [HttpGet("login")]
        [HttpGet("~/")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
