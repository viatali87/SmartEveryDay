using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartEveryDay.Controllers
{
    public class LogController : Controller, ILogController
    {
        // GET: Log
        public ActionResult Index()
        {
            return View();
        }
    }
}