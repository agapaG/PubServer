using Microsoft.Extensions.DependencyInjection;

using Client.Interfaces;
using Client.DAL.Entities.Alarm;

namespace Client.DAL
{
	public static class RepositoryRegistrator
	{
		public static IServiceCollection RegisterDbRepository(this IServiceCollection services) => services
			.AddTransient<IRepository<ClientsTbl>, DbRepositoryAlarm<ClientsTbl>>()
		;

	}
}
