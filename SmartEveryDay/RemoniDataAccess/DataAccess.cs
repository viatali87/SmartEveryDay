using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Web.Http.Results;
using System.Web.Mvc;
using SmartEveryDay.Models;
using SmartEveryDay.Controllers;

namespace SmartEveryDay.RemoniDataAccess
{
    public class DataAccess
    {
       /* private IRestClient client;
        private IRestRequest request;
        private IRestResponse response;

       
        private JsonResult GetResult() 
        {
            request = new RestRequest();
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("accept", "application/json");
            request.AddHeader("accept", "application/x-yaml");
            var myUrl = "https://qa.api.remoni.com/v1/Accounts?orderby=AccountId&top=10000";
            client = new RestClient(myUrl) { Authenticator = new HttpBasicAuthenticator("nadina77@gmail.com", "NADzuk3412.") };
            response = client.Execute(request);
            return Json(response.Content, JsonRequestBehavior.AllowGet);
            //return Json("Vitaly+ LEA", JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public Array getData()
        {
            
            var model = new Models.WaterModel();
            var result = GetResult();
            dynamic conv = JsonConvert.DeserializeObject(result.Data.ToString());
           
            return conv;
        }
        */
    }
}