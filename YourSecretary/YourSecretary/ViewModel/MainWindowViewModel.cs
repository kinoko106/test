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
		
		//OptionBarViewModel optionBar;

		public MainWindowViewModel()
		{
			Secretary = new SecretaryObjectViewModel(this);
			WindowHeight = Secretary.WindowHeight + 20;
			WindowWidth = Secretary.WindowWidth;

			SettingTab = new SettingTabViewModel(this);
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

		#region Secretary
		public SecretaryObjectViewModel _Secretary;

		public SecretaryObjectViewModel Secretary
		{
			get
			{ return _Secretary; }
			set
			{
				if (_Secretary == value)
					return;
				_Secretary = value;
				RaisePropertyChanged(nameof(Secretary));
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

		public void SetBlurEffect()
		{
			Secretary.SetBlurEffect(); 
		}

		public void SetImage(string path)
		{
			Secretary.UpdateImage(path);
		}
	}
}
