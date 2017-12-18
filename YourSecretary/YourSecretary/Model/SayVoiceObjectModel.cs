using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace YourSecretary.Model
{
	class SayVoiceObjectModel
	{
		private const string voiceDirectry = @".\Resource\Sound\Voice\";
		//private const string voiceDirectry = @"C:\Users\daichi\Source\Repos\test\YourSecretary\YourSecretary\bin\Debug\Resource\Sound\Voice\";
		private List<string> clickVoices = new List<string>();
		private SoundPlayer player = null;

		private bool _CanPlay = true;

		public SayVoiceObjectModel()
		{
			_CanPlay = true;
		}

		public bool CanPlay
		{
			get { return this._CanPlay; }
			set { this._CanPlay = value; }
		}

		public void PlayClickVoice()
		{
			if (!CanPlay)
				return;

			clickVoices.Clear();
			string[] files = Directory.GetFiles(voiceDirectry, "*", SearchOption.TopDirectoryOnly);
			clickVoices.AddRange(files);

			if (clickVoices.Count() == 0) return;

			player = !string.IsNullOrEmpty(clickVoices[0]) ? new SoundPlayer(clickVoices[0]) : null;
			player?.Play();
		}

		public void PlayClickVoice(int num)
		{
			clickVoices.Clear();
			string[] files = Directory.GetFiles(voiceDirectry, "*", SearchOption.TopDirectoryOnly);
			clickVoices.AddRange(files);

			player = !string.IsNullOrEmpty(clickVoices[0]) ? new SoundPlayer(clickVoices[0]) : null;
			player?.Play();
		}
	}
}
