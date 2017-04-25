using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Based_Project
{
    class MainPage
    {
        string username;
        string password;
        bool authentication_success;
        string[] options_name= {"Admin","PIM","Leave","Time","Recruitment","Performance" ,"Dashboard"};
        public MainPage()
        {
            authentication_success = false;
            
            while (!authentication_success)
            {
                Console.WriteLine("Welcome to HRM \nPlease Login\n\n");
                Console.WriteLine("Username: ");
                username = Console.ReadLine();
                Console.WriteLine("Password: ");
                password = Console.ReadLine();
                authentication_success = authenticate(username, password);
            }
            Console.WriteLine("Authentication Successful \n Press any key to proceed\n");
            Console.ReadKey();
            Console.Clear();
            PrintOptions();
            SelectOption();
        }

        private bool authenticate(string username, string password)
        {
            ///Enter Authentication Code Here
            return true;
        }


        public void PrintOptions()
        {
            Console.WriteLine("1. Admin\n2. PIM\n3.Leave\n4. Time\n5. Recruitment\n6. Performance\n7. Deshboard");
        }

        public void SelectOption ()
        {
            int select=0;

            
            while (true)
            {
                Console.Write("Select Option :");
                try
                {
                    select = Int32.Parse(Console.ReadLine().ToString());
                }
                catch (Exception e) { continue; }
                if (select > 0 && select < 6) break;
            }
            
            switch(select)
            {
                case 1:
                    Console.WriteLine("\nAdmin\n");
                    break;
                case 2:
                    Console.WriteLine("\nPIM\n");
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("\nLeave\n");
                    Console.WriteLine("1. Entitlements\n2. Reports\n3.Configure\n4. Leave Lists\n5. Assign Leave\n");
                    break;
                case 4:
                    Console.WriteLine("\nTime\n");
                    break;
                case 5:
                    Console.WriteLine("\nRecriutment\n");
                    break;
                case 6:
                    Console.WriteLine("\nPerformance\n");
                    break;
                case 7:
                    Console.WriteLine("\nDashboard\n");
                    break;
            }
        }
    }
}
