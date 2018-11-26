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
        JsonResult GetUser(Guid userId);
        JsonResult GetAllUsers();
        JsonResult EditUser(string val);
        JsonResult DeleteUser(Guid userId);

    }
}
