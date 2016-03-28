using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace knk
{
	public class Lab
	{
		public byte L { get; private set; }
		public byte a { get; private set; }
		public byte b { get; private set; }

		public Lab()
		{
			L = 0;a = 0;b = 0;
		}
		public Lab(Lab src)
		{
			L = src.L; a = src.a; b = src.b;
		}
		public Lab(byte L, byte a, byte b)
		{
			this.L = L; this.a = a; this.b = b;
		}
	}
}
