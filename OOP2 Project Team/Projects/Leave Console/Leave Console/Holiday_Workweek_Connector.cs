using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Leave.DataClients;
using System.Data;


namespace Leave
{
    namespace Modules
    {
        class Work_Week_Database_Connector
        {

            public static string server = @"(LocalDB)\v11.0",
                                    database = "Leave",
                                    tableName = "WorkDays",
                                    connection_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + System.IO.Directory.GetCurrentDirectory() + @"\Leave1.mdf" + ";Integrated Security=True",
                                    connection_string1 = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Leave;Integrated Security=True",
                                    connection_string2 = @"Data Source=Shubho,1234;Initial Catalog=Leave;User ID=group_leave;Password=oop2";



            public static void addTable()
            {
                Console.WriteLine("Input Holiday Name: ");
                string Name = Console.ReadLine();
                Console.WriteLine("Input Holiday Month: ");
                int    month=Int32.Parse(Console.ReadLine());
                Console.WriteLine("Input Holiday Date: ");
                int    date = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Input Holiday Year: ");
                int    year = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Input Holiday Type: ");
                string HolidayType = Console.ReadLine();
                Console.WriteLine("Repeats Annually? ");
                string RepeatsAnnually = Console.ReadLine();

                SqlConnection myConnection = new SqlConnection(connection_string2);
                myConnection.Open();


                try
                {
                    SqlCommand command = new SqlCommand("insert into Holidays values ('" + Name + "','" + (month) + "-" + (date) + "-" + (year) + "','" + HolidayType + "','" + RepeatsAnnually + "')", myConnection);
                    command.ExecuteNonQuery();
                    myConnection.Close();
                }
                catch (Exception e) { Console.WriteLine("Wrong Input"); }
               


            }

            public static string GetCurrentSatusByID(int id, bool connectionOff)
            {
                SqlConnection myConnection = new SqlConnection(Holiday_Database_Connector.connection_string2);
                myConnection.Open();
                SqlCommand command = new SqlCommand("select WorkDayType from WorkDays where Id=" + id, myConnection);
                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                string result = reader[0].ToString();
                if (connectionOff) myConnection.Close();
                command.Dispose();
                return result;
            }


            public static string getTable()
            {
                using (SqlTableTracker Databasetest = new SqlTableTracker(connection_string2, tableName))
                {
                    DataTable table = Databasetest.Select();
                    DataManager dm = new DataManager(table);

                    String columns = dm.ExtractColumns("\t\t") + "\n" + dm.ExtractData("\t\t");

                    return columns;
                }
            }

            public static void update(int id, string workDayType)
            {
                SqlConnection myConnection = new SqlConnection(Holiday_Database_Connector.connection_string2);
                myConnection.Open();
                SqlCommand command = new SqlCommand("Update WorkDays set WorkDayType='" + workDayType + "' where id=" + id, myConnection);
                command.ExecuteNonQuery();
                myConnection.Close();
            }

            public static void Change(String day, string change)
            {
                using (SqlTableTracker Databasetest = new SqlTableTracker(connection_string2, tableName))
                {
                    Databasetest.Update("WorkDayType='"+change+"'", "Name='" + day + "'");
                }
            }
        }
    }
}
