using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication2.Models;
using Livet;

namespace WpfApplication2.ViewModels
{

	//MainWindowのViewModel,こいつを基に色々なコントロールを管理してくことになる
	//ウィンドウ用ViewModelBaseを継承してることにする
	class MainWindowViewModel : WindowViewModelBase
	{
		FolderTreePanelViewModel folderTreeViewModel;
		FileViewPanelViewModel filePanelViewModel;

		#region 高さ
		public int _Height;
		public int Height
		{
			get { return this._Height; }
			set
			{
				if (this._Height != value)
				{
					_Height = value;
					this.RaisePropertyChanged(nameof(Height));
				}
			}
		}
		#endregion

		#region 幅
		public int _Width;
		public int Width
		{
			get { return this._Width; }
			set
			{
				if (this._Width != value)
				{
					_Width = value;
					this.RaisePropertyChanged(nameof(Width));
				}
			}
		}
		#endregion

		#region タイトル
		public string _WindowTitle;
		public string WindowTitle
		{
			get { return this._WindowTitle; }
			set
			{
				if(this._WindowTitle != value)
				{
					_WindowTitle = value;
					this.RaisePropertyChanged(nameof(WindowTitle));
				}
			}
		}
		#endregion

		public MainWindowViewModel()
		{
			//Windowの要素初期化
			WindowTitle = "ファイル交換君";
			//windowスタイルや各コントロールの挙動もどこかで初期化したい,xamlに直書きでよいのか

			//viewModelを作る
			folderTreeViewModel = new FolderTreePanelViewModel();
			filePanelViewModel = new FileViewPanelViewModel();
		}
	}
}
