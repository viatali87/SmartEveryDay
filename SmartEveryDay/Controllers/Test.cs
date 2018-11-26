using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartEveryDay.Models;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;


namespace SmartEveryDay.Controllers
{
    public class Test : Controller
    {
        // Creates a request and client and executes them and returns a IRestResponse
        // Same as ConnectRemoniApi from Base class except here we also make the client and execute it
        public IRestResponse CreateClient(string url, string email, string pw)
        {
            IRestRequest request = new RestRequest();
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("accept", "application/json");
            request.AddHeader("accept", "application/x-yaml");
            IRestClient client = new RestClient(url) { Authenticator = new HttpBasicAuthenticator(email, pw) };

            return client.Execute(request);
        }

        // Gets the units that the user wants to display the data in
        // Same as the old GetUnits from Water controller
        public JsonResult getChosenUnitsForUser(string email, string pw)
        {
            IRestResponse res = CreateClient("https://qa.api.remoni.com/v1/Accounts?orderby=AccountId&top=10000", email, pw);
            // Get a list of devices that the customer has
            var watersensors = getAllWaterSensors(email, pw);
            // Send that list back to the view
            return Json(watersensors, JsonRequestBehavior.AllowGet);
         }

        // Returns the water data
        // Same as the old GetDataByUnitId method from the water controller
        [HttpGet]
        public List<WaterModel> getData(string email, string pw)
        {
            List<WaterModel> temp = new List<WaterModel>();
            IRestResponse res = CreateClient("https://api.remoni.com/v1/Data?orderby=Timestamp&Timestamp=ge(2018-08-04)&Timestamp=lt(2018-08-09)&UnitId=eq(1502)&AggregateType=eq(Day)&top=10000", email, pw);
            dynamic conv = JsonConvert.DeserializeObject(res.Content);
            for (int i = 0; i < conv.Count; i++)
            {
                var data = new WaterModel
                {
                    DataType = conv[i].DataType,
                    Temperature = conv[i].Value,
                    timeStamp = conv[i].Timestamp
                };
                temp.Add(data);
            }
            return temp;

        }



        // Gets a list of water sensors that the user has
        // Same as the old GetAllUnitsByAccuntId method in base controller
        public List<WaterModel> getAllWaterSensors(string email, string pw)
        {
            List<WaterModel> temp = new List<WaterModel>();
            IRestResponse res = CreateClient("https://api.remoni.com/v1/Data?orderby=Timestamp&Timestamp=ge(2018-08-04)&Timestamp=lt(2018-08-09)&UnitId=eq(1502)&AggregateType=eq(Day)&top=10000", email, pw);
            dynamic conv = JsonConvert.DeserializeObject(res.Content);
            for (int i = 0; i < conv.Count; i++)
            {
                var data = new WaterModel
                {
                    DataType = conv[i].DataType,
                    Temperature = conv[i].Value,
                    timeStamp = conv[i].Timestamp
                };
                temp.Add(data);
            }
            return temp;

        }
        
    }
}