using Paychoice.API.Client.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paychoice.API.Client.Example
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //Please fill in
            string userName = "{your username}";
            string password = "{your password}";
            bool useSandbox = true;

            IPaychoiceService service = PaychoiceService.CreateForBasicAuth(userName, password, useSandbox, new ConsoleLog());

            ChargeCreditCard(service);
            StoreCreditCard(service);
            StoreCreditCardAndChargeToken(service);
            ListLastTenCharges(service);
            GetPublicAPIKey(service);
        }

        /// <summary>
        /// Charges the credit card.
        /// </summary>
        /// <param name="service">The Paychoice service.</param>
        private static void ChargeCreditCard(IPaychoiceService service)
        {
            CreditCard card = new CreditCard()
            {
                CardName = "John Smith",
                Number = "4111-1111-1111-1111",
                ExpiryMonth = 12,
                ExpiryYear = 16,
                CVV = "123"
            };

            string currency = "AUD";
            decimal amount = 5.00m;
            string reference = "Inv #" + Guid.NewGuid().ToString();

            Console.WriteLine("Charging credit card");
            var charge = service.Charge(card, reference, currency, amount);
            Console.WriteLine("Charge id: {0} {1}", charge.Id, charge.Status);
        }

        /// <summary>
        /// Gets the users public API key.
        /// </summary>
        /// <param name="service">The Paychoice service.</param>
        private static void GetPublicAPIKey(IPaychoiceService service)
        {
            Console.WriteLine("Getting Public API key");
            Console.WriteLine("Public key: {0}", service.GetPublicAPIKey());
        }

        /// <summary>
        /// Lists the last ten charges.
        /// </summary>
        /// <param name="service">The Paychoice service.</param>
        private static void ListLastTenCharges(IPaychoiceService service)
        {
            Console.WriteLine("Getting last 10 charges");
            var charges = service.GetCharges(0, 10);
            foreach (var charge in charges)
            {
                Console.WriteLine("{0} {1} {2} {3}", charge.Id, charge.Status, charge.Reference, charge.Amount);
            }
        }

        /// <summary>
        /// Stores the credit card.
        /// </summary>
        /// <param name="service">The Paychoice service.</param>
        private static void StoreCreditCard(IPaychoiceService service)
        {
            CreditCard card = new CreditCard()
            {
                CardName = "John Smith",
                Number = "4111-1111-1111-1111",
                ExpiryMonth = 12,
                ExpiryYear = 16,
                CVV = "123"
            };

            Console.WriteLine("Storing credit card");
            var storedCard = service.Store(card);
            Console.WriteLine("Token: {0}", storedCard.token);
        }

        /// <summary>
        /// Stores the credit card and charges the cards token.
        /// </summary>
        /// <param name="service">The Paychoice service.</param>
        private static void StoreCreditCardAndChargeToken(IPaychoiceService service)
        {
            CreditCard card = new CreditCard()
            {
                CardName = "John Smith",
                Number = "4111-1111-1111-1111",
                ExpiryMonth = 12,
                ExpiryYear = 16,
                CVV = "123"
            };

            string currency = "AUD";
            decimal amount = 12.00m;
            string reference = "Inv #" + Guid.NewGuid().ToString();

            var storedCard = service.Store(card);

            Console.WriteLine("Charging token");
            var charge = service.Charge(storedCard.token, reference, currency, amount);

            Console.WriteLine("Charge id: {0} {1}", charge.Id, charge.Status);
        }

        private class ConsoleLog : Paychoice.API.Client.Utilities.ILog
        {
            public void Log(string message)
            {
                Console.Out.WriteLine(message);
            }
        }
    }
}