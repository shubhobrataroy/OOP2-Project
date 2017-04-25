using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Config_Console_1
{
    class Program
    {
        static void Main(string[] args)
        {

            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;Integrated Security=True");
            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            SqlCommand command=new SqlCommand("INSERT INTO test.dbo.Group_Members "+"Values ('14-28570', 'Shubho Brata','Code')",connection);
            
            
        }
    }
}
