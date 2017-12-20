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
using System.Diagnostics;
using OpenCvSharp.Extensions;
using OpenCvSharp.CPlusPlus;
using System.Threading;
using knk;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace knktestWPF
{

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        List<String> winTitles = new List<string>();
        List<IntPtr> winHandles = new List<IntPtr>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_click(object sender, RoutedEventArgs e)
        {
            var process = Process.GetCurrentProcess();

            int maxlength = 0;
            int num = 0;
            var processes = Process.GetProcesses();
            foreach (Process p in processes)
            {
                if (p.MainWindowTitle.Length > 1)
                {
                    ProcessList.Items.Add(p.ProcessName);
                    winTitles.Add(p.MainWindowTitle);
                    winHandles.Add(p.MainWindowHandle);
                    if (p.ProcessName.Length > maxlength)
                    {
                        maxlength = p.ProcessName.Length;
                    }
                    num++;
                }
            }

            ProcessList.Width = maxlength * 9;
            ProcessList.Height = (ProcessList.FontSize * 2) * num;
            Width += (ProcessList.FontSize * 2) * num;


            Bitmap bmp = new Bitmap(480,600, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using(Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(480, 600, 0, 0, new System.Drawing.Size(480, 600), CopyPixelOperation.SourceCopy);
            }

            bmp.Save("capture.png", ImageFormat.Png);
        }

        private void ProcessList_changed(object sender, SelectionChangedEventArgs e)
        {
            title.Text = winTitles[ProcessList.SelectedIndex];
        }
    }
    #region 拾い物
    public class Win32APICall
    {
        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern IntPtr DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern IntPtr DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        public static extern bool BitBlt(IntPtr hdcDest, int nXDest,
            int nYDest, int nWidth, int nHeight, IntPtr hdcSrc,
            int nXSrc, int nYSrc, int dwRop);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc,
            int nWidth, int nHeight);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobjBmp);

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        public static Bitmap GetDesktop()
        {
            int screenX;
            int screenY;
            IntPtr hBmp;
            IntPtr hdcScreen = GetDC(GetDesktopWindow());
            IntPtr hdcCompatible = CreateCompatibleDC(hdcScreen);

            screenX = GetSystemMetrics(0);
            screenY = GetSystemMetrics(1);
            hBmp = CreateCompatibleBitmap(hdcScreen, screenX, screenY);

            if (hBmp != IntPtr.Zero)
            {
                IntPtr hOldBmp = (IntPtr)SelectObject(hdcCompatible, hBmp);
                BitBlt(hdcCompatible, 0, 0, screenX, screenY, hdcScreen, 0, 0, 13369376);

                SelectObject(hdcCompatible, hOldBmp);
                DeleteDC(hdcCompatible);
                ReleaseDC(GetDesktopWindow(), hdcScreen);

                Bitmap bmp = System.Drawing.Image.FromHbitmap(hBmp);

                DeleteObject(hBmp);
                GC.Collect();

                return bmp;
            }
            return null;
        }
    }
    #endregion
}
