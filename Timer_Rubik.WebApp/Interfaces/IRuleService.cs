using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Interfaces
{
    public interface IRuleService
    {
        ICollection<Rule> GetRules();

        Rule GetRule(Guid ruleId);

        bool RuleExists(Guid ruleId);
    }
}
