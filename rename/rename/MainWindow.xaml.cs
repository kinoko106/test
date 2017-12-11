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

namespace rename
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			string path = textBox.Text;
			string prefix = textBox1.Text;

			if (path == "" || prefix == "") return;

			string[] files = System.IO.Directory.GetFiles(path, "*", System.IO.SearchOption.AllDirectories);

			foreach(string file in files)
			{
				int pathLength = path.Length + 2;
				string beforeFileName = file.Substring(pathLength);
				string newName = prefix + "_" + beforeFileName;

				string newFilePath = path + "//" + newName;
				System.IO.File.Move(file, newFilePath);
			}
		}
	}
}
