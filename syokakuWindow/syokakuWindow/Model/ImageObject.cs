using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace syokakuWindow.Model
{
	class ImageObject
	{
		private string _ImagePath;

		private int _width;
		private int _height;

		public BitmapImage Image = null;

		DispatcherTimer dispatcherTimer;
		private SoundPlayer player;
		bool onPlay = false;

		private List<string> timerVoice;

		public ImageObject()
		{

		}

		public ImageObject(string path)
		{
			_ImagePath = path;

			Image = new BitmapImage();
			Image.BeginInit();
			Image.UriSource = new Uri(path);
			Image.DecodePixelHeight = 800;
			Image.EndInit();

			_width = (int)Image.Width;
			_height = (int)Image.Height;

			timerVoice = new List<string>();
			string timePath = @"C:\Users\daichi\Source\Repos\test\syokakuWindow\syokakuWindow\Material\timer\";
			for (int i = 0; i < 24; i++)
			{
				string tmpPath;
				string name = (30 + i).ToString();
				tmpPath = timePath + name + ".wav";

				try
				{
					timerVoice.Add(tmpPath);
				}catch
				{
					continue;
				}
			}

			dispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal);
			dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
			dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
			dispatcherTimer.Start();

		}

		public void dispatcherTimer_Tick(object sender, EventArgs e)
		{
			DateTime date = System.DateTime.Now;
			if(date.Minute == 0 && !onPlay)
			{
				PlayTimeSignal(date.Hour);
				onPlay = true;
			}
			else if(date.Minute == 1)
			{
				onPlay = false;
			}
			//throw new NotImplementedException();
		}

		public void PlayTimeSignal(int num)
		{
			player = new System.Media.SoundPlayer(timerVoice[num]);
			player.Play();
		}

		public string ImagePath
		{
			get { return _ImagePath; }
			set	{ _ImagePath = value;}
		}
		public int Width
		{
			get { return _width; }
			private set { _width = 0; }
		}

		public int Height
		{
			get { return _height; }
			private set { _height = 0; }
		}


	}
}
