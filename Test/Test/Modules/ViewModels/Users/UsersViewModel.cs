using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Navigation;
using Test.Core.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;
using Xamarin.Essentials;
using Test.Core.Helpers;

namespace Test.Modules.ViewModels.Users
{
	public class UsersViewModel: ViewModelBase
	{
        LocalDataService _localDataService { get; set; }

        public UsersViewModel(INavigationService navigationService):base(navigationService)
		{
            
        }

        List<Models.User> _listUsers;

        public List<Models.User> ListUsers { get => _listUsers; set { SetProperty(ref _listUsers, value); } }


        public override async void Initialize(INavigationParameters parameters)
        {
            await LoadUI();
        }

        public async Task LoadUI()
        {

            var l = new Core.UI.LoadingDialog();

            await PopupNavigation.Instance.PushAsync(l, true);

            _localDataService = await Core.Services.LocalDataService.Instance;
            var users = await _localDataService.GetItemsAsync();

            ListUsers = new List<Models.User>();

            if (users?.Count > 0)
            {
                
                var nusers = new List<Models.User>();
                foreach (var c in users)
                {
                    var newContact = new Models.User() { ID = c.ID, Name = c.Name, Photo = c.Photo, Date = c.Date , EditCommand = EditCommand, DeleteCommand = DeleteCommand };
                    nusers.Add(newContact);
                }

                ListUsers = new List<Models.User>(nusers);

            }

            l.IsTaskSuccess = true;
        }

        private bool CanButtonExecute = true;
        /// <summary>
        /// Command used to Add New User, First open camera, after open a little name dialog and save this data into LocalBD
        /// </summary>
        public Command<object> AddCommand => new Command<object>(async (sender) =>
        {
            if (CanButtonExecute)
            {
                CanButtonExecute = false;


                await  AddImageAsync();


                CanButtonExecute = true;
            }
        });

        /// <summary>
        /// Command used to Add or Edit New User, First open camera, after open a little name dialog and save this data into LocalBD
        /// </summary>
        private async Task AddImageAsync(int id =0, string name = null)
        {
            try
            {
                var source = await Application.Current.MainPage.DisplayActionSheet(
                "Where do you want to take the image?",
                "Cancel",
                null,
                "FromCamera");
                if (source == "Cancel")
                    return;

                if (source == "FromCamera")
                {

                        if (await FilePickerHelper.CheckPluginMediaAndPermission(source))
                        {
                            if (MediaPicker.IsCaptureSupported)
                            {
                                var mediapickerOptions = new MediaPickerOptions() { Title = "" };
                                var result = await MediaPicker.CapturePhotoAsync();
                                if (result != null)
                                {

                                 var input = await MaterialDialog.Instance.InputAsync(title: "Formulario nombre", "Agregue un nombre", inputPlaceholder: "Agregue un Nombre", inputText: name);

                                var l = new Core.UI.LoadingDialog();

                                await PopupNavigation.Instance.PushAsync(l, true);

                                var stream = await result.OpenReadAsync();
                                var bytes = FilePickerHelper.GetImageStreamAsBytes(stream);
                                string base64ImageRepresentation = Convert.ToBase64String(bytes);

                                var newUser = new Core.Models.LocalStorage.User() { ID = id, Date = DateTime.Now, Photo = base64ImageRepresentation, Name = input}; 

                                await _localDataService.SaveItemAsync(newUser);

                               

                                l.IsTaskSuccess = true;

                                await LoadUI();

                                }
                            }
                            else
                            {
                                await PopupNavigation.Instance.PushAsync(new Core.UI.AlertDialog(typeAlertDialog: Core.UI.TypeAlertDialog.Error, title: "Error", msj: "Camara no disponible"), true);
                                
                            }

                        }
                    
                    
                }
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

        /// <summary>
        /// Command used to Edit New User, First open camera, after open a little name dialog and save this data into LocalBD
        /// </summary>
        public Command<object> EditCommand => new Command<object>(async (sender) =>
        {
            if (CanButtonExecute)
            {
                CanButtonExecute = false;

                var item = sender as Models.User;

                switch (Device.Idiom)
                {
                    case TargetIdiom.Phone:
                        await  AddImageAsync(item.ID, item.Name);
                        break;
                    case TargetIdiom.Tablet:
                        break;
                }

                CanButtonExecute = true;
            }
        });

        /// <summary>
        /// Command used to Delete User and save this data into LocalBD
        /// </summary>
        public Command<object> DeleteCommand => new Command<object>(async (sender) =>
        {
            if (CanButtonExecute)
            {
                CanButtonExecute = false;


               var result = await MaterialDialog.Instance.ConfirmAsync(message: "Borrar visitante?",
                                    title: "Confirmar",
                                    confirmingText: "Si",
                                    dismissiveText: "No");

                if (result??false)
                {
                    var l = new Core.UI.LoadingDialog();

                    await PopupNavigation.Instance.PushAsync(l, true);

                    var item = sender as Models.User;

                    var user = await _localDataService.GetItemAsync(item.ID);

                    await _localDataService.DeleteItemAsync(user);


                    l.IsTaskSuccess = true;

                    await LoadUI();
                }

                CanButtonExecute = true;
            }
        });
    }
}

