using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YourSecretary.Model;
using Livet.Commands;

namespace YourSecretary.ViewModel
{
	class MainObjectViewModel : Livet.ViewModel
	{
		private MainObjectModel mainObject;

		public MainObjectViewModel()
		{
			mainObject= new MainObjectModel();

			WindowWidth = mainObject.Width;
			WindowHeight = mainObject.Height;
			ImagePath = mainObject.ImagePath;
		}

		private ViewModelCommand _SayClickVoice = null;

		public ViewModelCommand SayClickVoice
		{
			get
			{
				if (_SayClickVoice == null)
				{
					//_SayClickVoice = new ViewModelCommand(doMesod);
					_SayClickVoice = new ViewModelCommand(mainObject.SayClickVoice);
				}
				return _SayClickVoice;
			}
		}


		private int _WindowWidth;

		public int WindowWidth
		{
			get
			{ return _WindowWidth; }
			set
			{
				if (_WindowWidth == value)
					return;
				_WindowWidth = value;
				RaisePropertyChanged(nameof(WindowWidth));
			}
		}

		private int _WindowHeight;

		public int WindowHeight
		{
			get
			{ return _WindowHeight; }
			set
			{
				if (_WindowHeight == value)
					return;
				_WindowHeight = value;
				RaisePropertyChanged(nameof(WindowHeight));
			}
		}

		private string _ImagePath;

		public string ImagePath
		{
			get
			{ return _ImagePath; }
			set
			{
				if (_ImagePath == value)
					return;
				_ImagePath = value;
				RaisePropertyChanged(nameof(ImagePath));
			}
		}
	}
}
