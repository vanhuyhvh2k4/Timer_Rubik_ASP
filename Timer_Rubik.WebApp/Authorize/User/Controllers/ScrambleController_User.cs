using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.User.DTO;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.User.Controllers
{
    [ApiController]
    [Route("api/user/scramble")]
    public class ScrambleController_User : Controller
    {
        private readonly IScrambleRepository _scrambleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public ScrambleController_User(IScrambleRepository scrambleRepository, ICategoryRepository categoryRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _scrambleRepository = scrambleRepository;
            _categoryRepository = categoryRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        [HttpPut("{scrambleId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateScramble([FromRoute] Guid scrambleId, [FromBody] UpdateScrambleDTO_User updateScramble)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_categoryRepository.CategoryExists(updateScramble.CategoryId))
                {
                    return NotFound("Not Found Category");
                }

                if (!_scrambleRepository.ScrambleExists(scrambleId))
                {
                    return NotFound("Not Found Scramble");
                }

                var ownerId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(cl => cl.Type == "UserId")!.Value);

                var accountId = _accountRepository.GetAccountByScramble(scrambleId).Id;

                if (ownerId != accountId)
                {
                    return BadRequest("Id is not match");
                }

                var categoryMap = _mapper.Map<Scramble>(updateScramble);

                _scrambleRepository.UpdateScramble(scrambleId, categoryMap);

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

        [HttpDelete("{scrambleId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteScramble([FromRoute] Guid scrambleId)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_scrambleRepository.ScrambleExists(scrambleId))
                {
                    return NotFound("Not Found Scramble");
                }

                var ownerId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(cl => cl.Type == "UserId")!.Value);

                var accountId = _accountRepository.GetAccountByScramble(scrambleId).Id;

                if (ownerId != accountId)
                {
                    return BadRequest("Id is not match");
                }

                var scrambleEntity = _scrambleRepository.GetScramble(scrambleId);

                _scrambleRepository.DeleteScramble(scrambleEntity);

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
