﻿using System.Drawing;
using System.Windows.Forms;
using C64Color;

namespace TestProgram
{
    public partial class Form1 : Form
    {
        private readonly IResources _resources;
        private readonly Renderer _renderer;
        private readonly ColorButton _button1;
        private readonly ColorButton _button2;

        public Form1()
        {
            _resources = new Resources();
            _renderer = new Renderer();
            _button1 = new ColorButton(_renderer, new Rectangle(10, 100, 40, 40), ColorName.Black);
            _button2 = new ColorButton(_renderer, new Rectangle(10, 145, 40, 40), ColorName.White);
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            _renderer.Render(e.Graphics, _resources, new Rectangle(50, 50, 50,50), ColorName.Cyan, RendererFlags.None);
            _renderer.Render(e.Graphics, _resources, new Rectangle(80, 80, 50, 50), ColorName.Red, RendererFlags.None | RendererFlags.Selected);
            _renderer.Render(e.Graphics, _resources, new Rectangle(110, 110, 50, 50), ColorName.Violet, RendererFlags.Outline);
            _renderer.Render(e.Graphics, _resources, new Rectangle(140, 140, 50, 50), ColorName.Blue, RendererFlags.Outline | RendererFlags.Selected);
            _renderer.Render(e.Graphics, _resources, new Rectangle(170, 170, 50, 50), ColorName.LightRed, RendererFlags.Shadow);
            _renderer.Render(e.Graphics, _resources, new Rectangle(200, 200, 50, 50), ColorName.Green, RendererFlags.Shadow | RendererFlags.Selected);
            _renderer.Render(e.Graphics, _resources, new Rectangle(230, 230, 50, 50), ColorName.Violet, RendererFlags.Outline |  RendererFlags.Shadow);
            _renderer.Render(e.Graphics, _resources, new Rectangle(260, 260, 50, 50), ColorName.Blue, RendererFlags.Outline | RendererFlags.Shadow | RendererFlags.Selected);
            _button1.Render(e.Graphics, _resources);
            _button2.Render(e.Graphics, _resources);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_button1.HitTest(new Point(e.X, e.Y)))
                _button1.Selected = !_button1.Selected;

            if (_button2.HitTest(new Point(e.X, e.Y)))
                _button2.Selected = !_button2.Selected;

            Invalidate();
        }
    }
}