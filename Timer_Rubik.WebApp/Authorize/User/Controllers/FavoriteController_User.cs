using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.User.Interfaces;
using Timer_Rubik.WebApp.Dto;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.User.Controllers
{
    [ApiController]
    [Route("api/user/favorite")]
    public class FavoriteController_User : Controller
    {
        private readonly IFavoriteRepository_User _favoriteRepository_U;
        private readonly IAccountRepository _accountRepository;
        private readonly IScrambleRepository _scrambleRepository;
        private readonly IMapper _mapper;

        public FavoriteController_User(IFavoriteRepository_User favoriteRepository_U, IAccountRepository accountRepository, IScrambleRepository scrambleRepository, IMapper mapper)
        {
            _favoriteRepository_U = favoriteRepository_U;
            _accountRepository = accountRepository;
            _scrambleRepository = scrambleRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateFavorite([FromBody] FavoriteDto createFavorite)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_accountRepository.AccountExists(createFavorite.AccountId))
                {
                    return NotFound("User is not exists");
                }

                if (!_scrambleRepository.ScrambleExists(createFavorite.ScrambleId))
                {
                    return NotFound("Scramble is not exists");
                }

                var favoriteMap = _mapper.Map<Favorite>(createFavorite);

                _favoriteRepository_U.CreateFavorite(favoriteMap);

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
    }
}
