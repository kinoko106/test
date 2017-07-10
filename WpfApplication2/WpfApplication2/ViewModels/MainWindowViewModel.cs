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
		//通知飛ばしてみるオブジェクト
		public string _testText;
		public string testText
		{
			get { return this._testText; }
			set
			{
				if (this._testText != value)
				{
					_testText = value;
					this.RaisePropertyChanged(nameof(testText));
				}
			}
		}

		public MainWindowViewModel()
		{
			testText = "おためしwpf";
		}
	}
}
