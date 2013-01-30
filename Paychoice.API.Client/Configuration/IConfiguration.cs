using Paychoice.API.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paychoice.API.Client.Configuration
{
    public interface IConfiguration
    {
        /// <summary>
        /// Gets the services credentials.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        ICredential Credentials { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use the sandbox.
        /// </summary>
        /// <value>
        ///   <c>true</c> if requests are to be sent to the sandbox; otherwise, <c>false</c>.
        /// </value>
        bool UseSandbox { get; set; }

        /// <summary>
        /// Gets the services endpoint URL.
        /// </summary>
        /// <returns></returns>
        string GetEndpoint();
    }
}