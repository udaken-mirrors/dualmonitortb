using System.Drawing;
using System.Drawing.Drawing2D;
using DualMonitor.Entities;

namespace DualMonitor.GraphicUtils
{
    public static class GraphicsExtensions
    {
        public static Bitmap ConvertToBitmap(this Icon icon, bool big)
        {
            if (icon == null) return null;
            try
            {
                var imageHeight = big ? ButtonConstants.BigIconSize : ButtonConstants.SmallIconSize;
                return icon.ToBitmap().ResizeBitmap(imageHeight, imageHeight);
            }
            catch
            {
                // sometimes ToBitmap() fails, no idea why...
                return null;
            }
        }

        public static Bitmap ResizeBitmap(this Bitmap b, int nWidth, int nHeight)
        {
            if (b.Width == nWidth && b.Height == nHeight) return b;

            var result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = InterpolationMode.Bilinear;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            }
            return result;
        }
        
        public static void FillGradientRectangle(this Graphics g, Color start, Color end, Rectangle rect, LinearGradientMode direction)
        {
            using (var brush = new LinearGradientBrush(rect, start, end, direction))
            {
                g.FillRectangle(brush, rect);
            }
        }

        public static void FillGradientEllipse(this Graphics g, Color start, Color end, Rectangle rect)
        {
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(rect);
                using (var pgp = new PathGradientBrush(gp))
                {
                    pgp.CenterPoint = new PointF(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
                    pgp.CenterColor = start;
                    pgp.SurroundColors = new[] { end };

                    g.FillPath(pgp, gp);
                }
            }
        }
    }
}
