using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SmartEveryDay.Controllers
{

   
    public class LightController : Controller
    {

        private IRestClient client;
        private IRestResponse response;
        private IRestRequest request;
        // GET: Light
        public ActionResult Light()
        {
            return View();
        }

                
           //method that calls the aRest cloud and controll the light ON  
        public string TurnLightOn()
        {
           
            var client = new WebClient();
            var content = client.DownloadString("https://cloud.arest.io/light_id1/digital/2/0");

            return content;
        }

        public string TurnLightOff()
        {

            var client = new WebClient();
            var content = client.DownloadString("https://cloud.arest.io/light_id1/digital/2/1");

            return content;
        }


        //private JsonResult GetLightOn()
        //{

        //    var myUrl = "https://cloud.arest.io/light_id1/digital/2/0";
        //    client = new RestClient(myUrl) { };
        //    response = client.Execute(request);
        //    return Json(response.Content, JsonRequestBehavior.AllowGet);

        //}

    }
}