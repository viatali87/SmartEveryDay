
using System;
using System.Web.Mvc;

namespace SmartEveryDay.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult LogInView()
        {
            ViewBag.Title = "LogInView";

            return View();
        }

        public ActionResult Water()
        {
            ViewBag.Title = "Water";
          
            return View();
        }

        public ActionResult Light()
        {
            ViewBag.Title = "Light";
            
            return View();
        }
        public ActionResult Curtains()
        {
            ViewBag.Title = "Curtains";

            return View();
        }
        public ActionResult UserView()
        {
            ViewBag.Title = "UserView";

            return View();
        }

       

       


        // GET: User
        public ActionResult UserProfile()
        {
            return View();
        }


    }
}
