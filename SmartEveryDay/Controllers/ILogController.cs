using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartEveryDay.Controllers
{
    interface ILogController
    {
        JsonResult LogIn(string val);
        JsonResult LogOut(string val);

    }
}
