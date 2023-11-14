
using System.Net.Sockets;
using System.Net;

namespace TcpServer.LIB
{
#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
	public class Server
	{
		#region События
		public event EventHandler<RequestRecieveEventArgs> RequestRecieve;
		#endregion

		#region Поля
		private readonly string? _IpAddr;
		private readonly int _Port;
		private TcpListener tcpListener;
		private bool _Enabled;
		private readonly Object _Lock = new Object();
		#endregion

		#region Свойства
		public int Port => _Port;
		public bool Enabled { get => _Enabled; set { if (value) Start(); else Stop(); } }
		#endregion

		#region Методы
		public void Start()
		{
			//Если сервер включен - выходим
			if (_Enabled) return;

			lock (_Lock)
			{
				if (_Enabled) return; // Исключение повтороного запуска сервера

				tcpListener = new TcpListener(IPAddress.Parse(_IpAddr), _Port);
				_Enabled = true;
				ListeningAsync();
			}

		}

		public void Stop()
		{
			if (!_Enabled) return;

			lock (_Lock)
			{
				if (!_Enabled) return;

				//tcpListener.Stop();
				_Enabled = false;
			}
		}

		private async void ListeningAsync()
		{
			var Listener = tcpListener;

			TcpClient tcpСlient = null;

			try
			{
				Listener.Start();

				while (_Enabled)
				{
					tcpСlient = await Listener.AcceptTcpClientAsync();
					ProcessRequestAsync(tcpСlient);
				}

				Listener.Stop();
			}
			catch
			{
				throw new Exception("Повторный запуск сервера");
			}
			//catch (SocketException) { Console.WriteLine("Fatal error server...."); }
		}
		#endregion

		public Server(string ip, int port)
		{
			_IpAddr = ip;
			_Port = port;
		}

		private async void ProcessRequestAsync(TcpClient client)
		{
			await Task.Run(() => RequestRecieve?.Invoke(this, new RequestRecieveEventArgs(client)));
		}

	}
	public class RequestRecieveEventArgs : EventArgs
	{
		public TcpClient TcpClient { get; }

		public RequestRecieveEventArgs(TcpClient client) => TcpClient = client;

	}
#pragma warning restore CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.	



}
