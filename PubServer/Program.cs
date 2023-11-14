using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace PubServer
{
	internal class Program
	{
		[System.STAThreadAttribute()]
		internal static void Main(string[] Args)
		{
			try
			{
				PubServer.App app = new PubServer.App();
				app.InitializeComponent();
				app.Run();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] Args) => Host
			.CreateDefaultBuilder(Args)
			.ConfigureServices(App.ConfigureServices)
		;
	}
}
