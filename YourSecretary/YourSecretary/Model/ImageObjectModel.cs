﻿using Livet;
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
		//const string ImagePath = @".\Resource\Image\santa_syokaku_600.png";
		const string ImagePath = @"C:\Users\daichi\Source\Repos\test\YourSecretary\YourSecretary\bin\Debug\Resource\Image\santa_syokaku_600.png";
		private Uri current;
		private BitmapImage Image = null;

		private int _width;
		private int _height;
		private string _filepath;
		private double _mask;

		public ImageObjectModel()
		{
			//current = new Uri(System.IO.Directory.GetCurrentDirectory());

			//Image = new BitmapImage();

			//Image.BeginInit();
			//Image.UriSource = new Uri(current, ImagePath);
			//Image.DecodePixelHeight = 600;
			//Image?.EndInit();

			current = new Uri(System.IO.Directory.GetCurrentDirectory());

			Image = new BitmapImage();

			Image.BeginInit();
			Image.UriSource = new Uri(ImagePath);
			Image.DecodePixelHeight = 600;
			Image?.EndInit();

			_width = (int)Image?.PixelWidth;
			_height = (int)Image?.PixelHeight;
			_filepath = Image?.UriSource.ToString();

			_mask = 1;
		}

		public ImageObjectModel(string path,int side)
		{
			current = new Uri(System.IO.Directory.GetCurrentDirectory());

			Image = new BitmapImage();

			Image.BeginInit();
			Image.UriSource = new Uri(path);
			Image.DecodePixelHeight = side;
			Image?.EndInit();

			_width = (int)Image?.PixelWidth;
			_height = (int)Image?.PixelHeight;
			_filepath = Image?.UriSource.ToString();

			_mask = 1;
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

		public double Mask
		{
			get { return _mask; }
			set { _mask = value; }
		}

		public void UpdateImage(string path, int side)
		{
			Image = new BitmapImage();

			Image.BeginInit();
			Image.UriSource = new Uri(path);
			Image.DecodePixelHeight = side;
			Image?.EndInit();

			_width = (int)Image?.PixelWidth;
			_height = (int)Image?.PixelHeight;
			_filepath = Image?.UriSource.ToString();

			_mask = 1;
		}

		public void UpdateImage(string path)
		{
			_filepath = path;
		}
	}
}
