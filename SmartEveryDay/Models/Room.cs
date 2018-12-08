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
        public List<IDevice> RoomDevices { get; set; }

        public Room()
        {
            RoomDevices = new List<IDevice>();
        }

        public void AddDevice(IDevice dev)
        {
            RoomDevices.Add(dev);
        }

        public int getSizeOfDeviceList()
        {
            return RoomDevices.Count;
        }

        public void RemoveDevice(IDevice dev)
        {
            throw new NotImplementedException();
        }
    }
}