using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using syokakuWindow.Model;
using Microsoft.Win32;
using System.Windows.Threading;

namespace syokakuWindow
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		ImageObject mainObject;

		public MainWindow()
		{
			this.MouseLeftButtonDown += (sender, e) => { this.DragMove(); };

			string path = @"C:\Users\daichi\Source\Repos\test\syokakuWindow\syokakuWindow\Material\santa_syokaku.png";
			//string path = @"C:\Users\daichi\Source\Repos\test\syokakuWindow\syokakuWindow\Material\santa_syokaku_600.png";
			mainObject = new ImageObject(path);

			this.DataContext = new
							{
								path = mainObject.ImagePath ,
								WindowHeight = mainObject.Height,
								WindowWidth = mainObject.Width
							};

			InitializeComponent();
		}

		//public void dispatcherTimer_Tick(object sender, EventArgs e)
		//{
		//	DateTime date = System.DateTime.Now;
		//	if (date.Second == 0)
		//	{
		//		mainObject.PlayTimeSignal(0);
		//	}
		//	//throw new NotImplementedException();
		//}

		private void Image_MouseDown(object sender, MouseButtonEventArgs e)
		{
			string path;
			var dialog = new OpenFileDialog();
			dialog.ShowDialog();

			path = dialog.FileName;
			mainObject = new ImageObject(path);

			this.DataContext = new
			{
				path = mainObject.ImagePath,
				WindowHeight = mainObject.Height,
				WindowWidth = mainObject.Width
			};
		}
	}
}
