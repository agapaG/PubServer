
using Client.Interfaces;

namespace Client.DAL.Entities.Base
{
	public abstract class Entity : IEntity
	{
		public int Id { get; set; }
	}
}
