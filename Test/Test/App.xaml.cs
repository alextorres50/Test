using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Essentials;
using Prism.DryIoc;
using Test.Core.Services.Interfaces;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Test.Core.Router;
using Xamarin.Forms.Svg;

namespace Test
{
    public partial class App : PrismApplication
    {
        ISettingsService _settingsService;
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {

        }

        protected async override void OnInitialized()
        {
            InitializeComponent();

            XF.Material.Forms.Material.Init(this);
            //Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            Xamarin.Forms.Application.Current.UserAppTheme = OSAppTheme.Light;
            Xamarin.Forms.Application.Current.RequestedThemeChanged += (s, a) =>
            {
                // Respond to the theme change
               
            };
            SvgImageSource.RegisterAssembly();


            if (Container.IsRegistered<ISettingsService>())
            {

                _settingsService = Container.Resolve<ISettingsService>();
                var token = _settingsService.GetString<string>("oauthToken");
                if (!string.IsNullOrEmpty(token))
                {

                    switch (Device.Idiom)
                    {
                        case TargetIdiom.Phone:
                            var b = await NavigationService.NavigateAsync(RouterNavigation.DashBoardPageInit, animated: false);
                            break;
                        case TargetIdiom.Tablet:
                            break;
                    }

                }
                else
                {
                    switch (Device.Idiom)
                    {
                        case TargetIdiom.Phone:
                            var a = await NavigationService.NavigateAsync(RouterNavigation.LoginPageInit, null, false, animated: false);
                            break;
                        case TargetIdiom.Tablet:
                            break;

                    }
                }
            }
            else
            {
                switch (Device.Idiom)
                {
                    case TargetIdiom.Phone:
                        var a = await NavigationService.NavigateAsync(RouterNavigation.LoginPageInit, null, false, animated: false);
                        break;
                    case TargetIdiom.Tablet:
                        break;

                }
            }
        }

        protected async override void OnStart ()
        {
            var _translatorService = Container.Resolve<ITranslatorService>();
            await _translatorService.SetCultureInfo("en-US");
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            Core.Locator.Locator.RegisterTypes(containerRegistry);
        }
    }
}

