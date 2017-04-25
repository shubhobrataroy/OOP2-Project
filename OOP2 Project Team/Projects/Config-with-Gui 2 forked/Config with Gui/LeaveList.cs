using System.Data;
using System.Text;
using Leave.DataClients;

namespace Leave.Modules
{
	static class LeaveList
	{
		private static string	server	 = @".\SQLExpress",
								database = "Leave",
								table	 = "LeaveList";

		public static DataTable GetAllLeaveInfo(string rowConstraints = "")	{
			using(SqlTableTracker leaveListTracker = new SqlTableTracker(server, database, table))	{
				return leaveListTracker.Select( rowConstraints );
			}
		}

		public static DataTable GetAllDistributedLeaveInfo( string rowConstraints = "" )	{
			using( SqlTableTracker leaveListTracker = new SqlTableTracker( server, database, table + ", Employees, Entitlements" ) )	{
				DataManager dm = new DataManager(leaveListTracker.SelectSpecific("DISTINCT LeaveList.EmployeeID, EmployeeName, LeaveList.LeaveType, LeavingDate, JoiningDate, Entitlements.Balance, DaysCount, Comment", (rowConstraints.Length == 0 ? "" : rowConstraints + " AND ") + "Employees.EmployeeID = LeaveList.EmployeeID AND LeaveList.EmployeeID = Entitlements.EmployeeID AND LeaveList.LeaveType = Entitlements.LeaveType"));
				return dm.Trim( );
			}
		}

		public static DataTable GetSpecificDistributedLeaveInfo( string columns, string rowConstraints = "" )	{
			using( SqlTableTracker leaveListTracker = new SqlTableTracker( server, database, table + ", Employees, Entitlements" ))	{
				if( rowConstraints.Length > 0 ) rowConstraints += " AND ";
				return leaveListTracker.SelectSpecific( columns, rowConstraints + "Employees.EmployeeID = LeaveList.EmployeeID AND LeaveList.EmployeeID = Entitlements.EmployeeID AND LeaveList.LeaveType = Entitlements.LeaveType" );
			}
		}

		public static DataTable GetSpecificLeaveInfo(string columns, string rowConstraints = "")	{
			using(SqlTableTracker leaveListTracker = new SqlTableTracker(server, database, table))	{
				return leaveListTracker.SelectSpecific(columns, rowConstraints);
			}
		}

		public static void AssignLeave( string values, string columns = "" )	{
			using( SqlTableTracker leaveListTracker = new SqlTableTracker( server, database, table ) )	{
				leaveListTracker.Insert( values, columns );
			}
		}

		public static void Delete( string rowConstraints = "" )	{
			using( SqlTableTracker leaveListTracker = new SqlTableTracker( server, database, table ) )	{
				leaveListTracker.Delete( rowConstraints );
			}
		}
	}
}