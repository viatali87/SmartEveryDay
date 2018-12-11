using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Concurrent;
using System.Text;
using System;
using System.Web.Mvc;
using SmartEveryDay;
using SmartEveryDay.Controllers;
using Moq;
using SmartEveryDay.Data;
using SmartEveryDay.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace UnitTestSmartEveryDay
{
    [TestClass]  
    public class DeviceControllerUnitTest
    {
        private DeviceController controller;
        private Mock<IDatabaseAdapter> databaseAdapterMock;

        [TestInitialize]
        public void Initialize()
        {
            controller = new DeviceController();
            databaseAdapterMock = new Mock<IDatabaseAdapter>();

        }


        [TestMethod]
        public void GetAllDevices()
        {
            List<IDevice> list = new List<IDevice>
            {
                new Device {DeviceId="device id 1", DeviceName="name", DeviceType= 1, Room= "room", RoomId= new Guid(), IsOnline= true },
                new Device {DeviceId="device id 2", DeviceName="name2", DeviceType= 1, Room= "room", RoomId= new Guid(), IsOnline= true },
            };

            var result = controller.GetAllDevices() as JsonResult;

            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public void GetAllDevicesByHouseId()
        {
            Guid id = new Guid();
           string houseid = id.ToString();

            var result = controller.GetAllDevicesByHouseId(houseid) as JsonResult;

            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public void GetRoomsAndDevicesByHouseId()
        {
            Guid id = new Guid();
            string houseid = id.ToString();

            var result = controller.GetRoomsAndDevicesByHouseId(houseid) as JsonResult;

            Assert.IsNotNull(result.Data);

        }
       
        [TestMethod]
        public void GetRoomsAndDevicesByType()
        {
            Guid id = new Guid();
            string houseid = id.ToString();

            var result = controller.GetRoomsAndDevicesByHouseId(houseid) as JsonResult;

            Assert.IsNotNull(result.Data);
        }

       // [TestMethod]
        public void GetDevicesInARoomByType()
        {

            Guid id = new Guid();
            int type = 1;
            string houseid = id.ToString();

            var result = controller.GetDevicesInARoomByType(houseid, type) as JsonResult;

            Assert.IsNotNull(result.Data);
        }

        //[TestMethod]
        //public void GetDevicesByType()
        //{
        // use jsonconvert and serialize ????
        //    Guid id = new Guid();
        //    string houseid = id.ToString();

        //    var result = controller.GetDevicesByType(houseid) as JsonResult;

        //    Assert.IsNotNull(result.Data);
       // }

        
        [TestMethod]
        public void TurnCurtainsOpen()
        {
            // var client = new WebClient();

            var result = controller.TurnCurtainsOpen() as string;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void TurnCurtainsClosed()
        {
            var result = controller.TurnCurtainsClosed() as string;

            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void TurnLightOn()
        {

            var result = controller.TurnLightOn() as string;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TurnLightOnOrOff()
        {
            string deviceId = "light_id1";
            string type = "1";
            string pin = "2";
            string onOrOff = "4";

            var result = controller.TurnLightOnOrOff(deviceId, type, pin, onOrOff) as string;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void TurnLightOff()
        {
            var result = controller.TurnLightOff() as string;

            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void addRecord()
        {
            
        }
        [TestMethod]
        public void getStatus()
        {
           
        }
        [TestMethod]
        public void updateDeviceStatus()
        {
        }
    }
}
