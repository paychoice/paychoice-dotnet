using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paychoice.API.Client
{
    public class CreditCard
    {
        public string CardName { get; set; }

        public string CVV { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public string Number { get; set; }
    }
}