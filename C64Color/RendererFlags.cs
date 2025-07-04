#nullable enable
using System;

namespace C64Color;

[Flags]
public enum RendererFlags
{
    None = 0,
    Outline = 1,
    Selected = 2,
    Shadow = 4
}