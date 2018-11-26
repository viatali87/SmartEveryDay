using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartEveryDay.Models
{
    public class Room : IRoom
    {
        public string name { get; set; }
        public List<Device> roomDevices { get; set; }
    }
}