using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GorselProje5
{
    class Dikdortgen : PictureBox
    {
        public int en, boy, x, y;
        public Dikdortgen(int en, int boy, int x, int y)   //boyutlari ve konumu alinarak engeller belirli yer ve boyutlarda olusturulur.
        {
            this.en = en;
            this.boy = boy;
            this.x = x;
            this.y = y;
            this.BackColor = Color.Black;
            this.Size = new Size(en, boy);
            this.Location = new Point(x, y);
        }
    }
}
