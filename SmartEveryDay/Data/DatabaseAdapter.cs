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
    public class DatabaseAdapter
    {
        private static DatabaseAdapter instance = new DatabaseAdapter();

        SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

        private DatabaseAdapter()
        {

        }

        public static DatabaseAdapter Instance()
        {
            
                return instance;
            
        }
        
        /// <summary>
        /// Takes a user and saves a new row in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A user</returns>
        public User saveNewUser(User user)  // WORKS but the if statement is untested!
        {
            string attempt = sendQueryNoResponse("INSERT INTO Users(Users_id, username, real_first_name, real_surname, house_id, phonenumber, email, isAdmin) VALUES('" + user.UserId + "','" + user.Username + "','" + user.FirstName + "','" + user.LastName + "', '" + user.HouseId + "','" + user.PhoneNo + "', '" + user.Email + "', '" + user.IsAdmin + "')");
            return getUserById(user.UserId);
        }

        /// <summary>
        /// Gets all information about a user using the users Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A user</returns>
        public User getUserById(Guid userId)
        {
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
        /// Get a list of all users
        /// </summary>
        /// <returns>List of Users</returns>
        public List<User> getAllUsers()
        {
            string querystring = "SELECT Users_id, Users.username, Users.real_first_name, Users.real_surname, Users.house_id, Users.email, Users.phonenumber, Users.isAdmin FROM Users";

            //SqlDataReader reader = sendQueryGetResponse(querystring);
            List<User> userlist = new List<User>();
            try
            {
                using (con)
                {
                    using (SqlCommand command = new SqlCommand(querystring, con))
                    {
                        con.Open();
                        //command.ExecuteNonQuery();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                User us = new User();
                                us.UserId = (Guid)reader["users_id"];
                                us.Username = (string)reader["username"];
                                us.FirstName = (string)reader["real_first_name"];
                                us.LastName = (string)reader["real_surname"];
                                /*Guid temp = (Guid)reader["house_id"];
                                if (temp != null)
                                {
                                    us.HouseId = temp;
                                }*/
                                us.HouseId = (Guid)reader["house_id"];
                                us.PhoneNo = (string)reader["phonenumber"];
                                us.Email = (string)reader["email"];
                                us.IsAdmin = (bool)reader["isAdmin"];

                                userlist.Add(us);

                            }
                        }


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
        public List<Device> getDevicesByHouseId(Guid houseId)
        {
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
                        dev.DeviceType = (string)reader["device_type"];
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
        public List<Device> getAllDevices()
        {
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
                        dev.DeviceType = (string)reader["device_type"];
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
        public List<Device> getRoomsAndDevicesByHouseId(string houseId)
        {
            string querystring = @"
            
            SELECT D.device_id, D.status_id, D.device_type, D.device_name, D.is_online, R.room_id, R.room_name 
            FROM Device AS D
            INNER JOIN House_devices as HD
            ON HD.device_id = D.device_id
            INNER JOIN Rooms_devices AS RD
            ON RD.device_id = D.device_id
            INNER JOIN Rooms AS R
            ON R.room_id = RD.room_id
            WHERE HD.house_id = '" + houseId + "'";

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
                        dev.DeviceType = (string)reader["device_type"];
                        dev.DeviceName = (string)reader["device_name"];
                        dev.IsOnline = (bool)reader["is_online"];
                        dev.Room = (string)reader["room_name"];
                        dev.RoomId = (Guid)reader["room_id"];
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
        /// Send a query to the database without getting a responses
        /// </summary>
        /// <param name="query"></param>
        /// <returns>A string indicating if the query was successful</returns>
        private string sendQueryNoResponse(string query)
        {
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
        private SqlDataReader sendQueryGetResponse(string query)
        {
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
                //con.Close();
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
            us.HouseId = (Guid)record["house_id"];
            us.PhoneNo = (string)record["phonenumber"];
            us.Email = (string)record["email"];
            us.IsAdmin = (bool)record["isAdmin"];

            return us;



        }

        private void exampleMethod()
        {
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