using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using static System.Console;
using static System.IO.Directory;
using static System.IO.Path;
using static System.Environment;
using System.Threading;

namespace GorselProje5
{
	class jsonClass
	{
		Form1 form;
		public PlayPauseButton restoreButon = new PlayPauseButton(70, 60);
		public PlayPauseButton backupButon = new PlayPauseButton(190, 60);
		Random rand = new Random();
		OyunDurumu ballsFromJson;

		Oyun oyun;

		public jsonClass(Form1 form, ref Oyun oyun)
        {
			this.form = form;
			backupButon.Text = "Backup";
			restoreButon.Text = "Restore";
			form.Controls.Add(restoreButon);
			form.Controls.Add(backupButon);
			this.oyun = oyun;
		}

		private readonly string _path = Combine(CurrentDirectory, "gp_yedek.json");

		public void restoreButonClicked()
		{
			try
			{
				string jsonFromFile;
				using (var reader = new StreamReader(_path))
				{
					jsonFromFile = reader.ReadToEnd();					
				}

				var sifrelenmis = Sifreleme.Decrypt(jsonFromFile);
				ballsFromJson = JsonConvert.DeserializeObject<OyunDurumu>(sifrelenmis);
			}
			catch (Exception e)
			{
				
			}
		}

		public void backupButonClicked()
		{
			try
			{
				var oyundurumu = OyunBilgisiAl();
				var jsonToWrite = JsonConvert.SerializeObject(oyundurumu, Formatting.Indented);
				var sifreleme = Sifreleme.Encrypt(jsonToWrite);
				using (var writer = new StreamWriter(_path))
				{
					writer.Write(sifreleme);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		public void backupButonClickedThread()
		{
            while (true)
            {
				Thread.Sleep(100000);
				try
				{
					var oyundurumu = OyunBilgisiAl();

					var jsonToWrite = JsonConvert.SerializeObject(oyundurumu, Formatting.Indented);

					var sifreleme = Sifreleme.Encrypt(jsonToWrite);

					using (var writer = new StreamWriter(_path))
					{
						writer.Write(sifreleme);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}

		public void otomatikKayit()
		{
			backupButonClickedThread();
		}

		public OyunDurumu OyunBilgisiAl()
		{
			List<Top> toplar2 = new List<Top>();
            foreach (var i in oyun.toplar)
            {
				Top temp = new Top
				{
					topRengi = i.BackColor,
					topX = i.Location.X,
					topY = i.Location.Y,
					x = i.x,
					y = i.y
				};

				toplar2.Add(temp);
            }
			var oyunDurumu = new OyunDurumu
			{
				topSayisi = toplar2.Count,
				skor = oyun.score,
				toplar3 = toplar2,	
			};

			return oyunDurumu;
		}

		public void topOlustur()
		{
			restoreButonClicked();
			for (int i = 0; i < oyun.toplar.Count; i++)
			{
				form.Controls.Remove(oyun.toplar[i]);
			}
			oyun.toplar.Clear();

			foreach (var i in ballsFromJson.toplar3)
            {
				YuvarlakTop yuvarlakTop = new YuvarlakTop();
				yuvarlakTop.r = i.topRengi.R;
				yuvarlakTop.g = i.topRengi.G;
				yuvarlakTop.b = i.topRengi.B;
				yuvarlakTop.BackColor = Color.FromArgb(yuvarlakTop.r, yuvarlakTop.g, yuvarlakTop.b);
				yuvarlakTop.Location = new Point(i.topX, i.topY);
				yuvarlakTop.x = i.x;
				yuvarlakTop.y = i.y;

				oyun.toplar.Add(yuvarlakTop);
				form.Controls.Add(yuvarlakTop);
			}
			oyun.score = ballsFromJson.skor;
			oyun.scoreLabel.Text = "Score: "+oyun.score.ToString();
			
		}

		public void evetHayirSor()
		{
			const string message =
				"Yedekten Yükleme Yapılsın mı?";
			const string caption = "Yedekleme İşlemi";
			var result = MessageBox.Show(message, caption,
										 MessageBoxButtons.YesNo,
										 MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				topOlustur();
			}
		}
	}

	public class OyunDurumu
    {
		public int topSayisi { get; set; }
		public int skor { get; set; }
		public List<Top> toplar3 { get; set; }		
	}

}

