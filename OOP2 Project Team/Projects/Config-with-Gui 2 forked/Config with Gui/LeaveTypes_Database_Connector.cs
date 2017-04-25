using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Data.SqlClient;

namespace Config_with_Gui
{
    class LeaveTypes_Database_Connector
    {
        
        private static List<string> SelectedItems= new List<string>();
        public static void addCheckBoxes(DockPanel Dock)
        {
            SqlConnection myConnection = new SqlConnection(Leave.Modules.Holiday_Database_Connector.connection_string2);
            myConnection.Open();
            SqlCommand command = new SqlCommand("select * from LeaveTypes", myConnection);
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string result = reader[0].ToString();
                CheckBox ch2 = new CheckBox();
                ch2.Content = result;
                ch2.Margin = new Thickness(10, 10, 0, 0);
                ch2.SetValue(DockPanel.DockProperty, System.Windows.Controls.Dock.Top);
                ch2.Checked += new RoutedEventHandler(HandleCheck);
                Dock.Children.Add(ch2);
            }
            myConnection.Close();
        }
        private static void HandleCheck(object sender, RoutedEventArgs e)
        {
            SelectedItems.Add(((CheckBox)sender).Content.ToString());
        }

        public static void DeleteSelected()
        {
            
            using (Leave.DataClients.SqlTableTracker Databasetest = new Leave.DataClients.SqlTableTracker(Leave.Modules.Holiday_Database_Connector.connection_string2, "LeaveTypes"))
            {
                
                for (int i=0; i < SelectedItems.Count;i++ )
                {
                    Databasetest.Delete("LeaveType='" + SelectedItems[i] + "'");
                }
               
            }
           
        }
    }
}
