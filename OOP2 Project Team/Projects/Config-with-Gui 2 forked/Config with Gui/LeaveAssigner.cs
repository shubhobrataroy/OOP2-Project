using System.Data;
using Leave.DataClients;

namespace Leave.Modules
{
	static class LeaveAssigner
	{
		private static string	server	 = @".\SQLExpress",
								database = "Leave",
								table	 = "AssignedLeaves";

		public static DataTable GetAllAssignedLeaveInfo(string rowConstraints = "")	{
			using(SqlTableTracker assignedLeavesTracker = new SqlTableTracker(server, database, table))	{
				return assignedLeavesTracker.Select(rowConstraints);
			}
		}

		public static DataTable GetSpecificAssignedLeaveInfo(string columns, string rowConstraints = "")	{
			using(SqlTableTracker assignedLeavesTracker = new SqlTableTracker(server, database, table))	{
				return assignedLeavesTracker.SelectSpecific(columns, rowConstraints);
			}
		}

		public static void Assign(string values, string columns = "")	{
			using(SqlTableTracker assignedLeavesTracker = new SqlTableTracker(server, database, table))	{
				assignedLeavesTracker.Insert(values, columns);
			}
		}

		public static void UpdateAssigned(string changes, string rowConstraints = "")	{
			using(SqlTableTracker assignedLeavesTracker = new SqlTableTracker(server, database, table))	{
				assignedLeavesTracker.Update(changes, rowConstraints);
			}
		}

		public static void RemoveAssigned(string rowConstraints = "")	{
			using(SqlTableTracker assignedLeavesTracker = new SqlTableTracker(server, database, table))	{
				assignedLeavesTracker.Delete(rowConstraints);
			}
		}
	}
}
