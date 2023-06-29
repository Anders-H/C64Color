using System.Drawing;

namespace C64Color
{
    public class ColorButton
    {
        private readonly Renderer _renderer;
        public Rectangle Location { get; }
        public ColorName Color { get; set; }
        public bool Selected { get; set; }

        public ColorButton(Renderer renderer, Rectangle location, ColorName color)
        {
            _renderer = renderer;
            Location = location;
            Color = color;
        }

        public bool HitTest(Point point) =>
             Location.IntersectsWith(new Rectangle(point.X, point.Y, 1, 1));
    }
}