using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourSecretary.Model;
using Microsoft.Win32;
using Livet.Commands;

namespace YourSecretary.ViewModel
{
	class SettingTabViewModel : Livet.ViewModel
	{
		private ImageObjectModel m_ImageObject;
		ImageObjectModel m_FolderObject;

		public SettingTabViewModel()
		{
			string gearPath = @"C:\Users\daichi\Source\Repos\test\YourSecretary\YourSecretary\bin\Debug\Resource\Image\gear.png";
			string folderPath = @"C:\Users\daichi\Source\Repos\test\YourSecretary\YourSecretary\bin\Debug\Resource\Image\folder.png";
			m_ImageObject = new ImageObjectModel(gearPath, 20);
			m_FolderObject = new ImageObjectModel(folderPath, 20);

			GearImagePath = m_ImageObject.FilePath;
			FolderImagePath = m_FolderObject.FilePath;
			//PanelHeight = 30;
		}

		#region FolderImagePath
		private string _FolderImagePath;

		public string FolderImagePath
		{
			get
			{ return _FolderImagePath; }
			set
			{
				if (_FolderImagePath == value)
					return;
				_FolderImagePath = value;
				RaisePropertyChanged(nameof(FolderImagePath));
			}
		}
		#endregion

		#region GearImagePath
		private string _GearImagePath;

		public string GearImagePath
		{
			get
			{ return _GearImagePath; }
			set
			{
				if (_GearImagePath == value)
					return;
				_GearImagePath = value;
				RaisePropertyChanged(nameof(GearImagePath));
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

		#region OpenDialog
		private ViewModelCommand _OpenDialog = null;

		public ViewModelCommand OpenDialog
		{
			get
			{
				if (_OpenDialog == null)
				{
					_OpenDialog = new ViewModelCommand(SelectImage);
				}
				return _OpenDialog;
			}
		}
		#endregion


		void SelectImage()
		{
			string filePath = null;

			Action a = async() =>
			{
				await Task.Run(() => 
					{
						var dialog = new OpenFileDialog();
						dialog.Title = "ファイルを開く";
						dialog.Filter = "Image Files(*.jpg,*.jepg,*.png,*.bmp,*.tiff)|*.jpg;*.jepg;*.png;*.bmp;*.tiff|All files(*.*)|*.*";
						dialog.Multiselect = false;

						dialog.ShowDialog();
						
						filePath = dialog.FileName;
					}
				);
			};
			a();
		}
	}
}
