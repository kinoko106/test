using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace YourSecretary.Model
{
	class ImageObjectModel
	{
		//const string ImagePath = @".\Debug\Resource\Image\santa_syokaku_600.png";
		const string ImagePath = @"C:\Users\daichi\Source\Repos\test\YourSecretary\YourSecretary\bin\Debug\Resource\Image\santa_syokaku_600.png";
		private Uri current;
		private BitmapImage Image = null;

		private int _width;
		private int _height;
		private string _filepath;

		public ImageObjectModel()
		{
			//current = new Uri(System.IO.Directory.GetCurrentDirectory());

			//Image = new BitmapImage();

			//Image.BeginInit();
			//Image.UriSource = new Uri(current,ImagePath);
			//Image.DecodePixelHeight = 600;
			//Image?.EndInit();

			current = new Uri(System.IO.Directory.GetCurrentDirectory());

			Image = new BitmapImage();

			Image.BeginInit();
			Image.UriSource = new Uri(ImagePath);
			Image.DecodePixelHeight = 600;
			Image?.EndInit();

			//_width = (int)Image?.Width;
			//_height = (int)Image?.Height;
			_width = (int)Image?.PixelWidth;
			_height = (int)Image?.PixelHeight;
			_filepath = Image?.UriSource.ToString();
		}

		public int Width
		{
			get { return _width; }
			private set { _width = value; }
		}

		public int Height
		{
			get { return _height; }
			private set { _height = value; }
		}

		public string FilePath
		{
			get { return _filepath; }
			private set { _filepath = value; }
		}
	}
}
