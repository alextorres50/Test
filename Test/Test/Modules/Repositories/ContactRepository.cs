using System;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl;
using Test.Modules.Repositories.Interfaces;
using System.Collections.Generic;
using Test.Core.Models.WebApi.Contacts;

namespace Test.Modules.Repositories
{
	public class ContactRepository : Core.Services.WebApiService, IContactRepository
	{
		public ContactRepository(Core.Services.Interfaces.ISettingsService settingsService):base(settingsService)
		{
		}

        /// <summary>
        /// Get Contacts from WebApi
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<List<Contact>> GetContacts()
        {
            Errors.Clear();
            if (!DoIHaveInternet())
            {
                Errors.Add(new Core.Models.WebApi.Error() { Title = "No Connection", Message = $"No Network Connection" });
                return null;
            }
            if (!await IsAPIReachableAndRunning())
            {
                Errors.Add(new Core.Models.WebApi.Error() { Title = "No Connection", Message = "Not API Connection" });
                return null;
            }

            try
            {
                var url = Core.Constants.Constants.ApiBaseUrl
                                   .AppendPathSegment($"GetUser");

                var response = await url.WithTimeout(30)
                                             .GetJsonAsync<List<Contact>>();

                return response;
            }
            catch (Exception e)
            {
                if (e.GetType().Equals(typeof(FlurlHttpException)))
                {
                    var httpCall = ((FlurlHttpException)e);

                    var _data = await httpCall.GetResponseJsonAsync<Core.Models.WebApi.Error>();

                    switch (httpCall.Call.Response.ResponseMessage.StatusCode)
                    {
                        default:
                            Errors.Add(new Core.Models.WebApi.Error() { Message = _data?.Message, Title = "Error", Errors = _data?.Errors });
                            return null;
                    }
                }
                else
                {
                    Errors.Add(new Core.Models.WebApi.Error() { Message = e.Message });
                    return null;
                }
            }
        }
    }
}

