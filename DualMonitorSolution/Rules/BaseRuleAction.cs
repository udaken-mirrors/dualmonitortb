using DualMonitor.Entities;

namespace DualMonitor.Rules
{
    public abstract class BaseRuleAction
    {
        protected Win32Window Target { get; private set; }
        protected WindowManager WindowManager { get; private set; }

        protected BaseRuleAction(Win32Window target, WindowManager windowManager)
        {
            Target = target;
            WindowManager = windowManager;
        }

        public abstract void Handle();
    }
}
