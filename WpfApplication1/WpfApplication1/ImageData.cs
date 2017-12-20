using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

namespace WpfApplication1
{
	class ImageData
	{
		public ImageData()
		{
            Width = 0;
            Height = 0;
            DefalutWidth = 0;
            DefalutHeight = 0;
            FileName = null;
            MatImage = null;
            resizeImage = null;
		}
		public ImageData(String filePath)
		{
			MatImage = Cv2.ImRead(filePath);

			//get filename
			FileName = filePath;
			int lastIndex = FileName.LastIndexOf("\\");
			FileName = FileName.Substring(lastIndex + 1);

			Width = MatImage.Width;
			Height = MatImage.Height;

            resize(MatImage);
		}

		public Mat MatImage { get; set; }
		public Mat resizeImage { get; private set; }
		public string FileName { get; private set; }
		public int Height { get; private set; }
		public int Width { get; private set; }
		public int DefalutHeight { get; private set; }
		public int DefalutWidth { get; private set; }

        private void resize(Mat img)
        {
            resizeImage = new Mat();
            if (MatImage.Width > 600 || MatImage.Height > 600)
            {
                double range;
                Size s;
                if (MatImage.Height > MatImage.Width)
                {
                    range = 600.0 / MatImage.Height;
                    s = new Size(MatImage.Width * range, 600);
                }
                else
                {
                    range = 600.0 / MatImage.Width;
                    s = new Size(600, MatImage.Height * range);
                }

                Cv2.Resize(MatImage, resizeImage, s);
            }
            else
            {
                resizeImage = MatImage.Clone();
            }
        }
	}
}