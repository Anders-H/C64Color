using System.Drawing;
using System.Windows.Forms;
using C64Color;

namespace TestProgram
{
    public partial class Form1 : Form
    {
        private readonly IResources _resources = new Resources();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var r = new Renderer();

            r.Render(e.Graphics, _resources, new Rectangle(50, 50, 50,50), ColorName.Cyan, RendererFlags.None);

            r.Render(e.Graphics, _resources, new Rectangle(80, 80, 50, 50), ColorName.Red, RendererFlags.None | RendererFlags.Selected);

            r.Render(e.Graphics, _resources, new Rectangle(110, 110, 50, 50), ColorName.Violet, RendererFlags.Outline);

            r.Render(e.Graphics, _resources, new Rectangle(140, 140, 50, 50), ColorName.Blue, RendererFlags.Outline | RendererFlags.Selected);

            r.Render(e.Graphics, _resources, new Rectangle(170, 170, 50, 50), ColorName.LightRed, RendererFlags.Shadow);

            r.Render(e.Graphics, _resources, new Rectangle(200, 200, 50, 50), ColorName.Green, RendererFlags.Shadow | RendererFlags.Selected);

            r.Render(e.Graphics, _resources, new Rectangle(230, 230, 50, 50), ColorName.Violet, RendererFlags.Outline |  RendererFlags.Shadow);

            r.Render(e.Graphics, _resources, new Rectangle(260, 260, 50, 50), ColorName.Blue, RendererFlags.Outline | RendererFlags.Shadow | RendererFlags.Selected);
        }
    }
}