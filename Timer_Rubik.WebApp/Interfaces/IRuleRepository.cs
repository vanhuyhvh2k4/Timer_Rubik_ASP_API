﻿using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Interfaces
{
    public interface IRuleRepository
    {
        ICollection<Rule> GetRules();

        Rule GetRule(Guid ruleId);

        Rule GetRuleOfAccount(Guid accountId);

        bool RuleExists(Guid ruleId);
    }
}
