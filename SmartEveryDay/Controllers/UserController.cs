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
        public IDatabaseAdapter db;

        public UserController()
        {
            db = DatabaseAdapter.Instance();
        }


        // GET: User Views
        #region Views
        public ActionResult UserProfile()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index(User user)
        {
            
       
            if (user !=null)
            {
             
                return View("index", User);}

            else
            {
                return View("LogInView");
            }
           
            
        }

        public ActionResult CurtainRooms()
        {
            return View("CurtainRooms");
        }

        public ActionResult LightRooms()
        {
            return View("LightRooms");
        }
        public ActionResult Scenarios()
        {
            return View("Scenarios");
        }
        public ActionResult WaterRooms()
        {
            return View("WaterRooms");
        }
        public ActionResult Reports()
        {
            return View("Reports");
        }
        #endregion

        /// <summary>
        ///  Updates user details in the database
        /// </summary>
        /// <param name="userName">Name of user</param>
        /// <param name="firstName">First name of user</param>
        /// <param name="lastName">Last name of user</param>
        /// <param name="houseId">House id</param>
        /// <param name="phonenumber">Phone number</param>
        /// <param name="email">Email</param>
        /// <param name="isAdmin">Boolean if the user is an admin</param>
        /// <param name="userId">id of the user</param>
        /// <returns>The updated user from database</returns>
        [HttpPost] 
        public JsonResult EditUser(string userName, string firstName, string lastName, string houseId, string phonenumber, string email, bool isAdmin, string userId)
        {
            IDatabaseAdapter adapter = DatabaseAdapter.Instance();
            IUser us = adapter.GetUserById(new Guid(userId));
            
            User updatedUser = new User();
            updatedUser.UserId = us.UserId;
            updatedUser.FirstName = firstName; ;
            updatedUser.LastName = lastName;
            updatedUser.PhoneNo = phonenumber;
            updatedUser.HouseId = new Guid(houseId);
            updatedUser.Username = userName;
            updatedUser.Email = email;
            updatedUser.IsAdmin = isAdmin;

            try
            {
                return Json(adapter.EditUser(updatedUser));
            }
            catch
            {
                return Json("User not edited");
            }
        }

        [HttpPost]
        public JsonResult GetUser(string val)
        {
            IDatabaseAdapter adapter = DatabaseAdapter.Instance();
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
            IEnumerable<IUser> temp = null;
            IDatabaseAdapter adapter = DatabaseAdapter.Instance();
            //adapter = (DatabaseAdapter)adapter;

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
                throw new System.ArgumentException("Error in JSon request" + e);
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

        /// <summary>
        /// To delete a user from the database
        /// </summary>
        /// <param name="val">The Id of the user</param>
        /// <returns>String informing if the user has been deleted successfully</returns>
        [HttpPost]
        public JsonResult DeleteUser(string val)
        {
            string temp;
            IDatabaseAdapter adapter = DatabaseAdapter.Instance();
            try
            {
                /*string id;
                // Remove escape characters if needed - they may be automatically added in
                if (val.Length > 36)
                {
                    id = val.Substring(1, 36);
                }
                else
                {
                    id = val;
                }
                Guid userID = new Guid(id);*/

                // Send request to database adapter
                temp = adapter.DeleteUser(ConvertStringToGuid(val));
            }
            catch
            {
                return Json("User not deleted");
            }
            return Json(temp);

        }

        [HttpPost]
        public JsonResult GetUserByDeviceId(string deviceId)
        {
            IDatabaseAdapter adapter = DatabaseAdapter.Instance();

            try
            {
                return Json(adapter.GetUserByDeviceId(deviceId));
            } catch
            {
                return Json(Guid.Empty);
            }
        }

        private Guid ConvertStringToGuid(string id)
        {
            string temp = "";
            // Remove escape characters if needed - they may be automatically added in
            if (id.Length > 36)
            {
                temp = id.Substring(1, 36);
            }
            else
            {
                temp = id;
            }
            Guid convertedId = new Guid(temp);
            return convertedId;
        }

    }
}