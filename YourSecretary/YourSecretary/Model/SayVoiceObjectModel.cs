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
		//private const string voiceDirectry = @".\Resource\Sound\Voice\";
		private const string voiceDirectry = @"C:\Users\daichi\Source\Repos\test\YourSecretary\YourSecretary\bin\Debug\Resource\Sound\Voice\";
		private List<SoundPlayer> clickVoice = new List<SoundPlayer>();

		private bool canPlay = true;

		public SayVoiceObjectModel()
		{
			string[] files = Directory.GetFiles(voiceDirectry, "*", SearchOption.TopDirectoryOnly);

			foreach(string file in files)
			{
				clickVoice.Add(new SoundPlayer(file));
			}

			canPlay = true;
		}

		public void PlayClickVoice()
		{
			if (canPlay)
			{
				clickVoice[0].Play();
			}
		}

		public void PlayClickVoice(int num)
		{
			clickVoice[num].Play();
		}
	}
}
