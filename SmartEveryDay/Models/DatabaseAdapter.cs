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

namespace SmartEveryDay.Models
{
    public class DatabaseAdapter
    {
        SqlConnection con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

        public string saveData2(string country)
        {
            Console.Write("In saveData2");
            return "Great success!";
        }

        public string saveData(string data)
        {
            Console.Write("In Save data");
            if (data == null)
            {
                Console.Write("Obj is null when received in saveData");
                return "Obj is null";
            }
            else /*{
                Console.Write("Received: " + obj);
                return "yay!";
                    }*/ { 
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
        }

        /*public List<User> getData()
        {
            List<User> userlist = new List<User>();
            try
            {
                string queryString = "SELECT [Users_id],[username],[real_first_name],[real_surname],[house_id],[phonenumber],[email],[isAdmin] FROM [Users] WHERE [username] = 'Anna' ";
                using (con)
                {
                    SqlCommand command = new SqlCommand(queryString, con);
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //string tex = reader.GetValue(0).ToString() + " " + reader.GetValue(1).ToString();
                        //string nameOfStatus = (string)reader["name_of_status"];
                        // string nameOfCode = (string)reader["code_of_status"];
                        //int idOfStatus = Int32.TryParse(reader["status_id"]);
                        //string tex2 = reader[1].ToString();
                        //TextBox3.InnerText = ReadSingleRow((IDataRecord)reader);

                        userlist.Add(ReadMultipleRows((IDataRecord)reader));


                    }

                }
            }
            finally
            {
                con.Close();
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
    }
}