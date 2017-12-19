using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourSecretary.ViewModel
{
	class MainWindowViewModel : Livet.ViewModel
	{
		//メインウィンドウ
		//public SecretaryObjectViewModel secretary;
		OptionBarViewModel optionBar;

		public MainWindowViewModel()
		{
			//secretary = new SecretaryObjectViewModel();
		}

		//private int _WindowHeight;
		//public int WindowHeight
		//{
		//	get { return _WindowHeight; }
		//	set
		//	{
		//		if (_WindowHeight == value)
		//			return;
		//		_WindowHeight = value;
		//		RaisePropertyChanged(nameof(WindowHeight));
		//	}
		//}

		//private int _WindowWidth;
		//public int WindowWidth
		//{
		//	get { return _WindowWidth; }
		//	set
		//	{
		//		if (_WindowWidth == value)
		//			return;
		//		_WindowWidth = value;
		//		RaisePropertyChanged(nameof(WindowWidth));
		//	}
		//}
	}
}
