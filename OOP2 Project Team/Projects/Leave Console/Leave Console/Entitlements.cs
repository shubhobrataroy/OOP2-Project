using System.Data;
using Leave.DataClients;

namespace Leave
{
	namespace Modules
	{
		static class Entitlements	{
			private static string	server		= @".\SQLExpress",
									database	= "Leave",
									table		= "Entitlements",
                                    connection_string =@"Data Source=Shubho,1234;Initial Catalog=Leave;User ID=group_leave;Password=oop2";
			
			public static DataTable GetAllEntitlementInfo(string rowConstraints = "")	{
                using (SqlTableTracker entitlementsTracker = new SqlTableTracker(connection_string, table))
                {
					return entitlementsTracker.Select(rowConstraints);
				}
			}

			public static DataTable GetSpecificEntitlementInfo(string columns, string rowConstraints = "")	{
                using (SqlTableTracker entitlementsTracker = new SqlTableTracker(connection_string, table))
                {
					return entitlementsTracker.SelectSpecific(columns, rowConstraints);
				}
			}
			
			public static void AddEntitlementInfo(string values, string columnConstraints = "")	{
                using (SqlTableTracker entitlementsTracker = new SqlTableTracker(connection_string, table))
                {
					entitlementsTracker.Insert(values, columnConstraints);
				}
			}
			
			public static void UpdateEntitlementInfo(string changes, string rowConstraints = "")	{
                using (SqlTableTracker entitlementsTracker = new SqlTableTracker(connection_string, table))
                {
					entitlementsTracker.Update(changes, rowConstraints);
				}
			}
			
			public static void DeleteEntitlementInfo(string rowConstraints = "")	{
                using (SqlTableTracker entitlementsTracker = new SqlTableTracker(connection_string, table))
                {
					entitlementsTracker.Delete(rowConstraints);
				}
			}
		}
	}
}
