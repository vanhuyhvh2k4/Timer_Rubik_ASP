using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Interfaces;

namespace Timer_Rubik.WebApp.Authorize.General.Controllers
{
    [ApiController]
    [Route("api/favorite")]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteController(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        [HttpGet("scramble/{scrambleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetFavoritesByScramble([FromRoute] Guid scrambleId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var favorites = _favoriteRepository
                                    .GetFavoritesByScramble(scrambleId)
                                     .Select(fav => new
                                     {
                                         fav.Id,
                                         Account = new
                                         {
                                             Id = fav.AccountId,
                                             fav.Account.Name,
                                             fav.Account.Thumbnail,
                                         },
                                         Scramble = new
                                         {
                                             Id = fav.ScrambleId,
                                             fav.Scramble.Algorithm,
                                             fav.Scramble.Thumbnail,
                                             Category = fav.Scramble.Category.Name
                                         },
                                         fav.Time,
                                         fav.CreatedAt,
                                         fav.UpdatedAt,
                                     })
                                    .ToList(); ;

                if (favorites.Count == 0)
                {
                    return NotFound("Not Found Favorite");
                }

                return Ok(favorites);
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
