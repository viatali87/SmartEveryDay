using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartEveryDay.Controllers
{
    public class ScenarioController : Controller, IScenarioController
    {
        // GET: Scenario
        public ActionResult Index()
        {
            return View();
        }
    }
}