using Newtonsoft.Json;
using Remoni.API.DataAccess.Client;
using SmartEveryDay.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Remoni.API.DataAccess
{
    public class RestAPIClient : RemoniAPIClient, IRestAPIClient
    {
        private string password;

        public RestAPIClient(string baseUrl) : base(new Uri(baseUrl))
        {
            this.SerializationSettings.NullValueHandling = NullValueHandling.Include;
        }

        public void SetCredentials(string email, string password)
        {
            this.password = password;
            SetAuthHeader(email, password);
        }

        private void SetAuthHeader(string email, string password)
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
            {
                HttpClient.DefaultRequestHeaders.Authorization = null;
                return;
            }
            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("basic", $"{email}:{password}".Base64Encode());
        }
    }
}
