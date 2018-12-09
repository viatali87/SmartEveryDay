using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartEveryDay.Data
{
    public class RemoniDataAccess
    {
        private IRestClient client;
        private IRestRequest request;
        private IRestResponse response;

        public void ConnectAPI(Method method)
        {
            request = new RestRequest(method);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("accept", "application/json");
            request.AddHeader("accept", "application/x-yaml");
            
        }
        
        public IRestResponse ExecuteClient(string myUrl, string email, string password)
        {
           ConnectAPI(Method.GET);
           client = new RestClient(myUrl) { Authenticator = new HttpBasicAuthenticator(email, password) };
           response = client.Execute(request);
           return response;
        }
    }
}