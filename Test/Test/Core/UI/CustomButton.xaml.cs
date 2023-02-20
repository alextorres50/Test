using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Test.Core.UI
{
    public partial class CustomButton : ContentView
    {
        public enum Size_Button
        {
            Large,
            Small,
            ExtraSmall,
            ExtraExtraSmall
        }

        public enum Style_Button
        {
            Normal,
            Underline,
            ExtraExtraSmall,
            OpacityBackground
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomButton), default(string), BindingMode.TwoWay, propertyChanged:
            new BindableProperty.BindingPropertyChangedDelegate(HandleTextPropertyChanged));

        public string Text
        {
            get { return (string)base.GetValue(TextProperty); }
            set { base.SetValue(TextProperty, value); }
        }

        private static void HandleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (CustomButton)bindable;
            button.lbltitle.Text = ((string)newValue);
        }

        public static readonly BindableProperty BackGroundInitialColorProperty = BindableProperty.Create(nameof(BackGroundInitialColor), typeof(Color), typeof(CustomButton), Color.Default, BindingMode.TwoWay, propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleBackGroundInitialColorChanged));
        public Color BackGroundInitialColor
        {
            get { return (Color)base.GetValue(BackGroundInitialColorProperty); }
            set { base.SetValue(BackGroundInitialColorProperty, value); }
        }
        private static void HandleBackGroundInitialColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (CustomButton)bindable;
            button.BackGroundInitialColor = ((Color)newValue);
        }

        public static readonly BindableProperty BackGroundEndColorProperty = BindableProperty.Create(nameof(BackGroundEndColor), typeof(Color), typeof(CustomButton), Color.Default, BindingMode.TwoWay, propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleBackGroundEndColorChanged));
        public Color BackGroundEndColor
        {
            get { return (Color)base.GetValue(BackGroundEndColorProperty); }
            set { base.SetValue(BackGroundEndColorProperty, value); }
        }
        private static void HandleBackGroundEndColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (CustomButton)bindable;
            button.BackGroundEndColor = ((Color)newValue);

            var gradient = new Xamarin.Forms.PancakeView.GradientStopCollection();

            if (button.BackGroundInitialColor == default(Color))
            {
                gradient.Add(new Xamarin.Forms.PancakeView.GradientStop { Color = button.BackGroundEndColor, Offset = 0.0f });
                gradient.Add(new Xamarin.Forms.PancakeView.GradientStop { Color = button.BackGroundEndColor, Offset = 1.0f });
            }
            else
            {
                gradient.Add(new Xamarin.Forms.PancakeView.GradientStop { Color = button.BackGroundInitialColor, Offset = 0.0f });
                gradient.Add(new Xamarin.Forms.PancakeView.GradientStop { Color = button.BackGroundEndColor, Offset = 1.0f });
            }
            button.Gradient.BackgroundGradientStops = gradient;
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CustomButton), Color.White, BindingMode.TwoWay, propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleTextColorChanged));
        public Color TextColor
        {
            get { return (Color)base.GetValue(TextColorProperty); }
            set { base.SetValue(TextColorProperty, value); }
        }
        private static void HandleTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (CustomButton)bindable;
            button.card.FindByName<Label>("lbltitle").TextColor = ((Color)newValue);
            button.card.FindByName<BoxView>("underline").BackgroundColor = ((Color)newValue);
        }

        public static readonly BindableProperty EnabledProperty = BindableProperty.Create(nameof(Enabled), typeof(bool), typeof(CustomButton), propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleEnabledChanged), defaultBindingMode: BindingMode.TwoWay);
        public bool Enabled
        {
            get { return (bool)base.GetValue(EnabledProperty); }
            set { base.SetValue(EnabledProperty, value); }
        }

        private static void HandleEnabledChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (CustomButton)bindable;
            button.Enabled = ((bool)newValue);
            button.Opacity = button.Enabled ? 1 : 0.48;
            button.IsEnabled = button.Enabled;
        }


        public static readonly BindableProperty ClickCommandProperty = BindableProperty.Create(nameof(ClickCommand), typeof(ICommand), typeof(CustomButton), propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleClickCommandChanged));
        public ICommand ClickCommand
        {
            get { return (ICommand)base.GetValue(ClickCommandProperty); }
            set { base.SetValue(ClickCommandProperty, value); }
        }

        private static void HandleClickCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (CustomButton)bindable;
            button.ClickCommand = ((ICommand)newValue);

            button.card.GestureRecognizers.Add(new TapGestureRecognizer() { Command = ((ICommand)newValue), CommandParameter = button.ClickCommandParameter });
        }


        public static readonly BindableProperty ClickCommandParameterProperty = BindableProperty.Create(nameof(ClickCommandParameter), typeof(object), typeof(CustomButton), propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleClickCommandParameterChanged));
        public object ClickCommandParameter
        {
            get { return (object)base.GetValue(ClickCommandParameterProperty); }
            set { base.SetValue(ClickCommandParameterProperty, value); }
        }

        private static void HandleClickCommandParameterChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (CustomButton)bindable;
            button.ClickCommandParameter = ((object)newValue);
        }

        public static readonly BindableProperty SizeButtonProperty = BindableProperty.Create(nameof(SizeButton), typeof(Size_Button), typeof(CustomButton), propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleSizeButtonChanged));
        public Size_Button SizeButton
        {
            get { return (Size_Button)base.GetValue(SizeButtonProperty); }
            set { base.SetValue(SizeButtonProperty, value); }
        }

        private static void HandleSizeButtonChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (CustomButton)bindable;
            button.SizeButton = ((Size_Button)newValue);
            switch (button.SizeButton)
            {
                case Size_Button.Large:
                    button.BackgroundView.Margin = new Thickness(0, 15, 0, 15);
                    break;
                case Size_Button.Small:
                    button.BackgroundView.Margin = new Thickness(0, 11, 0, 11);
                    button.Shadow.CornerRadius = Device.RuntimePlatform == Device.Android ? new CornerRadius(48) : new CornerRadius(20);
                    button.card.CornerRadius = Device.RuntimePlatform == Device.Android ? 48 : 18;
                    button.Gradient.CornerRadius = Device.RuntimePlatform == Device.Android ? 0 : 18;
                    button.lbltitle.FontSize = Device.RuntimePlatform == Device.Android ? 13 : 14;
                    break;
                case Size_Button.ExtraSmall:
                    button.BackgroundView.Margin = new Thickness(0, 8, 0, 8);
                    button.Shadow.CornerRadius = Device.RuntimePlatform == Device.Android ? new CornerRadius(40) : new CornerRadius(10);
                    button.card.CornerRadius = Device.RuntimePlatform == Device.Android ? 40 : 15;
                    button.Gradient.CornerRadius = Device.RuntimePlatform == Device.Android ? 0 : 15;
                    button.lbltitle.FontSize = Device.RuntimePlatform == Device.Android ? 10 : 12;
                    break;
                case Size_Button.ExtraExtraSmall:
                    button.BackgroundView.Margin = Device.RuntimePlatform == Device.Android ? new Thickness(0, 2, 0, 2) : new Thickness(0, 3, 0, 3);
                    button.Shadow.CornerRadius = Device.RuntimePlatform == Device.Android ? new CornerRadius(40) : new CornerRadius(10);
                    button.card.CornerRadius = Device.RuntimePlatform == Device.Android ? 40 : 10;
                    button.Gradient.CornerRadius = Device.RuntimePlatform == Device.Android ? 0 : 10;
                    button.lbltitle.FontSize = Device.RuntimePlatform == Device.Android ? 8.65 : 8.65;
                    break;

            }

        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomButton), Color.Default, BindingMode.TwoWay, propertyChanged: HandleBorderColorChanged);
        public Color BorderColor
        {
            get { return (Color)base.GetValue(BorderColorProperty); }
            set { base.SetValue(BorderColorProperty, value); }
        }

        private static void HandleBorderColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (CustomButton)bindable;
            button.FindByName<XF.Material.Forms.UI.MaterialCard>("card").BorderColor = ((Color)newValue);
        }


        public static readonly BindableProperty StyleButtonProperty = BindableProperty.Create(nameof(StyleButton), typeof(Style_Button), typeof(CustomButton), propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(HandleStyleButtonChanged));
        public Style_Button StyleButton
        {
            get { return (Style_Button)base.GetValue(StyleButtonProperty); }
            set { base.SetValue(StyleButtonProperty, value); }
        }

        private static void HandleStyleButtonChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (CustomButton)bindable;
            button.StyleButton = ((Style_Button)newValue);
            var gradient = new Xamarin.Forms.PancakeView.GradientStopCollection();
            switch (button.StyleButton)
            {
                case Style_Button.Normal:
                    button.FindByName<BoxView>("underline").IsVisible = false;
                    button.FindByName<Label>("lbltitle").FontFamily = "DelaGothicRegularFont";
                    button.card.Opacity = 1;

                    if (button.BackGroundInitialColor == default(Color))
                    {
                        gradient.Add(new Xamarin.Forms.PancakeView.GradientStop { Color = button.BackGroundEndColor, Offset = 0.0f });
                        gradient.Add(new Xamarin.Forms.PancakeView.GradientStop { Color = button.BackGroundEndColor, Offset = 1.0f });
                    }
                    else
                    {
                        gradient.Add(new Xamarin.Forms.PancakeView.GradientStop { Color = button.BackGroundInitialColor, Offset = 0.0f });
                        gradient.Add(new Xamarin.Forms.PancakeView.GradientStop { Color = button.BackGroundEndColor, Offset = 1.0f });
                    }
                    button.Gradient.BackgroundGradientStops = gradient;
                    button.Shadow.Opacity = Device.RuntimePlatform == Device.Android ? .80 : .15;
                    break;
                case Style_Button.Underline:
                    button.FindByName<BoxView>("underline").IsVisible = true;
                    button.FindByName<Label>("lbltitle").FontFamily = "SemiBoldFont";
                    button.card.BackgroundColor = button.BackGroundEndColor.MultiplyAlpha(.33);
                    gradient.Add(new Xamarin.Forms.PancakeView.GradientStop { Color = Color.Transparent, Offset = 0.0f });
                    gradient.Add(new Xamarin.Forms.PancakeView.GradientStop { Color = Color.Transparent, Offset = 1.0f });
                    button.Gradient.BackgroundGradientStops = gradient;
                    button.Shadow.Opacity = 0;
                    break;
                case Style_Button.ExtraExtraSmall:
                    button.FindByName<BoxView>("underline").IsVisible = false;
                    button.FindByName<Label>("lbltitle").FontFamily = "RegularFont";
                    button.FindByName<Label>("lbltitle").TextColor = button.BackGroundInitialColor;
                    button.FindByName<Label>("lbltitle").Margin = new Thickness(10, 5, 10, 0);
                    button.card.BackgroundColor = Color.FromHex("#A1C6C6").MultiplyAlpha(.71);
                    gradient.Add(new Xamarin.Forms.PancakeView.GradientStop { Color = Color.Transparent, Offset = 0.0f });
                    gradient.Add(new Xamarin.Forms.PancakeView.GradientStop { Color = Color.Transparent, Offset = 1.0f });
                    button.Gradient.BackgroundGradientStops = gradient;
                    button.Shadow.Opacity = 0;
                    break;
                case Style_Button.OpacityBackground:
                    button.FindByName<BoxView>("underline").IsVisible = false;
                    button.FindByName<Label>("lbltitle").FontFamily = "SemiBoldFont";
                    button.card.BackgroundColor = button.BackGroundEndColor;
                    gradient.Add(new Xamarin.Forms.PancakeView.GradientStop { Color = Color.Transparent, Offset = 0.0f });
                    gradient.Add(new Xamarin.Forms.PancakeView.GradientStop { Color = Color.Transparent, Offset = 1.0f });
                    button.Gradient.BackgroundGradientStops = gradient;
                    button.Shadow.Opacity = 0;
                    break;
            }
        }

        public static readonly BindableProperty SourceLeftIconProperty = BindableProperty.Create(nameof(SourceLeftIcon), typeof(string), typeof(CustomButton), default(string), BindingMode.TwoWay, propertyChanged: HandleSourceLeftIconChanged);
        public string SourceLeftIcon
        {
            get { return (string)base.GetValue(SourceLeftIconProperty); }
            set { base.SetValue(SourceLeftIconProperty, value); }
        }

        private static void HandleSourceLeftIconChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = (CustomButton)bindable;
            button.SourceLeftIcon = ((string)newValue);
            button.FindByName<Image>("leftIcon").Source = Xamarin.Forms.Svg.SvgImageSource.FromSvgResource(button.SourceLeftIcon, 10, 20);
            button.FindByName<Grid>("leftIconGrid").IsVisible = !string.IsNullOrEmpty(button.SourceLeftIcon);
        }

        public CustomButton()
        {
            InitializeComponent();

        }
    }
}

