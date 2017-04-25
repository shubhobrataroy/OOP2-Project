using System.Data;
using Leave.DataClients;

namespace Leave
{
	namespace Modules
	{
		static class Entitlements	{
			public static string	server = @".\SQLExpress",
									database = "Leave",
									table = "Entitlements",
									connectionString = @"Data Source=.\SQLExpress;Initial Catalog=Leave;Integrated Security=True";
			
			public static DataTable GetAllEntitlementInfo(string rowConstraints = "")	{
				using(SqlTableTracker entitlementsTracker = new SqlTableTracker(connectionString, table))	{
					return entitlementsTracker.Select(rowConstraints);
				}
			}

			public static DataTable GetAllDistributedEntitlementInfo(string rowConstraints = "")	{
				using(SqlTableTracker entitlementsTracker = new SqlTableTracker(connectionString, table + ", Employees"))	{
					return entitlementsTracker.SelectSpecific("Entitlements.EmployeeID, EmployeeName, LeaveType, EntitlementType, ValidFrom, ValidTo, Balance", "Entitlements.EmployeeID = Employees.EmployeeID AND " + rowConstraints);
				}
			}

			public static DataTable GetSpecificDistributedEntitlementInfo( string columns, string rowConstraints = "" )	{
				using( SqlTableTracker entitlementsTracker = new SqlTableTracker( connectionString, table + ", Employees" ) )	{
					return entitlementsTracker.SelectSpecific( columns, "Entitlements.EmployeeID = Employees.EmployeeID" + ( rowConstraints.Length == 0 ? "" : " AND " + rowConstraints ) );
				}
			}
		 
			public static DataTable GetSpecificEntitlementInfo(string columns, string rowConstraints = "")	{
				using(SqlTableTracker entitlementsTracker = new SqlTableTracker(connectionString, table))	{
					return entitlementsTracker.SelectSpecific(columns, rowConstraints);
				}
			}
			
			public static void AddEntitlementInfo(string values, string columnConstraints = "")	{
				using(SqlTableTracker entitlementsTracker = new SqlTableTracker(connectionString, table))	{
					entitlementsTracker.Insert(values, columnConstraints);
				}
			}
			
			public static void UpdateEntitlementInfo(string changes, string rowConstraints = "")	{
				using(SqlTableTracker entitlementsTracker = new SqlTableTracker(connectionString, table))	{
					entitlementsTracker.Update(changes, rowConstraints);
				}
			}
			
			public static void DeleteEntitlementInfo(string rowConstraints = "")	{
				using(SqlTableTracker entitlementsTracker = new SqlTableTracker(connectionString, table))	{
					entitlementsTracker.Delete(rowConstraints);
				}
			}
		}
	}
}
