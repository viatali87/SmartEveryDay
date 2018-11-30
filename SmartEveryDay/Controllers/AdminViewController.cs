using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartEveryDay.Controllers
{
    public class AdminViewController : Controller
    {
        // GET: AdminView
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminAccounts()
        {
            return View("AdminAccounts");
        }

        public ActionResult AdminDevices()
        {
            return View("AdminDevices");
        }

        public ActionResult AdminReports()
        {
            return View("AdminReports");
        }
        public ActionResult AdminScenarios()
        {
            return View("AdminScenarios");
        }
        public ActionResult AdminSupportCases()
        {
            return View("AdminSupportCases");
        }


    }
}