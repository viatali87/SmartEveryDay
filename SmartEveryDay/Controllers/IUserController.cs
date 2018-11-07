using SmartEveryDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEveryDay.Controllers
{
    interface IUserController
    {
        User CreateUser(string userName, string firstName, string lastName, Guid houseId, string phonenumber, string email, bool isAdmin);
        String DeleteUser(Guid userId);
        User EditUser(Guid userId);
        User GetUser(Guid userId);

    }
}
