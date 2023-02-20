using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Test.Core.Helpers
{
	public class FilePickerHelper
	{
        /// <summary>
        /// Method used to check if app has Media Plugins permissions (Camera and Gallery permissions)
        /// </summary>
        /// <param name="typeRequestProfilePicture">type of permission to be checked (TypeRequestProfilePicture.Camera, TypeRequestProfilePicture.Galery)</param>
        /// <returns></returns>
        public static async Task<bool> CheckPluginMediaAndPermission(string typeRequestProfilePicture)
        {
            switch (typeRequestProfilePicture)
            {
                case "FromCamera":
                    var cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
                    if (cameraStatus != Xamarin.Essentials.PermissionStatus.Granted)
                    {
                        var results = await Permissions.RequestAsync<Permissions.Camera>();
                        if (results != Xamarin.Essentials.PermissionStatus.Granted)
                        {
                            return false;
                        }
                        return true;
                    }
                    break;
                case "FromGallery":
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        var storageStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                        var storageStatusw = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                        if (storageStatus != Xamarin.Essentials.PermissionStatus.Granted || storageStatusw != Xamarin.Essentials.PermissionStatus.Granted)
                        {
                            if (storageStatus != Xamarin.Essentials.PermissionStatus.Granted)
                            {
                                var results = await Permissions.RequestAsync<Permissions.StorageRead>();
                                if (results != Xamarin.Essentials.PermissionStatus.Granted)
                                {
                                    return false;
                                }
                            }
                            if (storageStatusw != Xamarin.Essentials.PermissionStatus.Granted)
                            {
                                var results = await Permissions.RequestAsync<Permissions.StorageWrite>();
                                if (results != Xamarin.Essentials.PermissionStatus.Granted)
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                        break;
                    }
                    else
                    {
                        var storageStatus = await Permissions.CheckStatusAsync<Permissions.Photos>();
                        if (storageStatus != Xamarin.Essentials.PermissionStatus.Granted)
                        {
                            var results = await Permissions.RequestAsync<Permissions.Photos>();
                            if (results != Xamarin.Essentials.PermissionStatus.Granted)
                            {
                                return false;
                            }
                            return true;
                        }
                        break;
                    }
            }
            return true;
        }

        public static byte[] GetImageStreamAsBytes(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}

