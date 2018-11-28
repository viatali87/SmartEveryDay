using SmartEveryDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace SmartEveryDay.Controllers
{
    interface IUserController
    {
        JsonResult CreateUser(string userName, string firstName, string lastName, string houseId, string phonenumber, string email, bool isAdmin);
        String DeleteUser(Guid userId);
        User EditUser(Guid userId);
        User GetUser(Guid userId);

    }
}
