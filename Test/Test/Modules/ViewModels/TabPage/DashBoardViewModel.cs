using System;
using System.Collections.Generic;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using Test.Core.UI;
using Test.Core.Router;
using Test.Core.Services.Interfaces;
using System.Linq;
using Xamarin.Forms;

namespace Test.Modules.ViewModels.TabPage
{
	public class DashBoardViewModel : ViewModelBase
    {
        public DashBoardViewModel(INavigationService navigationService, ITranslatorService translatorService) : base(navigationService)
        {
            _translatorService = translatorService;
        }
        private List<Models.Choice> _languages;
        public List<Models.Choice> Languages { get => _languages; set { SetProperty(ref _languages,value); RaisePropertyChanged(nameof(Languages)); } }

        public override async void Initialize(INavigationParameters parameters)
        {
            var actualLanguage = _translatorService.GetCultureInfo();

            var twoLetterISOLanguageName = actualLanguage.TwoLetterISOLanguageName;
            LoadUI();
            switch (twoLetterISOLanguageName)
            {
                case "en":
                    Language = Assets.i18n.AppResources.english;
                    break;
                case "es":
                    Language = Assets.i18n.AppResources.spanish;
                    break;
            }
            SaveUpdatedDate();

        }

        public bool IsToogle { get; set; }

        private string _language;
        public string Language
        {
            get => _language;
            set { SetProperty(ref _language, value); RaisePropertyChanged(nameof(Language)); }
        }

        private string _lblconfiguration;
        public string Lblconfiguration
        {
            get => _lblconfiguration;
            set { SetProperty(ref _lblconfiguration, value); RaisePropertyChanged(nameof(Lblconfiguration)); }
        }

        private string _lblswitch;
        public string Lblswitch
        {
            get => _lblswitch;
            set { SetProperty(ref _lblswitch, value); RaisePropertyChanged(nameof(Lblswitch)); }
        }

        private string _lbltheme;
        public string Lbltheme
        {
            get => _lbltheme;
            set { SetProperty(ref _lbltheme, value); RaisePropertyChanged(nameof(Lbltheme)); }
        }

        private string _lbldate;
        public string Lbldate
        {
            get => _lbldate;
            set { SetProperty(ref _lbldate, value); RaisePropertyChanged(nameof(Lbldate)); }
        }
        private string _lbllanguage;
        public string Lbllanguage
        {
            get => _lbllanguage;
            set { SetProperty(ref _lbllanguage, value); RaisePropertyChanged(nameof(Lbllanguage)); }
        }

        private string _date;
        public string Date
        {
            get => _date;
            set { SetProperty(ref _date, value); RaisePropertyChanged(nameof(Date)); }
        }
        Models.Choice _selectedItem;
        public Models.Choice SelectedItem { get => _selectedItem;set {
                SetProperty(ref _selectedItem,value);
                ChangeLanguage();
                
            }
        }
        private bool CanExecute = true;
        private async void ChangeLanguage()
        {
            if (CanExecute)
            {
                CanExecute = false;
                var l = new LoadingDialog();

                await PopupNavigation.Instance.PushAsync(l, true);
                var id = SelectedItem.Id;
                var culture = SelectedItem.Name == Assets.i18n.AppResources.english ? "en-US" : "es-US";
                await _translatorService.SetCultureInfo(culture);
                LoadUI();
                switch (culture)
                {
                    case "en-US":
                        Language = Assets.i18n.AppResources.english;
                        break;
                    case "es-US":
                        Language = Assets.i18n.AppResources.spanish;
                        break;
                }
                SaveUpdatedDate();
                l.IsTaskSuccess = true;
                CanExecute = true;
            }
            
        }

        public void LoadUI()
        {
            Languages = new List<Models.Choice>() { new Models.Choice() { Id = 0, Name = Assets.i18n.AppResources.english }, new Models.Choice() { Id = 1, Name = Assets.i18n.AppResources.spanish } };

            Lblconfiguration = Assets.i18n.AppResources.lblconfiguration;
            Lbllanguage = Assets.i18n.AppResources.lbllanguage;
            Lblswitch = Assets.i18n.AppResources.lblswitch;
            Lbltheme = IsToogle ? Assets.i18n.AppResources.lbltheme2 : Assets.i18n.AppResources.lbltheme1;
            Lbldate = Assets.i18n.AppResources.lbldate;
           
        }

        public void SaveUpdatedDate()
        {
            Date = DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss");
        }

        private bool CanButtonExecute = true;
        /// <summary>
        /// Command used to Open New Pages
        /// </summary>
        public Command<object> ButtonCommand => new Command<object>(async (sender) =>
        {
            if (CanButtonExecute)
            {
                CanButtonExecute = false;

                var option = sender as string;
                var route = string.Empty;
                switch (option)
                {
                    case "Weather":
                        route = $"{RouterNavigation.WeatherPage}";
                        break;
                    case "Multimedia":
                        route = $"{RouterNavigation.UsersPage}";
                        break;
                    case "Contacts":
                        route = $"{RouterNavigation.ContactsPage}";
                        break;
                }

                switch (Device.Idiom)
                {
                    case TargetIdiom.Phone:
                        var _a = await NavigationService.NavigateAsync(route, animated: false);
                        break;
                    case TargetIdiom.Tablet:
                        break;
                }

                CanButtonExecute = true;
            }
        });

        private bool CanBackButtonExecute = true;
        /// <summary>
        /// Command used to Simulate Logout Session
        /// </summary>
        public Command<object> LogoutCommand => new Command<object>(async (sender) =>
        {
            if (CanBackButtonExecute)
            {
                CanBackButtonExecute = false;

                switch (Device.Idiom)
                {
                    case TargetIdiom.Phone:
                        var route = $"{RouterNavigation.Absolute}{RouterNavigation.LoginPageInit}";
                        var _a = await NavigationService.NavigateAsync(route, animated: false);
                        break;
                    case TargetIdiom.Tablet:
                        break;
                }

                CanBackButtonExecute = true;
            }
        });
    }
}

