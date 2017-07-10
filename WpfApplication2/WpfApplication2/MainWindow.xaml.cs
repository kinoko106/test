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
using WpfApplication2.ViewModels;

namespace WpfApplication2
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		//window全体のViewModel君 いらない
		//private MainWindowViewModel mainWindow;

		public MainWindow()
		{
			InitializeComponent();

			//↓いらない wpf側でWindowにDataContextするのが正解らしい
			//mainWindow = new MainWindowViewModel();
		}
	}
}
