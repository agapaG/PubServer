using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;
using TestClient.Models;

namespace TestClient
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string IPAddr = "...";


			OperatorSett pass = new OperatorSett();
			pass.ClientSurname = "1";
			pass.Password = "1";

			TcpClient Client = new TcpClient();
			try
			{
				Client.Connect(System.Net.IPAddress.Parse(IPAddr), 8080);
				var stream = Client.GetStream();

				byte[] Request = _createRequest_Little_Endian(0x0100, 0xddcf);
				stream.Write(Request, 0, Request.Length);

				var jason = JsonConvert.SerializeObject(pass);

				Request = Encoding.UTF8.GetBytes(jason);
				// определяем размер данных
				byte[] size = BitConverter.GetBytes(Request.Length);
				// отправляем размер данных
				stream.Write(size, 0, size.Length);
				// отправляем данные
				stream.Write(Request, 0, Request.Length);

				// буфер для считывания размера данных
				byte[] ansBuffer = new byte[4];
				// сначала считываем размер данных
				stream.Read(ansBuffer, 0, ansBuffer.Length);
				
				if (ansBuffer[0] == 0x01)
				{
					Console.WriteLine("Вход осуществлен");
				}
				else if (ansBuffer[0] == 0x00)
				{
					Console.WriteLine("Нет доступа");
				}

			}
			catch (Exception ex)
			{ Console.WriteLine(ex.Message); }

			Console.ReadKey();
		}

		private static byte[] _createRequest_Little_Endian(ushort idReq, ushort idS)
		{
			byte[] Request = new byte[32];
			//Формирую ID запроса
			Request[0] = (byte)idReq;
			Request[1] = (byte)(idReq >> 8);

			//Формирую ID системы (сервер ИВС)
			Request[2] = (byte)idS;
			Request[3] = (byte)(idS >> 8);

			//Резерв + признак первого запроса
			Request[4] = 0b1;

			Request[31] = (byte)'\n';

			return Request;
		}

	}
}