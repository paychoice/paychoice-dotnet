using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paychoice.API.Client
{
    public class Charge
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        public ChargeStatus Status
        {
            get
            {
                return (ChargeStatus)Enum.ToObject(typeof(ChargeStatus), int.Parse(StatusCode));
            }
        }

        [JsonProperty("status_code")]
        public string StatusCode { get; set; }
    }
}