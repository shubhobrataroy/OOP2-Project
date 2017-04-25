using System;
using System.Threading;
using Leave.Modules;
using Leave.DataClients;
using System.Data.SqlClient;

namespace UserConsole
{
    class LeaveManagement
    {
        public LeaveManagement() {
            bool executionEnded = false;
            bool validOptionSelected;
            DataManager dataManager = new DataManager();

            do  {
                Console.Clear();
                validOptionSelected = false;
                try {
                    switch (GetUserChoice("Entitlements", "Reports", "Configure", "Leave List", "Assign Leave", "Exit"))    {
                        case 1:
                            do  {
                                Console.Clear();
                                try {
                                    switch (GetUserChoice("Add", "View", "Back"))   {
                                        case 1:
                                            validOptionSelected = true;
                                            Console.WriteLine("Entitlements will be added here.");
                                            Console.Write("Press any key to continue ...");
                                            Console.ReadKey();
                                            break;
                                        case 2:
                                            validOptionSelected = true;
                                            DataManager dm = new DataManager(Entitlements.GetAllEntitlementInfo());

                                            Console.WriteLine(dm.ExtractColumns("\t\t")+"\n"+dm.ExtractData("\t\t"));
                                            Console.Write("Press any key to continue ...");
                                            Console.ReadKey();
                                            break;
                                        case 3:
                                            validOptionSelected = true;
                                            break;
                                        default:
                                            Console.WriteLine("Wrong Input!");
                                            Thread.Sleep(1000);
                                            break;
                                    }
                                }   catch(FormatException e)    {
                                    Console.WriteLine("Wrong Input!");
                                    Thread.Sleep(1000);
                                }   catch(Exception e)  {
                                    Console.WriteLine("Wrong Input!");
                                    Thread.Sleep(1000);
                                }
                            }   while(!validOptionSelected);
                            break;

                        /*
                         switch (GetUserChoice("Leave Type", "Employee"))
                                        {
                                            case 1:
                                                Console.WriteLine("Enter Job Title");
                                                Console.WriteLine(Leave_Console.ReportsDatabaseConnector.getTable(Console.ReadLine()));
                                                break;
                                        }
                                        break;
   [[
                         */

                        case 2:
                            do  {
                                Console.Clear();
                                try {
                                    switch (GetUserChoice("Usage Reports by Leave Type", "Back"))
                                    {
                                        case 1:
                                            validOptionSelected = true;
                                            
                                            Console.WriteLine(Leave_Console.ReportsDatabaseConnector.getTable());
                                            Console.WriteLine("Press any key to continue");
                                            Console.ReadKey();
                                            break;
                                        
                                        case 2:
                                            validOptionSelected = true;
                                            break;
                                        default:
                                            Console.WriteLine("Wrong Input!");
                                            Thread.Sleep(1000);
                                            break;
                                    }
                                }   catch(FormatException e)    {
                                    Console.WriteLine("Wrong Input!");
                                    Thread.Sleep(1000);
                                }   catch(Exception e)  {
                                    Console.WriteLine("Wrong Input!");
                                    Thread.Sleep(1000);
                                }
                            }   while(!validOptionSelected);
                            break;
                        case 3:
                            do  {
                                Console.Clear();
                                try {
                                    switch (GetUserChoice("Leave Periods", "Leave Types", "Work Week", "Holidays","Back"))  {
                                        case 1:
                                            validOptionSelected = true;
                                            Console.WriteLine("Leave Periods will be displayed here.");
                                            Console.Write("Press any key to continue ...");
                                            Console.ReadKey();
                                            break;
                                        case 2:
                                            validOptionSelected = true;
                                            switch (GetUserChoice("View", "Add","Delete"))
                                            {
                                                case 1:
                                                    Console.WriteLine(Leave_Console.Leave_Type_Database_Connector.getTable());
                                                    break;
                                                case 2:
                                                    Leave_Console.Leave_Type_Database_Connector.add(Console.ReadLine());
                                                    break;
                                                case 3:
                                                    Leave_Console.Leave_Type_Database_Connector.Delete(Console.ReadLine());
                                                    break;

                                            }
                                            Console.Write("Press any key to continue ...");
                                            Console.ReadKey();
                                            break;
                                        case 3:
                                            validOptionSelected = true;
                                            switch (GetUserChoice("View", "Change"))
                                            {
                                                case 1:
                                                 Console.WriteLine(Work_Week_Database_Connector.getTable());
                                                 break;
                                                case 2:
                                                 Console.WriteLine("Enter The Work day you want to change :");
                                                 String day = Console.ReadLine();
                                                 Console.WriteLine("Enter Work Day type");
                                                 String workDayType = Console.ReadLine();
                                                 Leave.Modules.Work_Week_Database_Connector.Change(day, workDayType);
                                                 break;
                                                 default:
                                                 Console.WriteLine("Wrong Input");
                                                 break;
                                            }
                                            Console.Write("Press any key to continue ...");
                                            Console.ReadKey();
                                            break;
                                        case 4:
                                            validOptionSelected = true;
                                            switch (GetUserChoice("View", "Add","Delete"))
                                            {
                                                case 1:
                                                    validOptionSelected = true;
                                                    Console.WriteLine(Holiday_Database_Connector.getTableAll());        
                                                    break;
                                                case 2:
                                                    validOptionSelected = true;
                                                    Work_Week_Database_Connector.addTable();
                                                    break;

                                                case 3:
                                                    validOptionSelected = true;
                                                    Console.WriteLine("Enter The Name of the Holiday that you want to delete:");
                                                    string name=Console.ReadLine();
                                                    try
                                                    {
                                                        Holiday_Database_Connector.DeleteTable(name);
                                                    }
                                                    catch (Exception e) { Console.WriteLine("Invalid input"); }
                                                    break;


                                                default:
                                                    Console.WriteLine("Wrong Input!");
                                                    Thread.Sleep(1000);
                                                    break;
                                            }
                                            
                                            Console.Write("Press any key to continue ...");
                                            Console.ReadKey();
                                            break;
                                        case 5:
                                            validOptionSelected = true;
                                            break;
                                        default:
                                            Console.WriteLine("Wrong Input!");
                                            Thread.Sleep(1000);
                                            break;
                                    }
                                }   catch(FormatException e)    {
                                    Console.WriteLine("Wrong Input!");
                                    Thread.Sleep(1000);
                                }   catch(Exception e)  {
                                    Console.WriteLine("Wrong Input!");
                                    Thread.Sleep(1000);
                                }
                            }   while(!validOptionSelected);
                            break;
                        case 4:
                            do  {
                                Console.Clear();
                                try {
                                    switch (GetUserChoice("Back"))  {
                                        case 1:
                                            validOptionSelected = true;
                                            break;
                                        default:
                                            Console.WriteLine("Wrong Input!");
                                            Thread.Sleep(1000);
                                            break;
                                    }
                                }   catch(FormatException e)    {
                                    Console.WriteLine("Wrong Input!");
                                    Thread.Sleep(1000);
                                }   catch(Exception e)  {
                                    Console.WriteLine("Wrong Input!");
                                    Thread.Sleep(1000);
                                }
                            }   while(!validOptionSelected);
                            break;
                        case 5:
                            do  {
                                Console.Clear();
                                try {
                                    switch (GetUserChoice("Back"))  {
                                        case 1:
                                            validOptionSelected = true;
                                            break;
                                        default:
                                            Console.WriteLine("Wrong Input!");
                                            Thread.Sleep(1000);
                                            break;
                                    }
                                }   catch(FormatException e)    {
                                    Console.WriteLine("Wrong Input!");
                                    Thread.Sleep(1000);
                                }   catch(Exception e)  {
                                    Console.WriteLine("Wrong Input!");
                                    Thread.Sleep(1000);
                                }
                            }   while(!validOptionSelected);
                            break;
                        case 6:
                            executionEnded = true;
                            break;
                        default:
                            Console.WriteLine("Wrong Input!");
                            Thread.Sleep(1000);
                            break;

                    }
                }   catch(FormatException e)    {
                    Console.WriteLine("Wrong Input!");
                    Thread.Sleep(1000);
                }   catch(Exception e)  {
                    Console.WriteLine(e.Message);
                    Thread.Sleep(1000);
                }
            }   while(!executionEnded);
        }

        private int GetUserChoice(params string[] choices) {
            for(int i = 0; i < choices.Length; i++)
                Console.WriteLine((i + 1) + "\t" + choices[i]);
            Console.Write("\nChoice\t");
            return int.Parse(Console.ReadLine());
        }
    }
}