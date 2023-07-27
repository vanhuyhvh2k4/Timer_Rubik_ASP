using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.General.DTO;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.General.Controllers
{
    [ApiController]
    [Route("api/solve")]
    public class SolveController : Controller
    {
        private readonly ISolveRepository _solveRepository;
        private readonly IScrambleRepository _scrambleRepository;
        private readonly IMapper _mapper;

        public SolveController(ISolveRepository solveRepository, IScrambleRepository scrambleRepository, IMapper mapper)
        {
            _solveRepository = solveRepository;
            _scrambleRepository = scrambleRepository;
            _mapper = mapper;
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

                var scramble = _mapper.Map<GetSolveDTO>(_solveRepository.GetSolveOfScramble(scrambleId));

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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateSolve([FromBody] CreateSolveDTO createSolve)
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateSolve([FromRoute] Guid solveId, [FromBody] UpdateSolveDTO updateSolve)
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

                var solveMap = _mapper.Map<Solve>(updateSolve);

                _solveRepository.UpdateSolve(solveId, solveMap);

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

        [HttpDelete("{solveId}")]
        [Authorize]
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
