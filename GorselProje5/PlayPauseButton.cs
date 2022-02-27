using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GorselProje5
{
    class PlayPauseButton : Label
    {
        public PlayPauseButton(int x, int y)
        {
            this.BackColor = Color.Orange;
            this.Size = new Size(100, 40);
            this.Location = new Point(x, y);
            this.TextAlign = ContentAlignment.MiddleCenter;
        }
    }
}
