using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Attributes;
using Timer_Rubik.WebApp.Interfaces;

namespace Timer_Rubik.WebApp.Authorize.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/favorite")]
    public class FavoriteController_Admin : Controller
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController_Admin(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet]
        [AdminToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetFavorites()
        {
            try
            {
                var favorites = _favoriteService
                                    .GetFavorites()
                                    .Select(fav => new
                                    {
                                        fav.Id,
                                        Account = new
                                        {
                                            Id = fav.AccountId,
                                            fav.Account.Name,
                                            fav.Account.Thumbnail,
                                            fav.Account.Email
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
                                    .ToList();

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

        [HttpGet("{favoriteId}")]
        [AdminToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetFavorite([FromRoute] Guid favoriteId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var favorite = _favoriteService.GetFavorite(favoriteId);

                if (favorite == null)
                {
                    return NotFound("Not Found Favorite");
                }

                var favoriteRes = new
                {
                    favorite.Id,
                    Account = new
                    {
                        Id = favorite.AccountId,
                        favorite.Account.Name,
                        favorite.Account.Thumbnail,
                        favorite.Account.Email
                    },
                    Scramble = new
                    {
                        Id = favorite.ScrambleId,
                        favorite.Scramble.Algorithm,
                        favorite.Scramble.Thumbnail,
                        Category = favorite.Scramble.Category.Name
                    },
                    favorite.Time,
                    favorite.CreatedAt,
                    favorite.UpdatedAt,
                };

                return Ok(favoriteRes);
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
        [AdminToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetFavoritesOfAccount([FromRoute] Guid accountId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var favorites = _favoriteService
                                    .GetFavoritesOfAccount(accountId)
                                     .Select(fav => new
                                     {
                                         fav.Id,
                                         Account = new
                                         {
                                             Id = fav.AccountId,
                                             fav.Account.Name,
                                             fav.Account.Thumbnail,
                                             fav.Account.Email
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
