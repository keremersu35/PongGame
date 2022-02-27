using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GorselProje5
{
    public partial class Form1 : Form
    {
        Oyun oyun;
        jsonClass jsonClass1;
        public System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        public System.Windows.Forms.Timer myTimer2 = new System.Windows.Forms.Timer();
        public System.Windows.Forms.Timer myTimer3 = new System.Windows.Forms.Timer();

        public Form1()
        {   
            InitializeComponent();
            oyun = new Oyun(this);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            WindowState = FormWindowState.Maximized;
            jsonClass1 = new jsonClass(this, ref oyun);
            Task task = new Task(new Action(jsonClass1.otomatikKayit));
            task.Start();

            oyun.playButon.Click += new EventHandler(playButonClicked);
            oyun.pauseButon.Click += new EventHandler(pauseButonClicked);
            jsonClass1.restoreButon.Click += new EventHandler(restoreButonClicked);
            jsonClass1.backupButon.Click += new EventHandler(backupButonClicked);
            jsonClass1.evetHayirSor();

            myTimer.Tick += new EventHandler(topOlustur);
            myTimer.Interval = 10000;
            myTimer.Start();

            myTimer2.Tick += new EventHandler(hareket);
            myTimer2.Interval = 1;
            myTimer2.Start();

            myTimer2.Tick += new EventHandler(kontrol);
            myTimer2.Interval = 20;
            myTimer2.Start();

        }
        private void topOlustur(Object myObject, EventArgs myEventArgs)
        {
            oyun.topOlustur(myTimer);
        }

        private void pauseButonClicked(Object myObject, EventArgs myEventArgs)
        {
            oyun.pauseButonClicked(myTimer,myTimer2,myTimer3);
        }

        private void playButonClicked(Object myObject, EventArgs myEventArgs)
        {
            oyun.playButonClicked(myTimer, myTimer2, myTimer3);
        }

        private void restoreButonClicked(object sender, EventArgs myEventArgs)
        {
            jsonClass1.evetHayirSor();
        }

        private void backupButonClicked(Object myObject, EventArgs myEventArgs)
        {
            jsonClass1.backupButonClicked();
        }

        private void kontrol(Object myObject, EventArgs myEventArgs)
        {
            oyun.kontrol(myTimer, myTimer2, myTimer3);
        }

        private void hareket(Object myObject, EventArgs myEventArgs)
        {
            oyun.hareket();
        }
    }
}




