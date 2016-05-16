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
using System.Threading;
using Microsoft.Win32;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using OpenCvSharp.Extensions;
using knk;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        ImageData imageData;

        Dictionary<string, KeyPoint[]> kptData = new Dictionary<string, KeyPoint[]>();

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
                process_comboBox.IsEnabled = true;

                DataContext = new { Image = bitmapImage, Height = bitmapImage.Height, Width = bitmapImage.Width, FileName = imageData.FileName };
            }

        }

        private void Submited(object sender, RoutedEventArgs e)
        {
            if (imageData.MatImage == null) return;
            switch (process_comboBox.SelectedIndex)
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

                    DataContext = new {
                        FileName = imageData.FileName,
                        Image = bitmapImage, Edge = bitmapProcessed,
                        Height = bitmapImage.Height,
                        Width = bitmapImage.Width
                    };
                    break;
                case 1:
                    int th, kptnum;
                    KeyPoint[] keypoints;
                    th = int.Parse(FASTThreashold.Text);

                    gray = new Mat(Cv.Size(imageData.Width, imageData.Height), MatType.CV_8UC1);
                    Mat fast = imageData.MatImage.Clone();
                    Cv2.CvtColor(imageData.MatImage, gray, ColorConversion.BgrToGray);
                    Cv2.FAST(gray, out keypoints, th);

                    kptnum = keypoints.Length;
                    foreach (KeyPoint k in keypoints)
                    {
                        Cv2.Circle(fast, k.Pt, 1, new Scalar(0, 0, 255), -1);
                        Cv2.Circle(fast, k.Pt, 5, new Scalar(0, 0, 255));
                    }

                    bitmapProcessed = WriteableBitmapConverter.ToWriteableBitmap(fast);
                    bitmapImage = WriteableBitmapConverter.ToWriteableBitmap(imageData.resizeImage);

                    DataContext = new
                    {
                        FileName = imageData.FileName,
                        Image = bitmapImage,
                        Edge = bitmapProcessed,
                        Height = bitmapImage.Height,
                        Width = bitmapImage.Width,
                        KeypointNum = kptnum
                    };
                    break;
                case 2:
                    th = int.Parse(CDFASTThreashold.Text);
                    fast = imageData.MatImage.Clone();
                    if (kptData.ContainsKey(CDFASTThreashold.Text) == false)
                    {
                        KNK.CDFAST(fast, out keypoints, th);
                        kptData[CDFASTThreashold.Text] = keypoints;
                    }
                    else
                    {
                        keypoints = kptData[CDFASTThreashold.Text];
                    }
                    //keypoints = new KeyPoint[1]; keypoints[0] = new KeyPoint(1, 1, 1);
                    kptnum = keypoints.Length;
                    foreach (KeyPoint k in keypoints)
                    {
                        Cv2.Circle(fast, k.Pt, 1, new Scalar(0, 0, 255), -1);
                        Cv2.Circle(fast, k.Pt, 5, new Scalar(0, 0, 255));
                    }
                    bitmapProcessed = WriteableBitmapConverter.ToWriteableBitmap(fast);
                    bitmapImage = WriteableBitmapConverter.ToWriteableBitmap(imageData.resizeImage);

                    DataContext = new
                    {
                        FileName = imageData.FileName,
                        Image = bitmapImage,
                        Edge = bitmapProcessed,
                        Height = bitmapImage.Height,
                        Width = bitmapImage.Width,
                        KeypointNum = kptnum
                    };
                    break;
            }
        }

        public KeyPoint[] testFunc(Mat m)
        {
            List<KeyPoint> k = new List<KeyPoint>();
            KeyPoint[] kk = new KeyPoint[100];
            return kk;
        }

        private void comboBox_changed(object sender, RoutedEventArgs e)
        {
            switch (process_comboBox.SelectedIndex)
            {
                case 0://select canny
                    if (CannyPanel != null && FASTPanel != null)
                    {
                        Submit.Visibility = Visibility.Visible;
                        CannyPanel.Visibility = Visibility.Visible;
                        FASTPanel.Visibility = Visibility.Collapsed;
                        CDFASTPannel.Visibility = Visibility.Collapsed;
                        CDFAST_submit.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 1://select fast
                    Submit.Visibility = Visibility.Visible;
                    CannyPanel.Visibility = Visibility.Collapsed;
                    FASTPanel.Visibility = Visibility.Visible;
                    CDFASTPannel.Visibility = Visibility.Collapsed;
                    CDFAST_submit.Visibility = Visibility.Collapsed;
                    break;
                case 2://select cdfast
                    Submit.Visibility = Visibility.Collapsed;
                    CannyPanel.Visibility = Visibility.Collapsed;
                    FASTPanel.Visibility = Visibility.Collapsed;
                    CDFASTPannel.Visibility = Visibility.Visible;
                    CDFAST_submit.Visibility = Visibility.Visible;
                    break;
            }
        }

        //CDFAST用の非同期ボタンメソッド
        //引数取りたい
        private async void CDFAST_submit_Click(object sender, RoutedEventArgs e)
        {
            Load.IsEnabled = false;
            process_comboBox.IsEnabled = false;
            await Task.Run(() => {
                //ここにCDFAST
                //実際は10秒ぐらいかかる
                Thread.Sleep(3000);
            });
            Load.IsEnabled = true;
            process_comboBox.IsEnabled = true;
        }
    }
}
