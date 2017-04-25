using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Leave.Modules;
using System.Windows.Controls;

namespace Config_with_Gui
{
    class Reports_Database_Connector
    {
        public static void SetCombobox(ComboBox item, string tableName, int index)
        {

            SqlConnection myConnection = new SqlConnection(Holiday_Database_Connector.connection_string2);
            myConnection.Open();
            SqlCommand command = new SqlCommand("select * from " + tableName, myConnection);
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                item.Items.Add(reader[index].ToString());
            }

            command.Dispose();
            myConnection.Close();
        }

        public static void SetComboboxPeriodSpecific(ComboBox item)
        {

            SqlConnection myConnection = new SqlConnection(Holiday_Database_Connector.connection_string2);
            myConnection.Open();
            SqlCommand command = new SqlCommand("select * from LeavePeriods", myConnection);
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                item.Items.Add(reader[0].ToString() + " - " + reader[1].ToString());
            }

            command.Dispose();
            myConnection.Close();
        }


    }
}
