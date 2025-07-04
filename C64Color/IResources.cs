#nullable enable
using System;
using System.Drawing;

namespace C64Color;

public interface IResources : IDisposable
{
    SolidBrush GetColorBrush(ColorName color);
    SolidBrush GetShadowBrush();
}