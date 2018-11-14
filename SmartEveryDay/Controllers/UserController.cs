using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartEveryDay.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace SmartEveryDay.Controllers
{
    public class UserController : Controller, IUserController
    {
        public DatabaseAdapter db;

        public UserController()
        {
            db = new Models.DatabaseAdapter();
        }
        [HttpPost]
        public JsonResult sendData(string val)
        {
            if(val == null)
            {
                return Json("Data was null!");
            } else
            {
                Console.Write("In sendData in UserController, going to send the databack");
                var JSONObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(val);
                string country = JSONObj["country"];
                string res = db.saveData(country);
                return Json("Yayay! " + res);
            }

        }

        public User CreateUser(JsonObjectAttribute data)
        {
        Console.Write("In CreateUser in UserController");

        User newUser = new User();
           // db.saveData2(data.country);

            //string temp = db.saveData(data);
            //return Json(temp);
            return newUser; 
        }

        public string DeleteUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public User EditUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public User GetUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        // GET: User
        public ActionResult UserProfile()
        {
            return View();
        }

        public User CreateUser(string userName, string firstName, string lastName, Guid houseId, string phonenumber, string email, bool isAdmin)
        {
            throw new NotImplementedException();
        }
    }
}