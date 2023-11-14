namespace TestClient.Models
{
	internal class OperatorSett
	{
		public int Id { get; set; }
		public string? ClientSurname { get; set; }
		public string? Password { get; set; }
		public byte Online { get; set; }
		public bool? bReload { get; set; }
	}
}
