using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartEveryDay.Models
{
    public class testCanBeDeleted
    {

        /*public List<Room> GetRoomsAndDevicesByHouseId(Guid houseId)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string roomsByHouseId = @"

              SELECT R.room_id, R.room_name
              FROM Rooms AS R
              INNER JOIN
              Rooms_devices AS RD
              ON R.room_id = RD.room_id
              INNER JOIN
              House_devices as HD
              ON HD.device_id = RD.device_id AND HD.house_id = '" + houseId + "' GROUP BY R.room_id, R.room_name";

            //SqlDataReader reader = sendQueryGetResponse(querystring);
            List<Room> RoomList = new List<Room>();

            try
            {
                using (con)
                {
                    // Find all rooms belonging to a home
                    SqlCommand command = new SqlCommand(roomsByHouseId, con);
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Room R = new Room();
                        R.Name = (string)reader["room_name"];
                        R.RoomId = (Guid)reader["room_id"];
                        RoomList.Add(R);
                    }

                    // Get a list of devices for each room
                    foreach (var Room in RoomList)
                    {
                        SqlConnection conn = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

                        // This gets a list of device for a certain roomid from a certain house
                        string oneRoomQuery = "SELECT D.device_id, D.status_id, D.device_type, D.device_name, D.is_online FROM Device AS D INNER JOIN Rooms_devices as RD ON RD.device_id = D.device_id AND RD.room_id = '" + Room.RoomId + "'";
                        using (conn)
                        {
                            SqlCommand comm = new SqlCommand(oneRoomQuery, conn);
                            conn.Open();
                            SqlDataReader rdr = comm.ExecuteReader();

                            while (rdr.Read())
                            {
                                Device d = new Device();
                                d.DeviceId = (string)rdr["device_id"];
                                d.DeviceName = (string)rdr["device_name"];
                                d.StatusId = (int)rdr["status_id"];
                                d.DeviceType = (int)rdr["device_type"];
                                d.IsOnline = (bool)rdr["is_online"];
                                Room.AddDevice(d);
                            }
                        }

                    }
                }
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
            return RoomList;
        }*/
    }
}