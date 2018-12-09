

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

namespace UnitTestSmartEveryDay
{
    [TestClass]
    public class UserControllerUnitTest
    {
        private UserController controller;
        private Mock<DatabaseAdapter> databaseAdapterMock;

        [TestInitialize]
        public void Initialize()
        {
            controller = new UserController();
            databaseAdapterMock = new Mock<DatabaseAdapter>();

        }

        [TestMethod]
        public void EditUser(string val)
        {

        }
        [TestMethod]
        public void GetUser(string val)
        {
        }
        [TestMethod]
        public void GetAllUsers()
        {
        }

        [TestMethod]
        public void CanCreateUser()
        {
            //Arrange
            User newuser = new User
            {
                Username = "name",
                UserId = new Guid(),
                LastName = "lastname",
                FirstName = "firstname",
                PhoneNo = "1234",
                HouseId = new Guid(),
                Email = "email",
                IsAdmin = true
            };

            //Act
            var result = controller.CreateUser("name", "firstname", "lastname", newuser.HouseId.ToString(), "1234", "email", true) as JsonResult;
            var data = result.Data;
            //Assert
            Assert.IsNotNull(data);
        }

       
        [TestMethod]
        public void DeleteUser()
        {
            //Arrange
            string val = "guid string";

            //Act
            var result = controller.DeleteUser(val) as JsonResult;

            //Assert
            Assert.IsNotNull(result.Data);
        }
        [TestMethod]
        public void GetUserByDeviceId()
        {
            //Arrange
            string val = "device id";
            //Act
            var result = controller.GetUserByDeviceId(val) as JsonResult;
            //Assert
            Assert.IsNotNull(result.Data);
        }
    }
}
