using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartEveryDay.Models;
using System.Data;
using System.Data.SqlClient;

namespace SmartEveryDay.Data
{
    public interface IDatabaseAdapter
    {
        
        // User
        IUser SaveNewUser(IUser user);
        IUser GetUserById(Guid userId);
        IUser EditUser(IUser user);
        string DeleteUser(Guid val);
        List<IUser> GetAllUsers();
        List<IDevice> GetAllDevices();
        List<IDevice> GetDevicesByHouseId(Guid houseId);
        // Rooms
        List<IRoom> GetRoomsAndDevicesByHouseId(Guid houseId);
        List<IDevice> GetDevicesInARoomByType(Guid roomId, int type);
        List<IDevice> GetTypeOfDevicesByHouseId(Guid houseId, int type);
        // Devices
        IDevice AddNewDevice(IDevice device);
        string RemoveDeviceFromHome(string deviceId);
        string DisableDevice(string deviceId);
        IDevice EditDevice(IDevice updatedDevice);
        int GetStatus(string deviceId, int deviceType);
        string updateDeviceStatus(string deviceId, int newStatusId);
        string addLightRecord(DateTime now, string deviceId, int newStatus);
        string addCurtainsRecords(DateTime now, string deviceId, int newStatus);
        Guid GetUserByDeviceId(string deviceId);
    }
}
