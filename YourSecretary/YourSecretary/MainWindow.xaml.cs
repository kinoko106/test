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

using System.Windows.Interop;
using System.Runtime.InteropServices;

using YourSecretary.Model;
using Microsoft.Windows.Shell;

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

			//this.Loaded += (sender, e) =>
			//{
			//	//HwndSource source = HwndSource.FromVisual(this) as HwndSource;
			//	//IntPtr handle = source.Handle;
			//	//source.CompositionTarget.BackgroundColor = Color.FromArgb(0, 0, 0, 0);

			//	//MARGINS margins = new MARGINS
			//	//{ cxLeftWidth = -1, cxRightWidth = 0, cyTopHeight = 0, cyBottomHeight = 0 };
			//	//DwmExtendFrameIntoClientArea(handle, margins);
			//};
			this.Loaded += new RoutedEventHandler(MainWindow_Loaded);

			InitializeComponent();
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			// グラス効果が利用できるかを確認してスタイルを適用する
			this.Style = SystemParameters2.Current.IsGlassEnabled
				? (Style)Resources["GadgetStyle"] : null;
		}
		//[StructLayout(LayoutKind.Sequential)]
		//public class MARGINS
		//{
		//	public int cxLeftWidth;
		//	public int cxRightWidth;
		//	public int cyTopHeight;
		//	public int cyBottomHeight;
		//}

		//[DllImport("dwmapi.dll", PreserveSig = false)]
		//public static extern void DwmExtendFrameIntoClientArea(
		//	IntPtr hWnd, MARGINS pMargins);
	}
}
