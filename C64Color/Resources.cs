using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace C64Color
{
    public class Resources : IResources, IDisposable
    {
        private readonly SolidBrush[] _solids;
        private readonly SolidBrush _shadowBrush;

        public Resources()
        {
            _solids = new SolidBrush[16];
            _shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0));
            var palette = new Palette();

            for (var i = 0; i < 16; i++)
                _solids[i] = new SolidBrush(palette.GetColor(i));
        }

        public SolidBrush GetColorBrush(ColorName color) =>
            _solids[(int)color];

        public SolidBrush GetShadowBrush() =>
            _shadowBrush;

        public void Dispose()
        {
            try
            {
                _shadowBrush.Dispose();
            }
            catch
            {
                // ignored
            }
            
            for (var i = 0; i < 16; i++)
            {
                try
                {
                    _solids[i].Dispose();
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}