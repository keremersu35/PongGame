using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GorselProje5
{
    class Oyun
    {   
        public List<YuvarlakTop> toplar = new List<YuvarlakTop>();
        Dikdortgen cubuk = new Dikdortgen(285, 50, 880, 900);
        Form1 form;
        public Dikdortgen kenarEngel1 = new Dikdortgen(500, 50, 0, 0);
        public Dikdortgen kenarEngel2;
        public Dikdortgen kenarEngel3;
        Random rand = new Random();
        Dikdortgen kenarEngel4;

        public int count = 0;
        int x, y;

        public PlayPauseButton playButon = new PlayPauseButton(1490, 60);
        public PlayPauseButton pauseButon = new PlayPauseButton(1610,60);
        public PlayPauseButton scoreLabel = new PlayPauseButton(1730, 60);
        public int score = 0;
        
        public Oyun(Form1 form)
        {
            this.form = form;
            form.KeyPreview = true;
            form.KeyDown += new KeyEventHandler(keyboard);
            kenarEngel2 = new Dikdortgen(500, 50, form.ClientSize.Width - kenarEngel1.Width, 0);
            kenarEngel3 = new Dikdortgen(50, form.ClientSize.Height, 0, 0);
            kenarEngel4 = new Dikdortgen(50, form.ClientSize.Height, form.ClientSize.Width - kenarEngel1.Height, 0);
            AraclariOlustur();
        }

        public void AraclariOlustur()
        {
            form.Controls.Add(cubuk);
            form.Controls.Add(kenarEngel1);
            form.Controls.Add(kenarEngel2);
            form.Controls.Add(kenarEngel3);
            form.Controls.Add(kenarEngel4);
            playButon.Text = "Play";
            pauseButon.Text = "Pause";
            scoreLabel.Text = "Score: 0";
            form.Controls.Add(playButon);
            form.Controls.Add(pauseButon);
            form.Controls.Add(scoreLabel);
        }

        public void topOlustur(Timer myTimer)     //Her 10 saniyede bir rastgele bir yerde top olusturan fonksiyon
        {   

            if (count < 5)
            {
                count += 1;
                myTimer.Enabled = true;
                this.x = new RandomSayiEkle(kenarEngel1.Height, form.ClientSize.Width - 100 - kenarEngel1.Height).sayiyiAl();
                this.y = new RandomSayiEkle(kenarEngel1.Height, form.ClientSize.Height - 100 - kenarEngel1.Height).sayiyiAl();

                YuvarlakTop yuvarlakTop = new YuvarlakTop();
                yuvarlakTop.Location = new Point(x, y);      //ilk top da 10. saniyede olusturuluyor.

                toplar.Add(yuvarlakTop);
                form.Controls.Add(yuvarlakTop);
            }
            else
            {
                myTimer.Enabled = false;
            }
        }

        public void keyboard(object sender, KeyEventArgs e)   //alttaki cubugun saga ve sola klavye ile hareket ettirilmesini saglayan fonksiyon
        {

            if (e.KeyCode == Keys.Left)
            {
                if (cubuk.x >= 0 + kenarEngel1.Height + 10)
                {
                    cubuk.Left -= 20;
                    cubuk.x -= 20;
                }
            }

            else if (e.KeyCode == Keys.Right)
            {
                if (cubuk.x + cubuk.en <= form.ClientSize.Width - kenarEngel1.Height - 10)
                {
                    cubuk.Left += 20;
                    cubuk.x += 20;
                }
            }
        }

        public void hareket()    //topun lokasyonuna gore ne yapmasi gerektigini ayarlayan fonksiyon
        {

            for (int i = 0; i < toplar.Count; i++)
            {

                toplar[i].Location = new Point(toplar[i].Location.X + toplar[i].x, toplar[i].Location.Y + toplar[i].y);

                if (toplar[i].Location.X > form.ClientSize.Width - (toplar[i].Width + kenarEngel1.Height))
                {
                    toplar[i].x *= -1;
                }

                else if (toplar[i].Location.X < 0 + kenarEngel1.Height)
                {
                    toplar[i].x *= -1;
                }

                if (toplar[i].Location.Y > form.ClientSize.Height)
                {
                    form.Controls.Remove(toplar[i]);
                    toplar.Remove(toplar[i]);
                    ceza();
                    ceza();
                    score -= 20;
                    scoreLabel.Text = "Score: " + score.ToString();
                }

                if (toplar[i].Location.Y < 0 + kenarEngel1.Height && toplar[i].Location.X <= kenarEngel1.Width)
                {
                    toplar[i].y *= -1;
                }

                if (toplar[i].Location.Y < 0 + kenarEngel1.Height && toplar[i].Location.X >= kenarEngel1.Width + 300)
                {
                    toplar[i].y *= -1;
                }

                if (toplar[i].Location.Y > (cubuk.Location.Y - toplar[i].Height) && toplar[i].Location.Y <= cubuk.Location.Y + cubuk.Height)
                {
                    if (toplar[i].Location.X > cubuk.Location.X && toplar[i].Location.X <= cubuk.Location.X + cubuk.Width)
                    {   
                        toplar[i].y *= -1;
                        score += 1;
                        scoreLabel.Text = "Score: " + score.ToString();
                    }
                }

                if (toplar[i].Location.X > kenarEngel1.Width && toplar[i].Location.X < kenarEngel1.Width + 900)
                {
                    if (toplar[i].Location.Y < kenarEngel1.Height)
                    {
                        form.Controls.Remove(toplar[i]);
                        toplar.Remove(toplar[i]);
                        score += 10;
                        scoreLabel.Text = "Score: " + score.ToString();
                    }
                }
            }
        }

        public void ceza()
        {
            this.x = new RandomSayiEkle(kenarEngel1.Height, form.ClientSize.Width - 100 - kenarEngel1.Height).sayiyiAl();
            this.y = new RandomSayiEkle(kenarEngel1.Height, form.ClientSize.Height - 100 - kenarEngel1.Height).sayiyiAl();

            YuvarlakTop yuvarlakTop = new YuvarlakTop();
            yuvarlakTop.Location = new Point(x, y);      //ilk top da 10. saniyede olusturuluyor.

            toplar.Add(yuvarlakTop);
            form.Controls.Add(yuvarlakTop);
        }

        public void kontrol(Timer myTimer, Timer myTimer2, Timer myTimer3)
        {
            if (toplar.Count == 0 && count >= 4)
            {
                myTimer.Stop();
                myTimer2.Stop();
                myTimer3.Stop();
            }

            else if (toplar.Count >= 10)
            {
                myTimer.Stop();
                myTimer2.Stop();
                myTimer3.Stop();
            }
        }

        public void pauseButonClicked(Timer myTimer,Timer myTimer2,Timer myTimer3)
        {
            myTimer.Stop();
            myTimer2.Stop();
            myTimer3.Stop();
        }

        public void playButonClicked(Timer myTimer, Timer myTimer2, Timer myTimer3)
        {
            myTimer.Start();
            myTimer2.Start();
            myTimer3.Start();
        }
    }
}
