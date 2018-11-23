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

        public void ConnectRemoniAPI(Method method)
        {

            request = new RestRequest(method);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("accept", "application/json");
            request.AddHeader("accept", "application/x-yaml");


        }

        public IRestResponse ExecuteClient (string myUrl, string emeil, string password) { 
                      
        client = new RestClient(myUrl) { Authenticator = new HttpBasicAuthenticator(emeil, password) };
        response = client.Execute(request);

            return response;
        }

    }
}