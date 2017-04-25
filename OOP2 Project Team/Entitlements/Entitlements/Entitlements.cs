using DataClient;

namespace Leave
{
	namespace Modules
	{
		static class Entitlements	{
			private static string	server		= ".\\SQLExpress",
									database	= "Leave",
									tableName	= "Entitlements";
			
			public static System.Data.DataTable Select(string rowConstraints = "")	{
				using(TableTracker entitlementsTracker = new TableTracker(server, database, tableName))	{
					return entitlementsTracker.Select(rowConstraints);
				}
			}
			
			public static void Insert(string values, string columnConstraints = "")	{
				using(TableTracker entitlementsTracker = new TableTracker(server, database, tableName))	{
					entitlementsTracker.Insert(values, columnConstraints);
				}
			}
			
			public static void Update(string changes, string rowConstraints = "")	{
				using(TableTracker entitlementsTracker = new TableTracker(server, database, tableName))	{
					entitlementsTracker.Update(changes, rowConstraints);
				}
			}
			
			public static void Delete(string rowConstraints = "")	{
				using(TableTracker entitlementsTracker = new TableTracker(server, database, tableName))	{
					entitlementsTracker.Delete(rowConstraints);
				}
			}
		}
	}
}
