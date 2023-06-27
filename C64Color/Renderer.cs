using System.Drawing;
using System.Drawing.Drawing2D;

namespace C64Color
{
    public class Renderer
    {
        private readonly Palette _palette;

        public Renderer()
        {
            _palette = new Palette();
        }

        public void Render(Graphics g, Rectangle location, ColorName color, RendererFlags flags)
        {
            g.SmoothingMode = SmoothingMode.None;

            if (flags.HasFlag(RendererFlags.Shadow))
            {
                using (var shadow = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
                {
                    g.FillRectangle(shadow, location.X + 2, location.Y + 2, location.Width, location.Height);
                }

                using (var shadow = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
                {
                    g.FillRectangle(shadow, location.X + 1, location.Y + 1, location.Width + 2, location.Height + 2);
                }
            }

            using (var b = new SolidBrush(_palette.GetColor(color)))
            {
                g.FillRectangle(b, location);
            }

            if (flags.HasFlag(RendererFlags.Outline))
            {
                g.DrawRectangle(Pens.Black, location.X, location.Y, location.Width - 1, location.Height - 1);
                g.DrawLine(Pens.White, location.X, location.Y, location.X, location.Y + location.Height - 1);
                g.DrawLine(Pens.White, location.X, location.Y, location.X + location.Width - 1, location.Y);
            }

            if (flags.HasFlag(RendererFlags.Gradient))
            {
                using (var grad = new LinearGradientBrush(location, Color.FromArgb(50, 255, 255, 255), Color.FromArgb(50, 0, 0, 0), LinearGradientMode.Vertical))
                {
                    g.FillRectangle(grad, location);
                }
            }

            if (flags.HasFlag(RendererFlags.Selected))
            {
                g.DrawRectangle(Pens.White, location.X + 1, location.Y + 1, location.Width - 3, location.Height - 3);
                g.DrawRectangle(Pens.Blue, location.X + 2, location.Y + 2, location.Width - 5, location.Height - 5);
                g.DrawRectangle(Pens.Blue, location.X + 3, location.Y + 3, location.Width - 7, location.Height - 7);
                g.DrawRectangle(Pens.White, location.X + 4, location.Y + 4, location.Width - 9, location.Height - 9);
            }
        }
    }
}