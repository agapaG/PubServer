
using Microsoft.Extensions.DependencyInjection;
using PubServer.Services.Interfaces;

namespace PubServer.Services
{
	internal static class RegistratorServices
	{
		public static IServiceCollection RegisterServics(this IServiceCollection services) => services
			.AddSingleton<IServerService, ServerService>()
		;
	}
}
