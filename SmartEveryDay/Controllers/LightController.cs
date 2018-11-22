using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartEveryDay.Controllers
{
    public class LightController : Controller
    {
        // GET: Light
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public int turnOnLight()
        {

            //https://cloud.arest.io/wasdqe/digital/2/1

            return 1;
        }


    }
}