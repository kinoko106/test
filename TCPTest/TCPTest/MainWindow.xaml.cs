using System;
using System.Collections.Generic;
using System.IO;
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
using System.Diagnostics;

namespace TCPTest
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public string filePath;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void grid_PreviewDragOver(object sender, System.Windows.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop, true))
			{
				e.Effects = System.Windows.DragDropEffects.Copy;
			}
			else
			{
				e.Effects = System.Windows.DragDropEffects.None;
			}
			e.Handled = true;
		}
		private void grid_Drop(object sender, System.Windows.DragEventArgs e)
		{
			var dropFiles = e.Data.GetData(System.Windows.DataFormats.FileDrop) as string[];
			if (dropFiles == null) return;
			filePath = dropFiles[0];
			textBox2.Text = dropFiles[0];

			Stopwatch sw = new Stopwatch();
			sw.Start();
			using (BinaryReader br = new BinaryReader(File.OpenRead(filePath)))
			{
				List<byte> fileData = new List<byte>();
				try
				{
					for (;;)
					{
						fileData.Add(br.ReadByte());
					}
				}
				catch (EndOfStreamException)
				{
					
				}
			}

			textBlock3.Text = sw.ElapsedMilliseconds.ToString();
			//var directry = System.IO.Directory.GetParent(filePath);
			//string fileName = System.IO.Path.GetFileName(filePath);
			//string newFile = @directry.FullName + @"\newfile.txt";
		}
	}
}
