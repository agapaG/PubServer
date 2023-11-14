
using Microsoft.Extensions.DependencyInjection;

namespace PubServer.ViewModels
{
	internal class ViewModelLocator
	{
		public MainVindowViewModel MainViewModel =>
			App.Services.GetRequiredService<MainVindowViewModel>();
	}
}
