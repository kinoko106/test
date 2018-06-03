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
		public SecretaryObjectViewModel Secretary;
		//OptionBarViewModel optionBar;

		public MainWindowViewModel()
		{
			Secretary = new SecretaryObjectViewModel(this);
			WindowHeight = Secretary.WindowHeight + 20;
			WindowWidth = Secretary.WindowWidth;
			PanelHeight = Secretary.WindowHeight + 20;
			PanelWidth = Secretary.WindowWidth;
			ImageHeight = Secretary.WindowHeight;
			ImageWidth = Secretary.WindowWidth;
			ImagePath = Secretary.ImagePath;
			SettingTabHeight = WindowHeight;
			SettingTabWidth = Secretary.WindowWidth;

			SettingTab = new SettingTabViewModel();
			SettingTab.PanelHeight = Secretary.WindowHeight + 20; ;
			SettingTab.PanelWidth = Secretary.WindowWidth;
		}

		#region SettingTab
		public SettingTabViewModel _SettingTab;

		public SettingTabViewModel SettingTab
		{
			get
			{ return _SettingTab; }
			set
			{
				if (_SettingTab == value)
					return;
				_SettingTab = value;
				RaisePropertyChanged(nameof(SettingTab));
			}
		}
		#endregion

		#region WindowWidth
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

		#region WindowHeight
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

		#region PanelWidth
		private int _PanelWidth;

		public int PanelWidth
		{
			get
			{ return _PanelWidth; }
			set
			{
				if (_PanelWidth == value)
					return;
				_PanelWidth = value;
				RaisePropertyChanged(nameof(PanelWidth));
			}
		}
		#endregion

		#region PanelHeight
		private int _PanelHeight;

		public int PanelHeight
		{
			get
			{ return _PanelHeight; }
			set
			{
				if (_PanelHeight == value)
					return;
				_PanelHeight = value;
				RaisePropertyChanged(nameof(PanelHeight));
			}
		}
		#endregion

		#region ImageWidth
		private int _ImageWidth;

		public int ImageWidth
		{
			get
			{ return _ImageWidth; }
			set
			{
				if (_ImageWidth == value)
					return;
				_ImageWidth = value;
				RaisePropertyChanged(nameof(ImageWidth));
			}
		}
		#endregion

		#region ImageHeight
		private int _ImageHeight;

		public int ImageHeight
		{
			get
			{ return _ImageHeight; }
			set
			{
				if (_ImageHeight == value)
					return;
				_ImageHeight = value;
				RaisePropertyChanged(nameof(ImageHeight));
			}
		}
		#endregion

		#region SettingTabWidth
		private int _SettingTabWidth;

		public int SettingTabWidth
		{
			get
			{ return _SettingTabWidth; }
			set
			{
				if (_SettingTabWidth == value)
					return;
				_SettingTabWidth = value;
				RaisePropertyChanged(nameof(SettingTabWidth));
			}
		}
		#endregion

		#region SettingTabHeight
		private int _SettingTabHeight;

		public int SettingTabHeight
		{
			get
			{ return _SettingTabHeight; }
			set
			{
				if (_SettingTabHeight == value)
					return;
				_SettingTabHeight = value;
				RaisePropertyChanged(nameof(SettingTabHeight));
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

		#region ImagePath
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
	}
}
