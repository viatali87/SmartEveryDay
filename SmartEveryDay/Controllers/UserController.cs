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
        public DatabaseAdapter db;

        public UserController()
        {
            //db = new Models.DatabaseAdapter();
            db = DatabaseAdapter.Instance();
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
        public JsonResult getAllUsers(string val)
        {
            List<User> userslist = db.getAllUsers();
            foreach (var item in userslist)
            {
                Console.WriteLine(item.ToString());
            }
            return Json("All users: " + new JavaScriptSerializer().Serialize(userslist));

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


        public JsonResult getAllUsers ()
        {
            IEnumerable<User> temp = null;
            DatabaseAdapter adapter = DatabaseAdapter.Instance(); 

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

        [HttpPost]
        public JsonResult CreateUser(string userName, string firstName, string lastName, string houseId, string phonenumber, string email, bool isAdmin)
        {
            User newUser = new User();

            newUser.FirstName = firstName;
            newUser.LastName = lastName;
            newUser.PhoneNo = phonenumber;
            newUser.HouseId = new Guid(houseId);
            newUser.Username = userName;
            newUser.Email = email;
            newUser.IsAdmin = isAdmin;
            Guid id = Guid.NewGuid();
            newUser.UserId = id;

            try
            {
                return Json(db.saveNewUser(newUser));
            }
            catch (System.Exception e)
            {
                throw new System.ArgumentException("Error in JSon request" + e);
            }


        }

    }
}