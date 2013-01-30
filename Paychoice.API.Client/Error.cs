using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paychoice.API.Client
{
    [JsonObject]
    public class Error
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("param")]
        public string Parameter { get; set; }
    }
}