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
        private readonly IScrambleService _scrambleService;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public ScrambleController_User(IScrambleService scrambleService, ICategoryService categoryService, IAccountService accountService, IMapper mapper)
        {
            _scrambleService = scrambleService;
            _categoryService = categoryService;
            _accountService = accountService;
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

                if (!_categoryService.CategoryExists(updateScramble.CategoryId))
                {
                    return NotFound("Not Found Category");
                }

                if (!_scrambleService.ScrambleExists(scrambleId))
                {
                    return NotFound("Not Found Scramble");
                }

                var ownerId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(cl => cl.Type == "UserId")!.Value);

                var accountId = _accountService.GetAccountByScramble(scrambleId).Id;

                if (ownerId != accountId)
                {
                    return BadRequest("Id is not match");
                }

                var categoryMap = _mapper.Map<Scramble>(updateScramble);

                _scrambleService.UpdateScramble(scrambleId, categoryMap);

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

                if (!_scrambleService.ScrambleExists(scrambleId))
                {
                    return NotFound("Not Found Scramble");
                }

                var ownerId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(cl => cl.Type == "UserId")!.Value);

                var accountId = _accountService.GetAccountByScramble(scrambleId).Id;

                if (ownerId != accountId)
                {
                    return BadRequest("Id is not match");
                }

                var scrambleEntity = _scrambleService.GetScramble(scrambleId);

                _scrambleService.DeleteScramble(scrambleEntity);

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
