namespace PubServer.Services.Interfaces
{
	internal interface IServerService
	{
		bool Enabled { get; set; }
		void Start();
		void Stop();
	}
}
