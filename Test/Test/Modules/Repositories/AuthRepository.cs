using System;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl;
using Test.Core.Models.WebApi;
using Test.Modules.Repositories.Interfaces;

namespace Test.Modules.Repositories
{
	public class AuthRepository : Core.Services.WebApiService, IAuthRepository
    {
		public AuthRepository(Core.Services.Interfaces.ISettingsService settingsService):base(settingsService)
		{
            
        }

        /// <summary>
        /// Login with WebApi
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> Login(string _email = null, string _password = null)
        {
            Errors.Clear();
            if (!DoIHaveInternet())
            {
                Errors.Add(new Core.Models.WebApi.Error() { Title = "No Connection", Message = $"No Network Connection" });
                return false;
            }
            if (!await IsAPIReachableAndRunning())
            {
                Errors.Add(new Core.Models.WebApi.Error() { Title = "No Connection", Message = "Not API Connection" });
                return false;
            }

            try
            {
                var url = string.Empty;
                if(_email == "user.demo@mail.com" && _password == "Password2023*")
                {
                    url = Core.Constants.Constants.ApiBaseUrl
                                   .AppendPathSegment($"UserSignIn");
                }
                else
                {
                    url = Core.Constants.Constants.ApiBaseUrl
                                   .AppendPathSegment($"InvalidUser");
                }

                

                var response = await url.WithTimeout(30)
                                             .PostJsonAsync(new
                                             {
                                                 user_name = _email,
                                                 password = _password
                                             }).ReceiveJson<TokenResponse>();

                if(response.AuthorizationToken != null)
                {
                    await SaveToken(response.AuthorizationToken);
                    return true;
                }
                else
                {
                    Errors.Add(new Error() { Message = response.Status, Title = response.Status });
                    return false;
                }
                    
               
            }
            catch (Exception e)
            {
                if (e.GetType().Equals(typeof(FlurlHttpException)))
                {
                    var httpCall = ((FlurlHttpException)e);

                    var _data = await httpCall.GetResponseJsonAsync<TokenResponse>();

                    ////Crashes.TrackError(e, _properties);

                    switch (httpCall.Call.Response.ResponseMessage.StatusCode)
                    {
                        default:
                            Errors.Add(new Error() { Message = _data?.Status, Title = "Error" });
                            return false;
                    }

                }
                else
                {
                    Errors.Add(new Error() { Message = e.Message });
                    return false;
                }
            }
        }
    }
}

