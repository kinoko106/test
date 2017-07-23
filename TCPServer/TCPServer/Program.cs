using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TCPServer
{
	class Program
	{
		static void Main(string[] args)
		{
			IPEndPoint ipAdd = new IPEndPoint(IPAddress.Parse("192.168.0.3"), 8888);
			TcpListener listener = new TcpListener(ipAdd);
			try
			{
				listener.Start(0);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			Console.WriteLine("Port:8888のListenを開始しました。");

			TcpClient client = listener.AcceptTcpClient();
			Console.WriteLine("クライアントが接続しました。");

			if (client.Connected)
			{
				listener.Stop();
				NetworkStream netStream = client.GetStream();
				StreamReader sReader = new StreamReader(netStream, Encoding.UTF8);

				string str = String.Empty;

				do
				{
					str = sReader.ReadLine();
					if (null == str)
					{
						break;
					}
					Console.WriteLine(str);
				} while (!str.Equals("quit"));
				sReader.Close();
				client.Close();
			}
			Console.WriteLine("終了するには、Enterキーを押してください");
			Console.ReadLine();
		}
	}
}
