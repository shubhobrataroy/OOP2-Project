using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        
        static void Main(string[] args)
        {/*
            SqlConnection myConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True;");
            myConnection.Close();
            myConnection.Open();
            try
            {
                SqlDataReader myReader = null;

                SqlCommand myCommand1 = new SqlCommand("insert into Group_members values('Nigga2','Dipto Bitch2')",
                                                         myConnection);

                SqlCommand myCommand = new SqlCommand("select * from Group_Members",
                                                         myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    Console.WriteLine(myReader[0].ToString());
                    Console.WriteLine(myReader[1].ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.Read();
            myConnection.Close();
          */
            Database db = new Database();
            db.Command("select * from Group_Members");
            db.read(1, 2);
            db.Close();
        }
    }
}
