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
		public static double CIE1976()
		{
			return 0;
		}
		public static void CDFAST(Mat src,Mat dst,double threshold = 100.0, bool nonmaxSupression = false)
		{

		}
    }
}
