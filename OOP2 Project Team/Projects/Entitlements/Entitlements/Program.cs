using System;
using System.Data;

namespace LeaveManagement	{
	class Program	{
		static void Main(string[] args)	{
			bool notExited = true;
			int choice = 0;
			do  {
				try {
					choice = GetUserChoice("Employee Entitlements", "Set Entitlements", "Remove Entitlements", "Exit");
					Console.Clear();
					switch(choice)	{
						case 1:
							choice = GetUserChoice("All", "Specific", "Back");
							Console.Clear();
							switch(choice)	{
								case 1:
									PrintTable(Entitlements.Select());
									break;
								case 2:
									choice = GetUserChoice("ID", "Name", "Issue Date", "Expiring Date", "Cancel");
									Console.Clear();
									switch(choice)	{
										case 1:
											PrintTable(Entitlements.Select("employeeid = " + GetInt("ID")));
											break;
										case 2:
											PrintTable(Entitlements.Select("employeename like '%" + GetString("Name") + "%'"));
											break;
										case 3:
											PrintTable(Entitlements.Select("startingdate = " + GetVarchar("Issue Date")));
											break;
										case 4:
											PrintTable(Entitlements.Select("enddate = " + GetVarchar("Expiring Date")));
											break;
										case 5:
											break;
										default:
											Console.WriteLine("Invalid Option!");
											break;
									}
									break;
								case 3:
									break;
								default:
									Console.WriteLine("Invalid Option!");
									break;
							}
							break;
						case 2:
							Entitlements.Insert(GetInt("ID") + ", " + GetVarchar("Name") + ", " + GetVarchar("Issue Date") + ", " + GetVarchar("Expiring Date"));
							break;
						case 3:
							choice = GetUserChoice("All", "By ID", "By Name", "By Issue Date", "By End Date", "Back");
							Console.Clear();
							switch(choice)	{
								case 1:
									Entitlements.Delete();
									break;
								case 2:
									Entitlements.Delete("employeeid = " + GetInt("ID"));
									break;
								case 3:
									Entitlements.Delete("employeename = " + GetVarchar("Name"));
									break;
								case 4:
									Entitlements.Delete("startingdate = " + GetVarchar("Issue Date"));
									break;
								case 5:
									Entitlements.Delete("enddate = " + GetVarchar("Expiring Date"));
									break;
								case 6:
									break;
								default:
									Console.WriteLine("Invalid Option!");
									break;
							}
							break;
						case 4:
							notExited = false;
							break;
						default:
							Console.WriteLine("Invalid Option!");
							break;
					}
				}	catch(System.Data.SqlClient.SqlException e)	{
					Console.WriteLine(e.Message);
				}	catch(Exception e)	{
					Console.WriteLine(e.Message);
				}
			}	while(notExited);
		}

		private static int GetUserChoice(params string[] choices)	{
			Console.WriteLine();
			for(int i = 1; i <= choices.Length; i++)
				Console.WriteLine(i + "\t" + choices[i - 1]);
			Console.WriteLine();
			Console.Write("Choice\t");
			return int.Parse(Console.ReadLine());
		}

		private static void PrintTable(DataTable table)	{
			Console.WriteLine();
			foreach(DataColumn column in table.Columns)
				Console.Write(column.ColumnName + "\t\t");
			Console.WriteLine();
			foreach(DataRow row in table.Rows)	{
				foreach(DataColumn column in table.Columns)
					Console.Write(row[column] + "\t\t");
				Console.WriteLine();
			}
		}

		private static string GetString(string field)	{
			Console.Write("Enter " + field + ": ");
			return Console.ReadLine();
		}

		private static string GetVarchar(string field)	{
			return "'" + GetString(field) + "'";
		}

		private static string GetInt(string field)	{
			return "" + int.Parse(GetString(field));
		}
	}
}
