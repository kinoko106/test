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
                img = Cv2.ImRead("shokaku.jpg");
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

            int th = int.Parse(threashold.Text);
            Mat m = img.Clone();
            k = await Task.Run(() => KNK.CDFAST(m, th));

            foreach (KeyPoint key in k)
            {
                Cv2.Circle(m, key.Pt, 3, new Scalar(255, 0, 0));
            }

            var wbitmap = WriteableBitmapConverter.ToWriteableBitmap(m);
            DataContext = new { Height = this.Height, Width = this.Height, IMAGE = wbitmap ,key11 = k.Length };
        }

        private void button2_click(object sender, RoutedEventArgs e)
        {
            List<KeyPoint[]> keypoints = new List<KeyPoint[]>();
            int[] th = new int[10];

            for (int i = 0; i < 10; i++)
            {
                th[i] = 100 * (i + 1);
                keypoints.Add(KNK.CDFAST(img,th[i],false));
            }

            DataContext = new
            {
                key1 = keypoints[0].Length,
                key2 = keypoints[1].Length,
                key3 = keypoints[2].Length,
                key4 = keypoints[3].Length,
                key5 = keypoints[4].Length,
                key6 = keypoints[5].Length,
                key7 = keypoints[6].Length,
                key8 = keypoints[7].Length,
                key9 = keypoints[8].Length,
                key10 = keypoints[9].Length,
                th1 = th[0],
                th2 = th[1],
                th3 = th[2],
                th4 = th[3],
                th5 = th[4],
                th6 = th[5],
                th7 = th[6],
                th8 = th[7],
                th9 = th[8],
                th10 = th[9]
            };
        }
    }
}
