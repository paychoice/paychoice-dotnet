using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paychoice.API.Client.Configuration
{
    public interface ICredential
    {
        /// <summary>
        /// Gets the authorization header.
        /// </summary>
        /// <returns></returns>
        string GetAuthorizationHeader();
    }
}