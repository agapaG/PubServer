using Client.DAL.Entities.Base;

namespace Client.DAL.Entities.Alarm
{
	/// <summary>
	/// Контекст таблицы 'Clients' из БД 'alarm'
	/// Поля:
	/// Id [PK, int, not null]
	/// Surname [string/char(25), null]
	/// Name [string/char(25), null]
	/// Password [string/char(25), null]
	/// Online [byte/tinyint, null]
	/// </summary>
	public class ClientsTbl : Entity
	{
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public string? Password { get; set; }
		public byte Online { get; set; }
	}
}
