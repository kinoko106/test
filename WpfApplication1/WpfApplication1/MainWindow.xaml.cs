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
using Microsoft.Win32;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using OpenCvSharp.Extensions;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
		ImageData imageData;

        public MainWindow()
        {	
            InitializeComponent();
		}

        private void OnClick_load(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Multiselect = false,
                InitialDirectory = "C:\\Users\\03dai\\Documents\\Visual Studio 2015\\Projects\\WpfApplication1\\WpfApplication1\\Images",
                Filter = "Image Files(*.jpg,*.jepg,*.png,*.bmp,*.tiff)|*.jpg;*.jepg;*.png;*.bmp;*.tiff|All files(*.*)|*.*"
                //Filter = "Image Files(*.jpg,*.png)|*.jpg;*.png|All files(*.*)|*.*"
            };
            if (ofd.ShowDialog(Application.Current.MainWindow) == true)
            {
				//loadimage
				imageData = new ImageData(ofd.FileName);
				var bitmapImage = WriteableBitmapConverter.ToWriteableBitmap(imageData.MatImage);

				DataContext = new { Image = bitmapImage, Height = bitmapImage.Height, Width = bitmapImage.Width, FileName = imageData.FileName };
            }

        }

		private void Submited(object sender, RoutedEventArgs e)
		{
			if (imageData.MatImage == null)return;
			if (comboBox.SelectedIndex == 0)
			{
				//edge
				Mat gray = new Mat(Cv.Size(imageData.Width, imageData.Height),MatType.CV_8SC1),canny = new Mat(Cv.Size(imageData.Width, imageData.Height), MatType.CV_8SC1);
				Cv2.CvtColor(imageData.MatImage, gray, ColorConversion.BgrToGray);
				Cv2.Canny(gray, canny, 100, 200);
				var bitmapEdge = WriteableBitmapConverter.ToWriteableBitmap(canny);
				var bitmapImage = WriteableBitmapConverter.ToWriteableBitmap(imageData.MatImage);

				DataContext = new { FileName = imageData.FileName, Image = bitmapImage, Edge = bitmapEdge, Height = bitmapImage.Height, Width = bitmapImage.Width };
			}
			else if (comboBox.SelectedIndex == 1)
			{

			}
			else if (comboBox.SelectedIndex == 2)
			{

			}
		}
		private void comboBox_changed(object sender, RoutedEventArgs e)
		{
			if (comboBox.SelectedIndex == 0)
			{
				
			}
		}
	}
}
