using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace SmartEveryDay.Controllers
{
    public class CurtainsController : Controller
    {
        // GET: Curtains
        public ActionResult Index()
        {
            return View();

        }

        //public ActionResult Curtains()
        //{
        //    return View();
        //  }

        public string TurnCurtainsOpen()
        {

            var client = new WebClient();
            var content = client.DownloadString("https://cloud.arest.io/light_id1/digital/2/0");
            return content;
        }

        public string TurnCurtainsClosed()
        {

            var client = new WebClient();
            var content = client.DownloadString("https://cloud.arest.io/light_id1/digital/2/1");

            return content;
        }
    }
}