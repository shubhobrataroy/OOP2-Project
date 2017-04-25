using System;
using System.Data;
using System.Text;

namespace Leave
{
    namespace DataClients
    {
        class DataManager
        {
            private DataTable data;

            public DataManager()
            {
                this.data = new DataTable();
            }

            public DataManager(DataTable data)
            {
                this.data = data.Copy();
            }

            public DataTable Data
            {
                get { return data; }
                set { data = value.Copy(); }
            }

            public double Count(string column, string rowConstraints = "")
            {
                return GetResult("count", column, rowConstraints);
            }

            public double GetSum(string column, string rowConstraints = "")
            {
                return GetResult("sum", column, rowConstraints);
            }

            public double GetAvg(string column, string rowConstraints = "")
            {
                return GetResult("avg", column, rowConstraints);
            }

            public double GetMax(string column, string rowConstraints = "")
            {
                return GetResult("max", column, rowConstraints);
            }

            public double GetMin(string column, string rowConstraints = "")
            {
                return GetResult("min", column, rowConstraints);
            }

            public double GetVariance(string column, string rowConstraints = "")
            {
                return GetResult("var", column, rowConstraints);
            }

            public double GetStandardDeviation(string column, string rowConstraints = "")
            {
                return GetResult("stdev", column, rowConstraints);
            }

            public DataTable GetSortedData(string orderingSequence)
            {
                DataTable result = data.Copy();
                result.DefaultView.Sort = orderingSequence;
                return result.DefaultView.ToTable();
            }

            public string ExtractColumns(string columnFeed)
            {
                StringBuilder result = new StringBuilder();
                for (int col = 0; col < data.Columns.Count; col++)
                {
                    if (col > 0) result.Append(columnFeed);
                    result.Append(data.Columns[col].ColumnName);
                }
                return result.ToString();
            }

            public string ExtractData(string columnFeed, string rowFeed = "\n")
            {
                StringBuilder result = new StringBuilder();
                for (int row = 0; row < data.Rows.Count; row++)
                {
                    if (row > 0) result.Append(rowFeed);
                    for (int col = 0; col < data.Columns.Count; col++)
                    {
                        if (col > 0) result.Append(columnFeed);
                        result.Append(data.Rows[row][col]);
                    }
                }
                return result.ToString();
            }

            private double GetResult(string operation, string column, string rowConstraints)
            {
                object result = data.Compute(operation + "(" + column + ")", rowConstraints);
                if (result.GetType() == typeof(DBNull))
                    throw new InvalidOperationException("Column " + column + "(" + rowConstraints + ")" + " is empty!");
                return double.Parse(result.ToString());
            }
        }
    }
}