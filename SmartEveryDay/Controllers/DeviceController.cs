using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartEveryDay.Models;
using SmartEveryDay.Data;
using System.Web.Script.Serialization;

namespace SmartEveryDay.Controllers
{
    public class DeviceController : Controller, IDeviceController
    {
        DatabaseAdapter adapter;

        // GET: Device
        public ActionResult Index()
        {
            return View();
        }

        public DeviceController()
        {
            adapter = DatabaseAdapter.Instance();
        }

        // Returns a Device
        [HttpPost]
        public JsonResult AddNewDevice(string val)
        {
            var JSONObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(val);
            Device dev = getDeviceFromJson(JSONObj);
            return Json(adapter.AddNewDevice(dev));
        }

        private Device getDeviceFromJson(Dictionary<string, string> jasonObj)
        {
            Device dev = new Device();
            dev.DeviceId = jasonObj["deviceid"];
            dev.DeviceName = jasonObj["devicename"];
            dev.DeviceType = int.Parse(jasonObj["devicetype"]);
            dev.Room = jasonObj["room"];
            dev.RoomId = new Guid(jasonObj["roomid"]);
            dev.IsOnline = Convert.ToBoolean(jasonObj["isonline"]);
            return dev;
        }

        // Returns a string confirming if device was disabled or not.
        [HttpPost]
        public JsonResult DisableDevice(string deviceId)
        {
            return Json(adapter.DisableDevice(deviceId));
        }

        // Returns an updated Device
        [HttpPost]
        public JsonResult EditDevice(string val)
        {
            var JSONObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(val);
            Device dev = getDeviceFromJson(JSONObj);
            return Json(adapter.EditDevice(dev));
        }

        // Returns a list of all devices in the database
        [HttpPost]
        public JsonResult GetAllDevices()
        {
            List<Device> list = adapter.GetAllDevices();
            return Json(list);
        }

        // Returns a list of all of the devices in the home, includes what rooms they are in
        [HttpPost]
        public JsonResult GetAllDevicesByHouseId(string val)
        {

            return null;
        }

        // Retuns a list of Rooms and each one has a list of devices
        [HttpPost]
        public JsonResult GetRoomsAndDevicesByHouseId(string val)
        {
            return null;
        }

        // Removes the connection from a home to a certain device
        [HttpPost]
        public JsonResult RemoveDeviceFromHome(string deviceId)
        {
            return Json(adapter.RemoveDeviceFromHome(deviceId));
        }




        public string TurnCurtainsOpen()
        {



            var client = new WebClient();
            var content = client.DownloadString(" https://cloud.arest.io/light_id1/digital/2/0");

            return content;
        }

        public string TurnCurtainsClosed()
        {

            var client = new WebClient();
            var content = client.DownloadString("https://cloud.arest.io/light_id1/digital/2/1");

            return content;
        }

        public string TurnLightOn()
        {

            var client = new WebClient();
            var content = client.DownloadString("https://cloud.arest.io/light_id1/digital/2/0");
            return content;
        }

        public string TurnLightOff()
        {

            var client = new WebClient();
            var content = client.DownloadString("https://cloud.arest.io/light_id1/digital/2/1");

            return content;
        }

    }
}