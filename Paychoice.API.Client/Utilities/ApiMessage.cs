using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paychoice.API.Client.Utilities
{
    internal class ApiMessage
    {
        [JsonProperty("card")]
        public StoredCreditCard Card { get; set; }

        [JsonProperty("charge")]
        public Charge Charge { get; set; }

        [JsonProperty("charge_list")]
        public IEnumerable<Charge> ChargeList { get; set; }

        [JsonProperty("error")]
        public Error Error { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.
        /// </value>
        public bool HasError
        {
            get
            {
                return Error != null;
            }
        }

        [JsonProperty("object_type")]
        public string ObjectType { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }
    }
}