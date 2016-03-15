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
		}

		public Mat MatImage { get; set; }
		public string FileName { get; set; }
		public int Height { get; set; }
		public int Width { get; set; }
		public int DefalutHeight { get; private set; }
		public int DefalutWidth { get; private set; }
	}
}