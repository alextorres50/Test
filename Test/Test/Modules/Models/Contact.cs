using System;
using Xamarin.Forms;

namespace Test.Modules.Models
{
	public class Contact
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public DateTime CreatedDate { get; set; }

        public Color BackgroundColor {
            get {
                return Id % 2 == 0 ? Color.White : Color.DarkOrange;
            }
        }

        public Color TextColor
        {
            get
            {
                return Id % 2 == 0 ? Color.Black : Color.White;
            }
        }

        public string IsBlocked { get => (Id % 2 == 0).ToString(); }

        public string Created { get => CreatedDate.ToString("dd MMMM yyyy hh:mm:ss"); }

        public Command<object> ClickCardCommand { get; set; }
    }
}

