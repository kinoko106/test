using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MetroTrilithon.Lifetime;
using Livet;

namespace WpfApplication2
{
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App : Application
	{

		protected override void OnStartup(StartupEventArgs e)
		{
			string runText = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\t" + "Application Run" + Environment.NewLine;
			string filePath = Directory.GetCurrentDirectory() + "/app.log";
			File.AppendAllText(filePath, runText);

			//UIスレッドのオブジェクトにアクセスしたいマン
			DispatcherHelper.UIDispatcher = this.Dispatcher;
		}

	}
}
