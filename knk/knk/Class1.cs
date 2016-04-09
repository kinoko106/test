using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

namespace knk
{
	public class KNK
	{
		public static double Sigmoid(double val)
		{
			return 1.0 / (1.0 + Math.Exp(-val));
		}
		public static double SuperSqlt(double x)
		{
			double s, last;
			if (x <= 0.0) return 0.0;
			s = x > 1.0 ? x : 1.0;
			do
			{
				last = s;
				s = (x / s + s) * 0.5;
			} while (s < last);
			return last;
		}
		public static double CIE1976(Lab lab1, Lab lab2)
		{
			return (lab1.L - lab2.L) * (lab1.L - lab2.L) + (lab1.a - lab2.a) * (lab1.a - lab2.a) + (lab1.b - lab2.b) * (lab1.b - lab2.b);
		}
		public static double CIE1976(Vec3b lab1, Vec3b lab2)
		{
			return (lab1[0] - lab2[0]) * (lab1[0] - lab2[0]) + (lab1[1] - lab2[1]) * (lab1[1] - lab2[1]) + (lab1[2] - lab2[2]) * (lab1[2] - lab2[2]);
		}
		public static double CIE94(Lab lab1, Lab lab2)
		{
			return 0;
		}
		public static double CIE2000(Lab lab1, Lab lab2)
		{
			return 0;
		}
		public static void CDFAST(Mat src,out KeyPoint[] Keypoints, double threshold = 100.0, bool nonmaxSupression = false)
		{
			int[,] table = new int[16, 2] { { 0, -3 }, { 1, -3 }, { 2, -2 }, { 3, -1 }, { 3, 0 }, { 3, 1 }, { 2, 2 }, { 1, 3 }, { 0, 3 }, { -1, 3 }, { -2, 2 }, { -3, 1 }, { -3, 0 }, { -3, -1 }, { -2, -2 }, { -1, -3 } };
			int[] num = new int[30] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
			List<KeyPoint> kpt = new List<KeyPoint>();

			if (src.Channels() != 3)
			{
				Keypoints = kpt.ToArray();
				return;
			}

			for (int y = 0; y < src.Rows; y++)
			{
				for (int x = 0; x < src.Cols; y++)
				{
					int[] discrimination = new int[16];   //sililar 0 ; disimilar 1
					for (int i = 0; i < 16; i++)
					{
						Vec3b lab1 = src.At<Vec3b>(y, x);
						Vec3b lab2 = src.At<Vec3b>(y + table[i, 0], x + table[i, 1]);
						double deltaE = CIE1976(lab1, lab2);
						if ((deltaE - threshold) >= 0)
						{
							discrimination[i] = 1;
						}
						else
						{
							discrimination[i] = 0;
						}
					}
					int n = 0;
					for (int i = 0; i < 8; i++)
					{
						if ((discrimination[i] + discrimination[i + 8]) != 0)
							continue;
						n = 1;
						for (int j = i + 1; j < i + 8; j++)
						{
							if (discrimination[num[j]] == 1)
								n++;
							else
							{
								i = j;
								break;
							}
						}
						if (n >= 8)
						{
							kpt.Add(new KeyPoint(x, y, 0));
							break;
						}
					}
				}
			}
			Keypoints = kpt.ToArray();
		}
	}
}
}
