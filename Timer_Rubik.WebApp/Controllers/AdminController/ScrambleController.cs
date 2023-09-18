using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Attributes;
using Timer_Rubik.WebApp.DTO.Admin;
using Timer_Rubik.WebApp.Interfaces.Services.Admin;

namespace Timer_Rubik.WebApp.Controllers.AdminController
{
    public class ScrambleController : Controller
    {
        private readonly IScrambleService _scrambleService;

        public ScrambleController(IScrambleService scrambleService)
        {
            _scrambleService = scrambleService;
        }

        [HttpGet("scramble")]
        [AdminToken]
        public IActionResult GetScrambles([FromQuery(Name = "catId")] string categoryId)
        {
            try
            {
                string defaultCategory = "a81e2eed-1fab-11ee-9b01-a02bb82e10f9";
                var response = _scrambleService.GetScramblesByCategory(Guid.Parse(categoryId ?? defaultCategory));

                if (response.Status == 404)
                {
                    return RedirectToAction("Error", "Auth");
                }

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

        [HttpPost("scramble")]
        [AdminToken]
        public IActionResult CreateScramble(CreateScrambleDTO createScramble)
        {
            try
            {
                var token = HttpContext.User.FindFirst("UserId")!.Value;
                var response = _scrambleService.CreateScramble(Guid.Parse(token), createScramble);

                if (response.Status == 404)
                {
                    return RedirectToAction("Error", "Auth");
                }

                return RedirectToAction("GetScrambles");
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
