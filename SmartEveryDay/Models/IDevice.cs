using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEveryDay.Models
{
    interface IDevice
    {
        string DeviceId { get; set; }
        int StatusId { get; set; }
        int DeviceType { get; set; }
        string DeviceName { get; set; }
        bool IsOnline { get; set; }
        string Room { get; set; }
        Guid RoomId { get; set; }
    }
}
