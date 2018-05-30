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
using YourSecretary.Model;

namespace YourSecretary
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class Window : System.Windows.Window
	{
		public Window()
		{
			this.MouseLeftButtonDown += (sender, e) => { this.DragMove(); };
			//this.MouseLeftButtonUp += (sender, e) => { int a = 0; };

			this.Loaded += (sender, e) =>
			{	
				//mainObject = new MainObjectModel();

				//this.DataContext = new
				//{
				//	ImagePath = mainObject.ImagePath,
				//	WindowHeight = mainObject.Height,
				//	WindowWidth = mainObject.Width
				//};
			};

			InitializeComponent();
		}
	}
}
