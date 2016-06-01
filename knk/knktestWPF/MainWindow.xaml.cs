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
using OpenCvSharp.Extensions;
using OpenCvSharp.CPlusPlus;
using System.Threading;
using knk;

namespace knktestWPF
{

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {

        KeyPoint[] k;
        Mat img;
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                img = Cv2.ImRead("shokaku2.jpg");
            }
            catch (Exception e)
            {

            }

            double w = Width;
            double h = Height;
            var wbitmap = WriteableBitmapConverter.ToWriteableBitmap(img);
            DataContext = new { Height = h, Width = w ,IMAGE = wbitmap};
        }

        private async void button1_click(object sender, RoutedEventArgs e)
        {
            //過去の遺産
            //Task<KeyPoint[]> task = Task.Run(() => KNK.CDFAST(img, 100));
            //k = await Task.Run(() => KNK.CDFASTAsync(img, 100));
            //k = task.Result;

            Mat m = img.Clone();
            button2.IsEnabled = false;
            k = await Task.Run(() => KNK.CDFAST(m, 100));
            button2.IsEnabled = true;

            foreach (KeyPoint key in k)
            {
                Cv2.Circle(img, key.Pt, 3, new Scalar(255, 0, 0));
            }

            var wbitmap = WriteableBitmapConverter.ToWriteableBitmap(img);
            DataContext = new { Height = this.Height, Width = this.Height, IMAGE = wbitmap };
        }

        private void button2_click(object sender, RoutedEventArgs e)
        {
            Mat m = Cv2.ImRead("shokaku2.jpg");
            KeyPoint[] k;

            k = KNK.CDFAST(m, 100);

            Thread.Sleep(3000);

            foreach (KeyPoint key in k)
            {
                Cv2.Circle(img, key.Pt, 3, new Scalar(255, 0, 0));
            }

            var wbitmap = WriteableBitmapConverter.ToWriteableBitmap(img);
            DataContext = new { Height = this.Height, Width = this.Height, IMAGE = wbitmap };
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("foo!","foofoo");
        }
    }
}
