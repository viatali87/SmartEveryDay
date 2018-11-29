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

namespace SmartEveryDay.Data
{
    public class DatabaseAdapter : IDatabaseAdapter
    {
        private static DatabaseAdapter instance = new DatabaseAdapter();

        //SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

        private DatabaseAdapter()
        {

        }

        

        public static DatabaseAdapter Instance()
        {
            if(instance == null)
            {
               return new DatabaseAdapter();
            } else
            {
                return instance;
            }
            
                
            
        }

      


        public User SaveNewUser(User user)
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
        public User SaveNewUserSecure(User user)  // WORKS but the if statement is untested!
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

        public string DeleteUser(string val)
        {
            string attempt = SendQueryNoResponse("DELETE Users where Users_id = '" + val + "'");
            return "User deleted";
        }

        public string EditUser(User user)
        {
            try
            {
                string querystring = "UPDATE Users SET username = '" + user.Username + "', real_first_name = '" + user.FirstName + "', real_surname = '" + user.LastName + "', house_id = '" + user.HouseId + "', phonenumber = '" + user.PhoneNo + "', email = '" + user.Email + "', isAdmin = '" + user.IsAdmin + "' WHERE Users_id = '" + user.UserId + "'";
                string attempta = SendQueryNoResponse(querystring);
                return "Db adapter: Successfully edited a user";

            }
            catch (System.Exception e)
            {
                throw new System.ArgumentException("Error in dbAdapter " + e);

            }
        }

        User IDatabaseAdapter.EditUser(User user)
        {
            throw new NotImplementedException();
        }

        public Device AddNewDevice(Device device)
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

        public Device EditDevice(Device updatedDevice)
        {
            throw new NotImplementedException();
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
                return "Error in dbadapter";
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
        public User GetUserById(Guid userId)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string id = userId.ToString();
            string querystring = "SELECT Users_id, Users.username, Users.real_first_name, Users.real_surname, Users.house_id, Users.email, Users.phonenumber, Users.isAdmin FROM Users WHERE Users.Users_id = '" + id + "'";
            //SqlDataReader reader = sendQueryGetResponse(querystring);

            User us = new User();

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

        public User GetUserByIdSecure(Guid userId)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string id = userId.ToString();
            string querystring = 
                @"SELECT Users_id, Users.username, Users.real_first_name, Users.real_surname, Users.house_id, Users.email, Users.phonenumber, Users.isAdmin 
                FROM Users 
                WHERE Users.Users_id = @id";
            //SqlDataReader reader = sendQueryGetResponse(querystring);

            User us = new User();

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
        public List<User> GetAllUsers()
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string querystring = "SELECT Users_id, Users.username, Users.real_first_name, Users.real_surname, Users.house_id, Users.email, Users.phonenumber, Users.isAdmin FROM Users";

            //SqlDataReader reader = sendQueryGetResponse(querystring);
            List<User> userlist = new List<User>();
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
                        int counter = 0;
                        
                        //command.ExecuteNonQuery();
                        SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                counter += 1;
                                User us = new User();
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
                        

                        int yes = counter;
                    }
                        
                 }
             } catch
            {
                
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
        public List<Device> GetDevicesByHouseId(Guid houseId)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string id = houseId.ToString();
            string querystring = "SELECT D.device_id, D.status_id, D.device_type, D.device_name, D.is_online FROM Device AS D INNER JOIN House_devices AS HD ON D.device_id = HD.device_id WHERE HD.house_id = '" + id +"'";
            //SqlDataReader reader = sendQueryGetResponse(querystring);
            List<Device> deviceList = new List<Device>();

            try
            {
                using (con)
                {
                    SqlCommand command = new SqlCommand(querystring, con);
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Device dev = new Device();
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
        public List<Device> GetAllDevices()
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string querystring = "SELECT device_id, status_id, device_type, device_name, is_online FROM Device";
            //SqlDataReader reader = sendQueryGetResponse(querystring);
            List<Device> deviceList = new List<Device>();

            try
            {
                using (con)
                {
                    SqlCommand command = new SqlCommand(querystring, con);
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Device dev = new Device();
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
        public List<Room> GetRoomsAndDevicesByHouseId(Guid houseId)
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
        }

        // Get devices froma  home by type, all lights for example
        public List<Device> GetTypeOfDevicesByHouseId(Guid houseId, int type)
        {
            SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            string deviceByType = "  SELECT D.device_id, D.status_id, D.device_type, D.device_name, D.is_online FROM Device AS D INNER JOIN House_devices AS H ON D.device_id = H.device_id AND H.house_id = '" + houseId + "' WHERE D.device_type = " + type;

            try
            {
                SqlCommand command = new SqlCommand(deviceByType, con);
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

                return devlist;

            } catch
            {
                return null;
            } finally
            {
                con.Close();
            }
        }

        public List<Device> getDevicesInARoomByType(Guid roomId, int type)
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
                List<Device> devlist = new List<Device>();

                while (reader.Read())
                {
                    Device dev = new Device();
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

        /* public string saveData(string data)
         {
             Console.Write("In Save data");
             if (data == null)
             {
                 Console.Write("Obj is null when received in saveData");
                 return "Obj is null";
             }
             else {
                 Console.Write("Received: " + obj);
                 return "yay!";
                     } { 
             //var JSONObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(obj);
             try
             {
                 Console.Write("Text received in saveData: " + data);
                 con.Open();
                 SqlCommand cmd = con.CreateCommand();
                 cmd.CommandType = System.Data.CommandType.Text;
                 cmd.CommandText = "INSERT INTO Country(country_name) VALUES('" + data + "')";

                 cmd.ExecuteNonQuery();
             } catch {
                 return "Failed";
             } finally
             {
                 con.Close();
             }
             return "Success!";
         }
         }*/

        private static string ReadSingleRow(IDataRecord record)
        {
            return String.Format("{0}, {1}, {2},{3}", record[0], record[1], record[2], record[3]);
        }

        private User ReadMultipleRows(IDataRecord record)
        {
            User us = new User();
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