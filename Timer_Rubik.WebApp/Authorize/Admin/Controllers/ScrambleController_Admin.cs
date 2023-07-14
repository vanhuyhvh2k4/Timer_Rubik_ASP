using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Dto;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/scramble")]
    public class ScrambleController_Admin : Controller
    {
        private readonly IScrambleRepository_Admin _scrambleRepository_AD;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public ScrambleController_Admin(IScrambleRepository_Admin scrambleRepository_AD, ICategoryRepository categoryRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _scrambleRepository_AD = scrambleRepository_AD;
            _categoryRepository = categoryRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        [HttpPut("{scrambleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateScramble([FromRoute] Guid scrambleId, [FromBody] ScrambleDto updateScramble)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (scrambleId != updateScramble.Id)
                {
                    return BadRequest("Id is not match");
                }

                if (!_categoryRepository.CategoryExists(updateScramble.CategoryId))
                {
                    return NotFound("Not Found Category");
                }
                
                if (!_accountRepository.AccountExists(updateScramble.AccountId))
                {
                    return NotFound("Not Found Account");
                }

                var categoryMap = _mapper.Map<Scramble>(updateScramble);

                _scrambleRepository_AD.UpdateScramble(categoryMap);

                return Ok("Updated successfully");
            } catch (Exception ex)
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
