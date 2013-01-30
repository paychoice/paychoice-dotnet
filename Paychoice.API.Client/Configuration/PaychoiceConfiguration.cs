using Paychoice.API.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paychoice.API.Client.Configuration
{
    public class PaychoiceConfiguration : IConfiguration
    {
        /// <summary>
        /// Gets or sets the credential.
        /// </summary>
        /// <value>
        /// The credential.
        /// </value>
        public ICredential Credentials { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use the sandbox.
        /// </summary>
        /// <value>
        /// <c>true</c> if requests are to be sent to the sandbox; otherwise, <c>false</c>.
        /// </value>
        public bool UseSandbox { get; set; }

        /// <summary>
        /// Creates the configuration for a basic authentication user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="useSandbox">if set to <c>true</c> use the sandbox.</param>
        public static IConfiguration CreateForBasicAuth(string userName, string password, bool useSandbox)
        {
            var config = new PaychoiceConfiguration();

            config.UseSandbox = useSandbox;
            config.Credentials = new HttpBasicCredential()
            {
                UserName = userName,
                Password = password
            };

            return config;
        }

        /// <summary>
        /// Creates the configuration for an OAuth2 user.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="useSandbox">if set to <c>true</c> use the sandbox.</param>
        public static IConfiguration CreateForOAuth2(string accessToken, string refreshToken, bool useSandbox)
        {
            var config = new PaychoiceConfiguration();

            config.UseSandbox = useSandbox;
            config.Credentials = new OAuth2Credential()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return config;
        }

        /// <summary>
        /// Gets the service endpoint.
        /// </summary>
        /// <returns></returns>
        public string GetEndpoint()
        {
            return UseSandbox ? Endpoint.Sandbox : Endpoint.Live;
        }
    }
}