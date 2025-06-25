using System.Drawing;

namespace C64Color
{
    public class ScreenPalette
    {
        private readonly ColorButton[] _colorButtons;
        public bool Multicolor { get; }
        
        public ScreenPalette(Renderer renderer, bool multicolor, int width, int height, int margin)
        {
            Multicolor = multicolor;
            _colorButtons = multicolor ? new ColorButton[4] : new ColorButton[2];

            var x = 0;

            for (var i = 0; i < _colorButtons.Length; i++)
            {
                _colorButtons[i] = new ColorButton(renderer, new Rectangle(x, 0, width, height), (ColorName)i);
                x += width;
                x += margin;
            }
        }

        public int ButtonCount =>
            _colorButtons.Length;

        public void Render(Graphics g, IResources resources)
        {

        }
    }
}