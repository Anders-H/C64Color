using System.Drawing;

namespace C64Color
{
    public interface IResources
    {
        SolidBrush GetColorBrush(ColorName color);
        SolidBrush GetShadowBrush();
    }
}