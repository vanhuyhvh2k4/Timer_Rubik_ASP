using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Repository
{
    public class RuleRepository : IRuleRepository
    {
        private readonly DataContext _context;

        public RuleRepository(DataContext context)
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
