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
                    SelectButton(b, currentSelected, _selectedButtonIndex);
            }
            
            ReinitializeButtons();
            PositionButtons();
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

        if (_button1 != null)
            _button1.Selected = true;

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

        Invalidate();
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

        Invalidate();
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
            SelectButtonFromCoordinates(e.X, e.Y);
        }
        else if (e.Button == MouseButtons.Right)
        {
            ColorButton? button = null;
            var buttonIndex = -1;

            for (var i = 0; i < ButtonCount; i++)
            {
                var b = GetButton(i);

                if (b == null)
                    continue;

                if (b.HitTest(e.X, e.Y))
                {
                    button = b;
                    buttonIndex = i;
                    break;
                }
            }

            if (button == null)
                return;

            var oldColor = button.Color;
            using var dialog = new ColorPickerPaletteDialog();
            dialog.ButtonIndex = buttonIndex;
            dialog.CurrentColor = oldColor;

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            if (dialog.CurrentColor == oldColor)
                return;

            button.Color = dialog.CurrentColor;
            Refresh();
            PaletteChanged?.Invoke(this, new ColorButtonEventArgs(buttonIndex, button.Color, button.Selected, MultiColor));
        }
    }

    private void SelectButtonFromCoordinates(int x, int y)
    {
        var currentSelected = _selectedButtonIndex;

        for (var i = 0; i < ButtonCount; i++)
        {
            var b = GetButton(i);

            if (b == null || !b.HitTest(x, y))
                continue;

            SelectButton(b, currentSelected, i);
            break;
        }
    }

    private void SelectButton(ColorButton b, int oldSelectedIndex, int newSelectedIndex)
    {
        if (_button0 != null)
            _button0.Selected = false;

        if (_button1 != null)
            _button1.Selected = false;

        if (_button2 != null)
            _button2.Selected = false;

        if (_button3 != null)
            _button3.Selected = false;

        b.Selected = true;
        _selectedButtonIndex = newSelectedIndex;
        Refresh();

        if (oldSelectedIndex == _selectedButtonIndex)
            return;

        var eventArgs = CreateColorButtonEventArgs();

        if (eventArgs != null)
            SelectedColorChanged?.Invoke(this, eventArgs);
    }

    private ColorButtonEventArgs? CreateColorButtonEventArgs()
    {
        var button = GetButton(_selectedButtonIndex);
     
        return button == null
            ? null
            : new ColorButtonEventArgs(_selectedButtonIndex, button.Color, button.Selected, _multiColor);
    }
}