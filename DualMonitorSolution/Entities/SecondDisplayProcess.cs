using System;
using System.Drawing;
using System.Windows.Forms;

namespace DualMonitor.Entities
{
    public class SecondDisplayProcess : Win32Window
    {
        public Screen MoveToScreenOnFirstShow { get; set; }

        public Color HotTrackBrush { get; set; }

        #region .ctor
        private SecondDisplayProcess(IntPtr handle)
            :base(handle)
        {
        }

        public static new SecondDisplayProcess FromHandle(IntPtr handle)
        {
            return new SecondDisplayProcess(handle);
        } 
        #endregion

        /// <summary>
        /// Force to read properties again
        /// </summary>
        public void Refresh()
        {
            _title = null;
            _icon = null;
            _smallIcon = null;
            HotTrackBrush = Color.Empty;
        }
    }
}
