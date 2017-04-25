using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Config_Console_1
{
    class Database
    {
        //Variables
        private SqlConnection connection;
        private SqlCommand command;


        //Constructor
        public Database(string ConnectionString)
        {
            try
            {
                Console.WriteLine("Here");
                connection = new SqlConnection("Data Source=(LocalDB)\v11.0;Integrated Security=True");
                Console.WriteLine("Here");
                connection.Open();
                Console.WriteLine("Here");

            }
            catch (SqlException e) { Console.WriteLine("Connection could not be established\n"); }
        }


        //Methods

        public bool Connect(string ConnectionString) //Connect Method
        {
            try
            {
                connection = new SqlConnection(ConnectionString);
                connection.Open();
                return true;
            }
            catch (Exception e) { return false; }
        }


        public bool Command(String command)
        {
                this.command = new SqlCommand(command, connection);   
            return true;
            
        }
    }
}
