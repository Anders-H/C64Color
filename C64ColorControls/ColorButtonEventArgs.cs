#nullable enable
using System;
using C64Color;

namespace C64ColorControls;

public class ColorButtonEventArgs : EventArgs
{
    public int ButtonIndexPrimary { get; }
    public int ButtonIndexSecondary { get; }
    public ColorName ColorNamePrimary { get; }
    public ColorName ColorNameSecondary { get; }
    public bool IsMultiColor { get; }

    public ColorButtonEventArgs(int buttonIndexPrimary, int buttonIndexSecondary, ColorName colorNamePrimary, ColorName colorNameSecondary, bool isMultiColor)
    {
        ButtonIndexPrimary = buttonIndexPrimary;
        ButtonIndexSecondary = buttonIndexSecondary;
        ColorNamePrimary = colorNamePrimary;
        ColorNameSecondary = colorNameSecondary;
        IsMultiColor = isMultiColor;
    }
}