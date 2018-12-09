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

        // Returns an updated Device, takes a device
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

        // Returns a list of all of the devices in the home, includes what rooms they are in, takes a houseid
        [HttpPost]
        public JsonResult GetAllDevicesByHouseId(string val)
        {
            string id = val.Substring(1, 36);
            Guid houseId = new Guid(id);
            return Json(adapter.GetDevicesByHouseId(houseId));
        }

        public JsonResult GetAllDevicesByHouseI1d(string val)
        {
            Guid houseId;
            if (val.Length > 36)
            {
                string id = val.Substring(1, 36);
                houseId = new Guid(id);
            }
            else
            {
                houseId = new Guid(val);
            }
            return Json(adapter.GetDevicesByHouseId(houseId));
        }

        // Retuns a list of Rooms and each one has a list of devices, takes a house id
        [HttpPost]
        public JsonResult GetRoomsAndDevicesByHouseId(string val)
        {
            Guid HouseId;
            if (val.Length > 36)
            {
                string id = val.Substring(1, 36);
                HouseId = new Guid(id);
            } else
            {
                HouseId = new Guid(val);
            }
            return Json(adapter.GetRoomsAndDevicesByHouseId(HouseId));
        }

        // GET ROOMS IN A HOME THAT CONTAIN A SPECIFIC TYPE (1 = LIGHT, 2 = BLINDS, 3 = WATER)
        [HttpPost]
        public JsonResult GetRoomsAndDevicesByType(string houseId, int type)
        {
            Guid Id;
            if (houseId.Length > 36)
            {
                string Hid = houseId.Substring(1, 36);
                Id = new Guid(Hid);
            }
            else
            {
                Id = new Guid(houseId);
            }
            List<Room> newRoomList = new List<Room>();
            List<Room> roomlist = adapter.GetRoomsAndDevicesByHouseId(Id);
            foreach (Room R in roomlist)
            {
                Room temp = new Models.Room();
                temp.Name = R.Name;
                temp.RoomId = R.RoomId;
                foreach (Device dev in R.RoomDevices)
                {
                    if(dev.DeviceType == type)
                    {
                        temp.AddDevice(dev);
                    }
                }
                if(temp.getSizeOfDeviceList() > 0)
                {
                    newRoomList.Add(temp);
                }
            }
            return Json(newRoomList);
        }

        // Get devices of a specific type for a specific room, all lights in room with Id x for example
        [HttpPost]
        public JsonResult GetDevicesInARoomByType(string roomId, int type)
        {
            Guid Id;
            if (roomId.Length > 36)
            {
                string id = roomId.Substring(1, 36);
                Id = new Guid(id);
            }
            else
            {
                Id = new Guid(roomId);
            }
            return Json(adapter.getDevicesInARoomByType(Id, type));

        }

        // Get devices from a home by type, all lights for example, takes a houseid
        [HttpPost]
        public JsonResult GetDevicesByType(string val)
        {
            var JSONObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(val);
            string id = JSONObj["houseid"]; //.Substring(1, 36);
            Guid houseId = new Guid(id);
            int deviceType = Int32.Parse(JSONObj["type"]);

            return Json(adapter.GetTypeOfDevicesByHouseId(houseId, deviceType));

        }

        // Removes the connection from a home to a certain device
        [HttpPost]
        public JsonResult RemoveDeviceFromHome(string deviceId)
        {
            return Json(adapter.RemoveDeviceFromHome(deviceId));
        }

        // methods that controls curtains and puts in open position 
        public string TurnCurtainsOpen()
        {

            var client = new WebClient();
            var  content = client.DownloadString("https://cloud.arest.io/blinds/up?params");
            return content;
        }
        //methods that controls curtains and puts in closed position
        public string TurnCurtainsClosed()
        {
 
            var client = new WebClient();
            var content = client.DownloadString("https://cloud.arest.io/blinds/down?params");
            return content;
        }

        //methods that controls light and puts in ON position
        public string TurnLightOn()
        {
            var client = new WebClient();
            var content = client.DownloadString("https://cloud.arest.io/light_id1/digital/2/0");

            return content;
        }

        public string TurnLightOnOrOff(string deviceId, string type, string pin, string onOrOff)
        {
            int ConvertedStatus;
            int Status = (Int32.Parse(onOrOff));
            int DevType = (Int32.Parse(type));
            // Check to see if you are turning the lights on or off and translate the id's into 1 or 0 which the micorcontroller understands
            if (Status == 3)
            {
                ConvertedStatus = 0;
            }
            else if (Status == 4)
            {
                ConvertedStatus = 1;
            } else {
                throw new ArgumentException(String.Format("{0} is not a valid status", onOrOff),"onOrOff"); ;
            }

            // Assemble the url and send it on
            var client = new WebClient();
            string dlString = "https://cloud.arest.io/" + deviceId + "/digital/" + pin + "/" + ConvertedStatus;
            var content = client.DownloadString(dlString);

            // Extra - extract info if needed
            var res = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(content);
            string id = res["id"];
            var connected = res["connected"];

            // Status in db: 1 = open, 2 = closed, 3 = on, 4 = off. On relay: 0 = on and 1 = off)
            updateDeviceStatus(deviceId, Status);
            addRecord(deviceId, Status, DevType);
            
            return content;
        }

        //methods that controls light and puts in OFF position
        public string TurnLightOff()
        {

            var client = new WebClient();
            var content = client.DownloadString("https://cloud.arest.io/light_id1/digital/2/1");
            return content;
        }

        // Add a record of change of status of a device ( 1 = Light, 2 = Curtains, 3 = Water)
        public string addRecord(string deviceId, int newStatus, int deviceType)
        {
            if(deviceType == 1)
            {
                return adapter.addLightRecord(DateTime.Now, deviceId, newStatus);

            } if(deviceType == 2)
            {
                return adapter.addCurtainsRecords(DateTime.Now, deviceId, newStatus);

            } if(deviceType == 3)
            {
                throw new NotImplementedException();
            }
            return null;
        }

        // Gets the newest status of a device (type: 1 = Light, 2 = Curtains, 3 = Water. Status: 1 = open, 2 = closed, 3 = on, 4 = off)
        public int getStatus(string deviceId, string type)
        {
            int t = Int32.Parse(type);
            return adapter.GetStatus(deviceId, t);
        }

        public void updateDeviceStatus(string deviceId, int newStatus)
        {
            adapter.updateDeviceStatus(deviceId, newStatus);
        }

    }
}