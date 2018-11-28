using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartEveryDay.Models
{
    public class Device : IDevice
    {

        public string DeviceId { get; set; }
        public int StatusId { get; set; }
        public int DeviceType { get; set; }
        public string DeviceName { get; set; }
        public bool IsOnline { get; set; }
        public string Room { get; set; }
        public Guid RoomId { get; set; }

        public Device()
        {
            DeviceId = "";
            StatusId = -1;
            DeviceType = -1;
            DeviceName = "";
            IsOnline = false;
            Room = "";
        }

        public Device(string deviceId, int statusId, int deviceType, string deviceName, bool isOnline)
        {
            DeviceId = deviceId;
            StatusId = statusId;
            DeviceType = deviceType;
            DeviceName = deviceName;
            IsOnline = isOnline;

        }

        public Device(string deviceId, int statusId, int deviceType, string deviceName, bool isOnline, string room, Guid roomId)
        {
            DeviceId = deviceId;
            StatusId = statusId;
            DeviceType = deviceType;
            DeviceName = deviceName;
            IsOnline = isOnline;
            Room = room;
            RoomId = roomId;

        }
    }
}