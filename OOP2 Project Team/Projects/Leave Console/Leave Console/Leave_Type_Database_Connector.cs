using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leave.DataClients;
using System.Data;

namespace Leave_Console
{
    class Leave_Type_Database_Connector
    {
        private static string table = "LeaveTypes";

        public static string getTable()
        {
            using (SqlTableTracker Databasetest = new SqlTableTracker(Leave.Modules.Holiday_Database_Connector.connection_string2, table))
            {
                DataTable table2 = Databasetest.Select();
                DataManager dm = new DataManager(table2);

                String columns = dm.ExtractColumns("\t\t") + "\n" + dm.ExtractData("\t\t");

                return columns;
            }
        }

        public static void add (String Name)
        {
            using (Leave.DataClients.SqlTableTracker Databasetest = new Leave.DataClients.SqlTableTracker(Leave.Modules.Holiday_Database_Connector.connection_string2, "LeaveTypes"))
            {
                Databasetest.Insert("'" + Name + "'", "LeaveType");
            }
        }

        public static void Delete (String Name)
        {
            using (Leave.DataClients.SqlTableTracker Databasetest = new Leave.DataClients.SqlTableTracker(Leave.Modules.Holiday_Database_Connector.connection_string2, "LeaveTypes"))
            {
                Databasetest.Delete("LeaveType='" + Name + "'");
                
            }
        }
    }
}
