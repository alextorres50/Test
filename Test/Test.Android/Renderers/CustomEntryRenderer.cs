using System;
using Test.Droid.Renderers;
using Xamarin.Forms;
using Test.Core.UI;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Android.OS;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Widget;
using AndroidX.Core.Content;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace Test.Droid.Renderers
{
	public class CustomEntryRenderer : EntryRenderer
    {
        CustomEntry element;

        public CustomEntryRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            if (Control != null)
            {
                element = (CustomEntry)Element;

                if (!string.IsNullOrEmpty(element.Image))
                {
                    Control.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.Image), null, null, null);
                }

                Control.CompoundDrawablePadding = 20;
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
                {
                    Control.SetTextCursorDrawable(Resource.Drawable.my_cursor); //This API Intrduced in android 10
                }
                else
                {
                    IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                    IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
                    JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.my_cursor); // replace 0 with a Resource.Drawable.my_cursor
                }
                if (element.ForceFocus)
                {
                    Control.RequestFocus();

                }
                Control.SetSelection(Control.Text.Length);

            }
        }

        private Drawable GetDrawable(string imageEntryImage)
        {
            int resID = Resources.GetIdentifier(imageEntryImage, "drawable", this.Context.PackageName);
            return ContextCompat.GetDrawable(this.Context, resID);
        }
    }
}

