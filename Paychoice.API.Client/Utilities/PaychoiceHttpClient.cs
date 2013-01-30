using Newtonsoft.Json;
using Paychoice.API.Client.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Web;

namespace Paychoice.API.Client.Utilities
{
    internal class PaychoiceHttpClient
    {
        private readonly IConfiguration config;
        private readonly ILog log;

        internal PaychoiceHttpClient(IConfiguration config, ILog log)
        {
            this.config = config;
            this.log = log;

#if DEBUG
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
#endif
        }

        public PaychoiceResponse Delete(string url, IDictionary<string, string> paramValues = null)
        {
            var body = paramValues != null ? GetFormValues(paramValues) : null;
            var request = GetWebRequest(url, "DELETE", body);
            return Execute(request);
        }

        public PaychoiceResponse Get(string url, IDictionary<string, string> paramValues = null)
        {
            var query = paramValues != null ? GetQuerystring(paramValues) : null;
            var request = GetWebRequest(url + query, "GET");
            return Execute(request);
        }

        public string GetBaseUrl()
        {
            return config.GetEndpoint();
        }

        public PaychoiceResponse Post(string url, IDictionary<string, string> paramValues = null)
        {
            var body = paramValues != null ? GetFormValues(paramValues) : null;
            var request = GetWebRequest(url, "POST", body);
            return Execute(request);
        }

        public PaychoiceResponse Put(string url, IDictionary<string, string> paramValues = null)
        {
            var body = paramValues != null ? GetFormValues(paramValues) : null;
            var request = GetWebRequest(url, "PUT", body);
            return Execute(request);
        }

        private static string GetFormValues(IDictionary<string, string> paramValues)
        {
            StringBuilder formString = new StringBuilder();
            bool firstValue = true;

            foreach (var param in paramValues)
            {
                if (firstValue)
                {
                    firstValue = false;
                }
                else
                {
                    formString.Append("&");
                }

                formString.AppendFormat("{0}={1}", param.Key, param.Value);
            }

            return formString.ToString();
        }

        private static string GetQuerystring(IDictionary<string, string> paramValues)
        {
            StringBuilder queryString = new StringBuilder();
            bool firstValue = true;

            foreach (var param in paramValues)
            {
                if (firstValue)
                {
                    queryString.Append("?");
                    firstValue = false;
                }
                else
                {
                    queryString.Append("&");
                }

                queryString.AppendFormat("{0}={1}", HttpUtility.UrlEncode(param.Key), HttpUtility.UrlEncode(param.Value));
            }

            return queryString.ToString();
        }

        private static string ReadStream(Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        private PaychoiceResponse Execute(WebRequest webRequest)
        {
            var apiResponse = new PaychoiceResponse();

            try
            {
                using (var response = webRequest.GetResponse())
                {
                    apiResponse.RawResponse = ReadStream(response.GetResponseStream());
                    apiResponse.StatusCode = ((HttpWebResponse)response).StatusCode;
                }
            }
            catch (WebException webException)
            {
                if (webException.Response != null)
                {
                    var response = webException.Response;
                    apiResponse.RawResponse = ReadStream(response.GetResponseStream());
                    apiResponse.StatusCode = ((HttpWebResponse)response).StatusCode;
                }
            }

            TryLog(string.Format("{0} {1} {2}", (int)apiResponse.StatusCode, apiResponse.StatusCode, apiResponse.RawResponse));

            return apiResponse;
        }

        private WebRequest GetWebRequest(string url, string method, string body = null)
        {
            string authHeader = config.Credentials.GetAuthorizationHeader();

            var request = (HttpWebRequest)WebRequest.Create(config.GetEndpoint() + url);
            request.Method = method;
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Paychoice.Net Client";

            if (!string.IsNullOrEmpty(authHeader))
            {
                request.Headers.Add("Authorization", authHeader);
            }

            if (!string.IsNullOrEmpty(body))
            {
                TryLog(string.Format("{0} {1} {2}", request.Method, request.RequestUri.ToString(), body));
                byte[] bodyData = Encoding.UTF8.GetBytes(body);
                request.ContentLength = bodyData.Length;

                request.GetRequestStream().Write(bodyData, 0, bodyData.Length);
                request.GetRequestStream().Flush();
                request.GetRequestStream().Close();
            }

            return request;
        }

        private void TryLog(string message)
        {
            if (log != null)
            {
                log.Log(message);
            }
        }
    }
}