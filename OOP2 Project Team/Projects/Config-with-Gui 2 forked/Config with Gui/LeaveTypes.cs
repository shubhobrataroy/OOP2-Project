using System.Data;
using Leave.DataClients;

namespace Leave
{
	namespace Supplements
	{
		static class LeaveTypes
		{
            private static string   server	 = @".\SQLExpress",
									database = "Leave",
									table = "LeaveTypes";

			public static DataTable GetLeaveTypes(string rowConstraints = "")	{
				using(SqlTableTracker leaveTypesTracker = new SqlTableTracker(server, database, table))	{
					return leaveTypesTracker.SelectSorted( rowConstraints, "LeaveType" );
				}
			}
		}
	}
}
