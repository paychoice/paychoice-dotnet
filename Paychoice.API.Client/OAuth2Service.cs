using Newtonsoft.Json;
using Paychoice.API.Client.Configuration;
using Paychoice.API.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paychoice.API.Client
{
    public class OAuth2Service
    {
        private static readonly string tokenEndpoint = "/api/oauth2/token";
        private readonly PaychoiceHttpClient client;
        private readonly string clientId;

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuth2Service" /> class.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        public OAuth2Service(string clientId, bool useSandbox)
            : this(clientId, useSandbox, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaychoiceService" /> class.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="log">The log.</param>
        public OAuth2Service(string clientId, bool useSandbox, ILog log)
        {
            this.clientId = clientId;
            var config = new OAuth2GetTokenConfiguration() { UseSandbox = useSandbox };
            client = new PaychoiceHttpClient(config, log);
        }

        /// <summary>
        /// Gets the access token in exchange for the authorization code.
        /// </summary>
        /// <param name="authorizationCode">The authorization code.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <returns>The OAuth2 access and refresh token</returns>
        /// <exception cref="PaychoiceException"></exception>
        public OAuth2Token GetAccessToken(string authorizationCode, string clientSecret)
        {
            Dictionary<string, string> formValues = new Dictionary<string, string>();

            formValues.Add("client_id", clientId);
            formValues.Add("client_secret", clientSecret);
            formValues.Add("grant_type", "authorization_code");
            formValues.Add("code", authorizationCode);

            var response = client.Post(tokenEndpoint, formValues);
            var message = response.ConvertResponseTo<OAuth2Message>();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new PaychoiceException(message);
            }

            return (OAuth2Token)message;
        }

        /// <summary>
        /// Gets the authorization URL.
        /// </summary>
        /// <returns></returns>
        public string GetAuthorizationUrl()
        {
            return GetAuthorizationUrl(null);
        }

        /// <summary>
        /// Gets the authorization URL.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public string GetAuthorizationUrl(string state)
        {
            return string.Format("{0}api/oauth2/authorize?response_type=code&client_id={1}&scope=read_write&state={2}", client.GetBaseUrl(), clientId, state ?? string.Empty);
        }

        /// <summary>
        /// Refreshes the access token with a new access token in exchange for the refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <returns>The new OAuth2 access and refresh token</returns>
        /// <exception cref="PaychoiceException"></exception>
        public OAuth2Token RefreshAccessToken(string refreshToken, string clientSecret)
        {
            Dictionary<string, string> formValues = new Dictionary<string, string>();

            formValues.Add("client_id", clientId);
            formValues.Add("client_secret", clientSecret);
            formValues.Add("grant_type", "refresh_token");
            formValues.Add("refresh_token", refreshToken);

            var response = client.Post(tokenEndpoint, formValues);
            var message = response.ConvertResponseTo<OAuth2Message>();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new PaychoiceException(message);
            }

            return (OAuth2Token)message;
        }
    }
}