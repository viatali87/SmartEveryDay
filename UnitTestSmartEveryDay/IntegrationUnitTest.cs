using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartEveryDay.Data;
using System.Data.SqlClient;
using System.Data;
using SmartEveryDay.Models;

namespace UnitTestSmartEveryDay
{
    [TestClass]
    public class IntegrationUnitTest
    {
        SqlConnection con;

        [TestInitialize]
        public void MyTestInitialize()
        {
            //Here would go the code to restore the test database.
             con = new SqlConnection(@"Data Source=nadinavitalielea.database.windows.net;Initial Catalog=DB_Everyday;Persist Security Info=True;User ID=SED;Password=SmartEveryDay1");

            try
            {
                con.Open();
               
            }
            catch(System.Exception e)
            {
                throw new System.ArgumentException("Table cannot be added " + e);
            }
            
        }

        [TestMethod]
        public void SaveTestTableToDB()
        {
            //Arrange
            var query = @"                  
                            BEGIN
                            CREATE TABLE Test (
                              RowId     integer PRIMARY KEY NOT NULL,
                              Test1     varchar(200) NOT NULL,
                              Test2     varchar(200) NOT NULL,
                              Test3     varchar(200) NOT NULL
                            ); END";
            //Act
            var isInsert = DatabaseQuery(query);
            //Assert
            Assert.IsTrue(isInsert);

        }

        [TestMethod]
        public void SaveRowIntoTestTable()
        {
            //Arrange
            var insertString = "INSERT INTO Test(RowId, Test1, Test2, Test3) VALUES('1','t1', 't2', 't3')";
            //Act
            var isInsert = DatabaseQuery(insertString);
            //Assert
            Assert.IsTrue(isInsert);
        }
        [TestMethod]
        public void DeleteRowFromTestTable()
        {
            //Arrange
            var insertString = "DELETE FROM Test WHERE RowId = 1";
            //Act
            var isInsert = DatabaseQuery(insertString);
            //Assert
            Assert.IsTrue(isInsert);

        }
        [TestMethod]
        public void DeleteTestTableFromDB()
        {
            //Arrange
            var insertString = "DROP TABLE IF EXISTS Test";
            //Act
            var isInsert = DatabaseQuery(insertString);
            //Assert
            Assert.IsTrue(isInsert);

        }
        private bool DatabaseQuery(string query)
        {
            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [TestCleanup]
        public void MyClassCleanup()
        {
            //Here would go logic to drop the database.

            con.Close();
            con.Dispose();
        }
    }
}
