using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paychoice.API.Client.Configuration
{
    public class HttpBasicCredential : ICredential
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets the authorization header.
        /// </summary>
        /// <returns></returns>
        public string GetAuthorizationHeader()
        {
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", UserName, Password)));
            return string.Format("Basic {0}", token);
        }
    }
}