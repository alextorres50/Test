using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Test.Core.Models.WebApi;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Test.Core.Services.Interfaces;

namespace Test.Core.Services
{

    public class UntrustedCertClientFactory : DefaultHttpClientFactory
    {
        public override HttpMessageHandler CreateMessageHandler()
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (a, b, c, d) => true
            };
        }
    }

    public class WebApiService : IWebApiService
    {
        protected readonly ISettingsService _settingsService;
        protected int _numberCallsRefreshToken;
        public WebApiService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            Errors = new List<Error>();
            FlurlHttp.ConfigureClient(Constants.Constants.ApiBaseUrl, cli =>
                    cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());
        }

        /// <summary>
        /// The errors.
        /// </summary>
        public List<Error> Errors;
        /// <summary>
        /// Gets the get errors.
        /// </summary>
        /// <value>The get errors.</value>
        public List<Error> GetErrors
        {

            get => Errors;
        }

        /// <summary>
        /// Does the I Have internet.
        /// </summary>
        /// <returns><c>true</c>, if Have internet was done, <c>false</c> otherwise.</returns>
        public bool DoIHaveInternet()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        /// <summary>
        /// Ises the API reachable and running.
        /// </summary>
        /// <returns>The APIR eachable and running.</returns>
        public async Task<bool> IsAPIReachableAndRunning()
        {
            var connectivity = CrossConnectivity.Current;
            if (!connectivity.IsConnected)
                return false;
            try
            {
                var poco = await "http://www.google.com/".WithTimeout(5).GetAsync();
                var pocoHost = poco.ResponseMessage.RequestMessage.RequestUri.Host;
                return pocoHost == "www.google.com";
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Save the token.
        /// </summary>
        /// <returns>The token.</returns>
        /// <param name="response">Response.</param>
        protected async Task SaveToken(string response)
        {
            await Task.Run(() =>
            {
                _settingsService.SetString<string>("oauthToken", response);
            });
        }
    }
}

