using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Services
{
    public class RuleService_Admin : IRuleService_Admin
    {
        private readonly DataContext _context;

        public RuleService_Admin(DataContext context)
        {
            _context = context;
        }

        public Rule GetRule(Guid ruleId)
        {
            return _context.Rules.Find(ruleId);
        }

        public ICollection<Rule> GetRules()
        {
            return _context.Rules.OrderBy(rule => rule.Id).ToList();
        }

        public bool RuleExists(Guid ruleId)
        {
            return _context.Rules.Any(rule => rule.Id == ruleId);
        }
    }
}
