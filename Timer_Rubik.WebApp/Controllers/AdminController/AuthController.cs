using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Timer_Rubik.WebApp.Attributes;
using Timer_Rubik.WebApp.DTO.Admin;
using Timer_Rubik.WebApp.Interfaces.Services.Admin;

namespace Timer_Rubik.WebApp.Controllers.AdminController
{
    public class AuthController : Controller
    {
        private readonly IAccountService _accountService;

        public AuthController(IAccountService accountService)
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
            try
            {
                var response = _accountService.Login(login);

                if (response.Status == 403)
                {
                    ViewBag.Response = response;
                    return View();
                }
                else
                {
                    Response.Cookies.Append("token", response.Data!, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1),
                        HttpOnly = true
                    });
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

        [HttpGet("error")]
        public IActionResult Error()
        {
            return View();
        }

        [HttpGet("logout")]
        [AdminToken]
        public IActionResult Logout()
        {
            try
            {
                Response.Cookies.Delete("token");
                return RedirectToAction("Login");
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
