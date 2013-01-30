using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Paychoice.API.Client.Utilities
{
    internal class PaychoiceResponse
    {
        /// <summary>
        /// Gets or sets the raw response.
        /// </summary>
        /// <value>
        /// The raw response.
        /// </value>
        public string RawResponse { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Converts the response to.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <returns></returns>
        /// <exception cref="PaychoiceException"></exception>
        public TMessage ConvertResponseTo<TMessage>()
        {
            if (!string.IsNullOrEmpty(RawResponse))
            {
                return JsonConvert.DeserializeObject<TMessage>(RawResponse);
            }

            throw new PaychoiceException(StatusCode.ToString());
        }
    }
}