using System;
using System.Web.Mvc;
using SmartEveryDay.Models;
using SmartEveryDay.Data;

namespace SmartEveryDay.Controllers
{
    interface IDeviceController
    {
        JsonResult GetAllDevicesByHouseId(Guid houseId);
        JsonResult GetAllDevices();
        JsonResult GetRoomsAndDevicesByHouseId(Guid houseId);
        JsonResult AddNewDevice(string val);
        JsonResult RemoveDeviceFromHome(string deviceId);
        JsonResult DisableDevice(string deviceId);
        JsonResult EditDevice(string val);


    }
}
