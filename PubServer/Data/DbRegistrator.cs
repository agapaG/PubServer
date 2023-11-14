using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Client.DAL;
using Client.DAL.Context;
using Microsoft.EntityFrameworkCore;


namespace PubServer.Data
{
	internal static class DbRegistrator
	{
		public static IServiceCollection RegisterDB(this IServiceCollection services, IConfiguration Config) => services
			.AddDbContext<AlarmDB>( opt =>
			{
				var type = Config["Type"];
				switch (type)
				{
					case null: throw new InvalidOperationException("Не определен тип БД");
					default: throw new InvalidOperationException($"Тип подключения {type} не поддерживается");

					case "alarm":
						opt.UseSqlServer(Config.GetConnectionString(type));
						break;
				}

			})
			.RegisterDbRepository()
		;
	}
}
