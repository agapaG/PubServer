using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using Newtonsoft.Json;
using System.Linq;

using PubServer.ViewModels;
using PubServer.Models;
using PubServer.Services.Interfaces;
using TcpServer.LIB;
using Client.Interfaces;
using Client.DAL.Entities.Alarm;

namespace PubServer.Services
{
	internal class ServerService : IServerService
	{
		#region Поля
		private Server tcpServer = new Server(App.Ip, App.Port);
		
		private readonly IRepository<ClientsTbl> _Clients;
		#endregion


		#region Свойства
		public ServerViewModel? ViewModel { get; internal set; }
		#endregion


		public bool Enabled { get => tcpServer.Enabled; set => tcpServer.Enabled = value; }
		public void Start() => tcpServer.Start();
		public void Stop() => tcpServer.Stop();

		public ServerService(
			IRepository<ClientsTbl> Clients)
		{
			_Clients = Clients;

			tcpServer.RequestRecieve += TcpServer_RequestRecieve;
			
		}

		private void TcpServer_RequestRecieve(object? sender, RequestRecieveEventArgs e)
		{
			string? nameClient = null;

			var client = e.TcpClient;
			NetworkStream networkStream = client.GetStream();

			while (true)
			{
				try
				{
					byte[] query = new byte[32];
					networkStream.Read(query, 0, query.Length);

					//**********************************
					#region Подключение клиента-оператора
					if (query[1] == 0x01)
					{
						byte[] entpass = new byte[] { 0x00 };

						//буфер для считывания размера данных
						byte[] sizeBuffer = new byte[4];
						//сначала считываем размер данных
						networkStream.Read(sizeBuffer, 0, sizeBuffer.Length);
						//узнаем размер и создаем соответствующий буфер
						int size = BitConverter.ToInt32(sizeBuffer, 0);
						//создаем соответствующий буфер
						byte[] data = new byte[size];
						//считываем собственно данные
						int bytes = networkStream.Read(data, 0, size);

						string json = Encoding.UTF8.GetString(data);
						OperatorSett? EnrPas = JsonConvert.DeserializeObject<OperatorSett>(json);

						if (EnrPas != null)
						{
							entpass[0] = 0x01;

							var EnterPass = _Clients.Items
								.Select(x => x.Surname == EnrPas.ClientSurname &&
										x.Password == EnrPas.Password
										/*&& x.Online == (byte)0*/)
								.ToArray()
								.Contains(true);

							if (EnterPass)
							{
								lock (App._GlbOperatorLocObject)
								{
									nameClient = EnrPas.ClientSurname;

									ClientsTbl? val = _Clients.Items.
										SingleOrDefault(item => item.Surname == EnrPas.ClientSurname);
									if (val != null)
									{
										val.Online = 0b1;
										_Clients.Update(val);
									}

									ViewModel.ConnectedOperator += nameClient;
									ViewModel.ConnectedOperator += "\r\n";
									//}
									ViewModel.InfoInProcess += $"Клиент '{nameClient}' установил соединение";
									ViewModel.InfoInProcess += "\r\n";

								}
							}
						}

						networkStream.Write(entpass, 0, entpass.Length);

						return;

					}
					#endregion

					//...
				}
				catch (Exception ex) 
				{
					MessageBox.Show(ex.Message);
				}
				System.Threading.Thread.Sleep(10);
			}
		}
	}
}
