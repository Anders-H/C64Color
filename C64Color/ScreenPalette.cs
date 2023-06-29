using System.Drawing;

namespace C64Color
{
    public class ScreenPalette
    {
        public bool Multicolor { get; }
        

        public ScreenPalette(bool multicolor)
        {
            Multicolor = multicolor;
        }
    }
}