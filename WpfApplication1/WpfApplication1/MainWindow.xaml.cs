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
				var bitmapImage = WriteableBitmapConverter.ToWriteableBitmap(imageData.resizeImage);
				comboBox.IsEnabled = true;

				DataContext = new { Image = bitmapImage, Height = bitmapImage.Height, Width = bitmapImage.Width, FileName = imageData.FileName };
            }

        }

		private void Submited(object sender, RoutedEventArgs e)
		{
			if (imageData.MatImage == null)return;
			switch (comboBox.SelectedIndex)
			{
				case 0://edge
					int th1, th2;
					th1 = int.Parse(CannyThreshold1.Text);
					th2 = int.Parse(CannyThreshold2.Text);
					Mat gray = new Mat(Cv.Size(imageData.Width, imageData.Height), MatType.CV_8SC1), canny = new Mat(Cv.Size(imageData.Width, imageData.Height), MatType.CV_8SC1);
					Cv2.CvtColor(imageData.MatImage, gray, ColorConversion.BgrToGray);
					Cv2.Canny(gray, canny, th1, th2);
					var bitmapProcessed = WriteableBitmapConverter.ToWriteableBitmap(canny);
					var bitmapImage = WriteableBitmapConverter.ToWriteableBitmap(imageData.resizeImage);

					DataContext = new { FileName = imageData.FileName, Image = bitmapImage, Edge = bitmapProcessed, Height = bitmapImage.Height, Width = bitmapImage.Width };
					break;
				case 1://fast 変
					int th;
					KeyPoint[] keypoints;
					th = int.Parse(FASTThreashold.Text);
					gray = new Mat(Cv.Size(imageData.Width, imageData.Height), MatType.CV_8UC1);
					Mat fast = imageData.MatImage.Clone();
					Cv2.FAST(gray,out keypoints, th);
					foreach(KeyPoint k in keypoints)
					{
						Cv2.Circle(fast, k.Pt, 1,new Scalar(0, 0, 255),-1);
						Cv2.Circle(fast, k.Pt, 5, new Scalar(0, 0, 255));
					}
					bitmapProcessed = WriteableBitmapConverter.ToWriteableBitmap(fast);
					bitmapImage = WriteableBitmapConverter.ToWriteableBitmap(imageData.resizeImage);

					DataContext = new { FileName = imageData.FileName, Image = bitmapImage, Edge = bitmapProcessed, Height = bitmapImage.Height, Width = bitmapImage.Width };
					break;
				case 2:
					break;
			}
		}
		private void comboBox_changed(object sender, RoutedEventArgs e)
		{
			//後でswitch
			switch (comboBox.SelectedIndex)
			{
				case 0://select canny
					if (CannyPanel != null && FASTPanel != null)
					{
						CannyPanel.Visibility = Visibility.Visible;
						FASTPanel.Visibility = Visibility.Collapsed;
					}
					break;
				case 1://select fast
					CannyPanel.Visibility = Visibility.Collapsed;
					FASTPanel.Visibility = Visibility.Visible;
					break;
				case 2://select cdfast
					break;
			}
		}
	}
}
