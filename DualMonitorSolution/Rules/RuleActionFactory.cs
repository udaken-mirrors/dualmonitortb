using DualMonitor.Entities;

namespace DualMonitor.Rules
{
    public class RuleActionFactory
    {
        public static BaseRuleAction CreateAction(RuleActionType ruleActionType, string moveWhere, Win32Window window, WindowManager windowManager)
        {
            switch (ruleActionType)
            {
                case RuleActionType.Move:
                    return new MoveRuleAction(moveWhere, window, windowManager);
                default:
                    return null;
            }
        }
    }
}
