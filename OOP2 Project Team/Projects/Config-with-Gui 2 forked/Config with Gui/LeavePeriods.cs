using System.Data;
using Leave.DataClients;
using System;
using System.Data.SqlClient;

namespace Leave
{
	namespace Supplements
	{
		static class LeavePeriods	{
            private static string server = @"(LocalDB)\v11.0",
								  database	= "home",
								  table		= "LeavePeriods",
	                              connectionstr = @"Data Source=Shubho,1234;Initial Catalog=Leave;User ID=group_leave;Password=oop2",
                                  connectionstr2 = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Team Services C#\Projects\Config-with-Gui 2 forked\Config with Gui\Leave.mdf;Integrated Security=True",
                                  cstr = "Data Source=(LocalDB)\v11.0;Integrated Security=True",
								  connectionString  = @"Data Source=.\SQLExpress;Initial Catalog=Leave;Integrated Security=True";
            public static void AddLeavePeriod(String str1, String str2)
            {
                SqlConnection connection = new SqlConnection(cstr);
                connection.Open();
                SqlCommand com = new SqlCommand("Insert Into " + table + "(startdate, enddate) values("+str1+","+str2+")");
                com.ExecuteNonQuery();
                connection.Close();
            }

			public static DataTable GetLeavePeriods( string rowConstraints = "" )	{
				using( SqlTableTracker leavePeriodsTracker = new SqlTableTracker( connectionString, table ) )	{
					return leavePeriodsTracker.Select( rowConstraints );
				}
			}

			public static DataTable GetStartingDate( string rowConstraints = "" )	{
				using( SqlTableTracker leavePeriodsTracker = new SqlTableTracker( connectionString, table ) )	{
					return leavePeriodsTracker.SelectSpecific( "StartingDate", rowConstraints );
				}
			}

			public static DataTable GetEndingDate( string rowConstraints = "" )	{
				using( SqlTableTracker leavePeriodsTracker = new SqlTableTracker( connectionString, table ) )	{
					return leavePeriodsTracker.SelectSpecific( "EndingDate", rowConstraints );
				}
			}
        }
	}
}
