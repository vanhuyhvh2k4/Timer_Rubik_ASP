
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Interfaces
{
    public interface IRuleService_Admin
    {
        ICollection<Rule> GetRules();

        Rule GetRule(Guid ruleId);

        bool RuleExists(Guid ruleId);
    }
}
