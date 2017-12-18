using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YourSecretary.Model;
using Livet.Commands;
using System.Windows;
using System.Drawing;

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

			//WindowTop = 100;
		}
		
		#region ClickvoiceCommand
		private ViewModelCommand _SayClickVoice = null;

		public ViewModelCommand SayClickVoice
		{
			get
			{
				if (_SayClickVoice == null)
				{
					//_SayClickVoice = new ViewModelCommand(mainObject.MouseMoveEnd);
					_SayClickVoice = new ViewModelCommand(LeftButtonUp);
				}
				return _SayClickVoice;
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

		#region SwitchClickableCommand
		private ViewModelCommand _SwitchClickable = null;

		public ViewModelCommand SwitchClickable
		{
			get
			{
				if (_SwitchClickable == null)
				{
					_SwitchClickable = new ViewModelCommand(UpdateMask);
				}
				return _SwitchClickable;
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
		public string Mask
		{
			get { return mainObject.Mask; }
			set
			{
				if (mainObject.Mask == value)
					return;
				mainObject.Mask = value;
				RaisePropertyChanged("Mask");
			}
		}
		#endregion

		public void UpdateMask()
		{
			Mask = mainObject.Mask;
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
