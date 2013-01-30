using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paychoice.API.Client
{
    public interface IPaychoiceService
    {
        /// <summary>
        /// Charges the specified credit card token.
        /// </summary>
        /// <param name="cardToken">The card token.</param>
        /// <param name="reference">The reference for the charge.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="amount">The amount.</param>
        /// <exception cref="PaychoiceException">Thrown if an error object was returned from Paychoice</exception>
        Charge Charge(string cardToken, string reference, string currency, decimal amount);

        /// <summary>
        /// Charges the specified credit card.
        /// </summary>
        /// <param name="card">The credit card.</param>
        /// <param name="reference">The reference number for the charge.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="amount">The amount.</param>
        /// <exception cref="PaychoiceException">Thrown if an error object was returned from Paychoice</exception>
        Charge Charge(CreditCard card, string reference, string currency, decimal amount);

        /// <summary>
        /// Gets the stored credit card info for a token.
        /// </summary>
        /// <param name="cardToken">The card token.</param>
        /// <exception cref="PaychoiceException">Thrown if an error object was returned from Paychoice</exception>
        StoredCreditCard GetCard(string cardToken);

        /// <summary>
        /// Gets a charge by it's specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <exception cref="PaychoiceException">Thrown if an error object was returned from Paychoice</exception>
        Charge GetCharge(string id);

        /// <summary>
        /// Gets a list of charges.
        /// </summary>
        /// <exception cref="PaychoiceException">Thrown if an error object was returned from Paychoice</exception>
        IEnumerable<Charge> GetCharges(int skip = 0, int take = 10);

        /// <summary>
        /// Gets the public API key which can be used in paychoice.js
        /// </summary>
        /// <returns>The public API key</returns>
        /// <exception cref="PaychoiceException">Thrown if an error object was returned from Paychoice</exception>
        string GetPublicAPIKey();

        /// <summary>
        /// Stores the specified credit card.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <exception cref="PaychoiceException">Thrown if an error object was returned from Paychoice</exception>
        StoredCreditCard Store(CreditCard card);
    }
}