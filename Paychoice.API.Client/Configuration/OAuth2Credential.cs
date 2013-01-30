using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paychoice.API.Client.Configuration
{
    public class OAuth2Credential : ICredential
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        /// <value>
        /// The refresh token.
        /// </value>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets the authorization header.
        /// </summary>
        /// <returns></returns>
        public string GetAuthorizationHeader()
        {
            return string.Format("Bearer {0}", AccessToken);
        }
    }
}