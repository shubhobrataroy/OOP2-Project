using System.Data;
using Leave.DataClients;

namespace Leave.Supplements
{
	static class WorkHolidays
	{
		private static string	server	 = @".\SQLExpress",
								database = "Leave",
								table	 = "Holidays";

		public static DataTable GetAllHolidayInfo( string rowConstraints = "" )	{
			using( SqlTableTracker holidaysTracker = new SqlTableTracker( server, database, table ) )	{
				return holidaysTracker.Select( rowConstraints );
			}
		}

		public static DataTable GetSpecificHolidayInfo( string columns, string rowConstraints = "" )	{
			using( SqlTableTracker holidaysTracker = new SqlTableTracker( server, database, table ) )	{
				return holidaysTracker.SelectSpecific( columns, rowConstraints );
			}
		}
	}
}
