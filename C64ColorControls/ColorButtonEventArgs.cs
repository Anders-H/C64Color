#nullable enable
using System;
using C64Color;

namespace C64ColorControls;

public class ColorButtonEventArgs : EventArgs
{
    public int ButtonIndex { get; }
    public ColorName ColorName { get; }
    public bool IsSelected { get; }
    public bool IsMultiColor { get; }

    public ColorButtonEventArgs(int buttonIndex, ColorName colorName, bool isSelected, bool isMultiColor)
    {
        ButtonIndex = buttonIndex;
        ColorName = colorName;
        IsSelected = isSelected;
        IsMultiColor = isMultiColor;
    }
}