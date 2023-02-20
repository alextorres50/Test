using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Test.Modules.UI.TabPage
{
    public partial class DashBoardPage : ContentPage
    {
        public DashBoardPage()
        {
            InitializeComponent();
        }

        void Switch_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            
            Xamarin.Forms.Application.Current.UserAppTheme = e.Value ? OSAppTheme.Dark : OSAppTheme.Light;
            var dashboard = BindingContext as ViewModels.TabPage.DashBoardViewModel;
            dashboard.LoadUI();
            dashboard.SaveUpdatedDate();
        }

    }
}

