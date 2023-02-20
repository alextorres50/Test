using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Test.Core.UI
{
    public partial class LoadingDialog : Rg.Plugins.Popup.Pages.PopupPage
    {
        public string UploadProgress { get; set; }
        public string TextResult { get; set; }
        public object Binding { get; set; }
        public bool ShowEndIcon { get; set; }
        bool isTaskSuccess;
        public bool IsTaskSuccess { get => isTaskSuccess; set { isTaskSuccess = value; ChangeStateAnimation(); } }
        public LoadingDialog()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
                AnimationView.PlayMinAndMaxFrame(1, 74);


        }

        // ### Methods for supporting animations in your popup page ###
        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();

            if (Binding != null)
            {
                this.BindingContext = Binding;
            }
            if (UploadProgress != null)
            {
                lbldescription.SetBinding(Label.TextProperty, nameof(UploadProgress));
            }


        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {

            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {

            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return true;
        }

        async void ChangeStateAnimation()
        {
            if (Device.RuntimePlatform == Device.Android)
                await Task.Delay(100);
            try
            {
                await PopupNavigation.Instance.RemovePageAsync(this);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

    }
}


