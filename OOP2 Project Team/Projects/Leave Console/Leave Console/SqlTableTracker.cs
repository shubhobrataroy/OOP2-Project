using System.Data;
using System.Data.SqlClient;

namespace Leave
{
    namespace DataClients
    {
        sealed class SqlTableTracker : System.IDisposable
        {
            private string table;
            private string connectionString;
            private SqlConnection connection;
            private SqlCommand command;

            public SqlTableTracker(string connectionString,
                                   string table)
            {
                this.table = table;
                this.connectionString = connectionString;
                connection = new SqlConnection(connectionString);
                connection.Open();
                command = new SqlCommand("", connection);
            }

            public SqlTableTracker(string server,
                                   string database,
                                   string table) :
                this("Server = " + server + "; database = " + database + "; Trusted_Connection = True;", table) { }

            public void Dispose()
            {
                connection.Close();
                connection.Dispose();
                command.Dispose();
            }

            public DataTable Select(string rowConstraints = "")
            {
                return GetTableData("*", rowConstraints, "");
            }

            public DataTable SelectSorted(string orderingSequence)
            {
                return GetTableData("*", "", orderingSequence);
            }

            public DataTable SelectSorted(string rowConstraints, string orderingSequence)
            {
                return GetTableData("*", rowConstraints, orderingSequence);
            }

            public DataTable SelectSpecific(string columns, string rowConstraints = "")
            {
                return GetTableData(columns, rowConstraints, "");
            }

            public DataTable SelectSpecificSorted(string columns, string orderingSequence)
            {
                return GetTableData(columns, "", orderingSequence);
            }

            public DataTable SelectSpecificSorted(string columns, string rowConstraints, string orderingSequence)
            {
                return GetTableData(columns, rowConstraints, orderingSequence);
            }

            public void Insert(string values, string columns = "")
            {
                command.CommandText = "INSERT INTO " + table;
                if (columns.Length != 0) command.CommandText += "(" + columns + ")";
                command.CommandText += " VALUES(" + values + ")";
                command.ExecuteNonQuery();
            }

            public void Update(string changes, string rowConstraints = "")
            {
                command.CommandText = "UPDATE " + table + " SET " + changes;
                if (rowConstraints.Length != 0) command.CommandText += " WHERE " + rowConstraints;
                command.ExecuteNonQuery();
            }

            public void Delete(string rowConstraints = "")
            {
                command.CommandText = "DELETE FROM " + table;
                if (rowConstraints.Length != 0) command.CommandText += " WHERE " + rowConstraints;
                command.ExecuteNonQuery();
            }

            private DataTable GetTableData(string columns, string rowConstraints, string orderingSequence)
            {
                command.CommandText = "SELECT " + columns + " FROM " + table;
                if (rowConstraints.Length != 0)
                    command.CommandText += " WHERE " + rowConstraints;
                if (orderingSequence.Length != 0)
                    command.CommandText += " ORDER BY " + orderingSequence;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable result = new DataTable();
                    result.Load(reader);
                    return result;
                }
            }

            public string Table { get { return table; } }

            public string ConnectionString { get { return connectionString; } }

            public DataColumnCollection Columns
            {
                get { return Select("0 <> 0").Columns; }
            }
        }
    }
}