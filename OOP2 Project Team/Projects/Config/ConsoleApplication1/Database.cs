using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    

    class Database
    {
        //Variables
           private SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True");
           private SqlCommand command;
           private SqlDataReader reader;

        //Methods
           public void Command(String command){ //Executes Commands 
               try
               {
                   connection.Open();

                   this.command = new SqlCommand(command, connection);
                   reader=this.command.ExecuteReader();
                   
               }
               catch (Exception e)
               {
                   Console.WriteLine(e.ToString());
               }
           }

           public String read(params int [] columns) //Reads Data 
           {
               
               StringBuilder row= new StringBuilder();
               while(reader.Read())
               {
                   for (int i = 0; i < columns.Length;i++ )
                   { 
                       //Console.Write(reader[columns[i] - 1]+" ");
                       row.Append(reader[i] + ",");
                   }
                   row[row.Length - 1] = '\n';
                  Console.WriteLine(row);
               }

               return row.ToString();
           }


       
           public void Close() { connection.Close(); }
    }
}
