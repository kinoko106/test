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
		public Mat matImage { get; set; }
		public IplImage iplImage { get; set; }
		public string fileName { get; set; }
		public int Height { get; set; }
		public int Width { get; set; }
		public int defalutHeight { get; private set; }
		public int defalutWidth { get; private set; }
	}
}
