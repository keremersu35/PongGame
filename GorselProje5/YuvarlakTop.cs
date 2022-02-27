using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using static System.Windows.Forms.Control;
using System;

namespace GorselProje5
{
    class YuvarlakTop : PictureBox
    {
        public int[] hizList = new int[] { -4, 4 };
        Random rand = new Random();
        public int x;
        public int y;
        public int r;
        public int g;
        public int b;

        public YuvarlakTop()
        {
            int randSayi = rand.Next(0, 2);
            int randSayi1 = rand.Next(0, 2);

            this.x = hizList[randSayi];
            this.y = hizList[randSayi1];
            
            this.Size = new Size(100, 100);
            this.r = rand.Next(0, 255);
            this.g = rand.Next(0, 255);
            this.b = rand.Next(0, 255);
            randomRenk(r,g,b);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            var graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new Region(graphicsPath);

            base.OnPaint(e);
        }

        void randomRenk(int r, int g,int b)       //rastgele renkler olusturarak toplara rastgele renkler veriliyor.
        {
            Color.FromArgb(r, g, b);
            this.BackColor = Color.FromArgb(r, g, b);
        }
    }
}
