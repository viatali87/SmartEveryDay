using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEveryDay.Models
{
    public interface IRoom
    {
        Guid RoomId { get; set; }
        string Name { get; set; }
        List<IDevice> RoomDevices { get; set; }

        void AddDevice(IDevice dev);
        void RemoveDevice(IDevice dev);
        int getSizeOfDeviceList();
    }
}
