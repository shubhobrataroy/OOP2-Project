using System;
using System.Data;
using Leave.DataClients;

namespace Leave.Modules
{
	static class LeaveApplications	{
		private static string	server	 = @".\SQLExpress",
								database = "Leave",
								table	 = "LeaveApplications";

		public static DataTable FetchApplications(string rowConstraints = "")	{
			using(SqlTableTracker leaveApplicationsTracker = new SqlTableTracker(server, database, table))	{
				return leaveApplicationsTracker.Select(rowConstraints);
			}
		}

		public static DataTable FetchDetails( string columns, string rowConstraints = "" )	{
			using( SqlTableTracker leaveApplicationsTracker = new SqlTableTracker( server, database, table ))	{
				return leaveApplicationsTracker.SelectSpecificSorted( columns, rowConstraints, columns );
			}
		}

		public static void SendApplication(string employeeID, string leaveType, DateTime leavingDate, DateTime joiningDate, string description)	{
			using(SqlTableTracker leaveApplicationsTracker = new SqlTableTracker(server, database, table))	{
				leaveApplicationsTracker.Insert(employeeID + ", '" + leaveType + "', '" + new DateString(leavingDate) + "', '" + new DateString(joiningDate) + "', '" + description + "'");
			}
		}

		public static void RejectApplications( string rowConstraints = "" )	{
			using(SqlTableTracker leaveApplicationsTracker = new SqlTableTracker(server, database, table))	{
				leaveApplicationsTracker.Delete( rowConstraints );
			}
		}
	}
}