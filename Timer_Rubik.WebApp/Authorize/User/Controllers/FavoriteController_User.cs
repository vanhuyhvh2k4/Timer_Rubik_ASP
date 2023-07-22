using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.User.DTO;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.User.Controllers
{
    [ApiController]
    [Route("api/user/favorite")]
    public class FavoriteController_User : Controller
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IAccountService _accountService;
        private readonly IScrambleService _scrambleService;
        private readonly IMapper _mapper;

        public FavoriteController_User(IFavoriteService favoriteService, IAccountService accountService, IScrambleService scrambleService, IMapper mapper)
        {
            _favoriteService = favoriteService;
            _accountService = accountService;
            _scrambleService = scrambleService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateFavorite([FromBody] CreateFavoriteDTO_User createFavorite)
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateFavorite([FromRoute] Guid favoriteId, [FromBody] UpdateFavoriteDTO_User updateFavorite)
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

                var account = _accountService.GetAccountByFavorite(favoriteId);

                var userIdToken = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value!);

                if (account.Id != userIdToken)
                {
                    return BadRequest("Token and slug is not match");
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

                if (!_favoriteService.FavoriteExists(favoriteId))
                {
                    return NotFound("Not Found Favorite");
                }

                var account = _accountService.GetAccountByFavorite(favoriteId);

                var userIdToken = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value!);

                if (account.Id != userIdToken)
                {
                    return BadRequest("Token and slug is not match");
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
