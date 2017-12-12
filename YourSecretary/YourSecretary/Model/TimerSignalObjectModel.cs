using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace YourSecretary.Model
{
	class TimerSignalObjectModel
	{
		private const int Timers = 24;
		private List<SoundPlayer> timerSignals = new List<SoundPlayer>();

		//private const string timerDirectry = @".\Resource\Sound\TimerSignal\";
		private const string timerDirectry = @"C:\Users\daichi\Source\Repos\test\YourSecretary\YourSecretary\bin\Debug\Resource\Sound\TimerSignal\";

		public TimerSignalObjectModel()
		{
			string[] files = Directory.GetFiles(timerDirectry, "*", SearchOption.TopDirectoryOnly);
			int fileCount = files.Length;

			int counter = fileCount < Timers ? fileCount : Timers;
			for(int i = 0;i < counter; i++)
			{
				//string filename = files[i].Substring(timerDirectry.Length);
				timerSignals.Add(new SoundPlayer(files[i]));
			}
		}

		public void PlayTimerSignal(int num)
		{
			timerSignals[num].Play();
		}
	}
}
