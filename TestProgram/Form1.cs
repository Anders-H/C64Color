using System.Drawing;
using System.Windows.Forms;
using C64Color;

namespace TestProgram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var r = new Renderer();

            r.Render(e.Graphics, new Rectangle(50, 50, 50,50), ColorName.Cyan, RendererFlags.None);

            r.Render(e.Graphics, new Rectangle(80, 80, 50, 50), ColorName.Red, RendererFlags.None | RendererFlags.Selected);

            r.Render(e.Graphics, new Rectangle(110, 110, 50, 50), ColorName.Violet, RendererFlags.Outline);

            r.Render(e.Graphics, new Rectangle(140, 140, 50, 50), ColorName.Blue, RendererFlags.Outline | RendererFlags.Selected);

            r.Render(e.Graphics, new Rectangle(170, 170, 50, 50), ColorName.LightRed, RendererFlags.Shadow);

            r.Render(e.Graphics, new Rectangle(200, 200, 50, 50), ColorName.Green, RendererFlags.Shadow | RendererFlags.Selected);

            r.Render(e.Graphics, new Rectangle(230, 230, 50, 50), ColorName.Cyan, RendererFlags.Gradient);

            r.Render(e.Graphics, new Rectangle(260, 260, 50, 50), ColorName.Red, RendererFlags.Gradient | RendererFlags.Selected);

            r.Render(e.Graphics, new Rectangle(290, 290, 50, 50), ColorName.Violet, RendererFlags.Outline | RendererFlags.Gradient | RendererFlags.Shadow);

            r.Render(e.Graphics, new Rectangle(320, 320, 50, 50), ColorName.Blue, RendererFlags.Outline | RendererFlags.Gradient | RendererFlags.Shadow | RendererFlags.Selected);
        }
    }
}