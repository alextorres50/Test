using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Test.Core.UI
{	
	public partial class CustomInput : ContentView
    {
        private bool _edited;

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomInput), string.Empty,
                BindingMode.TwoWay);

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomInput), string.Empty, propertyChanged: OnPropertyChanged);

        public static readonly BindableProperty KeyboardProperty =
            BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(CustomInput), Keyboard.Plain);

        public static readonly BindableProperty ErrorMessageProperty =
            BindableProperty.Create(nameof(ErrorMessage), typeof(string), typeof(CustomInput), string.Empty);

        public static readonly BindableProperty ShowErrorProperty =
            BindableProperty.Create(nameof(ShowError), typeof(bool), typeof(CustomInput), true);

        public static readonly BindableProperty ForceFocusProperty =
            BindableProperty.Create(nameof(ForceFocus), typeof(bool), typeof(CustomInput), false,
                propertyChanged: OnSetFocusPropertyChanged);

        public static readonly BindableProperty ExpressionProperty =
            BindableProperty.Create(nameof(Expression), typeof(string), typeof(CustomInput), string.Empty);

        public static readonly BindableProperty ErrorColorProperty =
            BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(CustomInput), Color.FromHex("762d70"));

        public static readonly BindableProperty ActiveColorProperty =
            BindableProperty.Create(nameof(ActiveColor), typeof(Color), typeof(CustomInput), Color.FromHex("3db1c8"));

        public static readonly BindableProperty InactiveColorProperty =
            BindableProperty.Create(nameof(InactiveColor), typeof(Color), typeof(CustomInput), Color.FromHex("8c8c8c"));

        public static readonly BindableProperty HintProperty =
            BindableProperty.Create(nameof(Hint), typeof(string), typeof(CustomInput), string.Empty);

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(CustomInput), false);

        public static readonly BindableProperty IsReadonlyProperty =
            BindableProperty.Create(nameof(IsReadonly), typeof(bool), typeof(CustomInput), false);

        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(CustomInput), false, BindingMode.OneWayToSource);

        public static readonly BindableProperty ShowInfoCommandProperty =
            BindableProperty.Create(nameof(ShowInfoCommand), typeof(ICommand), typeof(CustomInput), null);

        public static readonly BindableProperty SourceIconProperty =
            BindableProperty.Create(nameof(SourceIcon), typeof(ImageSource), typeof(CustomInput), null);

        public static readonly BindableProperty HasInfoIconProperty =
            BindableProperty.Create(nameof(HasInfoIcon), typeof(bool), typeof(CustomInput), false);

        public CustomInput()
        {
            InitializeComponent();

            SetDefaultAppearance();
        }

        private void SetDefaultAppearance()
        {
            InputLabel.Text = Placeholder;
            InputLabel.TextColor = InactiveColor;
            Edit.Placeholder = Placeholder;

            EditContainer.Border = new Xamarin.Forms.PancakeView.Border
            {
                Thickness = 1,
                Color = InactiveColor
            };

        }

        public ICommand ShowInfoCommand
        {
            get => (ICommand)GetValue(ShowInfoCommandProperty);
            set => SetValue(ShowInfoCommandProperty, value);
        }

        public ImageSource SourceIcon
        {
            get => (ImageSource)GetValue(SourceIconProperty);
            set => SetValue(SourceIconProperty, value);
        }

        public bool HasInfoIcon
        {
            get => (bool)GetValue(HasInfoIconProperty);
            set => SetValue(HasInfoIconProperty, value);
        }

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public bool IsReadonly
        {
            get => (bool)GetValue(IsReadonlyProperty);
            set => SetValue(IsReadonlyProperty, value);
        }

        public string Hint
        {
            get => (string)GetValue(HintProperty);
            set => SetValue(HintProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }


        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public string ErrorMessage
        {
            get => (string)GetValue(ErrorMessageProperty);
            set => SetValue(ErrorMessageProperty, value);
        }

        public bool ShowError
        {
            get => (bool)GetValue(ShowErrorProperty);
            set => SetValue(ShowErrorProperty, value);
        }

        public string Expression
        {
            get => (string)GetValue(ExpressionProperty);
            set => SetValue(ExpressionProperty, value);
        }

        public Color ActiveColor
        {
            get => (Color)GetValue(ActiveColorProperty);
            set => SetValue(ActiveColorProperty, value);
        }

        public Color InactiveColor
        {
            get => (Color)GetValue(InactiveColorProperty);
            set => SetValue(InactiveColorProperty, value);
        }

        public Color ErrorColor
        {
            get => (Color)GetValue(ErrorColorProperty);
            set => SetValue(ErrorColorProperty, value);
        }

        public bool ForceFocus
        {
            get => (bool)GetValue(ForceFocusProperty);
            set => SetValue(ForceFocusProperty, value);
        }

        private static async void OnSetFocusPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bool)newValue)
            {
                var _control = (CustomInput)bindable;
                _control.Edit.ForceFocus = false;
                return;
            }

            await Task.Delay(500);
            var control = (CustomInput)bindable;
            control.Edit.ForceFocus = true;
            control.Edit.Focus();
        }

        private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;

            var control = (CustomInput)bindable;
            control.UpdateInputAppareance();
        }

        private void Edit_Focused(object sender, FocusEventArgs e)
        {
            _edited = true;
            UpdateInputAppareance();
        }

        private void Edit_Unfocused(object sender, FocusEventArgs e)
        {
            _edited = true;
            UpdateInputAppareance();
        }

        private void Edit_TextChanged(object sender, TextChangedEventArgs e)
        {
            _edited = true;
            UpdateInputAppareance();
        }

        private void UpdateInputAppareance()
        {
            if (!_edited)
            {
                SetDefaultAppearance();
                return;
            }

            if (!string.IsNullOrWhiteSpace(Expression) &&
                !string.IsNullOrWhiteSpace(Edit.Text))
            {
                InputLabel.Text = !Regex.IsMatch(Edit.Text, Expression) ? ErrorMessage : Placeholder;
                InputLabel.TextColor = !Regex.IsMatch(Edit.Text, Expression) ? ErrorColor : InactiveColor;
                IsValid = Regex.IsMatch(Edit.Text, Expression);
            }
            else
            {
                InputLabel.Text = Placeholder;
                InputLabel.TextColor = InactiveColor;
                IsValid = true;
            }

            Edit.Placeholder = Placeholder;

            if (string.IsNullOrEmpty(InputLabel.Text))
                InputLabel.Text = Placeholder;

            EditContainer.Border.Color = InputLabel.TextColor;

            if (IsPassword)
                TintPassEffect.SetTintColor(Edit, InputLabel.TextColor.ToHex());

            Edit.IsReadOnly = IsReadonly;
        }
    }

    public static class TintPassEffect
    {
        public static BindableProperty TintColorProperty =
            BindableProperty.CreateAttached("TintColor",
                                            typeof(string),
                                            typeof(TintPassEffect),
                                            "#3db1c8",
                                            propertyChanged: OnTintColorPropertyPropertyChanged);

        public static string GetTintColor(BindableObject element)
        {
            return (string)element.GetValue(TintColorProperty);
        }

        public static void SetTintColor(BindableObject element, string value)
        {
            element.SetValue(TintColorProperty, value);
        }


        static void OnTintColorPropertyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            AttachEffect(bindable as CustomEntry, Color.FromHex(newValue as string));
        }

        static void AttachEffect(CustomEntry element, Color color)
        {
            var effect = element.Effects.FirstOrDefault(x => x is ShowHidePassEffect) as ShowHidePassEffect;

            if (effect != null)
            {
                element.Effects.Remove(effect);
            }

            element.Effects.Add(new ShowHidePassEffect(color));
        }

    }
}

