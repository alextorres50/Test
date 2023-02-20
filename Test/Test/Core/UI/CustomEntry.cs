using System;
using Xamarin.Forms;

namespace Test.Core.UI
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomEntry), string.Empty);

        public static readonly BindableProperty ForceFocusProperty =
            BindableProperty.Create(nameof(ForceFocus), typeof(bool), typeof(CustomEntry), false,
                propertyChanged: OnSetFocusPropertyChanged);

        public string Image
        {
            get => (string)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public bool ForceFocus
        {
            get => (bool)GetValue(ForceFocusProperty);
            set => SetValue(ForceFocusProperty, value);
        }

        private static void OnSetFocusPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bool)newValue)
            {
                var _control = (CustomEntry)bindable;
                _control.ForceFocus = false;
                _control.Unfocus();
                return;
            }
        }


        public CustomEntry()
        {
        }
    }

    public class PassWordEntry : CustomEntry
    {
        public PassWordEntry()
        {
            this.Effects.Add(new ShowHidePassEffect(Color.FromHex("#3db1c8")));
        }
    }

    public class ShowHidePassEffect : RoutingEffect
    {
        public Color TintColor { get; private set; }

        public string EntryText { get; set; }
        public ShowHidePassEffect(Color color) : base("com.test.test.Test.ShowHidePassEffect")
        {

            TintColor = color;
        }
    }
}

