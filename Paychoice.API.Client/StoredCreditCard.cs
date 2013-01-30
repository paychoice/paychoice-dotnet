using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paychoice.API.Client
{
    public class StoredCreditCard
    {
        [JsonProperty("card_type")]
        public string card_type { get; set; }

        [JsonProperty("card_name")]
        public string CardName { get; set; }

        [JsonProperty("expiry_month")]
        public string ExpiryMonth { get; set; }

        [JsonProperty("expiry_year")]
        public string ExpiryYear { get; set; }

        [JsonProperty("masked_number")]
        public string Number { get; set; }

        [JsonProperty("token")]
        public string token { get; set; }
    }
}