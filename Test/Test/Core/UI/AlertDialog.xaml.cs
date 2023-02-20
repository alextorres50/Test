using System;
using System.Collections.Generic;
using System.Diagnostics;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Test.Core.UI
{
    public enum TypeAlertDialog
    {
        Success,
        Error
    }

    public partial class AlertDialog : PopupPage
    {
        public AlertDialog()
        {
            InitializeComponent();
            icon_close.GestureRecognizers.Add(new TapGestureRecognizer() { Command = CloseButtonCommand });
        }
        Command<object> command;
        public Command<object> Command { get => command; set { command = value; if (command != null) { button.GestureRecognizers.Add(new TapGestureRecognizer() { Command = Command }); } } }

        public AlertDialog(TypeAlertDialog typeAlertDialog, string title = null, string msj = null)
        {
            InitializeComponent();
            switch (typeAlertDialog)
            {
                case TypeAlertDialog.Success:
                    break;
                case TypeAlertDialog.Error:
                    break;
            }


            lbltitle.Text = title;
            lblsubtitle.Text = msj;
            icon_close.GestureRecognizers.Add(new TapGestureRecognizer() { Command = CloseButtonCommand });
            if (Command != null)
                button.GestureRecognizers.Add(new TapGestureRecognizer() { Command = Command });
            else
                button.GestureRecognizers.Add(new TapGestureRecognizer() { Command = CloseButtonCommand });
        }

        public AlertDialog(TypeAlertDialog typeAlertDialog, string title = null, string msj = null, Command<object> command = null)
        {
            InitializeComponent();
            switch (typeAlertDialog)
            {
                case TypeAlertDialog.Success:
                    break;
                case TypeAlertDialog.Error:
                    break;
            }


            lbltitle.Text = title;
            lblsubtitle.Text = msj;
            icon_close.GestureRecognizers.Add(new TapGestureRecognizer() { Command = CloseButtonCommand });
            if (command != null)
            {
                var nCommand = new Command<object>(async (obj) => {
                    command.Execute(null);
                    CloseButtonCommand.Execute(null);
                });
                button.GestureRecognizers.Add(new TapGestureRecognizer() { Command = nCommand });
            }
            else
                button.GestureRecognizers.Add(new TapGestureRecognizer() { Command = CloseButtonCommand });
        }

        private bool CanCloseButtonExecute = true;
        public Command<object> CloseButtonCommand => new Command<object>(async (sender) =>
        {
            if (CanCloseButtonExecute)
            {
                CanCloseButtonExecute = false;
                try
                {
                    await PopupNavigation.Instance.RemovePageAsync(this, true);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
                CanCloseButtonExecute = true;
            }
        });
    }
}

