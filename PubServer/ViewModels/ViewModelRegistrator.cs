
using Microsoft.Extensions.DependencyInjection;

namespace PubServer.ViewModels
{
	internal static class ViewModelRegistrator
	{
		public static IServiceCollection RegisterViewModel(this IServiceCollection services) => services
			.AddSingleton<MainVindowViewModel>()
			.AddSingleton<ServerViewModel>()
		;
	}
}
