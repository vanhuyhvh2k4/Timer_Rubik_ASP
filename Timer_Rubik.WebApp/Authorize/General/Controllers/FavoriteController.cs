using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.General.DTO;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.General.Controllers
{
    [ApiController]
    [Route("api/admin/favorite")]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IAccountService _accountService;
        private readonly IScrambleService _scrambleService;
        private readonly IMapper _mapper;

        public FavoriteController(IFavoriteService favoriteService, IAccountService accountService, IScrambleService scrambleService, IMapper mapper)
        {
            _favoriteService = favoriteService;
            _accountService = accountService;
            _scrambleService = scrambleService;
            _mapper = mapper;
        }

        [HttpGet]
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
                else
                {
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

                var favorites = _favoriteService
                                    .GetFavoritesByScramble(scrambleId)
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateFavorite([FromBody] CreateFavoriteDTO createFavorite)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_accountService.AccountExists(createFavorite.AccountId))
                {
                    return NotFound("Not Found Account");
                }

                if (!_scrambleService.ScrambleExists(createFavorite.ScrambleId))
                {
                    return NotFound("Not Found Scramble");
                }

                var favoriteMap = _mapper.Map<Favorite>(createFavorite);

                _favoriteService.CreateFavorite(favoriteMap);

                return Ok("Created successfully");
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

        [HttpPut("{favoriteId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateFavorite([FromRoute] Guid favoriteId, [FromBody] UpdateFavoriteDTO updateFavorite)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (favoriteId != updateFavorite.Id)
                {
                    return BadRequest("Id is not match");
                }

                if (!_favoriteService.FavoriteExists(favoriteId))
                {
                    return NotFound("Not Found Favorite");
                }

                if (!_accountService.AccountExists(updateFavorite.AccountId))
                {
                    return NotFound("Not Found Account");
                }

                if (!_scrambleService.ScrambleExists(updateFavorite.ScrambleId))
                {
                    return NotFound("Not Found Scramble");
                }

                var favoriteMap = _mapper.Map<Favorite>(updateFavorite);

                _favoriteService.UpdateFavorite(favoriteMap);

                return Ok("Updated successfully");
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteFavorite([FromRoute] Guid favoriteId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_favoriteService.FavoriteExists(favoriteId))
                {
                    return NotFound("Not Found Favorite");
                }

                var favoriteEntity = _favoriteService.GetFavorite(favoriteId);

                _favoriteService.DeleteFavorite(favoriteEntity);

                return Ok("Deleted successfully");
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
