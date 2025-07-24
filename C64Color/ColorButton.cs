#nullable enable
using System;
using System.Drawing;

namespace C64Color;

public class ColorButton
{
    public static Bitmap Primary;
    public static Bitmap Secondary;
    private readonly Renderer _renderer;
    public Rectangle Location { get; set; }
    public ColorName Color { get; set; }
    public ButtonSelected Selected { get; set; }

    static ColorButton()
    {
        Primary = CreateBitmap("000000000000" +
                               "00110000000." +
                               "0111000000.." +
                               "001100000..." +
                               "00110000...." +
                               "0011000....." +
                               "011110......" +
                               "00000......." +
                               "0000........" +
                               "000........." +
                               "00.........." +
                               "0...........");

        Secondary = CreateBitmap("000000000000" +
                                 "00111000000." +
                                 "0110110000.." +
                                 "000011000..." +
                                 "00011000...." +
                                 "0011000....." +
                                 "011111......" +
                                 "00000......." +
                                 "0000........" +
                                 "000........." +
                                 "00.........." +
                                 "0...........");
    }

    public ColorButton(Renderer renderer, Rectangle location, ColorName color)
    {
        _renderer = renderer;
        Location = location;
        Color = color;
    }

    private static Bitmap CreateBitmap(string bitmapString)
    {
        var bitmap = new Bitmap(12, 12);
        
        for (var y = 0; y < 12; y++)
        {
            for (var x = 0; x < 12; x++)
            {
                var pixel = bitmapString[y * 12 + x];

                if (pixel == '.')
                    continue;

                bitmap.SetPixel(x, y, pixel == '1' ? System.Drawing.Color.White : System.Drawing.Color.Black);
            }
        }

        return bitmap;
    }

    public bool HitTest(Point point) =>
        Location.IntersectsWith(new Rectangle(point.X, point.Y, 1, 1));

    public bool HitTest(int x, int y) =>
        Location.IntersectsWith(new Rectangle(x, y, 1, 1));

    public void Render(Graphics g, IResources resources)
    {
        switch (Selected)
        {
            case ButtonSelected.False:
                break;
            case ButtonSelected.True:
                break;
            case ButtonSelected.Secondary:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        switch (Selected)
        {
            case ButtonSelected.False:
                _renderer.Render(g, resources, Location, Color, RendererFlags.Outline | RendererFlags.Shadow, null);
                break;
            case ButtonSelected.True:
                _renderer.Render(g, resources, Location, Color, RendererFlags.Outline | RendererFlags.Shadow | RendererFlags.Selected, Primary);
                break;
            case ButtonSelected.Secondary:
                _renderer.Render(g, resources, Location, Color, RendererFlags.Outline | RendererFlags.Shadow | RendererFlags.Selected, Secondary);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}