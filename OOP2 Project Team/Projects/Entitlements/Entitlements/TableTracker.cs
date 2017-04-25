using System;
using System.Data;
using System.Data.SqlClient;

namespace LeaveManagement	
{
	class TableTracker : IDisposable
	{
		private string tableName;
		private SqlConnection connection;
		private SqlCommand command;
		
		public TableTracker(string server,
							string database, 
							string tableName)
		{
			connection = new SqlConnection("Server = " + server + "; database = " + database + "; Trusted_Connection = True;");
			connection.Open();
			command = new SqlCommand("", connection);
			this.tableName = tableName;
		}
		
		public void Dispose()	{
			connection.Close();
			connection.Dispose();
			command.Dispose();
		}
		
		public DataTable Select(string rowConstraints = "")	{
			return GetTableData("*", rowConstraints, "");
		}

		public DataTable SelectSorted(string orderingSequence)	{
			return GetTableData("*", "", orderingSequence);
		}

		public DataTable SelectSorted(string rowConstraints, string orderingSequence)	{
			return GetTableData("*", rowConstraints, orderingSequence);
		}

		public DataTable SelectSpecific(string columns, string rowConstraints = "")	{
			return GetTableData(columns, rowConstraints, "");
		}

		public DataTable SelectSpecificSorted(string columns, string orderingSequence)	{
			return GetTableData(columns, "", orderingSequence);
		}

		public DataTable SelectSpecificSorted(string columns, string rowConstraints, string orderingSequence)	{
			return GetTableData(columns, rowConstraints, orderingSequence);
		}

		public void Insert(string values, string columns = "")	{
			command.CommandText = "INSERT INTO " + tableName;
			if(columns.Length != 0) command.CommandText += "(" + columns + ")";
			command.CommandText += " VALUES(" + values + ")";
			command.ExecuteNonQuery();
		}

		public void Update(string changes, string rowConstraints = "")	{
			command.CommandText = "UPDATE " + tableName + " SET " + changes;
			if(rowConstraints.Length != 0) command.CommandText += " WHERE " + rowConstraints;
			command.ExecuteNonQuery();
		}

		public void Delete(string rowConstraints = "")	{
			command.CommandText = "DELETE FROM " + tableName;
			if(rowConstraints.Length != 0) command.CommandText += " WHERE " + rowConstraints;
			command.ExecuteNonQuery();
		}

		private DataTable GetTableData(string columns, string rowConstraints, string orderingSequence)	{
			command.CommandText = "SELECT " + columns + " FROM " + tableName;
			if(rowConstraints.Length != 0)
				command.CommandText += " WHERE " + rowConstraints;
			if(orderingSequence.Length != 0)
				command.CommandText += " ORDER BY " + orderingSequence;
			using(SqlDataAdapter tableLoader = new SqlDataAdapter(command))	{
				DataTable result = new DataTable();
				tableLoader.Fill(result);
				return result;
			}
		}

		public string TableName	{ get { return tableName; } }

		public string[] Columns	{
			get	{
				command.CommandText = "SELECT TOP 0 * FROM " + tableName;
				using(SqlDataReader reader = command.ExecuteReader())	{
					string[] columns = new string[reader.FieldCount];
					for(int i = 0; i < columns.Length; i++)
						columns[i] = reader.GetName(i);
					return columns;
				}
			}
		}
	}
}
