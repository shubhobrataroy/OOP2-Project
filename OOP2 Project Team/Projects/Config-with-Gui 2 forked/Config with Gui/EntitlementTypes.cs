using System;
using System.Data;
using Leave.DataClients;

namespace Leave.Supplements
{
	static class EntitlementTypes
	{
		public static string	connectionString = @"Data Source=.\SQLExpress;Initial Catalog=Leave;Integrated Security=True",
								table			 = "EntitlementTypes";

		public static DataTable GetEntitlementTypes( string rowConstraints = "" )	{
			using( SqlTableTracker entitlementTypesTracker = new SqlTableTracker( connectionString, table ) )	{
				return entitlementTypesTracker.Select( rowConstraints );
			}
		}

		public static void AddEntitlementType( string value )	{
			using( SqlTableTracker entitlementTypesTracker = new SqlTableTracker( connectionString, table ) )	{
				entitlementTypesTracker.Insert( value );
			}
		}

		public static void UpdateEntitlementType( string changes, string rowConstraints = "" )	{
			using( SqlTableTracker entitlementTypesTracker = new SqlTableTracker( connectionString, table ) )	{
				entitlementTypesTracker.Update( changes, rowConstraints );
			}
		}

		public static void RemoveEntitlementType( string rowConstraints = "" )	{
			using( SqlTableTracker entitlementTypesTracker = new SqlTableTracker( connectionString, table ) )	{
				entitlementTypesTracker.Delete( rowConstraints );
			}
		}
	}
}
