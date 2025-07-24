#nullable enable
using System;
using System.Drawing;
using System.Windows.Forms;
using C64Color;

namespace C64ColorControls;

public partial class ColorPicker: UserControl
{
    internal static readonly Renderer Renderer;
    internal static readonly IResources Resources;
    private ColorButton? _button0;
    private ColorButton? _button1;
    private ColorButton? _button2;
    private ColorButton? _button3;
    private int _selectedButtonIndex;
    private int _secondaryButtonIndex;

    public event SelectedColorChangedDelegate? SelectedColorChanged;
    public event PaletteChangedDelegate? PaletteChanged;

    private bool _multiColor;
    public bool MultiColor
    {
        get => _multiColor;
        set
        {
            var currentSelected = _selectedButtonIndex;
            _multiColor = value;

            if (_selectedButtonIndex < 0)
                _selectedButtonIndex = 0;

            if (_selectedButtonIndex >= ButtonCount)
                _selectedButtonIndex = ButtonCount - 1;

            if (currentSelected != _selectedButtonIndex)
            {
                var b = GetButton(_selectedButtonIndex);

                if (b != null)
                    SelectButton(b, currentSelected, true);
            }
            
            EnsureOneIsSelected(ButtonSelected.Secondary);
            ReinitializeButtons();
            PositionButtons();
            Refresh();
        }
    }

    static ColorPicker()
    {
        Renderer = new Renderer();
        Resources = new Resources();
    }

    public ColorPicker()
    {
        InitializeComponent();
    }

    public void SetPaletteAsInt(int index, int color)
    {
        var b = GetButton(index);

        if (color < 0 || color > 15)
            throw new ArgumentOutOfRangeException(nameof(color), @"Color index must be between 0 and 15.");

        if (b == null)
            throw new ArgumentOutOfRangeException(nameof(index), @"Index must be between 0 and 1 in monochrome mode and 0 and 3 in multicolor mode.");

        b.Color = (ColorName)color;
        Refresh();
    }

    public void SetPalette(int index, ColorName color)
    {
        var b = GetButton(index);

        if ((int)color < 0 || (int)color > 15)
            throw new ArgumentOutOfRangeException(nameof(color), @"Color index must be between 0 and 15.");

        if (b == null)
            throw new ArgumentOutOfRangeException(nameof(index), @"Index must be between 0 and 1 in monochrome mode and 0 and 3 in multicolor mode.");

        b.Color = color;
        Refresh();
    }

    public int GetPaletteAsInt(int index)
    {
        var b = GetButton(index);

        if (b == null)
            throw new ArgumentOutOfRangeException(nameof(index), @"Index must be between 0 and 1 in monochrome mode and 0 and 3 in multicolor mode.");

        return (int)b.Color;
    }

    public ColorName GetPalette(int index)
    {
        var b = GetButton(index);

        if (b == null)
            throw new ArgumentOutOfRangeException(nameof(index), @"Index must be between 0 and 1 in monochrome mode and 0 and 3 in multicolor mode.");

        return b.Color;
    }

    private int ButtonCount =>
        MultiColor ? 4 : 2;

    private ColorButton? GetButton(int index) =>
        index switch
        {
            0 => _button0,
            1 => _button1,
            2 => _button2,
            3 => _button3,
            _ => throw new ArgumentOutOfRangeException(nameof(index), @"Index must be between 0 and 3.")
        };

    private void ColorPicker_Load(object sender, EventArgs e)
    {
        _selectedButtonIndex = 1;
        _secondaryButtonIndex = 0;

        if (_button1 != null)
            _button1.Selected = ButtonSelected.True;

        if (_button0 != null)
            _button0.Selected = ButtonSelected.Secondary;

        ReinitializeButtons();
        PositionButtons();
    }

    private void ColorPicker_Resize(object sender, EventArgs e)
    {
        if (Width < 20)
            Width = 20;

        if (Height < 10)
            Height = 10;

        PositionButtons();
        Refresh();
    }

    private void ReinitializeButtons()
    {
        if (MultiColor)
        {
            _button0 ??= new ColorButton(Renderer, new Rectangle(0, 0, 10, 10), ColorName.Black);
            _button1 ??= new ColorButton(Renderer, new Rectangle(0, 0, 20, 10), ColorName.White);
            _button2 ??= new ColorButton(Renderer, new Rectangle(0, 0, 30, 10), ColorName.Red);
            _button3 ??= new ColorButton(Renderer, new Rectangle(0, 0, 40, 10), ColorName.Cyan);
        }
        else
        {
            _button0 ??= new ColorButton(Renderer, new Rectangle(0, 0, 10, 10), ColorName.Black);
            _button1 ??= new ColorButton(Renderer, new Rectangle(0, 0, 20, 10), ColorName.White);
            _button2 = null;
            _button3 = null;
        }

        Refresh();
    }

    private void PositionButtons()
    {
        var buttonWidth = (int)Math.Floor(Width / (MultiColor ? 4.0 : 2.0));
        buttonWidth -= 4;
        var buttonHeight = Height - 4;
        var x = 2;

        for (var i = 0; i < ButtonCount; i++)
        {
            var b = GetButton(i);

            if (b == null)
                return;

            b.Location = new Rectangle(x, 0, buttonWidth, buttonHeight);
            x += (buttonWidth + 4);
        }

        Refresh();
    }

    private void ColorPicker_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.Clear(BackColor);
        _button0?.Render(e.Graphics, Resources);
        _button1?.Render(e.Graphics, Resources);
        _button2?.Render(e.Graphics, Resources);
        _button3?.Render(e.Graphics, Resources);
    }

    private void ColorPicker_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            SelectButtonFromCoordinates(e.X, e.Y, true);
        }
        else if (e.Button == MouseButtons.Right)
        {
            SelectButtonFromCoordinates(e.X, e.Y, false);
        }

        EnsureOneIsSelected(ButtonSelected.Secondary);
        Refresh();
    }

    private void SelectButtonFromCoordinates(int x, int y, bool primary)
    {
        for (var i = 0; i < ButtonCount; i++)
        {
            var b = GetButton(i);

            if (b == null || !b.HitTest(x, y))
                continue;

            SelectButton(b, i, primary);
            break;
        }

        Refresh();
    }

    private void SelectButton(ColorButton b, int newSelectedIndex, bool primary)
    {
        if (primary)
        {
            var oldSelectedIndex = _selectedButtonIndex;

            if (_button0 is { Selected: ButtonSelected.True })
                _button0.Selected = ButtonSelected.False;

            if (_button1 is { Selected: ButtonSelected.True })
                _button1.Selected = ButtonSelected.False;

            if (_button2 is { Selected: ButtonSelected.True })
                _button2.Selected = ButtonSelected.False;

            if (_button3 is { Selected: ButtonSelected.True })
                _button3.Selected = ButtonSelected.False;

            b.Selected = ButtonSelected.True;
            _selectedButtonIndex = newSelectedIndex;
            Refresh();

            if (oldSelectedIndex == _selectedButtonIndex)
                return;

            var eventArgs = CreateColorButtonEventArgs();

            if (eventArgs != null)
                SelectedColorChanged?.Invoke(this, eventArgs);
        }
        else
        {
            var oldSelectedIndex = _secondaryButtonIndex;

            if (_button0 is { Selected: ButtonSelected.Secondary })
                _button0.Selected = ButtonSelected.False;

            if (_button1 is { Selected: ButtonSelected.Secondary })
                _button1.Selected = ButtonSelected.False;

            if (_button2 is { Selected: ButtonSelected.Secondary })
                _button2.Selected = ButtonSelected.False;

            if (_button3 is { Selected: ButtonSelected.Secondary })
                _button3.Selected = ButtonSelected.False;

            b.Selected = ButtonSelected.Secondary;
            _secondaryButtonIndex = newSelectedIndex;
            Refresh();

            if (oldSelectedIndex == _secondaryButtonIndex)
                return;

            var eventArgs = CreateColorButtonEventArgs();

            if (eventArgs != null)
                SelectedColorChanged?.Invoke(this, eventArgs);
        }

        EnsureOneIsSelected(primary ? ButtonSelected.Secondary : ButtonSelected.True);
        Refresh();
    }

    private void EnsureOneIsSelected(ButtonSelected selected)
    {
        for (var i = 0; i < ButtonCount; i++)
        {
            var b = GetButton(i);

            if (b == null)
                continue;

            if (b.Selected == selected)
                return;
        }

        for (var i = 0; i < ButtonCount; i++)
        {
            var b = GetButton(i);

            if (b == null)
                continue;

            if (b.Selected == ButtonSelected.False)
            {
                b.Selected = selected;
                return;
            }
        }

        Refresh();
    }

    private ColorButtonEventArgs? CreateColorButtonEventArgs()
    {
        var button = GetButton(_selectedButtonIndex);
     
        return button == null
            ? null
            : new ColorButtonEventArgs(_selectedButtonIndex, button.Color, button.Selected, _multiColor);
    }
}