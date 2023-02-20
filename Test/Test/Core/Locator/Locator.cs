using System;
using Prism.Ioc;
using Test.Core.Services;
using Test.Core.Services.Interfaces;
using Test.Modules.Repositories;
using Test.Modules.Repositories.Interfaces;
using Test.Modules.UI.Init;
using Test.Modules.UI.TabPage;
using Test.Modules.UI.Contacts;
using Test.Modules.UI.Users;
using Test.Modules.UI.Weather;
using Test.Modules.ViewModels.Init;
using Test.Modules.ViewModels.TabPage;
using Test.Modules.ViewModels.Contacts;
using Test.Modules.ViewModels.Users;
using Test.Modules.ViewModels.Weather;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace Test.Core.Locator
{
    public static class Locator
    {

        public static void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.RegisterSingleton<ISettingsService, SettingsService>();
            containerRegistry.RegisterSingleton<ITranslatorService, TranslatorService>();
            containerRegistry.RegisterSingleton<IValidationService, ValidationService>();
            containerRegistry.RegisterSingleton<IWebApiService, WebApiService>();
            containerRegistry.RegisterSingleton<ILocalDataService, LocalDataService>();

            //Repositories
            containerRegistry.RegisterSingleton<IAuthRepository, AuthRepository>();
            containerRegistry.RegisterSingleton<IContactRepository, ContactRepository>();

            //ViewModels
            //Used Device.Idiom to register views depends phone o ipad device
            switch (Device.Idiom)
            {
                case TargetIdiom.Phone:
                    containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
                    containerRegistry.RegisterForNavigation<DashBoardPage, DashBoardViewModel>();
                    containerRegistry.RegisterForNavigation<ContactsPage, ContactsViewModel>();
                    containerRegistry.RegisterForNavigation<UsersPage, UsersViewModel>();
                    containerRegistry.RegisterForNavigation<WeatherPage, WeatherViewModel>();
                    break;
                case TargetIdiom.Tablet:
                    break;

            }

            Application.Current.Resources.Add("IoC", containerRegistry);
        }


    }
}

