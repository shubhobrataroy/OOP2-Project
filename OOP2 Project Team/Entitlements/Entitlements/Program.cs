using System;
using System.Data;
using Leave.Modules;
using Leave.Supplements;

namespace UserConsole
{
	class Program
	{
		static void Main(string[] args)	{
			bool userExited = false;

			do	{
				Console.Clear();
				try	{
					switch(GetUserChoice("Add Entitlements", "Employee Entitlements", "Exit"))	{
						case 1:
						/*
						 * 
						 * 
						 * 
						 * 
						 */
							Console.WriteLine("Process Under Construction.");
							Console.Write("Press any key to continue ...");
							Console.ReadKey();
							break;
						case 2:
							PrintTable(Entitlements.Select());
							Console.Write("Press any key to continue ...");
							Console.ReadKey();
							break;
						case 3:
							userExited = true;
							break;
						default:
							Console.WriteLine("Error: Invalid Option!");
							break;
					}
				}	catch(Exception e)	{
					Console.WriteLine(e.Message);
				}
			}	while(!userExited);
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
		/*	foreach(DataColumn column in table.Columns)
				Console.Write(column.ColumnName + "\t");	*/
			Console.WriteLine();
			foreach(DataRow row in table.Rows)	{
				foreach(DataColumn column in table.Columns)
					Console.Write(row[column] + "\t");
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
