using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using YourSecretary.Model;
using Livet.Commands;
using System.Drawing;

namespace YourSecretary.ViewModel
{
	class SecretaryObjectViewModel : Livet.ViewModel
	{
		private MainObjectModel mainObject;
		private MainWindowViewModel _owner;

		public SecretaryObjectViewModel()
		{
			mainObject= new MainObjectModel();

			WindowWidth = mainObject.Width;
			WindowHeight = mainObject.Height + 50;
			ImagePath = mainObject.ImagePath;
			Mask = 1;
		}

		public SecretaryObjectViewModel(MainWindowViewModel owner)
		{
			_owner = owner;

			mainObject = new MainObjectModel();

			WindowWidth = mainObject.Width;
			WindowHeight = mainObject.Height + 50;
			ImagePath = mainObject.ImagePath;
			Mask = 1;
		}

		#region MouseLeftButtonUpCommand
		private ViewModelCommand _MouseLeftButtonUp = null;

		public ViewModelCommand MouseLeftButtonUp
		{
			get
			{
				if (_MouseLeftButtonUp == null)
				{
					_MouseLeftButtonUp = new ViewModelCommand(LeftButtonUp);
				}
				return _MouseLeftButtonUp;
			}
		}
		#endregion

		#region MouseLeftButtonDown
		private ViewModelCommand _MouseLeftButtonDown = null;

		public ViewModelCommand MouseLeftButtonDown
		{
			get
			{
				if (_MouseLeftButtonDown == null)
				{
					_MouseLeftButtonDown = new ViewModelCommand(LeftButtonDown);
				}
				return _MouseLeftButtonDown;
			}
		}
		#endregion

		#region MouseRightButtonDown
		private ViewModelCommand _MouseRightButtonDown = null;

		public ViewModelCommand MouseRightButtonDown
		{
			get
			{
				if (_MouseRightButtonDown == null)
				{
					_MouseRightButtonDown = new ViewModelCommand(UpdateMask);
				}
				return _MouseRightButtonDown;
			}
		}
		#endregion

		#region MouseEnter
		private ViewModelCommand _MouseEnter = null;

		public ViewModelCommand MouseEnter
		{
			get
			{
				if (_MouseEnter == null)
				{
					_MouseEnter = new ViewModelCommand(UpdateMask);
				}
				return _MouseEnter;
			}
		}
		#endregion

		#region width
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
		#endregion

		#region height
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
		#endregion

		#region top
		private int _WindowTop;

		public int WindowTop
		{
			get
			{ return _WindowTop; }
			set
			{
				if (_WindowTop == value)
					return;
				_WindowTop = value;
				RaisePropertyChanged(nameof(WindowTop));
			}
		}
		#endregion

		#region left
		private int _WindowLeft;

		public int WindowLeft
		{
			get
			{ return _WindowLeft; }
			set
			{
				if (_WindowLeft == value)
					return;
				_WindowLeft = value;
				RaisePropertyChanged(nameof(WindowLeft));
			}
		}
		#endregion

		#region imagepath
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
		#endregion

		#region Mask
		public double _Mask;
		public double Mask
		{
			get { return _Mask; }
			set
			{
				if (_Mask == value)
					return;
				_Mask = value;
				RaisePropertyChanged(nameof(Mask));
			}
		}
		#endregion

		public void UpdateMask()
		{
			Mask = mainObject.ToggleTransparent();
			mainObject.ToggleSayVoiceState();
		}

		bool _MAskFlag = false;
		public void ToggleMaskFlag()
		{
			_MAskFlag = _MAskFlag ? true : false;
		}

		private int startX;
		private int startY;
		private int endX,endY;
		public void LeftButtonDown()
		{
			startY = WindowTop;
			startX = WindowLeft;
		}

		public void LeftButtonUp()
		{
			endY = WindowTop;
			endX = WindowLeft;

			if (startY == endY && startX == endX)
				mainObject.SayClickVoice();
		}
	}
}
