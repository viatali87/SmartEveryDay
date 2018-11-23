using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartEveryDay.Models;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using SmartEveryDay.Data;

namespace SmartEveryDay.Controllers
{
    public class WaterController : BaseController
    {
        private IRestClient client;
        private IRestRequest request;
        private IRestResponse response;

        // GET: Water
        public ActionResult Water()
        {
            return View();

        }
        //this method should call RemoniDataAccess ExecuteClient - but I got errors.
        public JsonResult getUnits(string emeil, string password)
        {
            List<UnitModel> temp = new List<UnitModel>();

            var myUrl = "https://qa.api.remoni.com/v1/Accounts?orderby=AccountId&top=10000";

          //  RemoniDataAccess response =  ExecuteClient(myUrl, emeil, password);
           // dynamic conv = JsonConvert.DeserializeObject(response.Content);

/*
            for (int i = 0; i < conv.Count; i++)
            {
                var unit = new UnitModel
                {
                    UnitId = conv[i].UnitId,
                    UnitName = conv[i].Name
                };
                temp.Add(unit);
            };*/

            
             return Json(temp, JsonRequestBehavior.AllowGet);

        }






        private JsonResult GetResult()
        {
            this.ConnectRemoniAPI(Method.GET);
            var myUrl = "https://qa.api.remoni.com/v1/Accounts?orderby=AccountId&top=10000";
            client = new RestClient(myUrl) { Authenticator = new HttpBasicAuthenticator("nadina77@gmail.com", "NADzuk3412.") };
            response = client.Execute(request);
            return Json(response.Content, JsonRequestBehavior.AllowGet);

        }



        [HttpGet]
        public JsonResult getData()
        {

            /*var model = new Models.WaterModel();
            var result = GetResult();
            dynamic conv = JsonConvert.DeserializeObject(result.Data.ToString());
            string temp = conv[0].Address;
            return Json(temp, JsonRequestBehavior.AllowGet);*/

            request = new RestRequest();
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("accept", "application/json");
            request.AddHeader("accept", "application/x-yaml");
            var myUrl = "https://qa.api.remoni.com/v1/Accounts?orderby=AccountId&top=10000";
            client = new RestClient(myUrl) { Authenticator = new HttpBasicAuthenticator("nadina77@gmail.com", "NADzuk3412.") };
            response = client.Execute(request);

            int id = getAccountIdbylogin("nadina77@gmail.com", "NADzuk3412.");

            getAllUnitsByAccountId(id, "nadina77@gmail.com", "NADzuk3412.");

            return Json(response.Content, JsonRequestBehavior.AllowGet);


        }

        public JsonResult getUnits()
        {
            request = new RestRequest();
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("accept", "application/json");
            request.AddHeader("accept", "application/x-yaml");
            var myUrl = "https://qa.api.remoni.com/v1/Accounts?orderby=AccountId&top=10000";
            client = new RestClient(myUrl) { Authenticator = new HttpBasicAuthenticator("nadina77@gmail.com", "NADzuk3412.") };

            int id = getAccountIdbylogin("nadina77@gmail.com", "NADzuk3412.");
            var temp = getAllUnitsByAccountId(id, "nadina77@gmail.com", "NADzuk3412.");

            return Json(temp, JsonRequestBehavior.AllowGet);

        }

        public JsonResult getDataByUnitid(int id)
        {

            List<WaterModel> temp = new List<WaterModel>();
            var emeil = "Nadina77@gmail.com";
            var password = "NADzuk3412.";
            request = new RestRequest();
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("accept", "application/json");
            request.AddHeader("accept", "application/x-yaml");
            //still hardcoded value of id -1502, and hardcoded dates
            var myUrl = "https://api.remoni.com/v1/Data?orderby=Timestamp&Timestamp=ge(2018-08-04)&Timestamp=lt(2018-08-04)&UnitId=eq(1502)&AggregateType=eq(Day)&top=10000";
            client = new RestClient(myUrl) { Authenticator = new HttpBasicAuthenticator(emeil, password) };

            response = client.Execute(request);

            dynamic conv = JsonConvert.DeserializeObject(response.Content);
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
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        // GET: Water/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Water/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Water/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Water/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Water/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Water/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Water/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}