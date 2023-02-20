using System;
using System.IO;
using Xamarin.Forms;
namespace Test.Modules.Models
{
	public class User
	{
		public int ID { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public DateTime Date { get; set; }
        public string UpdateDate { get => Date.ToString(); }
        public string Hour { get => Date.ToString("hh:mm tt"); }
        public Command<object> EditCommand { get; set; }
        public Command<object> DeleteCommand { get; set; }
        private Xamarin.Forms.ImageSource image;
        public Xamarin.Forms.ImageSource Image
        {
            get
            {
                if (image == null)
                {
                    image = Xamarin.Forms.ImageSource.FromStream(
                        () => new MemoryStream(Convert.FromBase64String(Photo)));
                }
                return image;
            }
        }


    }
}

