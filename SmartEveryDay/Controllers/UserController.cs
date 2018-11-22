using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartEveryDay.Models;
using SmartEveryDay.Data;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace SmartEveryDay.Controllers
{
    public class UserController : Controller, IUserController
    {
        public Data.DatabaseAdapter db;

        public UserController()
        {
            //db = new Models.DatabaseAdapter();
            db = Data.DatabaseAdapter.Instance();
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
                //string res = db.saveData(country);
                return Json("Yay!"); //Json("Yayay! " + res);
            }

        }

        [HttpPost]
        public JsonResult createUser(string val)
        {
        Console.Write("In CreateUser in UserController");
        var JSONObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(val);
        User newUser = new User();
            newUser.FirstName = JSONObj["firstname"];
            newUser.LastName = JSONObj["lastname"];
            newUser.PhoneNo = JSONObj["phonenumber"];
            newUser.HouseId = new Guid(JSONObj["houseid"]);
            newUser.Username = JSONObj["username"];
            newUser.Email = JSONObj["email"];
            newUser.IsAdmin = Convert.ToBoolean(JSONObj["isadmin"]);
            Guid id = Guid.NewGuid();
            newUser.UserId = id;

            

            // db.saveData2(data.country);

            //string temp = db.saveData(data);
            //return Json(temp);
            return Json("Result: " + new JavaScriptSerializer().Serialize(db.saveNewUser(newUser)));
            //return Json("Result: " + new JavaScriptSerializer().Serialize(db.getAllUsers()));
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

        public JsonResult getAllUsers ()
        {
            IEnumerable<User> temp = null;
            DatabaseAdapter adapter = new DatabaseAdapter();

            try
            {
                temp = adapter.getAllUsers();
            }

            catch (System.Exception e)
            {
                throw new System.ArgumentException("Error in getAllUsers() request  Nadina" + e);
            }

            try
            {
            return Json(temp.ToList(), JsonRequestBehavior.AllowGet);

            }
            catch (System.Exception e)
            {
                throw new System.ArgumentException("Error in JSon request"+ e);
            }

        }
    }
}