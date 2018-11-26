using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SmartEveryDay.Controllers
{
    public class DeviceController : Controller
    {
        // GET: Device
        public ActionResult Index()
        {
            return View();
        }

        public string TurnCurtainsOpen()
        {

            var client = new WebClient();
            var content = client.DownloadString(" https://cloud.arest.io/light_id1/digital/2/0");

            return content;
        }

        public string TurnCurtainsClosed()
        {

            var client = new WebClient();
            var content = client.DownloadString("https://cloud.arest.io/light_id1/digital/2/1");

            return content;
        }

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

    }
}