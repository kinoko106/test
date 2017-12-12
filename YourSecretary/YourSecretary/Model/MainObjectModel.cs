using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Livet;

namespace YourSecretary.Model
{
	class MainObjectModel
	{
		private TimerSignalObjectModel timerSignal = null;
		private ImageObjectModel mainImage = null;
		private SayVoiceObjectModel voice = null;

		public MainObjectModel()
		{
			timerSignal = new TimerSignalObjectModel();
			mainImage = new ImageObjectModel();
			voice = new SayVoiceObjectModel();
		}

		public int Width
		{
			get { return mainImage.Width; }
		}

		public int Height
		{
			get { return mainImage.Height; }
		}

		public string ImagePath
		{
			get { return mainImage.FilePath; }
		}

		public void SayClickVoice()
		{
			voice.PlayClickVoice();

		}
	}
}
