using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartEveryDay.Controllers
{
    public class RoomController : Controller, IRoomController
    {
        public JsonResult ConnectRoomToHome(Guid roomId, Guid houseId)
        {
            throw new NotImplementedException();
        }

        public JsonResult CreateRoom(Guid houseId, string roomName)
        {
            throw new NotImplementedException();
        }

        public JsonResult DisconnectRoomFromHome(Guid roomId, Guid houseId)
        {
            throw new NotImplementedException();
        }

        public JsonResult EditRoomName(Guid roomId, string newRoomName)
        {
            throw new NotImplementedException();
        }

        // GET: Room
        public ActionResult Index()
        {
            return View();
        }
    }
}