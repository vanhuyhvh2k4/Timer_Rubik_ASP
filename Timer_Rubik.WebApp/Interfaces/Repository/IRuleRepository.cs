using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Interfaces.Repository
{
    public interface IRuleRepository
    {
        ICollection<Rule> GetRules();

        Rule GetRule(Guid ruleId);

        bool RuleExists(Guid ruleId);
    }
}
