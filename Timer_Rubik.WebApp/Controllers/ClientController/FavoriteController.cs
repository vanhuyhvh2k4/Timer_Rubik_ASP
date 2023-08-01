using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.DTO.Client;
using Timer_Rubik.WebApp.Interfaces.Services.Client;

namespace Timer_Rubik.WebApp.Controllers.ClientController
{
    [ApiController]
    [Route("api/favorite")]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet("scramble/{scrambleId}")]
        public IActionResult GetFavoritesByScramble([FromRoute] Guid scrambleId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _favoriteService.GetFavoritesByScramble(scrambleId);

                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    ex.Message,
                });
            }
        }

        [HttpGet("account/{accountId}")]
        public IActionResult GetFavoritesOfAccount([FromRoute] Guid accountId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _favoriteService.GetFavoritesOfAccount(accountId);

                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    ex.Message,
                });
            }
        }

        [HttpGet("{favoriteId}")]
        public IActionResult GetFavorite([FromRoute] Guid favoriteId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _favoriteService.GetFavorite(favoriteId);

                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    ex.Message,
                });
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateFavorite([FromBody] CreateFavoriteDTO createFavorite)
        {
            try
            {
                Guid ownerId = Guid.Parse(HttpContext.User.FindFirst("UserId")!.Value);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _favoriteService.CreateFavorite(ownerId, createFavorite);

                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    ex.Message,
                });
            }
        }

        [HttpDelete("{favoriteId}")]
        [Authorize]
        public IActionResult DeleteFavorite([FromRoute] Guid favoriteId)
        {
            try
            {
                var ownerId = Guid.Parse(HttpContext.User.FindFirst("UserId")!.Value);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = _favoriteService.DeleteFavorite(ownerId, favoriteId);

                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    ex.Message,
                });
            }
        }
    }
}
