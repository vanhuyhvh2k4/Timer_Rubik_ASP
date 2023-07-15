using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.General.DTO;

namespace Timer_Rubik.WebApp.Authorize.General.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                return Ok();
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
