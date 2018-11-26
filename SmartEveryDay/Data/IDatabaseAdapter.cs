﻿using System;
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
        User SaveNewUser(User user);
        User GetUserById(Guid userId);
        User EditUser(User user);
        string DeleteUser(Guid userId);
        List<User> GetAllUsers();
        List<Device> GetAllDevices();
        List<Device> GetDevicesByHouseId(Guid houseId);
        List<List<Device>> GetRoomsAndDevicesByHouseId(Guid houseId);
        Device AddNewDevice(Device device);
        string RemoveDeviceFromHome(string deviceId);
        string DisableDevice(string deviceId);
        Device EditDevice(Device updatedDevice);

    }
}