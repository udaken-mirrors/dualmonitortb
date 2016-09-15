using System.Drawing;

namespace DualMonitor.VisualStyle
{
    class ButtonBorderDecorator
    {
        public static void Draw(Graphics g, int x, int y, int width, int height, bool isClicked)
        {
            var darkPen = new Pen(Color.FromKnownColor(KnownColor.ControlDark));
            var lighterPen = new Pen(Color.FromKnownColor(KnownColor.ControlLightLight));
            var darkerPen = new Pen(Color.FromKnownColor(KnownColor.ControlDarkDark));
            var lightPen = new Pen(Color.FromKnownColor(KnownColor.ControlLight));

            if (!isClicked)
            {
                g.DrawLine(darkPen, width, height, width, y);
                g.DrawLine(darkPen, width, height, x, height);

                g.DrawLine(darkerPen, width + 1, height + 1, width + 1, y);
                g.DrawLine(darkerPen, width + 1, height + 1, x, height + 1);

                g.DrawLine(lighterPen, x, y, width, y);
                g.DrawLine(lighterPen, x, y, x, height);
            }
            else
            {
                g.DrawLine(lightPen, width, height, width, y);
                g.DrawLine(lightPen, width, height, x, height);

                g.DrawLine(lighterPen, width + 1, height + 1, width + 1, y);
                g.DrawLine(lighterPen, width + 1, height + 1, x, height + 1);

                g.DrawLine(darkerPen, x, y, width, y);
                g.DrawLine(darkerPen, x, y, x, height);

                g.DrawLine(darkPen, x + 1, y + 1, width - 1, y + 1);
                g.DrawLine(darkPen, x + 1, y + 1, x + 1, height - 1);
            }
        }

        public static void DrawSingle(Graphics g, int x, int y, int width, int height)
        {
            DrawSingle(g, x, y, width, height, true);
        }

        public static void DrawSingle(Graphics g, int x, int y, int width, int height, bool isRaised)
        {
            Pen darkPen = new Pen(Color.FromKnownColor(KnownColor.ControlDark));
            Pen lighterPen = new Pen(Color.FromKnownColor(KnownColor.ControlLightLight));

            if (isRaised)
            {
                var temp = darkPen;
                darkPen = lighterPen;
                lighterPen = temp;
            }
            
            g.DrawLine(darkPen, width, height, width, y);
            g.DrawLine(darkPen, width, height, x, height);

            g.DrawLine(lighterPen, x, y, width - 1, y);
            g.DrawLine(lighterPen, x, y, x, height - 1);            
        }
    }
}
