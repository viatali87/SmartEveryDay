using SmartEveryDay.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Remoni.API.DataAccess
{
    public class RemoniApiCreateClient
    {
        private const string ConnectionString = "https://qa.api.remoni.com";
        public Client.RemoniAPIClient CreateRemoniApiClient(string email, string password)
        {
            Client.RemoniAPIClient httpClient = new Client.RemoniAPIClient(new Uri(ConnectionString));
            httpClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", $"{email}:{password}".Base64Encode());
            httpClient.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }
    }
}
