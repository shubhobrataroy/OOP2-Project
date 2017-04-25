using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Leave.DataClients;

namespace Leave_Console
{
    class ReportsDatabaseConnector
    {
        public static string getTable()
        {
            //try
            //{
                using (Leave.DataClients.SqlTableTracker test = new Leave.DataClients.SqlTableTracker(Leave.Modules.Holiday_Database_Connector.connection_string2, "Employees" + ", LeaveList"))
                {
                    DataTable table = test.SelectSpecific("Employees.EmployeeName,LeaveType,LeavingDate,JoiningDate,Balance,DaysCount", "Employees.EmployeeID=LeaveList.EmployeeID" );
                    DataManager dm = new DataManager(table);
                    String columns = dm.ExtractColumns("\t") + "\n" + dm.ExtractData("\t");

                    return columns;
                }
            }
            
        //}
    }
}
