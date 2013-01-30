using Newtonsoft.Json;
using Paychoice.API.Client.Configuration;
using Paychoice.API.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paychoice.API.Client
{
    public class PaychoiceService : IPaychoiceService
    {
        private static readonly string chargeEndpoint = "api/v3/charge/";
        private static readonly string publicKeyEndpoint = "api/v3/publickey/";
        private static readonly string tokenEndpoint = "api/v3/token/";
        private readonly PaychoiceHttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaychoiceService" /> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="log">The log.</param>
        public PaychoiceService(IConfiguration config, ILog log)
        {
            client = new PaychoiceHttpClient(config, log);
        }

        public static IPaychoiceService CreateForBasicAuth(string userName, string password, bool useSandbox)
        {
            return CreateForBasicAuth(userName, password, useSandbox, null);
        }

        public static IPaychoiceService CreateForBasicAuth(string userName, string password, bool useSandbox, ILog log)
        {
            return new PaychoiceService(PaychoiceConfiguration.CreateForBasicAuth(userName, password, useSandbox), log);
        }

        public static IPaychoiceService CreateForOAuth2(string accessToken, string refreshToken, bool useSandbox)
        {
            return CreateForOAuth2(accessToken, refreshToken, useSandbox, null);
        }

        public static IPaychoiceService CreateForOAuth2(string accessToken, string refreshToken, bool useSandbox, ILog log)
        {
            return new PaychoiceService(PaychoiceConfiguration.CreateForOAuth2(accessToken, refreshToken, useSandbox), log);
        }

        public Charge Charge(string cardToken, string reference, string currency, decimal amount)
        {
            var endpointParams = new Dictionary<string, string>();
            endpointParams.Add("currency", currency);
            endpointParams.Add("amount", amount.ToString());
            endpointParams.Add("reference", reference);
            endpointParams.Add("card_token", cardToken);

            var response = client.Post(chargeEndpoint, endpointParams);
            var message = response.ConvertResponseTo<ApiMessage>();
            if (message.HasError)
            {
                throw new PaychoiceException(message.Error);
            }

            return message.Charge;
        }

        public Charge Charge(CreditCard card, string reference, string currency, decimal amount)
        {
            var endpointParams = new Dictionary<string, string>();
            endpointParams.Add("currency", currency);
            endpointParams.Add("amount", amount.ToString());
            endpointParams.Add("reference", reference);
            endpointParams.Add("card[name]", card.CardName);
            endpointParams.Add("card[number]", card.Number);
            endpointParams.Add("card[expiry_month]", card.ExpiryMonth.ToString());
            endpointParams.Add("card[expiry_year]", card.ExpiryYear.ToString());
            endpointParams.Add("card[cvv]", card.CVV);

            var response = client.Post(chargeEndpoint, endpointParams);
            var message = response.ConvertResponseTo<ApiMessage>();

            if (message.HasError)
            {
                throw new PaychoiceException(message.Error);
            }

            return message.Charge;
        }

        public StoredCreditCard GetCard(string cardToken)
        {
            var response = client.Get(tokenEndpoint + cardToken);
            var message = response.ConvertResponseTo<ApiMessage>();

            if (message.HasError)
            {
                throw new PaychoiceException(message.Error);
            }

            return message.Card;
        }

        /// <summary>
        /// Gets a charge by it's specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>

        public Charge GetCharge(string id)
        {
            var response = client.Get(chargeEndpoint + id);
            var message = response.ConvertResponseTo<ApiMessage>();

            if (message.HasError)
            {
                throw new PaychoiceException(message.Error);
            }

            return message.Charge;
        }

        public IEnumerable<Charge> GetCharges(int pageNumber = 0, int pageItems = 10)
        {
            var endpointParams = new Dictionary<string, string>();
            endpointParams.Add("page[number]", pageNumber.ToString());
            endpointParams.Add("page[items]", pageItems.ToString());

            var response = client.Get(chargeEndpoint, endpointParams);
            var message = response.ConvertResponseTo<ApiMessage>();

            if (message.HasError)
            {
                throw new PaychoiceException(message.Error);
            }

            return message.ChargeList;
        }

        /// <summary>
        /// Gets the public API key which can be used in paychoice.js
        /// </summary>
        /// <returns>
        /// The public API key
        /// </returns>
        /// <exception cref="PaychoiceException"></exception>
        public string GetPublicAPIKey()
        {
            var response = client.Get(publicKeyEndpoint);
            var message = response.ConvertResponseTo<ApiMessage>();

            if (message.HasError)
            {
                throw new PaychoiceException(message.Error);
            }

            return message.PublicKey;
        }

        public StoredCreditCard Store(CreditCard card)
        {
            var endpointParams = new Dictionary<string, string>();
            endpointParams.Add("card[name]", card.CardName);
            endpointParams.Add("card[number]", card.Number);
            endpointParams.Add("card[expiry_month]", card.ExpiryMonth.ToString());
            endpointParams.Add("card[expiry_year]", card.ExpiryYear.ToString());
            endpointParams.Add("card[cvv]", card.CVV);

            var response = client.Post(tokenEndpoint, endpointParams);
            var message = response.ConvertResponseTo<ApiMessage>();

            if (message.HasError)
            {
                throw new PaychoiceException(message.Error);
            }

            return message.Card;
        }
    }
}