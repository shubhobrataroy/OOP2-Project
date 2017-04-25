using System.Data;
using Leave.DataClients;

namespace Leave.Supplements
{
	static class WorkDays
	{
		private static string	server	 = @".\SQLExpress",
								database = "Leave",
								table	 = "WorkDays";

		public static DataTable GetWorkDayInfo( string rowConstraints = "" )	{
			using( SqlTableTracker workDaysTracker = new SqlTableTracker( server, database, table ) )	{
				return workDaysTracker.Select( rowConstraints );
			}
		}
	}
}