using System.Windows.Input;

using PubServer.Infrastructure.Commands;
using PubServer.Services;
using PubServer.Services.Interfaces;
using PubServer.ViewModels.Base;

namespace PubServer.ViewModels
{
	internal class ServerViewModel : BaseViewModel
	{
		#region Поля
		private readonly IServerService _Server;

		#endregion

		#region Свойства

		public bool Enabled
		{
			get => _Server.Enabled;
			set
			{
				_Server.Enabled = value;
				OnPropertyChanged(nameof(Enabled));
			}
		}

		#region ConnectedOperator
		private string _ConnectedOperator = string.Empty;
		public string ConnectedOperator { get => _ConnectedOperator; set => Set(ref _ConnectedOperator, value); }
		#endregion

		#region InfoInProcess
		private string _InfoInProcess = string.Empty;
		public string InfoInProcess { get => _InfoInProcess; set => Set(ref _InfoInProcess, value); }
		#endregion

		#endregion

		#region Commands

		#region StartServer
		private ICommand? _StartServerCommand;

		public ICommand StartServerCommand => _StartServerCommand
			??= new LambdaCommand(OnStartServerCommandExecuted, CanStartServerCommandExecute);

		private void OnStartServerCommandExecuted(object param)
		{
			_Server.Start();
			OnPropertyChanged(nameof(Enabled));
		}
		private bool CanStartServerCommandExecute(object param) => !Enabled;
		#endregion

		#endregion


		public ServerViewModel(IServerService server)
		{
			_Server = server;
			((ServerService)_Server).ViewModel = this;

		}
	}
}
