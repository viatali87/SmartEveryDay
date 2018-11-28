using SmartEveryDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace SmartEveryDay.Controllers
{
    public interface IUserController
    {
        JsonResult CreateUser(string val);
        JsonResult GetUser(string val);
        JsonResult GetAllUsers();
        JsonResult EditUser(string val);
        JsonResult DeleteUser(string val);

    }
}
