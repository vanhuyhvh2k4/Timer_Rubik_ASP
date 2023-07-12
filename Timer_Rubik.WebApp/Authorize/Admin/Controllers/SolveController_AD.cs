using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.Admin.Dto;
using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/solve")]
    public class SolveController_AD : Controller
    {
        private readonly ISolveRepository_AD _solveRepository_AD;
        private readonly IMapper _mapper;

        public SolveController_AD(ISolveRepository_AD solveRepository_AD, IMapper mapper)
        {
            _solveRepository_AD = solveRepository_AD;
            _mapper = mapper;
        }

        [HttpPut("{solveId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateSolve([FromRoute] Guid solveId, [FromBody] SolveDto_AD updateSolve)
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

                var solveMap = _mapper.Map<Solve>(updateSolve);

                _solveRepository_AD.UpdateSolve(solveMap);

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
