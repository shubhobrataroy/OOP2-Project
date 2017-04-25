using System.Data;
using Leave.DataClients;

namespace Leave.Supplements
{
	static class Users
	{
		private static string	server	 = @".\SQLExpress",
								database = "Leave",
								table	 = "Users";

		public static DataTable GetAllUserInfo(string rowConstraints = "")	{
			using(SqlTableTracker usersTracker = new SqlTableTracker(server, database, table))	{
				return usersTracker.Select(rowConstraints);
			}
		}

		public static DataTable GetSpecificUserInfo(string columns, string rowConstraints = "")	{
			using(SqlTableTracker usersTracker = new SqlTableTracker(server, database, table))	{
				return usersTracker.SelectSpecific(columns, rowConstraints);
			}
		}

		public static void AddUserInfo(string values, string columns = "")	{
			using(SqlTableTracker usersTracker = new SqlTableTracker(server, database, table))	{
				usersTracker.Insert(values, columns);
			}
		}

		public static void UpdateUserInfo(string changes, string rowConstraints = "")	{
			using(SqlTableTracker usersTracker = new SqlTableTracker(server, database, table))	{
				usersTracker.Update(changes, rowConstraints);
			}
		}

		public static void DeleteUserInfo(string rowConstraints = "")	{
			using(SqlTableTracker usersTracker = new SqlTableTracker(server, database, table))	{
				usersTracker.Delete(rowConstraints);
			}
		}
	}
}