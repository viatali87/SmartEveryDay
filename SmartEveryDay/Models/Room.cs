using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartEveryDay.Models
{
    public class Room : IRoom
    {
        public Guid RoomId { get; set; }
        public string Name { get; set; }
        public List<Device> RoomDevices { get; set; }

        public Room()
        {
            RoomDevices = new List<Device>();
        }

        public void AddDevice(Device dev)
        {
            RoomDevices.Add(dev);
        }

        public int getSizeOfDeviceList()
        {
            return RoomDevices.Count;
        }
    }
}