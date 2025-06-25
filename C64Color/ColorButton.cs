using System.Drawing;

namespace C64Color
{
    public class ColorButton
    {
        private readonly Renderer _renderer;
        public Rectangle Location { get; set; }
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

        public bool HitTest(int x, int y) =>
            Location.IntersectsWith(new Rectangle(x, y, 1, 1));

        public void Render(Graphics g, IResources resources)
        {
            if (Selected)
                _renderer.Render(g, resources, Location, Color, RendererFlags.Outline | RendererFlags.Shadow | RendererFlags.Selected);
            else
                _renderer.Render(g, resources, Location, Color, RendererFlags.Outline | RendererFlags.Shadow);
        }
    }
}