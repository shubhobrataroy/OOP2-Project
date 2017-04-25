using DataClient;

namespace Leave
{
	namespace Supplements
	{
		static class LeavePeriods	{
			private static string	server		= ".\\SQLExpress",
									database	= "Leave",
									tableName	= "LeavePeriods";

			public static System.Data.DataTable Select(string rowConstraints = "")	{
				using(TableTracker leavePeriodsTracker = new TableTracker(server, database, tableName))	{
					return leavePeriodsTracker.Select(rowConstraints);
				}
			}
		}
	}
}
