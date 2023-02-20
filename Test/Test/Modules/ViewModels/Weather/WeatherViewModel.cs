using System;
using Prism.Navigation;

namespace Test.Modules.ViewModels.Weather
{
	public class WeatherViewModel : ViewModelBase
	{
		public WeatherViewModel(INavigationService navigationService):base(navigationService)
		{
		}
	}
}

