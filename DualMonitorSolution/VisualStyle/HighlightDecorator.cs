﻿using System.Drawing;
using System.Drawing.Drawing2D;
using DualMonitor.GraphicUtils;
using DualMonitor.Entities;

namespace DualMonitor.VisualStyle
{
    class HighlightDecorator
    {
        private static Bitmap _cacheBitmap;
        private static readonly Blend HighlightBlend;

        static HighlightDecorator()
        {
            HighlightBlend = new Blend
            {
                Factors = new[] {0f, .16f, .95f, 1f},
                Positions = new[] {0f, .19f, .75f, 1f}
            };
        }

        /// <summary>
        /// Paint the highlight effect on taskbar buttons
        /// </summary>
        public static void Paint(Graphics graphics, Rectangle bounds)
        {
            if (_cacheBitmap == null || _cacheBitmap.Width != bounds.Width || _cacheBitmap.Height != bounds.Height) 
            {
                _cacheBitmap?.Dispose();
                _cacheBitmap = CreateHighlight(bounds);
            }

            graphics.DrawImage(_cacheBitmap, bounds);

            graphics.FillGradientRectangle(Theme.ButtonHighlight, Theme.ButtonHighlightTransparent,
                    new Rectangle(bounds.Left, bounds.Top, bounds.Width, 20), LinearGradientMode.Vertical);
        }

        private static Bitmap CreateHighlight(Rectangle bounds)
        {
            // could have done this using images but this will work too
            // this is cached so it should be fast enough

            // for antialiasing to work with Clip region, we need to render bigger and scale later.
            using (Bitmap bmp = new Bitmap(bounds.Width * 4, bounds.Height * 4))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    Rectangle clientRectangle = new Rectangle(0, 0, bmp.Width, bmp.Height);

                    using (GraphicsPath clipGP = new GraphicsPath())
                    {
                        clipGP.AddEllipse(-60, 22, bmp.Width * 2 + 180, bmp.Height * 2);

                        using (Region r = new Region(clipGP))
                        {
                            r.Complement(clientRectangle);
                            g.Clip = r;

                            using (GraphicsPath gp = new GraphicsPath())
                            {
                                int x = bmp.Width / 6;
                                int width = 5 * (bmp.Width - x) / 3;

                                gp.AddEllipse(x - width / 2, -bmp.Height / 2 + 20, width, bmp.Height + 40);

                                using (PathGradientBrush pgp = new PathGradientBrush(gp))
                                {
                                    pgp.CenterPoint = new PointF(x, -20);
                                    pgp.CenterColor = Theme.ButtonHighlight;
                                    pgp.Blend = HighlightBlend;
                                    pgp.SurroundColors = new[] { Theme.ButtonHighlightTransparent };

                                    g.FillRectangle(pgp, clientRectangle);
                                }
                            }
                        }
                    }
                }

                // resize to normal values (with antialiasing)
                return bmp.ResizeBitmap(bounds.Width, bounds.Height);
            }
        }
    }
}
