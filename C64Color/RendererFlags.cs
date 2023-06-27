using System;

namespace C64Color
{
    [Flags]
    public enum RendererFlags
    {
        None = 0,
        Outline = 1,
        Gradient = 2,
        Selected = 4,
        Shadow = 8
    }
}