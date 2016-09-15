using System.Linq;
using DualMonitor.Entities;
using System.Windows.Forms;

namespace DualMonitor.Rules
{
    public class MoveRuleAction : BaseRuleAction
    {
        private readonly string _moveWhere;

        public MoveRuleAction(string moveWhere, Win32Window window, WindowManager windowManager)
            : base (window, windowManager)
        {
            _moveWhere = moveWhere;
        }

        public override void Handle()
        {
            var source = Target.Screen;
            if (string.IsNullOrEmpty(_moveWhere)) return;

            var destination = _moveWhere == Rule.MOVE_MONITOR_WITH_CURSOR ? Screen.FromPoint(Cursor.Position) : Screen.AllScreens.FirstOrDefault(s => s.DeviceName.Equals(_moveWhere));

            if (destination == null || destination.DeviceName.Equals(source.DeviceName))
            {
                return;
            }

            WindowManager.MoveWindowBetweenScreens(Target, source, destination);
        }
    }
}
