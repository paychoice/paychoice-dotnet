using Paychoice.API.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paychoice.API.Client.Configuration
{
    public class OAuth2GetTokenConfiguration : IConfiguration, ICredential
    {
        public ICredential Credentials
        {
            get
            {
                return this;
            }
            set
            { }
        }

        public bool UseSandbox
        {
            get;
            set;
        }

        public string GetAuthorizationHeader()
        {
            return string.Empty;
        }

        public string GetEndpoint()
        {
            return UseSandbox ? Endpoint.Sandbox : Endpoint.Live;
        }
    }
}