using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Collections.ObjectModel;
using Leave.Modules;

namespace Config_with_Gui
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    /// 


    public partial class Reports : Page
    {
        private String pastEmployee="No";
        bool init = false;
        public Reports()
        {
            InitializeComponent();
            init = true;
            Reports_Database_Connector.SetCombobox(leaveTypeComboBox, "LeaveTypes", 0);
            Reports_Database_Connector.SetCombobox(jobTitleComboBox, "JobTitles", 0);
            
        }




        public Page getPage() { return reports_page; }

        public Page getPage(double p1, double p2)
        {
            this.Height = p1;
            this.Width = p2;
            return this;
        }


        private void ViewClicked(object sender, RoutedEventArgs e)
        {
            using (Leave.DataClients.SqlTableTracker test = new Leave.DataClients.SqlTableTracker(Leave.Modules.Holiday_Database_Connector.connection_string2, "Employees" + ",LeaveList"))
            {
                if (generateComboBox.Text != "Employee")
                {
                    if (pastEmployee != "Yes")
                    {
                        /*
                        DataTable Table2 = test.SelectSpecific("EmployeeID,EmployeeName", "Designation ='" + jobTitleComboBox.Text + "'" + " and PastEmployee='" + pastEmployee + "'");
                        String command = "LeaveType,LeavingDate,JoiningDate,Balance,DaysCount";
                        StringBuilder rowconstraints = new StringBuilder("EmployeeID in(");
                        int i = 0;

                        foreach (DataRow row in Table2.Rows)
                        {
                            if (i == 0)
                            {
                                rowconstraints.Append(row["EmployeeID"].ToString());
                                i++;
                            }
                            else
                                rowconstraints.Append("," + row["EmployeeID"].ToString());

                        }
                        rowconstraints.Append(")");
                        MessageBox.Show(rowconstraints.ToString());
                        DataTable finalTable = Table2.Copy();
                        
                       
                        //Holiday_Database_Connector.ResizeMyTable(Table);*/
                        if (jobTitleComboBox.Text != "All")
                        {
                            if (leaveTypeComboBox.Text != "All")
                                Table.ItemsSource = test.SelectSpecific("Employees.EmployeeName,LeaveType,LeavingDate,JoiningDate,Balance,DaysCount", "Employees.EmployeeID=LeaveList.EmployeeID" + " and Designation ='" + jobTitleComboBox.Text + "'" + " and PastEmployee ='No'" + " and LeaveType='" + leaveTypeComboBox.Text + "'").AsDataView();
                            else
                                Table.ItemsSource = test.SelectSpecific("Employees.EmployeeName,LeaveType,LeavingDate,JoiningDate,Balance,DaysCount", "Employees.EmployeeID=LeaveList.EmployeeID" + " and Designation ='" + jobTitleComboBox.Text + "'" + " and PastEmployee ='No'").AsDataView();
                        }
                        else
                        {
                            if (leaveTypeComboBox.Text != "All")
                                Table.ItemsSource = test.SelectSpecific("Employees.EmployeeName,LeaveType,LeavingDate,JoiningDate,Balance,DaysCount", "Employees.EmployeeID=LeaveList.EmployeeID" + " and PastEmployee ='No'" + " and LeaveType='" + leaveTypeComboBox.Text + "'").AsDataView();
                            else
                                Table.ItemsSource = test.SelectSpecific("Employees.EmployeeName,LeaveType,LeavingDate,JoiningDate,Balance,DaysCount", "Employees.EmployeeID=LeaveList.EmployeeID" + " and PastEmployee ='No'").AsDataView();
                        }
                        Holiday_Database_Connector.ResizeMyTable(Table);
                    }
                    else
                    {
                        if (jobTitleComboBox.Text != "All")
                        {
                            if (leaveTypeComboBox.Text != "All")
                                Table.ItemsSource = test.SelectSpecific("Employees.EmployeeName,LeaveType,LeavingDate,JoiningDate,Balance,DaysCount", "Employees.EmployeeID=LeaveList.EmployeeID" + " and Designation ='" + jobTitleComboBox.Text + "'" + " and LeaveType='" + leaveTypeComboBox.Text + "'").AsDataView();
                            else
                                Table.ItemsSource = test.SelectSpecific("Employees.EmployeeName,LeaveType,LeavingDate,JoiningDate,Balance,DaysCount", "Employees.EmployeeID=LeaveList.EmployeeID" + " and Designation ='" + jobTitleComboBox.Text + "'").AsDataView();
                        }
                        else
                        {
                            if (leaveTypeComboBox.Text != "All")
                                Table.ItemsSource = test.SelectSpecific("Employees.EmployeeName,LeaveType,LeavingDate,JoiningDate,Balance,DaysCount", "Employees.EmployeeID=LeaveList.EmployeeID" + " and LeaveType='" + leaveTypeComboBox.Text + "'").AsDataView();
                            else
                                Table.ItemsSource = test.SelectSpecific("Employees.EmployeeName,LeaveType,LeavingDate,JoiningDate,Balance,DaysCount", "Employees.EmployeeID=LeaveList.EmployeeID").AsDataView();
                            Holiday_Database_Connector.ResizeMyTable(Table);
                        }
                    }
                }
                else Table.ItemsSource = test.SelectSpecific("Employees.EmployeeName,LeaveType,LeavingDate,JoiningDate,Balance,DaysCount", "Employees.EmployeeID=LeaveList.EmployeeID" +  " and EmployeeName='" + employeeTextBox.Text + "'").AsDataView();
                Holiday_Database_Connector.ResizeMyTable(Table);
            }
             
        }

        private void defaultSelected(object sender, RoutedEventArgs e)
        {
            if (init)
            {
                employeeLabel.Visibility = Visibility.Collapsed;
                employeeTextBox.Visibility = Visibility.Collapsed;

                leaveTypeLabel.Visibility = Visibility.Collapsed;
                leaveTypeComboBox.Visibility = Visibility.Collapsed;

                

                jobTitleLabel.Visibility = Visibility.Collapsed;
                jobTitleComboBox.Visibility = Visibility.Collapsed;

                checkBox.Visibility = Visibility.Collapsed;
                viewButton.Visibility = Visibility.Collapsed;
            }
        }


        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            this.pastEmployee = "Yes"; 
        }

        private void HandleUnchecked(object sender, RoutedEventArgs e)
        {
            this.pastEmployee = "No";
        }

        private void leaveTypeSelected(object sender, RoutedEventArgs e)
        {

            employeeLabel.Visibility = Visibility.Collapsed;
            employeeTextBox.Visibility = Visibility.Collapsed;


            leaveTypeLabel.Visibility = 0;
            leaveTypeComboBox.Visibility = 0;

           

            jobTitleLabel.Visibility = 0;
            jobTitleComboBox.Visibility = 0;

          
            checkBox.Visibility = 0;

            viewButton.Visibility = 0;

        }

        private void employeeSelected(object sender, RoutedEventArgs e)
        {
            leaveTypeLabel.Visibility = Visibility.Collapsed;
            leaveTypeComboBox.Visibility = Visibility.Collapsed;

           

            jobTitleLabel.Visibility = Visibility.Collapsed;
            jobTitleComboBox.Visibility = Visibility.Collapsed;

    
            checkBox.Visibility = Visibility.Collapsed;


            employeeLabel.Visibility = 0;
            employeeTextBox.Visibility = 0;

           

            viewButton.Visibility = 0;
        }

        private void employeeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
