using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartEveryDay.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        /* public ActionResult Index()
         {
             return View();
         }*/

        private IRestClient client;
        private IRestRequest request;
        private IRestResponse response;

        public void  ConnectRemoniAPI(Method method)
        {

            request = new RestRequest(method);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("accept", "application/json");
            request.AddHeader("accept", "application/x-yaml");
           

        }


        public int getAccountIdbylogin(string emeil, string password)
        {
            ConnectRemoniAPI(Method.GET);

            var myUrl = "https://api.remoni.com/v1/Accounts?orderby=AccountId&top=10000";
            client = new RestClient(myUrl) { Authenticator = new HttpBasicAuthenticator(emeil, password) };
            response = client.Execute(request);
           
            dynamic conv = JsonConvert.DeserializeObject(response.Content);
            int temp = conv[0].AccountId;


            return temp;
        }

        public int getAllUnitsByAccountId (int id, string emeil, string password)
        {



            ConnectRemoniAPI(Method.GET);
            var myUrl = "https://api.remoni.com/v1/Units?orderby=UnitId&top=10000";
            client = new RestClient(myUrl) { Authenticator = new HttpBasicAuthenticator(emeil, password) };
            response = client.Execute(request);

            dynamic conv = JsonConvert.DeserializeObject(response.Content);
            int temp = conv[0].AccountId;

            return temp;

        }


       
      






    }
}