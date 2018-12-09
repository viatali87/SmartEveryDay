using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using SmartEveryDay.Models;
using System.Text;

namespace SmartEveryDay.Data
{
    public class DatabaseAdapter : IDatabaseAdapter
    {
        private static IDatabaseAdapter _instance { get; set;  }

        public static IDatabaseAdapter Instance()
        {
            if(_instance == null)
            {
                _instance = new DatabaseAdapter();
            } 
            return _instance;
        }

        public IUser SaveNewUser(IUser user)
        {
            try
            {
                string attempt = SendQueryNoResponse("INSERT INTO Users(Users_id, username, real_first_name, real_surname, house_id, phonenumber, email, isAdmin) VALUES('" + user.UserId + "','" + user.Username + "','" + user.FirstName + "','" + user.LastName + "', '" + user.HouseId + "','" + user.PhoneNo + "', '" + user.Email + "', '" + user.IsAdmin + "')");
                return user;

            }
            catch (System.Exception e)
            {
                throw new System.ArgumentException("Error in dbAdapter " + e);
                
            }
        }


        /// <summary>
        /// Takes a user and saves a new row in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A user</returns>
        public IUser SaveNewUserSecure(IUser user)  // WORKS but the if statement is untested!
        {
            /*Guid userid = user.UserId;
            string username = user.Username;
            string firstname = user.FirstName;
            string lastname = user.LastName;
            Guid houseid = user.HouseId;
            string phonenr = user.PhoneNo;
            string email = user.Email;
            bool isadmin = user.IsAdmin;*/
            string sql = @"INSERT INTO Users(Users_id, username, real_first_name, real_surname, house_id, phonenumber, email, isAdmin)
                VALUES(@userid, @username, @firstname, @lastname, @houseid, @phonenr, @email, @isadmin)";

            //string attempt = sendQueryNoResponse(sql);
            //var cmd = new SqlCommand(sql, con);
            List<SqlParameter> list = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@userid", SqlDbType = SqlDbType.UniqueIdentifier, Value = user.UserId },
                new SqlParameter() { ParameterName = "@username", SqlDbType = SqlDbType.VarChar, Value = user.Username },
                new SqlParameter() { ParameterName = "@firstname", SqlDbType = SqlDbType.VarChar, Value = user.FirstName },
                new SqlParameter() { ParameterName = "@lastname", SqlDbType = SqlDbType.VarChar, Value = user.LastName },
                new SqlParameter() { ParameterName = "@houseid", SqlDbType = SqlDbType.UniqueIdentifier, Value = user.HouseId },
                new SqlParameter() { ParameterName = "@phonenr", SqlDbType = SqlDbType.VarChar, Value = user.PhoneNo },
                new SqlParameter() { ParameterName = "@email", SqlDbType = SqlDbType.VarChar, Value = user.Email },
                new SqlParameter() { ParameterName = "@isadmin", SqlDbType = SqlDbType.Bit, Value = user.IsAdmin }

            };
            SendQueryNoResponse(sql);
            //sendQueryNoResponse(sql);
            /*
            cmd.Parameters.Add("@userid", user.UserId);
            cmd.Parameters.Add("@username", user.Username);
            cmd.Parameters.Add("@firstname", user.FirstName);
            cmd.Parameters.Add("@lastname", user.LastName);
            cmd.Parameters.Add("@houseid", user.HouseId);
            cmd.Parameters.Add("@phonenr", user.PhoneNo);
            cmd.Parameters.Add("@email", user.Email);
            cmd.Parameters.Add("@isadmin", user.IsAdmin);*/

                //con.Open();
                /*SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;*/
                //cmd.ExecuteNonQuery();
                //return "Query processed successfully";
        
                //con.Close();
            
            return GetUserById(user.UserId);
        }

        public string DeleteUser(Guid userId)
        {
            string attempt = SendQueryNoResponse("DELETE Users where Users_id = '" + userId + "'");
            return "User deleted";
        }

        public IUser EditUser(IUser user)
        {
            try
            {
                string querystring = "UPDATE Users SET username = '" + user.Username + "', real_first_name = '" + user.FirstName + "', real_surname = '" + user.LastName + "', house_id = '" + user.HouseId + "', phonenumber = '" + user.PhoneNo + "', email = '" + user.Email + "', isAdmin = '" + user.IsAdmin + "' WHERE Users_id = '" + user.UserId + "'";
                string attempta = SendQueryNoResponse(querystring);
                return GetUserById(user.UserId);

            }
            catch (System.Exception e)
            {
                throw new System.ArgumentException("Error in dbAdapter " + e);

            }
        }

        public Guid GetUserByDeviceId(string deviceId)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string querystring = "SELECT Users_id, username, ROW_NUMBER() OVER (ORDER BY username) AS SNO FROM Users AS U INNER JOIN House_devices AS H ON H.device_id = '" + deviceId + "' WHERE H.house_id = U.house_id ORDER BY SNO DESC";

            try
            {
                SqlCommand command = new SqlCommand(querystring, con);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                Guid usersId = Guid.Empty;

                while (reader.Read())
                {
                    usersId = (Guid)reader["users_id"];
                }

                return usersId;
            }
            catch
            {
                throw new KeyNotFoundException();
            }
            finally
            {
                con.Close();
            }
        }

        public IDevice AddNewDevice(IDevice device)
        {
            throw new NotImplementedException();
        }

        public string RemoveDeviceFromHome(string deviceId)
        {
            throw new NotImplementedException();
        }

        public string DisableDevice(string deviceId)
        {
            throw new NotImplementedException();
        }

        public IDevice EditDevice(IDevice updatedDevice)
        {
            throw new NotImplementedException();
        }

        // Adds a record of status change in lights for a device
        public string addLightRecord( DateTime dt, string deviceId, int newStatusId)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string id = deviceId.ToString();
            string declaration = " DECLARE @date DATETIME, @time time SET @date='" + dt.Year + "-" + dt.Month + "-" + dt.Day + "' SET @time='" + dt.Hour + ":" + dt.Minute + ":" + dt.Second + "'SET @date=@date+CAST(@time AS DATETIME) SELECT @date AS DATETIME"; 
            string insertquery = " INSERT INTO Light_records(device_id, device_status_id, date_time) VALUES('" + id + "', " + newStatusId + ", @date)";
            string finalQuery = declaration + insertquery;
            return SendQueryNoResponse(finalQuery);
            

        }

        // Adds a record of status change in curtains for a device
        public string addCurtainsRecords(DateTime dt, string deviceId, int newStatusId)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");
            string id = deviceId.ToString();
            string declaration = " DECLARE @date DATETIME, @time time SET @date='" + dt.Year + "-" + dt.Month + "-" + dt.Day + "' SET @time='" + dt.Hour + ":" + dt.Minute + ":" + dt.Second + "'SET @date=@date+CAST(@time AS DATETIME) SELECT @date AS DATETIME";
            string insertquery = " INSERT INTO Curtain_records(device_id, device_status_id, date_time) VALUES('" + id + "', " + newStatusId + ", @date)";
            string finalQuery = declaration + insertquery;
            return SendQueryNoResponse(finalQuery);
        }

        // Adds a record of status change in curtains for a device
        public string updateDeviceStatus(string deviceId, int newStatusId)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");
            string Id = deviceId.ToString();
            string Query = "UPDATE Device SET Device.status_id = '" + newStatusId + "' WHERE Device.device_id = '" + deviceId + "'";
            string FinalQuery = Query;
            return SendQueryNoResponse(FinalQuery);
        }


        // Gets the newest status of a device. Returns an int (1 = open, 2 = closed, 3 = on, 4 = off)
        public int GetStatus(string deviceId, int deviceType)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");
            string Id = deviceId.ToString();
            string Table;
            int DeviceStatus = -1;
            string querystring = "";
            if (deviceType == 1) // If light
            {
                Table = "Light_records";
            } 
            else if(deviceType == 2) // If curtains
            {
                Table = "Curtain_records";
            }
            else // If water
            {
                Table = "Water_records";
            }
            querystring = "SELECT TOP 1 L.device_status_id, L.date_time, S.name_of_status, S.code_of_status  FROM " + Table + " AS L INNER JOIN Statuses AS S ON L.device_status_id = S.status_id WHERE L.device_id = '" + deviceId + "' ORDER BY L.date_time DESC";

            try
            {
                using (con)
                {
                    SqlCommand command = new SqlCommand(querystring, con);
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        DeviceStatus = (int)reader["device_status_id"];
                    }
                    return DeviceStatus;
                }
            }
            catch
            {
                return -1;
            }
            finally
            {
                con.Close();
            }
        }

        /*public List<List<Device>> GetRoomsAndDevicesByHouseId(Guid houseId)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string id = houseId.ToString();
            string querystring = @"SELECT D.device_id, D.status_id, D.device_type, D.device_name, D.is_online, R.room_id, R.room_name 
                                  FROM Device AS D
                                  INNER JOIN House_devices as HD
                                  ON HD.device_id = D.device_id
                                  INNER JOIN Rooms_devices AS RD
                                  ON RD.device_id = D.device_id
                                  INNER JOIN Rooms AS R
                                  ON R.room_id = RD.room_id
                                  WHERE HD.house_id = '" + id + "'";
            
            List<Room> roomlist = new List<Room>();

            try
            {
                using (con)
                {
                    SqlCommand command = new SqlCommand(querystring, con);
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    List<Device> devlist = new List<Device>();
                    while (reader.Read())
                    {
                        Device dev = new Device();
                        dev.DeviceId = (string)reader["device_id"];
                        dev.StatusId = (int)reader["status_id"];
                        dev.DeviceType = (int)reader["device_type"];
                        dev.DeviceName = (string)reader["device_name"];
                        dev.IsOnline = (bool)reader["is_online"];
                        dev.Room = (string)reader["room_name"];
                        dev.RoomId = (Guid)reader["room_id"];
                        devlist.Add(dev);
                    }
                }
                
                // Make a room and add it to the room list
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
            return null;
        }*/

        private string SendQueryNoResponse(List<SqlParameter> list)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandText = query;
                cmd.Parameters.AddRange(list.ToArray());
                cmd.ExecuteNonQuery();
                return "Query processed successfully";
            }
            catch
            {
                return "Error processing request in dbadapter";
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Gets all information about a user using the users Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A user</returns>
        public IUser GetUserById(Guid userId)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string id = userId.ToString();
            string querystring = "SELECT Users_id, Users.username, Users.real_first_name, Users.real_surname, Users.house_id, Users.email, Users.phonenumber, Users.isAdmin FROM Users WHERE Users.Users_id = '" + id + "'";
            //SqlDataReader reader = sendQueryGetResponse(querystring);

            IUser us = new User();

            try
            {
                using (con)
                {
                    SqlCommand command = new SqlCommand(querystring, con);
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        us.UserId = (Guid)reader["users_id"];
                        us.Username = (string)reader["username"];
                        us.FirstName = (string)reader["real_first_name"];
                        us.LastName = (string)reader["real_surname"];
                        us.HouseId = (Guid)reader["house_id"];
                        us.PhoneNo = (string)reader["phonenumber"];
                        us.Email = (string)reader["email"];
                        us.IsAdmin = (bool)reader["isAdmin"];
                    }
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
            return us;
        }

        /// <summary>
        /// Gets all information about a user using the users Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A user</returns>

        public IUser GetUserByIdSecure(Guid userId)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string id = userId.ToString();
            string querystring = 
                @"SELECT Users_id, Users.username, Users.real_first_name, Users.real_surname, Users.house_id, Users.email, Users.phonenumber, Users.isAdmin 
                FROM Users 
                WHERE Users.Users_id = @id";
            //SqlDataReader reader = sendQueryGetResponse(querystring);

            IUser us = new User();

            try
            {
                using (con)
                {
                    SqlCommand command = new SqlCommand(querystring, con);
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        us.UserId = (Guid)reader["users_id"];
                        us.Username = (string)reader["username"];
                        us.FirstName = (string)reader["real_first_name"];
                        us.LastName = (string)reader["real_surname"];
                        us.HouseId = (Guid)reader["house_id"];
                        us.PhoneNo = (string)reader["phonenumber"];
                        us.Email = (string)reader["email"];
                        us.IsAdmin = (bool)reader["isAdmin"];
                    }
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
            return us;
        }

        /// <summary>
        /// Get a list of all users
        /// </summary>
        /// <returns>List of Users</returns>
        public List<IUser> GetAllUsers()
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string querystring = "SELECT Users_id, Users.username, Users.real_first_name, Users.real_surname, Users.house_id, Users.email, Users.phonenumber,Users.isAdmin FROM Users";
            
            List<IUser> userlist = new List<IUser>();
            try
            {

                using (con)
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (SqlCommand command = new SqlCommand(querystring, con))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        

                            while (reader.Read())
                            {
                                IUser us = new User();
                                us.UserId = (Guid)reader["users_id"];
                                us.Username = (string)reader["username"];
                                us.FirstName = (string)reader["real_first_name"];
                                us.LastName = (string)reader["real_surname"];
                                us.PhoneNo = (string)reader["phonenumber"];
                                us.Email = (string)reader["email"];
                                us.IsAdmin = (bool)reader["isAdmin"];
                                if(us.IsAdmin)
                                {
                                    // Skip saving house id
                                } else
                                {
                                    us.HouseId = (Guid)reader["house_id"];
                                }
                                userlist.Add(us);
                            }
                    }
                 }
             } catch (SqlException e)
            {
                StringBuilder errorMessage = new StringBuilder();
                for (int i = 0; i < e.Errors.Count; i++)
                {
                    errorMessage.Append("Index #" + i + "\n" +
                        "Message: " + e.Errors[i].Message + "\n" +
                        "LineNumber: " + e.Errors[i].LineNumber + "\n" +
                        "Source: " + e.Errors[i].Source + "\n" +
                        "Procedure: " + e.Errors[i].Procedure + "\n");
                }
                Console.WriteLine(errorMessage.ToString());
            } finally
            {
                con.Close();
            }
            return userlist;
        }

        /// <summary>
        /// Get all devices associated with a certain house id
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns>A list of devices</returns>
        public List<IDevice> GetDevicesByHouseId(Guid houseId)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string id = houseId.ToString();
            string querystring = "SELECT D.device_id, D.status_id, D.device_type, D.device_name, D.is_online FROM Device AS D INNER JOIN House_devices AS HD ON D.device_id = HD.device_id WHERE HD.house_id = '" + id +"'";
            //SqlDataReader reader = sendQueryGetResponse(querystring);
            List<IDevice> deviceList = new List<IDevice>();

            try
            {
                using (con)
                {
                    SqlCommand command = new SqlCommand(querystring, con);
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        IDevice dev = new Device();
                        dev.DeviceId = (string)reader["device_id"];
                        dev.StatusId = (int)reader["status_id"];
                        dev.DeviceType = (int)reader["device_type"];
                        dev.DeviceName = (string)reader["device_name"];
                        dev.IsOnline = (bool)reader["is_online"];

                        deviceList.Add(dev);
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

            return deviceList;
        }

        /// <summary>
        /// Get all devices from all users
        /// </summary>
        /// <returns>A list of devices</returns>
        public List<IDevice> GetAllDevices()
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string querystring = "SELECT device_id, status_id, device_type, device_name, is_online FROM Device";
            //SqlDataReader reader = sendQueryGetResponse(querystring);
            List<IDevice> deviceList = new List<IDevice>();

            try
            {
                using (con)
                {
                    SqlCommand command = new SqlCommand(querystring, con);
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        IDevice dev = new Device();
                        dev.DeviceId = (string)reader["device_id"];
                        dev.StatusId = (int)reader["status_id"];
                        dev.DeviceType = (int)reader["device_type"];
                        dev.DeviceName = (string)reader["device_name"];
                        dev.IsOnline = (bool)reader["is_online"];

                        deviceList.Add(dev);
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
            return deviceList;

        }

        /// <summary>
        /// Get a table containing device id, status id, device type, device name for devices 
        /// for a certain home and what rooms they are in showing room id and room name.
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns>A list of devices</returns>
        public List<IRoom> GetRoomsAndDevicesByHouseId(Guid houseId)
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
            List<IRoom> RoomList = new List<IRoom>();

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
                        IRoom R = new Room();
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
                                IDevice d = new Device();
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
        }

        // Get devices froma  home by type, all lights for example
        public List<IDevice> GetTypeOfDevicesByHouseId(Guid houseId, int type)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string deviceByType = "  SELECT D.device_id, D.status_id, D.device_type, D.device_name, D.is_online FROM Device AS D INNER JOIN House_devices AS H ON D.device_id = H.device_id AND H.house_id = '" + houseId + "' WHERE D.device_type = " + type;

            try
            {
                SqlCommand command = new SqlCommand(deviceByType, con);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<IDevice> devlist = new List<IDevice>();

                while (reader.Read())
                {
                    IDevice dev = new Device();
                    dev.DeviceId = (string)reader["device_id"];
                    dev.StatusId = (int)reader["status_id"];
                    dev.DeviceType = (int)reader["device_type"];
                    dev.DeviceName = (string)reader["device_name"];
                    dev.IsOnline = (bool)reader["is_online"];
                    dev.Room = (string)reader["room_name"];
                    dev.RoomId = (Guid)reader["room_id"];
                    devlist.Add(dev);
                }

                return devlist;

            } catch
            {
                return null;
            } finally
            {
                con.Close();
            }
        }

        // Get devices of a specific type for a specific room, all lights in room with Id x for example
        public List<IDevice> GetDevicesInARoomByType(Guid roomId, int type)
        {

            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string deviceByType = @"SELECT D.device_id, D.status_id, D.device_type, D.device_name, D.is_online
                                    FROM Device AS D
                                    INNER JOIN Rooms_devices AS RD
                                    ON D.device_id = RD.device_id AND RD.room_id = '" + roomId + "' AND D.device_type = " + type + " ";
            try
            {
                SqlCommand command = new SqlCommand(deviceByType, con);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<IDevice> devlist = new List<IDevice>();

                while (reader.Read())
                {
                    IDevice dev = new Device();
                    dev.DeviceId = (string)reader["device_id"];
                    dev.StatusId = (int)reader["status_id"];
                    dev.DeviceType = (int)reader["device_type"];
                    dev.DeviceName = (string)reader["device_name"];
                    dev.IsOnline = (bool)reader["is_online"];
                    dev.RoomId = roomId;
                    devlist.Add(dev);
                }

                return devlist;
            } catch
            {
                return null;
            } finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Send a query to the database without getting a responses
        /// </summary>
        /// <param name="query"></param>
        /// <returns>A string indicating if the query was successful</returns>
        private string SendQueryNoResponse(string query)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                return "Query processed successfully";
            }
            catch
            {
                return "Error";
            } finally
            {
                con.Close();
            }
        }



        /// <summary>
        /// Send a query that returns a response, a SqlDataReader containing the data
        /// </summary>
        /// <param name="query"></param>
        /// <returns>A SqlDataReader containing the information asked for</returns>
        private SqlDataReader SendQueryGetResponse(string query)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            try
            {
                using (con)
                {
                    SqlCommand command = new SqlCommand(query, con);
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    return reader;
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        private static string ReadSingleRow(IDataRecord record)
        {
            return String.Format("{0}, {1}, {2},{3}", record[0], record[1], record[2], record[3]);
        }

        private IUser ReadMultipleRows(IDataRecord record)
        {
            IUser us = new User();
            us.UserId = (Guid)record["users_id"];
            us.Username = (string)record["username"];
            us.FirstName = (string)record["real_first_name"];
            us.LastName = (string)record["real_surname"];
            Guid temp = (Guid)record["house_id"];
            if(temp == Guid.Empty)
            {
                //us.HouseId = Guid.Empty;
            } else
            {
                us.HouseId = (Guid)record["house_id"];
            }
            us.PhoneNo = (string)record["phonenumber"];
            us.Email = (string)record["email"];
            us.IsAdmin = (bool)record["isAdmin"];

            return us;



        }

        private void ExampleMethod()
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string querystring = "";
            try
            {
                using (con)
                {
                    SqlCommand command = new SqlCommand(querystring, con);
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

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
        }

    }
}