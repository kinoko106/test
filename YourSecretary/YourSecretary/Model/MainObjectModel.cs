using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Livet;
using System.Windows.Input;
using System.Windows.Controls;

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

		public string Mask
		{
			get { return mainImage.Mask; }
			set { mainImage.Mask = value; }
		}

		public void MouseMoveStart()
		{
		}

		public void MouseMoveEnd()
		{

			voice.PlayClickVoice();
		}

		public void SayClickVoice()
		{
			voice.PlayClickVoice();
		}

		public void SwitchClickable()
		{
			mainImage.Mask = "";
		}
	}
}
