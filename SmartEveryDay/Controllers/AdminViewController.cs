﻿using System;
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

    }
}