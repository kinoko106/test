using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourSecretary.Model;
using Microsoft.Win32;
using Livet.Commands;
using System.Windows.Media.Effects;

namespace YourSecretary.ViewModel
{
	class SettingTabViewModel : Livet.ViewModel
	{
		private MainWindowViewModel Observer = null;
		private ImageObjectModel m_ImageObject;
		private ImageObjectModel m_FolderObject;
		private ImageObjectModel m_PreviewObject;

		public SettingTabViewModel()
		{
			string gearPath = @"C:\Users\daichi\Source\Repos\test\YourSecretary\YourSecretary\bin\Debug\Resource\Image\gear.png";
			string folderPath = @"C:\Users\daichi\Source\Repos\test\YourSecretary\YourSecretary\bin\Debug\Resource\Image\folder.png";
			m_ImageObject = new ImageObjectModel(gearPath, 20);
			m_FolderObject = new ImageObjectModel(folderPath, 20);

			GearImagePath = m_ImageObject.FilePath;
			FolderImagePath = m_FolderObject.FilePath;
		}

		public SettingTabViewModel(object owner)
		{
			//string gearPath = @"C:\Users\daichi\Source\Repos\test\YourSecretary\YourSecretary\bin\Debug\Resource\Image\gear.png";
			//string folderPath = @"C:\Users\daichi\Source\Repos\test\YourSecretary\YourSecretary\bin\Debug\Resource\Image\folder.png";
			string gearPath = "gear.png";
			string folderPath = "folder.png";
			m_ImageObject = new ImageObjectModel(gearPath, 20);
			m_FolderObject = new ImageObjectModel(folderPath, 20);

			GearImagePath = m_ImageObject.FilePath;
			FolderImagePath = m_FolderObject.FilePath;

			Observer = (MainWindowViewModel)owner;

			//BlurEffect blur = new BlurEffect();
			//blur.Radius = 20;
			//blur.KernelType = KernelType.Gaussian;
			//BlurEffect = blur;
		}

		#region BlurEffect
		private Effect _BlurEffect;

		public Effect BlurEffect
		{
			get
			{ return _BlurEffect; }
			set
			{
				if (_BlurEffect == value)
					return;
				_BlurEffect = value;
				RaisePropertyChanged(nameof(BlurEffect));
			}
		}
		#endregion

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

		#region MainImagePath
		private string _MainImagePath;

		public string MainImagePath
		{
			get
			{ return _MainImagePath; }
			set
			{
				if (_MainImagePath == value)
					return;
				_MainImagePath = value;
				RaisePropertyChanged(nameof(MainImagePath));
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

		#region ApplyImage
		private ViewModelCommand _ApplyImage = null;

		public ViewModelCommand ApplyImage
		{
			get
			{
				if (_ApplyImage == null)
				{
					_ApplyImage = new ViewModelCommand(UpdateImage);
				}
				return _ApplyImage;
			}
		}
		#endregion

		#region SettingPanelOpen
		private ViewModelCommand _SettingPanelOpen = null;

		public ViewModelCommand SettingPanelOpen
		{
			get
			{
				if (_SettingPanelOpen == null)
				{
					_SettingPanelOpen = new ViewModelCommand(ApplyBlurEffect);
				}
				return _SettingPanelOpen;
			}
		}
		#endregion

		void ApplyBlurEffect()
		{
			Observer?.SetBlurEffect();
		}

		void UpdateImage()
		{
			Observer?.SetImage(MainImagePath);
			MainImagePath = "";
		}

		//リサイズしたい
		void UpdatePreview()
		{
			m_PreviewObject = new ImageObjectModel(MainImagePath, 400);
		}

		private void SelectImage()
		{
			string filePath = null;

			Action a = async () =>
			{
				await Task.Run(() =>
					{
						var dialog = new OpenFileDialog();
						dialog.Title = "ファイルを開く";
						dialog.Filter = "Image Files(*.jpg,*.jepg,*.png,*.bmp,*.tiff)|*.jpg;*.jepg;*.png;*.bmp;*.tiff|All files(*.*)|*.*";
						dialog.Multiselect = false;

						string defaultPath = System.IO.Directory.GetCurrentDirectory();
						defaultPath += @"\Resource\Image\";
						dialog.InitialDirectory = defaultPath;

						dialog.ShowDialog();

						filePath = dialog.FileName;
						if (!string.IsNullOrEmpty(filePath))
						{
							//Observer?.SetImage(filePath);
							MainImagePath = filePath;
						}
					}
				);
			};
			a();
		}
	}
}
