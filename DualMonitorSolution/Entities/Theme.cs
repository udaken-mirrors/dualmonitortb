using System.Drawing;

namespace DualMonitor.Entities
{
    public class Theme
    {
        private readonly Color _ButtonHighlight = Color.FromArgb(100, Color.White);
        private readonly Color _ButtonHighlightTransparent = Color.FromArgb(0, Color.White);
        private readonly Color _TaskbarBackground = Color.FromArgb(255, 129, 148, 170);
        private readonly SolidBrush _TooltipBackground = new SolidBrush(Color.FromArgb(150, 140, 156, 174));
        private readonly Pen _TooltipBorder = new Pen(Color.FromArgb(255, 163, 177, 194));
        private readonly SolidBrush _ButtonTextColor = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
        private readonly SolidBrush _ButtonBackgroundHover = new SolidBrush(Color.FromArgb(180, 0, 102, 204));
        private readonly SolidBrush _ButtonBackgroundFocused = new SolidBrush(Color.FromArgb(95, 255, 255, 255));
        private readonly SolidBrush _DarkBackground = new SolidBrush(Color.FromArgb(70, Color.Black));
        private readonly Color _Transparent = Color.FromArgb(0, 129, 148, 170);
        private readonly Pen _TaskbarTopLine1 = new Pen(Color.FromArgb(255, 67, 77, 88));
        private readonly Pen _TaskbarTopLine2 = new Pen(Color.FromArgb(255, 201, 216, 234));
        private readonly Pen _ButtonOuterBorder = new Pen(Color.FromArgb(150, 61, 70, 81));
        private readonly Pen _ButtonInnerBorder = new Pen(Color.FromArgb(150, 217, 223, 230));
        private readonly Pen _ButtonInnerBorderLighter = new Pen(Color.FromArgb(70, 217, 223, 230));
        private readonly SolidBrush _ClockColor = new SolidBrush(Color.White);

        private readonly Color _DarkLineFrom = Color.FromArgb(0, 74, 87, 103);
        private readonly Color _DarkLineTo = Color.FromArgb(255, 74, 87, 103);
        private readonly Color _BrightLineFrom = Color.FromArgb(0, 166, 179, 193);
        private readonly Color _BrightLineTo = Color.FromArgb(255, 166, 179, 193);

        private readonly Color _HoverBackLightFrom = Color.FromArgb(200, 217, 243, 255);
        private readonly Color _HoverBackLightTo = Color.FromArgb(0, 197, 223, 252);

        private readonly Color _ButtonFlashBackgroundFrom = Color.FromArgb(255, 243, 235, 114);
        private readonly Color _ButtonFlashBackgroundTo = Color.FromArgb(255, 227, 131, 60);

        private readonly Color _PinPressedBackDark = Color.FromArgb(100, 0, 0, 0);

        private readonly Color _ProgressBarErrorLight = Color.FromArgb(255, 221, 49, 49);
        private readonly Color _ProgressBarErrorDark = Color.FromArgb(255, 160, 40, 40);

        private readonly Color _ProgressBarNormalLight = Color.FromArgb(255, 48, 231, 24);
        private readonly Color _ProgressBarNormalDark = Color.FromArgb(255, 78, 141, 89);

        private readonly Color _ProgressBarPausedLight = Color.FromArgb(255, 250, 250, 21);
        private readonly Color _ProgressBarPausedDark = Color.FromArgb(255, 149, 149, 21);

        public static Color ProgressBarNormalLight => Instance._ProgressBarNormalLight;

        public static Color ProgressBarNormalDark => Instance._ProgressBarNormalDark;

        public static Color ProgressBarPausedLight => Instance._ProgressBarPausedLight;

        public static Color ProgressBarPausedDark => Instance._ProgressBarPausedDark;

        public static Color ProgressBarErrorLight => Instance._ProgressBarErrorLight;

        public static Color ProgressBarErrorDark => Instance._ProgressBarErrorDark;

        public static Color PinPressedBackDark => Instance._PinPressedBackDark;

        public static Color ButtonFlashBackgroundFrom => Instance._ButtonFlashBackgroundFrom;

        public static Color ButtonFlashBackgroundTo => Instance._ButtonFlashBackgroundTo;

        public static Color HoverBackLightFrom => Instance._HoverBackLightFrom;

        public static Color HoverBackLightTo => Instance._HoverBackLightTo;

        public static Color DarkLineFrom => Instance._DarkLineFrom;

        public static Color DarkLineTo => Instance._DarkLineTo;

        public static Color BrightLineFrom => Instance._BrightLineFrom;

        public static Color BrightLineTo => Instance._BrightLineTo;

        public static Color ButtonHighlight => Instance._ButtonHighlight;

        public static Color ButtonHighlightTransparent => Instance._ButtonHighlightTransparent;

        public static Color TaskbarBackground => Instance._TaskbarBackground;

        public static SolidBrush TooltipBackground => Instance._TooltipBackground;

        public static Pen TooltipBorder => Instance._TooltipBorder;

        public static Pen TaskbarTopLine1 => Instance._TaskbarTopLine1;

        public static Pen TaskbarTopLine2 => Instance._TaskbarTopLine2;

        public static Pen ButtonOuterBorder => Instance._ButtonOuterBorder;

        public static Pen ButtonInnerBorder => Instance._ButtonInnerBorder;

        public static Pen ButtonInnerBorderLighter => Instance._ButtonInnerBorderLighter;

        public static SolidBrush ButtonTextColor => Instance._ButtonTextColor;

        public static SolidBrush ButtonBackgroundHover => Instance._ButtonBackgroundHover;

        public static SolidBrush ButtonBackgroundFocused => Instance._ButtonBackgroundFocused;

        public static Color Transparent => Instance._Transparent;

        public static SolidBrush ClockColor => Instance._ClockColor;

        public static Brush DarkBackground => Instance._DarkBackground;

        private static readonly Theme Instance = new Theme();
        
    }
}
