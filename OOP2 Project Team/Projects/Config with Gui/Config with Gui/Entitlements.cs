using System.Data;
using Leave.DataClients;

namespace Leave
{
	namespace Modules
	{
		static class Entitlements	{
			private static string	server		= ".\\SQLExpress",
									database	= "Leave",
									table		= "Entitlements";
			
			public static DataTable GetEntitlements(string rowConstraints = "")	{
				using(SqlTableTracker entitlementsTracker = new SqlTableTracker(server, database, table))	{
					return entitlementsTracker.Select(rowConstraints);
				}
			}
			
			public static void AddEntitlements(string values, string columnConstraints = "")	{
				using(SqlTableTracker entitlementsTracker = new SqlTableTracker(server, database, table))	{
					entitlementsTracker.Insert(values, columnConstraints);
				}
			}
			
			public static void UpdateEntitlements(string changes, string rowConstraints = "")	{
				using(SqlTableTracker entitlementsTracker = new SqlTableTracker(server, database, table))	{
					entitlementsTracker.Update(changes, rowConstraints);
				}
			}
			
			public static void DeleteEntitlements(string rowConstraints = "")	{
				using(SqlTableTracker entitlementsTracker = new SqlTableTracker(server, database, table))	{
					entitlementsTracker.Delete(rowConstraints);
				}
			}
		}
	}
}
