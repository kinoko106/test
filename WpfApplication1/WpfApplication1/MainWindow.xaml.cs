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
using OpenCvSharp.Blob;
using OpenCvSharp.UserInterface;
using OpenCvSharp.Utilities;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {

		IplImage loadImage;
		string fileName;

        public MainWindow()
        {
            InitializeComponent();
            //CvMat matimg;
            //gazou = new IplImage("C:\\Users\\03dai\\Documents\\Visual Studio 2015\\Projects\\WpfApplication1\\WpfApplication1\\Images\\shokaku.jpg", LoadMode.Color);
            //int height, width;
            //height = gazou.Height;
            //width = gazou.Width;
            

            //var bitmapImage = WriteableBitmapConverter.ToWriteableBitmap(gazou);
            //DataContext = new { Height = 0,Width = 0 };
        }

        private void OnClick_load(object sender, RoutedEventArgs e)
        {

            IplImage tmpImg;
            var ofd = new OpenFileDialog
            {
                Multiselect = true,
                InitialDirectory = "C:\\Users\\03dai\\Documents\\Visual Studio 2015\\Projects\\WpfApplication1\\WpfApplication1\\Images",
                Filter = "Image Files(*.jpg,*.jepg,*.png,*.bmp,*.tiff)|*.jpg;*.jepg;*.png;*.bmp;*.tiff|All files(*.*)|*.*"
                //Filter = "Image Files(*.jpg,*.png)|*.jpg;*.png|All files(*.*)|*.*"
            };
            if (ofd.ShowDialog(Application.Current.MainWindow) == true)
            {
				//loadimage
                loadImage = new IplImage(ofd.FileName);
                double margin = 1.0;
                if(loadImage.Height > 400 || loadImage.Width > 400)
                {
                    if(loadImage.Height > loadImage.Width)
                    {
                        margin = 400.0 / loadImage.Height;
                    }
                    else
                    {
						margin = 400.0 / loadImage.Width;
                    }
                }
                CvSize size = new CvSize((int)(loadImage.Width * margin), (int)(loadImage.Height * margin));
                IplImage dst = new IplImage(size, loadImage.Depth, loadImage.NChannels);
                Cv.Resize(loadImage, dst);
                var bitmapImage = WriteableBitmapConverter.ToWriteableBitmap(dst);
                double height =bitmapImage.Height, width = bitmapImage.Width;

				//name
                string name = ofd.FileName;
                int lastIndex = name.LastIndexOf("\\");
                name = name.Substring(lastIndex+1);

				DataContext = new { Image = bitmapImage, Height = height, Width = width, FileName = name };
            }

        }

		private void Submited(object sender, RoutedEventArgs e)
		{
			if (comboBox.SelectedIndex == 0)
			{
				//edge
				IplImage gray = Cv.CreateImage(new CvSize(loadImage.Width, loadImage.Height), BitDepth.U8, 1), canny = Cv.CreateImage(new CvSize(loadImage.Width, loadImage.Height), BitDepth.U8, 1);
				Cv.CvtColor(loadImage, gray, ColorConversion.BgrToGray);
				Cv.Canny(gray, canny, 100, 200);
				var bitmapEdge = WriteableBitmapConverter.ToWriteableBitmap(canny);

				DataContext = new { Edge = bitmapEdge };
			}
			else if (comboBox.SelectedIndex == 1)
			{

			}
			else if (comboBox.SelectedIndex == 2)
			{

			}
		}
	}
}
