using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace YourSecretary.Model
{
	class TimerSignalObjectModel
	{
		private const int Timers = 24;
		private List<SoundPlayer> timerSignals = new List<SoundPlayer>();

		private List<string> filePathes = new List<string>();
		private SoundPlayer player = null;

		DispatcherTimer timer = null;
		bool doPlayFlg;

		//private const string timerDirectry = @".\Resource\Sound\TimerSignal\";
		private const string timerDirectry = @"C:\Users\daichi\Source\Repos\test\YourSecretary\YourSecretary\bin\Debug\Resource\Sound\TimerSignal\";

		public TimerSignalObjectModel()
		{
			string[] files = Directory.GetFiles(timerDirectry, "*", SearchOption.TopDirectoryOnly);
			filePathes.AddRange(files);

			//int fileCount = files.Length;

			//int counter = fileCount < Timers ? fileCount : Timers;
			//for(int i = 0;i < counter; i++)
			//{
			//	//string filename = files[i].Substring(timerDirectry.Length);
			//	timerSignals.Add(new SoundPlayer(files[i]));
			//}

			timer = new DispatcherTimer();
			timer.Interval = new TimeSpan(0, 0, 1);
			timer.Tick += new EventHandler(dispatcherTimer_Tick);
			timer.Start();

			doPlayFlg = false;
		}

		public void PlayTimerSignal(int num)
		{
			player = !string.IsNullOrEmpty(filePathes[num]) ? new SoundPlayer(filePathes[num]) : null;
			player?.Play();
		}

		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{
			DateTime dateTime = DateTime.Now;
			if (dateTime.Minute == 0 && !doPlayFlg)
			{
				PlayTimerSignal(dateTime.Hour);
				doPlayFlg = true;
			}
			else if (dateTime.Minute == 1)
			{
				doPlayFlg = false;
			}
		}
	}
}
