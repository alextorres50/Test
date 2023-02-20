using System;
using Prism.Mvvm;
using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;
using Test.Core.Services.Interfaces;

namespace Test.Modules.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible, IInitializeAsync
    {

        public INavigationService NavigationService { get; set; }
        protected ISettingsService _settingsService { get; set; }
        protected ITranslatorService _translatorService { get; set; }
        protected IValidationService _validationService { get; set; }


        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _uploadProgress;
        public string UploadProgress
        {
            get => _uploadProgress;
            set { SetProperty(ref _uploadProgress, value); RaisePropertyChanged(nameof(UploadProgress)); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;

        }

        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }


        public virtual void Destroy()
        {

        }

        public virtual void Clear()
        {

        }


        public async virtual Task InitializeAsync(INavigationParameters parameters)
        {

        }

        private bool CanBackButtonExecute = true;
        /// <summary>
        /// Command used to Go Backbutton
        /// </summary>
        public Command<object> BackButtonCommand => new Command<object>(async (sender) =>
        {
            if (CanBackButtonExecute)
            {
                CanBackButtonExecute = false;
                await NavigationService.GoBackAsync();
                CanBackButtonExecute = true;
            }
        });

    }
}

