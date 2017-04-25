using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Leave
{
    namespace Modules
    {
        class Work_Week_Database_Connector
        {
            public static string GetCurrentSatusByID(int id, bool connectionOff)
            {
                SqlConnection myConnection = new SqlConnection(Holiday_Database_Connector.connection_string2);
                myConnection.Open();
                SqlCommand command = new SqlCommand("select WorkDayType from WorkDays where Id="+id,myConnection);
                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                string result = reader[0].ToString();
                if (connectionOff) myConnection.Close();
                command.Dispose();
                return result;   
            }

            public static void update(int id, string workDayType)
            {
                SqlConnection myConnection = new SqlConnection(Holiday_Database_Connector.connection_string2);
                myConnection.Open();
                SqlCommand command = new SqlCommand("Update WorkDays set WorkDayType='" + workDayType + "' where id=" + id,myConnection);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
        }
    }
}
