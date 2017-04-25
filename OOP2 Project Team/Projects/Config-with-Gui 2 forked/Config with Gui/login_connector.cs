using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Config_with_Gui
{
    class login_connector
    {
        private static string server = @"(LocalDB)\v11.0",
                                   database = "Leave",
                                   tableName = "Holidays",
                                   connection_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + System.IO.Directory.GetCurrentDirectory() + @"\Leave1.mdf" + ";Integrated Security=True",
                                   connection_string1 = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Leave;Integrated Security=True",
                                   connection_string2 = @"Data Source=Shubho,1234;Initial Catalog=Leave;User ID=group_leave;Password=oop2";
        public static string privilege = "";



        public static bool GetAuthentication(string username, string password)
        {

            SqlConnection myConnection = new SqlConnection(connection_string2);
            SqlCommand command;
            SqlDataReader data;

            myConnection.Open();
            command = new SqlCommand("select * from user_login where username='" + username + "'", myConnection);
            try
            {
                command.ExecuteNonQuery();
                data = command.ExecuteReader();
                data.Read();
                privilege = data[2].ToString();
                if (password == data[1].ToString()) return true;
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); return false; }

            return false;
        }
    }
}
