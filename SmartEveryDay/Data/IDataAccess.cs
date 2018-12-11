using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEveryDay.Data
{
    interface IDataAccess
    {
        void ConnectAPI(Method method);
    }
}
