using System.Collections.Generic;
using System.Web.Mvc;
using SmartEveryDay.Models;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using SmartEveryDay.Data;
using Newtonsoft.Json.Linq;
using System;

namespace SmartEveryDay.Controllers
{
    public class WaterController : Controller
    {
       
        // GET: Water
        public ActionResult Water()
        {
            return View();

        }
       

        public int GetAccountId(string email, string password)
        {
            var myUrl = "https://api.remoni.com/v1/Accounts?orderby=AccountId&top=10000";

            RemoniDataAccess RemoniDataAccess = new RemoniDataAccess();
            IRestResponse response = RemoniDataAccess.ExecuteClient(myUrl, email, password);

            dynamic conv = JsonConvert.DeserializeObject(response.Content);
            int temp = conv[0].AccountId;

            return temp;
        }

        public JsonResult GetDevices(string email, string password)
        {

            List<UnitModel> temp = new List<UnitModel>();
            RemoniDataAccess RemoniDataAccess = new RemoniDataAccess();
            
            var myUrl = "https://api.remoni.com/v1/Units?orderby=UnitId&top=10000";

            IRestResponse response = RemoniDataAccess.ExecuteClient(myUrl, email, password);
            dynamic conv = JsonConvert.DeserializeObject(response.Content);

            for (int i = 0; i < conv.Count; i++)
            {
                var unit = new UnitModel
                {
                    UnitId = conv[i].UnitId,
                    UnitName = conv[i].Name
                };
                temp.Add(unit);
            };


            return Json(temp, JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetWaterValues(int id, string email, string password, string date1, string date2)
        {
            var myUrl = "https://api.remoni.com/v1/Data?orderby=Timestamp&Timestamp=ge(" + date1 + ")&Timestamp = lt("+date2 + ")&UnitId=eq("+ id + ")&AggregateType=eq(Day)&top=10000";

            List <WaterModel> temp = new List<WaterModel>();
            RemoniDataAccess RemoniDataAccess = new RemoniDataAccess();
            IRestResponse response = RemoniDataAccess.ExecuteClient(myUrl, email, password);
            
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