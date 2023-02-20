using System;
using Test.iOS.Renders;
using Test.Core.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;
using System.Drawing;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace Test.iOS.Renders
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            if (this.Control != null)
            {
                var element = (CustomEntry)Element;

                Control.TintColor = UIColor.Black;
                Control.BorderStyle = UITextBorderStyle.None;
                Control.RightView = new UIView(new CGRect(0, 0, 8, this.Control.Frame.Height));
                Control.RightViewMode = UITextFieldViewMode.Always;
                Element.HeightRequest = 30;

                Control.LeftViewMode = UITextFieldViewMode.Always;
                Control.LeftView = !string.IsNullOrEmpty(element.Image) ?
                    GetImageView(element.Image, 16, 16) :
                    new UIView(new CGRect(0, 0, 8, Control.Frame.Height));

                Control.SpellCheckingType = UIKit.UITextSpellCheckingType.Yes;
                Control.AutocorrectionType = UIKit.UITextAutocorrectionType.Yes;
                Control.AutocapitalizationType = UIKit.UITextAutocapitalizationType.None;
            }
        }

        private UIView GetImageView(string image, int height, int width)
        {
            var imageView = new UIImageView(UIImage.FromBundle(image))
            {
                Frame = new RectangleF(0, 0, height, width)
            };

            UIView objLeftView = new UIView(new System.Drawing.Rectangle(0, 0, width + 10, height));
            objLeftView.AddSubview(imageView);

            return objLeftView;
        }
    }
}

