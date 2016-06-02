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
        public const double PI2 = Math.PI * 2.0;
        public const double PIh = Math.PI * 0.5;
        public const double PI_6 = Math.PI * 0.16666666666666666667;

        public static double Sigmoid(double val)
        {
            return 1.0 / (1.0 + Math.Exp(-val));
        }
        public static double SuperSqrt(double x)
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
        //とりあえず移植
        public static double CIE2000(Vec3b lab1, Vec3b lab2)
        {
            double deltaLd, deltaCd, deltahd, deltaHd, deltaTheta;
            double Cbar, Ldbar, Cdbar, Hdbar;
            double c1, c2, ad1, ad2, Cd1, Cd2, hd1, hd2;
            double G, T, Sl, Sc, Sh, Rc, Rt;

            double Cdbar7, tmp, sqCdbar;

            Ldbar = ((lab1[0] + lab2[0]) >> 1);
            c1 = SuperSqrt((lab1[1] * lab1[1]) + (lab1[2] * lab1[2]));
            c2 = SuperSqrt((lab2[1] * lab2[1]) + (lab2[2] * lab2[2]));
            Cbar = (c1 + c2) * 0.5;
            Cdbar7 = Cbar * Cbar * Cbar * Cbar * Cbar * Cbar * Cbar;
            sqCdbar = SuperSqrt(Cdbar7 / (Cdbar7 + 6103515625.0));
            G = 1.5 - sqCdbar * 0.5;
            ad1 = lab1[1] * G;
            ad2 = lab2[1] * G;
            var lab1b2 = lab1[2] * lab1[2];
            Cd1 = SuperSqrt((ad1 * ad1) + lab1b2);
            Cd2 = SuperSqrt((ad2 * ad2) + lab1b2);
            Cdbar = (Cd1 + Cd2) * 0.5;
            if ((hd1 = Math.Atan(lab1[2] / ad1)) < 0)
                hd1 += PI2;
            if ((hd2 = Math.Atan(lab2[2] / ad2)) < 0)
                hd2 += PI2;
            Hdbar = (hd1 + hd2) * 0.5;
            if (Math.Abs((deltahd = hd2 - hd1)) > (PIh))
            {
                Hdbar += Math.PI;
                if (deltahd < 0)
                {
                    deltahd += PI2;
                }else
                {
                    deltahd -= PI2;
                }
            }
            T = 1.0 - 0.17 * Math.Cos(Hdbar - (PI_6)) + 0.24 * Math.Cos(2.0 * Hdbar) + 0.32 * Math.Cos(3.0 * Hdbar + 0.1047) - 0.2 * Math.Cos(Hdbar * 4.39824);
            deltaLd = lab2[0] - lab1[0];
            deltaCd = Cd2 - Cd1;
            deltaHd = 2.0 * SuperSqrt(Cd1 * Cd2) * Math.Sin(deltahd * 0.5);
            tmp = (Ldbar - 50.0) * (Ldbar - 50.0);
            Sl = 1.0 + (0.015 * tmp) / SuperSqrt(20.0 + tmp);
            Sc = 1.0 + 0.045 * Cdbar;
            Sh = 1.0 + 0.015 * Cdbar * T;
            tmp = (Hdbar - 4.8063888888888) * 0.04;
            deltaTheta = 30.0 * Math.Exp(-1.0 * tmp * tmp);
            Rc = 2.0 * sqCdbar;
            Rt = -Rc * Math.Sin(2.0 * deltaTheta);

            double dLdSl = deltaLd / Sl;
            double dCdSc = deltaCd / Sc;
            double dHdSh = deltaHd / Sh;


            return dLdSl * dLdSl + dCdSc * dCdSc + dHdSh * dHdSh + Rt * dCdSc * dHdSh;
        }
        //遅いの何とかする
        public static void CDFAST(Mat src, out KeyPoint[] Keypoints, double threshold = 100.0, bool nonmaxSupression = false)
        {
            int[,] table = new int[16, 2] { { 0, -3 }, { 1, -3 }, { 2, -2 }, { 3, -1 }, { 3, 0 }, { 3, 1 }, { 2, 2 }, { 1, 3 }, { 0, 3 }, { -1, 3 }, { -2, 2 }, { -3, 1 }, { -3, 0 }, { -3, -1 }, { -2, -2 }, { -1, -3 } };
            int[] num = new int[30] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            long step = src.Step();
            int channels = src.Channels();
            List<KeyPoint> kpt = new List<KeyPoint>();

            if (src.Channels() != 3)
            {
                Keypoints = kpt.ToArray();
                return;
            }
            int x = 0, y = 0;

            unsafe
            {
                byte* ptr = src.DataPointer;
                for (y = 3; y < src.Rows - 3; y++)
                {
                    for (x = 3; x < src.Cols - 3; x++)
                    {
                        int[] discrimination = new int[16];   //sililar 0 ; disimilar 1
                        for (int i = 0; i < 16; i++)
                        {
                            //Vec3b lab1 = src.At<Vec3b>(y, x);
                            //Vec3b lab2 = src.At<Vec3b>(y + table[i, 0], x + table[i, 1]);
                            Vec3b lab1 = new Vec3b(ptr[y * step + x * channels], ptr[y * step + x * channels + 1], ptr[y * step + x * channels + 2]);
                            Vec3b lab2 = new Vec3b(ptr[(y + table[i, 0]) * step + (x + table[i, 1]) * channels], ptr[(y + table[i, 0]) * step + (x + table[i, 1]) * channels + 1], ptr[(y + table[i, 0]) * step + (x + table[i, 1]) * channels + 2]);
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
            }
            Keypoints = kpt.ToArray();
        }
        //正しく動いてない気がする
        public static KeyPoint[] CDFAST(Mat src, double threshold = 100.0, bool nonmaxSupression = false)
        {
            int[,] table = new int[16, 2] { { 0, -3 }, { 1, -3 }, { 2, -2 }, { 3, -1 }, { 3, 0 }, { 3, 1 }, { 2, 2 }, { 1, 3 }, { 0, 3 }, { -1, 3 }, { -2, 2 }, { -3, 1 }, { -3, 0 }, { -3, -1 }, { -2, -2 }, { -1, -3 } };
            int[] num = new int[30] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            long step = src.Step();
            int channels = src.Channels();
            List<KeyPoint> kpt = new List<KeyPoint>();

            if (src.Channels() != 3)
            {
                return kpt.ToArray();
            }
            int x = 0, y = 0;

            unsafe
            {
                byte* ptr = src.DataPointer;
                for (y = 3; y < src.Rows - 3; y++)
                {
                    for (x = 3; x < src.Cols - 3; x++)
                    {
                        int[] discrimination = new int[16];   //sililar 0 ; disimilar 1
                        for (int i = 0; i < 16; i++)
                        {
                            Vec3b lab1 = new Vec3b(
                                ptr[y * step + x * channels], 
                                ptr[y * step + x * channels + 1], 
                                ptr[y * step + x * channels + 2]);
                            Vec3b lab2 = new Vec3b(
                                ptr[(y + table[i, 0]) * step + (x + table[i, 1]) * channels], 
                                ptr[(y + table[i, 0]) * step + (x + table[i, 1]) * channels + 1], 
                                ptr[(y + table[i, 0]) * step + (x + table[i, 1]) * channels + 2]);
                            double deltaE = CIE2000(lab1, lab2);
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
            }
            return kpt.ToArray();
        }

        //作った意味なし
        public static Task<KeyPoint[]> CDFASTAsync(Mat src, double threshold = 100.0, bool nonmaxSupression = false)
        {
            return new Task<KeyPoint[]>(() => CDFAST(src, threshold));
        }
    }
}
