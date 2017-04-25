using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Leave.DataClients;
using System.Data.SqlClient;
using System.Windows.Controls;

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


                using (SqlTableTracker Databasetest = new SqlTableTracker(connection_string2, tableName))
                {
                    Databasetest.Delete(command);
                }
            }

            public static DataTable SearchByDate(String startdate , string endDate)
            {
                SqlConnection myConnection = new SqlConnection(connection_string2);
                myConnection.Open();
                SqlCommand command = new SqlCommand("select * from Holidays where Date>='" + startdate + "' and Date<='" + endDate + "'", myConnection);
                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();

                DataTable result= new DataTable();
                result.Load(reader);

                myConnection.Close();
                command.Dispose();
                return result;
            }

            public static void ResizeMyTable(DataGrid table)
            {
                foreach (DataGridColumn column in table.Columns)
                    column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
            }
        }
    }
}
