using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartEveryDay.Controllers
{
    interface IRoomController
    {
        JsonResult CreateRoom(Guid houseId, string roomName);
        JsonResult EditRoomName(Guid roomId, string newRoomName);
        JsonResult ConnectRoomToHome(Guid roomId, Guid houseId);
        JsonResult DisconnectRoomFromHome(Guid roomId, Guid houseId);
    }
}
