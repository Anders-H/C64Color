#nullable enable
using System;
using System.Drawing;
using System.Windows.Forms;
using C64Color;

namespace C64ColorControls;

internal partial class ColorPickerPaletteDialog : Form
{
    private ColorButton[] _buttons;
    public int ButtonIndex { get; set; }
    public ColorName CurrentColor { get; set; }

    public ColorPickerPaletteDialog()
    {
        _buttons = new ColorButton[16];
        InitializeComponent();
    }

    private void ColorPickerPaletteDialog_Load(object sender, EventArgs e)
    {
        const int marginX = 5;
        const int marginY = 5;
        Text = $@"Select a color for index {ButtonIndex}";
        var buttonWidth = (ClientSize.Width - (marginX * 2)) / 8;
        var buttonHeight = (ClientSize.Height - (marginY * 2)) / 2;
        buttonWidth -= 2;
        var x = marginX;
        var y = marginY;

        for (var i = 0; i < 16; i++)
        {
            _buttons[i] = new ColorButton(ColorPicker.Renderer, new Rectangle(x, y, buttonWidth, buttonHeight - 2), (ColorName)i);

            if (i == ButtonIndex)
                _buttons[i].Selected = true;

            x += buttonWidth + 2;

            if (i == 7)
            {
                x = marginX;
                y += buttonHeight + 2;
            }
        }
    }

    private void ColorPickerPaletteDialog_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.Clear(BackColor);

        foreach (var colorButton in _buttons)
            colorButton.Render(e.Graphics, ColorPicker.Resources);
    }

    private void ColorPickerPaletteDialog_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left)
            return;

        for (var i = 0; i < _buttons.Length; i++)
        {
            if (!_buttons[i].HitTest(e.X, e.Y))
                continue;

            ButtonIndex = i;
            CurrentColor = _buttons[i].Color;
            DialogResult = DialogResult.OK;
            return;
        }
    }
}