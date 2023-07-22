using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Interfaces;

namespace Timer_Rubik.WebApp.Authorize.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/scramble")]
    public class ScrambleController_Admin : Controller
    {
        private readonly IScrambleService _scrambleService;

        public ScrambleController_Admin(IScrambleService scrambleService)
        {
            _scrambleService = scrambleService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetScrambles()
        {
            try
            {
                var scrambles = _scrambleService
                                    .GetScrambles()
                                    .Select(scramble => new
                                    {
                                        Id = scramble.Id,
                                        Category = new
                                        {
                                            Id = scramble.CategoryId,
                                            Name = scramble.Category.Name
                                        },
                                        Account = new
                                        {
                                            Id = scramble.AccountId,
                                            Name = scramble.Account.Name,
                                            Thumbnail = scramble.Account.Thumbnail,
                                            Email = scramble.Account.Email
                                        },
                                        Solve = scramble.Solve.Answer,
                                        Algorithm = scramble.Algorithm,
                                        Thumbnail = scramble.Thumbnail,
                                        CreatedAt = scramble.CreatedAt,
                                        UpdatedAt = scramble.UpdatedAt,
                                    })
                                    .ToList();

                if (scrambles.Count == 0)
                {
                    return NotFound("Not Found Scramble");
                }

                return Ok(scrambles);
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
    }
}
