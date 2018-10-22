
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



        public async System.Threading.Tasks.Task<ActionResult> Water()
        {
            ViewBag.Title = "Water";
           // var remoniclient = new RemoniApiCreateClient();
           // var other = remoniclient.CreateRemoniApiClient("nadina77@gmail.com", "NADzuk3412.");
            // var alarms = other.Alarms.GetCollectionAsyncByqueryOptionsWithHttpMessagesAsync().Result;
           // var temp = await other.Accounts.GetCollectionAsyncByqueryOptionsWithHttpMessagesAsync();
          //  var name = temp.Response.StatusCode;
            return View();
        }


        public ActionResult AdminView()
        {
            ViewBag.Title = "AdminView";

            return View();
        }


    }
}
