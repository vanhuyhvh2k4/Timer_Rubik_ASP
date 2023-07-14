using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.Admin.DTO;
using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Authorize.Admin.Services;

namespace Timer_Rubik.WebApp.Authorize.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/rule")]
    public class RuleController_Admin : Controller
    {
        private readonly IRuleService_Admin _ruleService_Admin;
        private readonly IMapper _mapper;

        public RuleController_Admin(IRuleService_Admin ruleService_Admin, IMapper mapper)
        {
            _ruleService_Admin = ruleService_Admin;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetRules()
        {
            try
            {
                var rules = _mapper.Map<List<GetRuleDTO_Admin>>(_ruleService_Admin.GetRules());

                if (rules.Count == 0)
                {
                    return NotFound("Not Found Rule");
                }

                return Ok(rules);
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

        [HttpGet("{ruleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetRule([FromRoute] Guid ruleId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var rule = _mapper.Map<GetRuleDTO_Admin>(_ruleService_Admin.GetRule(ruleId));

                if (rule == null)
                {
                    return NotFound("Not Found Rule");
                }

                return Ok(rule);
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
