﻿using System;
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

            if(imageData != null)
            {
                Submit.IsEnabled = true;
                CDFAST_submit.IsEnabled = true;
            }else
            {
                MessageBox.Show("imagedata faild to load!");
            }
        }

        private void Submited(object sender, RoutedEventArgs e)
        {
            if (imageData.MatImage == null) return;
            switch (process_comboBox.SelectedIndex)
            {
                case 0://edge
                    int th1, th2;
                    //TODO canny閾値のエラーチェック
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
                    //TODO fast閾値のエラーチェック
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
            }
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
        //引数取りたい ← とれた
        private async void CDFAST_submit_Click(object sender, RoutedEventArgs e)
        {
            KeyPoint[] keypoints;
            Mat cdfast = imageData.MatImage.Clone();

            //TODO CDFAST閾値エラーチェック
            int threashold = int.Parse(CDFASTThreashold.Text);

            if (kptData.ContainsKey(CDFASTThreashold.Text) == true)
            {
                keypoints = kptData[CDFASTThreashold.Text];
            }
            else
            {
                Load.IsEnabled = false;
                process_comboBox.IsEnabled = false;
                //非同期 CDFASTの返り値がkptdataに代入されている
                kptData[CDFASTThreashold.Text] = await Task.Run(() => { return KNK.CDFAST(cdfast, threashold); });
                keypoints = kptData[CDFASTThreashold.Text];
                Load.IsEnabled = true;
                process_comboBox.IsEnabled = true;
            }

            var kptnum = keypoints.Length;
            foreach (KeyPoint k in keypoints)
            {
                Cv2.Circle(cdfast, k.Pt, 1, new Scalar(0, 0, 255), -1);
                Cv2.Circle(cdfast, k.Pt, 5, new Scalar(0, 0, 255));
            }
            var bitmapProcessed = WriteableBitmapConverter.ToWriteableBitmap(cdfast);
            var bitmapImage = WriteableBitmapConverter.ToWriteableBitmap(imageData.resizeImage);

            DataContext = new
            {
                FileName = imageData.FileName,
                Image = bitmapImage,
                Edge = bitmapProcessed,
                Height = bitmapImage.Height,
                Width = bitmapImage.Width,
                KeypointNum = kptnum
            };
        }
    }
}
