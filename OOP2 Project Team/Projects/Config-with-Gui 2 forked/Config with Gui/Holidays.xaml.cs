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
using Leave.Modules;

namespace Config_with_Gui
{
    /// <summary>
    /// Interaction logic for Holidays.xaml
    /// </summary>
    public partial class Holidays : Page
    {
        private DataView lastTable; 
        public Holidays()
        {
           InitializeComponent();
           
           
        }

        public Page getPage(double p1, double p2)
        {
            leave_holidays.Height = p1;
            leave_holidays.Width = p2;
            return leave_holidays;
           
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lastTable = Holiday_Database_Connector.GetTable().AsDataView();
            holiday_table.ItemsSource = lastTable;
            
            foreach (DataGridColumn column in holiday_table.Columns)
                column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
        }

        private void Cellchanged(object sender, EventArgs e)
        {
            int where_is_user_now = holiday_table.CurrentColumn.DisplayIndex;

            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StringBuilder command = new StringBuilder();
            for (int i = 0; i < holiday_table.SelectedItems.Count; i++)
            {
                if (i > 0) command.Append(" OR ");
                command.Append("(Name = '" + (holiday_table.SelectedItems[i] as DataRowView)[0] + "')");
            }
            Holiday_Database_Connector.DeleteTable(command.ToString());
            
            lastTable = Holiday_Database_Connector.GetTable().AsDataView();
            holiday_table.ItemsSource = lastTable;

            foreach (DataGridColumn column in holiday_table.Columns)
                column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
        }


        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            Holiday_Add adder = new Holiday_Add(Holiday_Database_Connector.connection_string2);
            adder.ShowDialog();
        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {
            DateTime? startdate = startDate.SelectedDate;
            DateTime? enddate = EndDate.SelectedDate;

            lastTable= Holiday_Database_Connector.SearchByDate(startdate.ToString(), enddate.ToString()).AsDataView();
            holiday_table.ItemsSource = lastTable;
            foreach (DataGridColumn column in holiday_table.Columns)
                column.Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
        }
    }
}
