using Microsoft.EntityFrameworkCore;

using Client.DAL.Entities.Alarm;

namespace Client.DAL.Context
{
	public class AlarmDB : DbContext
	{
		public DbSet<ClientsTbl> Operators { get; set; }

		public AlarmDB(DbContextOptions<AlarmDB> options) : base(options) { }
	}
}
