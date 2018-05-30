using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourSecretary.ViewModel
{
	class SettingTabViewModel : Livet.ViewModel
	{
		public SettingTabViewModel()
		{
			//PanelHeight = 30;
		}
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
	}
}
