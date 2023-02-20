using System;
using Newtonsoft.Json;

namespace Test.Core.Models.WebApi.Contacts
{
	public class Contact
	{
        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}

