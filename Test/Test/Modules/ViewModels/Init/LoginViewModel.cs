using System;
using System.Linq;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using Test.Core.Models;
using Test.Core.Router;
using Test.Core.UI;
using Test.Modules.Repositories.Interfaces;
using Xamarin.Forms;
using Test.Core.Services.Interfaces;

namespace Test.Modules.ViewModels.Init
{
	public class LoginViewModel : ViewModelBase
	{
        protected IAuthRepository _authRepository { get; set; }
        public LoginViewModel(INavigationService navigationService, IAuthRepository authRepository, IValidationService validationService):base(navigationService)
		{
            _authRepository = authRepository;
            _validationService = validationService;
		}

        public override async void Initialize(INavigationParameters parameters)
        {
            Email = null;
            Password = null;
           
        }

        private string _email;
		public string Email
		{
			get => _email;
			set { SetProperty(ref _email,value); RaisePropertyChanged(nameof(Email)); }
		}
        private string _password ;
		public string Password
		{
			get => _password;
			set { SetProperty(ref _password, value); RaisePropertyChanged(nameof(Password)); }
		}

        public bool HasErrorEmail
        {
            get
            {
                if (string.IsNullOrEmpty(_email)) return false;

                return !_validationService.IsValidEmail(_email);
            }
        }

        private bool isPassword = true;
        public bool IsPassword
        {
            get => this.isPassword;
            set { SetProperty(ref this.isPassword, value); RaisePropertyChanged(nameof(IsPassword)); RaisePropertyChanged(nameof(SourceIcon)); }
        }


        public ImageSource SourceIcon { get=> IsPassword ? Xamarin.Forms.Svg.SvgImageSource.FromSvgResource("Test.Core.Resources.showpassword.svg", 32, 32) : Xamarin.Forms.Svg.SvgImageSource.FromSvgResource("Test.Core.Resources.hidepassword.svg", 32, 32); }

        private bool CanButtonExecute = true;
        /// <summary>
        /// Command used to Verify LoginCredentials with API and navigated to Home
        /// </summary>
        public Command<object> LoginCommand => new Command<object>(async (sender) =>
        {
            if (CanButtonExecute)
            {
                CanButtonExecute = false;
                var l = new LoadingDialog();

                await PopupNavigation.Instance.PushAsync(l, true);

                if(!HasErrorEmail && !string.IsNullOrEmpty(_email))
                {

                    l.IsTaskSuccess = true;

                    if (await _authRepository.Login(_email, _password))
                    {
                        l.IsTaskSuccess = true;

                        switch (Device.Idiom)
                        {
                            case TargetIdiom.Phone:
                                var route = $"{RouterNavigation.Absolute}{RouterNavigation.DashBoardPageInit}";
                                var _a = await NavigationService.NavigateAsync(route, animated: false);
                                break;
                            case TargetIdiom.Tablet:
                                break;
                        }

                    }
                    else
                    {
                        l.IsTaskSuccess = false;
                        await PopupNavigation.Instance.PushAsync(new AlertDialog(typeAlertDialog: TypeAlertDialog.Error, title: "Error", msj: _authRepository.GetErrors.FirstOrDefault()?.Message), true);
                    }
                }
                CanButtonExecute = true;
            }
        });

        /// <summary>
        /// Command used to General button
        /// </summary>
        public Command<object> ButtonCommand => new Command<object>(async (sender) =>
        {
            if (CanButtonExecute)
            {
                CanButtonExecute = false;

                var l = new AlertDialog(TypeAlertDialog.Success,title:"Information",msj: sender as string);

                await PopupNavigation.Instance.PushAsync(l, true);

                CanButtonExecute = true;
            }
        });

        /// <summary>
        /// Command used to Show/Hide Password button
        /// </summary>
        public Command<object> ShowHidePasswordCommand => new Command<object>(async (sender) =>
        {
            if (CanButtonExecute)
            {
                CanButtonExecute = false;

                IsPassword = !isPassword;

                CanButtonExecute = true;
            }
        });
    }
}

