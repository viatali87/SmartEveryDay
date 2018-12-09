using System;
using System.Web.Mvc;
using SmartEveryDay.Models;
using SmartEveryDay.Data;

namespace SmartEveryDay.Controllers
{
    interface IDeviceController
    {
        JsonResult GetAllDevicesByHouseId(string val);
        JsonResult GetAllDevices();
        JsonResult GetRoomsAndDevicesByHouseId(string val);
        JsonResult GetRoomsAndDevicesByType(string houseId, int type);
        JsonResult GetDevicesInARoomByType(string roomId, int type);
        JsonResult GetDevicesByType(string val);
        int getStatus(string deviceId, string type);
        void updateDeviceStatus(string deviceId, int newStatus);
        JsonResult AddNewDevice(string val);
        JsonResult RemoveDeviceFromHome(string deviceId);
        JsonResult DisableDevice(string deviceId);
        JsonResult EditDevice(string val);


    }
}
