using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace knk
{
	public static class TimeCounter
	{
		static private long startCounter;

		[DllImport("kernel32.dll")]
		static extern bool QueryPerformanceCounter(ref long lpPerformanceCount);
		[DllImport("kernel32.dll")]
		static extern bool QueryPerformanceFrequency(ref long lpFrequency);

		public static void start()
		{
			startCounter = 0;
			QueryPerformanceCounter(ref startCounter);
		}
		public static void End()
		{
			long stopCounter = 0;
			QueryPerformanceCounter(ref stopCounter);
			long frequency = 0;
			QueryPerformanceFrequency(ref frequency);
			double t = (stopCounter - startCounter) * 1000.0 / frequency;
			Console.WriteLine(t.ToString() + "ms");
		}
	}
}
