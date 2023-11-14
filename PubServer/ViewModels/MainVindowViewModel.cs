
using PubServer.ViewModels.Base;

namespace PubServer.ViewModels
{
	internal class MainVindowViewModel : BaseViewModel
	{
		#region Свойства

		public ServerViewModel ServerModel { get; }


		#region Title
		private string? _Title = string.Empty;
		public string Title { get => _Title; set => Set(ref _Title, value); }
		#endregion
		#endregion

		public MainVindowViewModel(ServerViewModel ServerModel)
		{
			Title = "Тест";
			this.ServerModel = ServerModel;
		}
	}
}
