using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.DTO.Client;
using Timer_Rubik.WebApp.Interfaces.Repository;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Controllers.ClientController
{
    [ApiController]
    [Route("api/favorite")]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IScrambleRepository _scrambleRepository;
        private readonly IMapper _mapper;

        public FavoriteController(IFavoriteRepository favoriteRepository, IAccountRepository accountRepository, IScrambleRepository scrambleRepository, IMapper mapper)
        {
            _favoriteRepository = favoriteRepository;
            _accountRepository = accountRepository;
            _scrambleRepository = scrambleRepository;
            _mapper = mapper;
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

        [HttpGet("account/{accountId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetFavoritesOfAccount([FromRoute] Guid accountId)
        {
            try
            {
                Guid ownerId = Guid.Parse(HttpContext.User.FindFirst("UserId")!.Value);

                if (accountId != ownerId)
                {
                    return BadRequest("Id is not match");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var favorites = _favoriteRepository
                                    .GetFavoritesOfAccount(accountId)
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

        [HttpGet("{favoriteId}")]
        [Authorize]
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

                var favorite = _favoriteRepository.GetFavorite(favoriteId);

                if (favorite == null)
                {
                    return NotFound("Not Found Favorite");
                }

                var account = _accountRepository.GetAccountByFavorite(favoriteId);

                var ownerId = Guid.Parse(HttpContext.User.FindFirst("UserId")!.Value);

                if (account.Id != ownerId)
                {
                    return BadRequest("Id is not match");
                }
                var favoriteRes = new
                {
                    favorite.Id,
                    Account = new
                    {
                        Id = favorite.AccountId,
                        favorite.Account.Name,
                        favorite.Account.Thumbnail,
                    },
                    Scramble = new
                    {
                        Id = favorite.ScrambleId,
                        favorite.Scramble.Algorithm,
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

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateFavorite([FromBody] CreateFavoriteDTO createFavorite)
        {
            try
            {
                Guid ownerId = Guid.Parse(HttpContext.User.FindFirst("UserId")!.Value);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_scrambleRepository.ScrambleExists(createFavorite.ScrambleId))
                {
                    return NotFound("Not Found Scramble");
                }

                var favoriteEntity = _favoriteRepository.GetFavoritesOfAccount(ownerId).Where(fav => fav.ScrambleId == createFavorite.ScrambleId);

                if (favoriteEntity != null)
                {
                    return Conflict("Favorite already exist");
                }

                var favoriteMap = _mapper.Map<Favorite>(createFavorite);

                _favoriteRepository.CreateFavorite(ownerId, favoriteMap);

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

        [HttpDelete("{favoriteId}")]
        [Authorize]
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

                if (!_favoriteRepository.FavoriteExists(favoriteId))
                {
                    return NotFound("Not Found Favorite");
                }

                var account = _accountRepository.GetAccountByFavorite(favoriteId);

                var ownerId = Guid.Parse(HttpContext.User.FindFirst("UserId")!.Value);

                if (account.Id != ownerId)
                {
                    return BadRequest("Id is not match");
                }

                var favoriteEntity = _favoriteRepository.GetFavorite(favoriteId);

                _favoriteRepository.DeleteFavorite(favoriteEntity);

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
