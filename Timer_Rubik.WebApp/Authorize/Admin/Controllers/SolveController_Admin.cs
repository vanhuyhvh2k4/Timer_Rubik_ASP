using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.Admin.DTO;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/solve")]
    public class SolveController_Admin : Controller
    {
        private readonly ISolveService _solveRepository;
        private readonly IScrambleService _scrambleRepository;
        private readonly IMapper _mapper;

        public SolveController_Admin(ISolveService solveRepository, IScrambleService scrambleRepository, IMapper mapper)
        {
            _solveRepository = solveRepository;
            _scrambleRepository = scrambleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetSolves()
        {
            try
            {
                var solves = _mapper.Map<List<GetSolveDTO_Admin>>(_solveRepository.GetSolves());

                if (solves.Count == 0)
                {
                    return NotFound("Not Found Solve");
                }

                return Ok(solves);
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

        [HttpGet("{solveId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetSolve([FromRoute] Guid solveId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var scramble = _mapper.Map<GetSolveDTO_Admin>(_solveRepository.GetSolve(solveId));

                if (scramble == null)
                {
                    return NotFound("Not Found Solve");
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

        [HttpGet("scramble/{scrambleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetSolveOfScramble([FromRoute] Guid scrambleId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var scramble = _mapper.Map<GetSolveDTO_Admin>(_solveRepository.GetSolveOfScramble(scrambleId));

                if (scramble == null)
                {
                    return NotFound("Not Found Solve");
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateSolve([FromBody] CreateSolveDTO_Admin createSolve)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_scrambleRepository.ScrambleExists(createSolve.ScrambleId))
                {
                    return NotFound("Not Found Scramble");
                }

                var solveMap = _mapper.Map<Solve>(createSolve);

                _solveRepository.CreateSolve(solveMap);

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

        [HttpPatch("{solveId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateSolve([FromRoute] Guid solveId, [FromBody] UpdateSolveDTO_Admin updateSolve)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (solveId != updateSolve.Id)
                {
                    return BadRequest("Id is not match");
                }

                if (!_solveRepository.SolveExists(solveId))
                {
                    return NotFound("Not Found Solve");
                }

                var solveMap = _mapper.Map<Solve>(updateSolve);

                _solveRepository.UpdateSolve(solveMap);

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

        [HttpDelete("{solveId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteSolve([FromRoute] Guid solveId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_solveRepository.SolveExists(solveId))
                {
                    return NotFound("Not Found Solve");
                }

                var solveEntity = _solveRepository.GetSolve(solveId);

                _solveRepository.DeleteSolve(solveEntity);

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
