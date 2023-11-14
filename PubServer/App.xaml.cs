using System;
using System.Configuration;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using PubServer.Data;
using PubServer.Services;
using PubServer.ViewModels;

namespace PubServer
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static string Ip => ConfigurationManager.AppSettings["ip"];
		public static int Port => int.Parse(ConfigurationManager.AppSettings["Port"]);

		#region Поля блокировки
		internal static Object _GlbOperatorLocObject = new Object();

		#endregion



		internal static IHost __Host;
		public static IHost Host => __Host
			??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
		public static IServiceProvider Services => __Host.Services;

		internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
			.RegisterViewModel()
			.RegisterServics()
			.RegisterDB(host.Configuration.GetSection("DataBaseAlarm"))
		;

		protected override async void OnStartup(StartupEventArgs e)
		{
			var host = Host;
			base.OnStartup(e);
			await host.StartAsync().ConfigureAwait(false);
		}

		protected override async void OnExit(ExitEventArgs e)
		{
			using var host = Host;
			base.OnExit(e);
			await host.StopAsync().ConfigureAwait(false);
			Host.Dispose();
		}
	}
}
