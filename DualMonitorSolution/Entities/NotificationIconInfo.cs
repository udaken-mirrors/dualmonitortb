using DualMonitor.Win32;

namespace DualMonitor.Entities
{
    public class NotificationIconInfo
    {
        public int BitmapIndex { get; set; }
        public string Tooltip { get; set; }
        public ulong DataIdentifier { get; set; }
        public Native.SysTrayData Data { get; set; }
    }
}
