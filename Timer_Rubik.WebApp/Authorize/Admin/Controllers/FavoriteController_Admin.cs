using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.Admin.DTO;
using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/favorite")]
    public class FavoriteController_Admin : Controller
    {
        private readonly IFavoriteService_Admin _favoriteService_Admin;
        private readonly IAccountService_Admin _accountService_Admin;
        private readonly IScrambleService_Admin _scrambleService_Admin;
        private readonly IMapper _mapper;

        public FavoriteController_Admin(IFavoriteService_Admin favoriteService_Admin, IAccountService_Admin accountService_Admin, IScrambleService_Admin scrambleService_Admin, IMapper mapper)
        {
            _favoriteService_Admin = favoriteService_Admin;
            _accountService_Admin = accountService_Admin;
            _scrambleService_Admin = scrambleService_Admin;
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
                var favorites = _favoriteService_Admin
                                    .GetFavorites()
                                    .Select(fav => new 
                                    {
                                        Id = fav.Id,
                                        Account = new {
                                            Id = fav.AccountId,
                                            Name = fav.Account.Name,
                                            Thumbnail = fav.Account.Thumbnail,
                                            Email = fav.Account.Email
                                        },
                                        Scramble = new {
                                            Id = fav.ScrambleId,
                                            Algorithm = fav.Scramble.Algorithm,
                                            Thumbnail = fav.Scramble.Thumbnail,
                                            Category = fav.Scramble.Category.Name
                                        },
                                        Time = fav.Time,
                                        CreatedAt = fav.CreatedAt,
                                        UpdatedAt = fav.UpdatedAt,
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
                    Message = ex.Message,
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

                var favorite = _favoriteService_Admin.GetFavorite(favoriteId);

                if (favorite == null)
                {
                    return NotFound("Not Found Favorite");
                } else
                {
                    var favoriteRes = new
                    {
                        Id = favorite.Id,
                        Account = new
                        {
                            Id = favorite.AccountId,
                            Name = favorite.Account.Name,
                            Thumbnail = favorite.Account.Thumbnail,
                            Email = favorite.Account.Email
                        },
                        Scramble = new
                        {
                            Id = favorite.ScrambleId,
                            Algorithm = favorite.Scramble.Algorithm,
                            Thumbnail = favorite.Scramble.Thumbnail,
                            Category = favorite.Scramble.Category.Name
                        },
                        Time = favorite.Time,
                        CreatedAt = favorite.CreatedAt,
                        UpdatedAt = favorite.UpdatedAt,
                    };

                    return Ok(favoriteRes);
                }
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

                var favorites = _favoriteService_Admin
                                    .GetFavoritesOfAccount(accountId)
                                     .Select(fav => new
                                     {
                                         Id = fav.Id,
                                         Account = new
                                         {
                                             Id = fav.AccountId,
                                             Name = fav.Account.Name,
                                             Thumbnail = fav.Account.Thumbnail,
                                             Email = fav.Account.Email
                                         },
                                         Scramble = new
                                         {
                                             Id = fav.ScrambleId,
                                             Algorithm = fav.Scramble.Algorithm,
                                             Thumbnail = fav.Scramble.Thumbnail,
                                             Category = fav.Scramble.Category.Name
                                         },
                                         Time = fav.Time,
                                         CreatedAt = fav.CreatedAt,
                                         UpdatedAt = fav.UpdatedAt,
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
                    Message = ex.Message,
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

                var favorites = _favoriteService_Admin
                                    .GetFavoritesByScramble(scrambleId)
                                     .Select(fav => new
                                     {
                                         Id = fav.Id,
                                         Account = new
                                         {
                                             Id = fav.AccountId,
                                             Name = fav.Account.Name,
                                             Thumbnail = fav.Account.Thumbnail,
                                             Email = fav.Account.Email
                                         },
                                         Scramble = new
                                         {
                                             Id = fav.ScrambleId,
                                             Algorithm = fav.Scramble.Algorithm,
                                             Thumbnail = fav.Scramble.Thumbnail,
                                             Category = fav.Scramble.Category.Name
                                         },
                                         Time = fav.Time,
                                         CreatedAt = fav.CreatedAt,
                                         UpdatedAt = fav.UpdatedAt,
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
                    Message = ex.Message,
                });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateFavorite([FromBody] CreateFavoriteDTO_Admin createFavorite)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_accountService_Admin.AccountExists(createFavorite.AccountId))
                {
                    return NotFound("Not Found Account");
                }

                if (!_scrambleService_Admin.ScrambleExists(createFavorite.ScrambleId))
                {
                    return NotFound("Not Found Scramble");
                }

                var favoriteMap = _mapper.Map<Favorite>(createFavorite);

                _favoriteService_Admin.CreateFavorite(favoriteMap);

                return Ok("Created successfully");
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

        [HttpPut("{favoriteId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateFavorite([FromRoute] Guid favoriteId, [FromBody] UpdateFavoriteDTO_Admin updateFavorite)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (favoriteId!= updateFavorite.Id)
                {
                    return BadRequest("Id is not match");
                }

                if (!_favoriteService_Admin.FavoriteExists(favoriteId))
                {
                    return NotFound("Not Found Favorite");
                }
                
                if (!_accountService_Admin.AccountExists(updateFavorite.AccountId))
                {
                    return NotFound("Not Found Account");
                }
                
                if (!_scrambleService_Admin.ScrambleExists(updateFavorite.ScrambleId))
                {
                    return NotFound("Not Found Scramble");
                }

                var favoriteMap = _mapper.Map<Favorite>(updateFavorite);

                _favoriteService_Admin.UpdateFavorite(favoriteMap);

                return Ok("Updated successfully");
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

                if (!_favoriteService_Admin.FavoriteExists(favoriteId))
                {
                    return NotFound("Not Found Favorite");
                }

                var favoriteEntity= _favoriteService_Admin.GetFavorite(favoriteId);

                _favoriteService_Admin.DeleteFavorite(favoriteEntity);

                return Ok("Deleted successfully");
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
