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
            db = DatabaseAdapter.Instance();
        }


        // GET: User
        public ActionResult UserProfile()
        {
            return View();
        }

        [HttpPost]
        public JsonResult getAllUsers(string val)
        {
            string temp;
            DatabaseAdapter adapter = DatabaseAdapter.Instance();
            try
            {
                // Remove escape characters that are automatically added in
                string query = val.Substring(1, 36);
                // Send request to database adapter
                temp = adapter.DeleteUser(query);
            } catch
            {
                return Json("User not deleted");
            }
            return Json(temp);
        }

        [HttpPost]
        public JsonResult EditUser(string val)
        {
            DatabaseAdapter adapter = DatabaseAdapter.Instance();

            var JSONObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(val);
            User updatedUser = new User();
            updatedUser.FirstName = JSONObj["firstname"];
            updatedUser.LastName = JSONObj["lastname"];
            updatedUser.PhoneNo = JSONObj["phonenumber"];
            updatedUser.HouseId = new Guid(JSONObj["houseid"]);
            updatedUser.Username = JSONObj["username"];
            updatedUser.Email = JSONObj["email"];
            updatedUser.IsAdmin = Convert.ToBoolean(JSONObj["isadmin"]);
            Guid id = Guid.NewGuid();
            updatedUser.UserId = id;

            try
            {
                return Json(adapter.EditUser(updatedUser));
            } catch
            {
                return Json("User not edited");
            }
        }

        [HttpPost]
        public JsonResult GetUser(string val)
        {
            DatabaseAdapter adapter = DatabaseAdapter.Instance();
            try
            {
                return Json(adapter.GetUserById(new Guid(val)));
            }
            catch (System.Exception e)
            {
                throw new System.ArgumentException("Error, can not get user. " + e);
            }
        }

        
        public JsonResult GetAllUsers ()
        {
            IEnumerable<User> temp = null;
            DatabaseAdapter adapter = DatabaseAdapter.Instance(); 

            try
            {
                temp = adapter.GetAllUsers();
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
                return Json(db.SaveNewUser(newUser));
            }
            catch (System.Exception e)
            {
                throw new System.ArgumentException("Error in JSon request" + e);
            }


        }

        public JsonResult DeleteUser(string val)
        {
            throw new NotImplementedException();
        }
    }
}