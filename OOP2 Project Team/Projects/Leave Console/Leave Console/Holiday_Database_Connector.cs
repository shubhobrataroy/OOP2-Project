using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Leave.DataClients;
using System.Data.SqlClient;

namespace Leave
{
    namespace Modules
    {
        class Holiday_Database_Connector
        {
            public static string server = @"(LocalDB)\v11.0",
                                    database = "Leave",
                                    tableName = "Holidays",
                                    connection_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + System.IO.Directory.GetCurrentDirectory() + @"\Leave1.mdf" + ";Integrated Security=True",
                                    connection_string1 = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Leave;Integrated Security=True",
                                    connection_string2 = @"Data Source=Shubho,1234;Initial Catalog=Leave;User ID=group_leave;Password=oop2";

            public static DataTable GetTable()
            {


                using (SqlTableTracker Databasetest = new SqlTableTracker(connection_string2, tableName))
                {
                    DataTable table = Databasetest.Select();
                    DataView table_view = table.AsDataView();
                    return table;
                }
            }

            public static void DeleteTable(String command)
            {
                SqlConnection myConnection =new SqlConnection(connection_string2);
                myConnection.Open();
                
                try
                {
                    SqlCommand command2 = new SqlCommand("Delete from Holidays where Name='" + command + "'", myConnection);
                    command2.ExecuteNonQuery();
                }
                catch (Exception e) { System.Console.WriteLine("Invalid Input"); }
            }

            public static string getTableAll()
            {
                using (SqlTableTracker Databasetest = new SqlTableTracker(connection_string2, tableName))
                {
                    DataTable table = Databasetest.Select();
                    DataManager dm = new DataManager(table);

                    String columns = dm.ExtractColumns("\t\t") + "\n" + dm.ExtractData("\t\t");

                    return columns;
                }
            }
        }
    }
}
