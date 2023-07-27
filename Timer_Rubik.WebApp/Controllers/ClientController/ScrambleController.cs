using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.DTO.Client;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Controllers.ClientController
{
    [ApiController]
    [Route("api/scramble")]
    public class ScrambleController : Controller
    {
        private readonly IScrambleRepository _scrambleRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ScrambleController(IScrambleRepository scrambleRepository, IAccountRepository accountRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _scrambleRepository = scrambleRepository;
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
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

                var scramble = _scrambleRepository
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
                                        },
                                        Algorithm = scramble.Algorithm,
                                        Thumbnail = scramble.Thumbnail,
                                        Solve = scramble.Solve,
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

                var scramble = _scrambleRepository
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
                                       },
                                       Algorithm = scramble.Algorithm,
                                       Thumbnail = scramble.Thumbnail,
                                       Solve = scramble.Solve,
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
                    Message = ex,
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

                var scramble = _scrambleRepository
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
                        },
                        Algorithm = scramble.Algorithm,
                        Thumbnail = scramble.Thumbnail,
                        Solve = scramble.Solve,
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody] CreateScrambleDTO createScramble)
        {
            try
            {
                var ownerId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(cl => cl.Type == "UserId")!.Value);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_categoryRepository.CategoryExists(createScramble.CategoryId))
                {
                    return NotFound("Category is not exists");
                }

                var scrambleMap = _mapper.Map<Scramble>(createScramble);

                _scrambleRepository.CreateScramble(ownerId, scrambleMap);

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
        [Authorize]
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

                if (!_categoryRepository.CategoryExists(updateScramble.CategoryId))
                {
                    return NotFound("Not Found Category");
                }

                if (!_scrambleRepository.ScrambleExists(scrambleId))
                {
                    return NotFound("Not Found Scramble");
                }

                var ownerId = Guid.Parse(HttpContext.User.FindFirst("UserId")!.Value);

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

                var ownerId = Guid.Parse(HttpContext.User.FindFirst("UserId")!.Value);

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
