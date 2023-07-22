using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.General.DTO;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.General.Controllers
{
    [ApiController]
    [Route("api/scramble")]
    public class ScrambleController : Controller
    {

        private readonly IScrambleService _scrambleService;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ScrambleController(IScrambleService scrambleService, IAccountService accountService, ICategoryService categoryService, IMapper mapper)
        {
            _scrambleService = scrambleService;
            _accountService = accountService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("account/{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetScrambleOfAccount([FromRoute] Guid accountId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var scramble = _scrambleService
                                   .GetScramblesOfAccount(accountId)
                                   .Select(scramble => new
                                   {
                                       Id = scramble.Id,
                                       Category = new
                                       {
                                           Id = scramble.CategoryId,
                                           Name = scramble.Category.Name
                                       },
                                       Account = new
                                       {
                                           Id = scramble.AccountId,
                                           Name = scramble.Account.Name,
                                           Thumbnail = scramble.Account.Thumbnail,
                                           Email = scramble.Account.Email
                                       },
                                       Solve = scramble.Solve.Answer,
                                       Algorithm = scramble.Algorithm,
                                       Thumbnail = scramble.Thumbnail,
                                       CreatedAt = scramble.CreatedAt,
                                       UpdatedAt = scramble.UpdatedAt,
                                   })
                                   .ToList();

                if (scramble.Count == 0)
                {
                    return NotFound("Not Found Scramble");
                }

                return Ok(scramble);
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

        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetScrambleByCategory([FromRoute] Guid categoryId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var scramble = _scrambleService
                                    .GetScrambleByCategory(categoryId)
                                    .Select(scramble => new
                                    {
                                        Id = scramble.Id,
                                        Category = new
                                        {
                                            Id = scramble.CategoryId,
                                            Name = scramble.Category.Name
                                        },
                                        Account = new
                                        {
                                            Id = scramble.AccountId,
                                            Name = scramble.Account.Name,
                                            Thumbnail = scramble.Account.Thumbnail,
                                            Email = scramble.Account.Email
                                        },
                                        Solve = scramble.Solve.Answer,
                                        Algorithm = scramble.Algorithm,
                                        Thumbnail = scramble.Thumbnail,
                                        CreatedAt = scramble.CreatedAt,
                                        UpdatedAt = scramble.UpdatedAt,
                                    })
                                    .ToList();

                if (scramble.Count == 0)
                {
                    return NotFound("Not Found Scramble");
                }

                return Ok(scramble);
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

        [HttpGet("{scrambleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetScramble([FromRoute] Guid scrambleId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var scramble = _scrambleService
                                    .GetScramble(scrambleId);

                if (scramble == null)
                {
                    return NotFound("Not Found Scramble");
                }
                else
                {
                    var scrambleRes = new
                    {
                        Id = scramble.Id,
                        Category = new
                        {
                            Id = scramble.CategoryId,
                            Name = scramble.Category.Name
                        },
                        Account = new
                        {
                            Id = scramble.AccountId,
                            Name = scramble.Account.Name,
                            Thumbnail = scramble.Account.Thumbnail,
                            Email = scramble.Account.Email
                        },
                        Solve = scramble.Solve.Answer,
                        Algorithm = scramble.Algorithm,
                        Thumbnail = scramble.Thumbnail,
                        CreatedAt = scramble.CreatedAt,
                        UpdatedAt = scramble.UpdatedAt,
                    };
                    return Ok(scrambleRes);
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody] CreateScrambleDTO createScramble)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_accountService.AccountExists(createScramble.AccountId))
                {
                    return NotFound("Account is not exists");
                }

                if (!_categoryService.CategoryExists(createScramble.CategoryId))
                {
                    return NotFound("Category is not exists");
                }

                var scrambleMap = _mapper.Map<Scramble>(createScramble);

                _scrambleService.CreateScramble(scrambleMap);

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

        [HttpPut("{scrambleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateScramble([FromRoute] Guid scrambleId, [FromBody] UpdateScrambleDTO updateScramble)
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

                if (!_categoryService.CategoryExists(updateScramble.CategoryId))
                {
                    return NotFound("Not Found Category");
                }

                if (!_accountService.AccountExists(updateScramble.AccountId))
                {
                    return NotFound("Not Found Account");
                }

                var categoryMap = _mapper.Map<Scramble>(updateScramble);

                _scrambleService.UpdateScramble(categoryMap);

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
