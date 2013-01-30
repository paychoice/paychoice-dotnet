using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paychoice.API.Client
{
    public class OAuth2Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; internal set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; internal set; }

        [JsonProperty("token")]
        public string TokenType { get; internal set; }
    }
}