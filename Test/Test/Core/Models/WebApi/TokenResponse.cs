using System;
using Newtonsoft.Json;
namespace Test.Core.Models.WebApi
{
	public class TokenResponse
	{
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("authorizationToken")]
        public string AuthorizationToken { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

    }
}

