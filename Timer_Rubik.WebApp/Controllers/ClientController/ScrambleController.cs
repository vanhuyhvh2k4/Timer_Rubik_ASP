using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.DTO.Client;
using Timer_Rubik.WebApp.Interfaces.Services.Client;

namespace Timer_Rubik.WebApp.Controllers.ClientController
{
    [ApiController]
    [Route("api/scramble")]
    public class ScrambleController : Controller
    {
        private readonly IScrambleService _scrambleService;

        public ScrambleController(IScrambleService scrambleService)
        {
            _scrambleService = scrambleService;
        }

        [HttpGet("category/{categoryId}")]
        public IActionResult GetScrambleByCategory([FromRoute] Guid categoryId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _scrambleService.GetScramblesByCategory(categoryId);

                return StatusCode(response.Status, response);
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

        [HttpGet("account/{accountId}")]
        public IActionResult GetScrambleOfAccount([FromRoute] Guid accountId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _scrambleService.GetScramblesOfAccount(accountId);

                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    Message = ex,
                });
            }
        }


        [HttpGet("{scrambleId}")]
        public IActionResult GetScramble([FromRoute] Guid scrambleId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _scrambleService.GetScramble(scrambleId);

                return StatusCode(response.Status, response);
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


        [HttpPost]
        [Authorize]
        public IActionResult CreateCategory([FromBody] CreateScrambleDTO createScramble)
        {
            try
            {
                Guid ownerId = Guid.Parse(HttpContext.User.FindFirst("UserId")!.Value);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _scrambleService.CreateScramble(ownerId, createScramble);

                return StatusCode(response.Status, response);
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

        [HttpPut("{scrambleId}")]
        [Authorize]
        public IActionResult UpdateScramble([FromRoute] Guid scrambleId, [FromBody] UpdateScrambleDTO updateScramble)
        {
            try
            {
                Guid ownerId = Guid.Parse(HttpContext.User.FindFirst("UserId")!.Value);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _scrambleService.UpdateScramble(ownerId, scrambleId, updateScramble);

                return StatusCode(response.Status, response);
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

        [HttpDelete("{scrambleId}")]
        [Authorize]
        public IActionResult DeleteScramble([FromRoute] Guid scrambleId)
        {
            try
            {
                Guid ownerId = Guid.Parse(HttpContext.User.FindFirst("UserId")!.Value);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _scrambleService.DeleteScramble(ownerId, scrambleId);

                return StatusCode(response.Status, response);
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
