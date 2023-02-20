using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Prism.Navigation;
using Test.Modules.Repositories.Interfaces;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;


namespace Test.Modules.ViewModels.Contacts
{
	public class ContactsViewModel : ViewModelBase
	{
		protected IContactRepository _contactRepository { get; set; }

        public ContactsViewModel(INavigationService navigationService, IContactRepository contactRepository) : base(navigationService)
        {
			_contactRepository = contactRepository;
		}

        public List<string> Descriptions { get; set; }

        List<Models.Contact> _listContacts;

        public List<Models.Contact> ListContacts { get => _listContacts; set { SetProperty(ref _listContacts, value); } }


        public override async void Initialize(INavigationParameters parameters)
		{
           await LoadUI();
		}

        public  async Task LoadUI()
        {

            var l = new Core.UI.LoadingDialog();

            await PopupNavigation.Instance.PushAsync(l, true);

            var contacts = await _contactRepository.GetContacts();

            ListContacts = new List<Models.Contact>();

            if (contacts?.Count > 0)
            {
                await InitDescriptions();

                var ncontacts = new List<Models.Contact>();
                foreach (var c in contacts)
                {
                    var newContact = new Models.Contact() { Id = c.Id, Name = c.Name, CreatedDate = c.CreatedAt ?? default(DateTime), UrlImage = c.Avatar };
                    newContact.Description = GetRandomDescription();
                    ncontacts.Add(newContact);
                }

                ListContacts = new List<Models.Contact>(ncontacts);

            }

            l.IsTaskSuccess = true;
        }


        private async Task InitDescriptions()
        {
            string FileName = "frases-tecnologia.txt";
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"Test.Core.Resources.{FileName}");
            using (var reader = new System.IO.StreamReader(stream))
            {
                var fileString = reader.ReadToEnd();

                //Converting JSON Array Objects into generic list    
                var desc = fileString.Split(
                        new string[] { "\r\n", "\r", "\n" },
                        StringSplitOptions.None
                    );
                Descriptions = new List<string>(desc);
            }
        }

        private string GetRandomDescription()
        {
            var rnd = new Random();

            var index = rnd.Next(0, Descriptions.Count -1);

            return Descriptions[index];
        }

        private bool CanButtonExecute = true;

        public Command<object> ClickCardCommand => new Command<object>(async (sender) =>
        {
            if (CanButtonExecute)
            {
                CanButtonExecute = false;
                var item = sender as Models.Contact;
                var l = new Core.UI.AlertDialog(Core.UI.TypeAlertDialog.Success, title: item.Name, msj: item.Description);

                await PopupNavigation.Instance.PushAsync(l, true);

                CanButtonExecute = true;
            }
        });

        bool isRefreshing = false;
        public bool IsRefreshing { get => isRefreshing; set { SetProperty(ref isRefreshing, value); RaisePropertyChanged(nameof(IsRefreshing)); } }

        
        public Command<object> RefreshCommand => new Command<object>(async (sender) =>
        {
            if (CanButtonExecute)
            {
                CanButtonExecute = false;
                
                if (IsRefreshing) return;

                await LoadUI();
                IsRefreshing = false;

                CanButtonExecute = true;
            }
        });

    }
}

