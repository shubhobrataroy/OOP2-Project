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
using System.Windows.Shapes;

namespace Config_with_Gui
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           try
            {
                using (Leave.DataClients.SqlTableTracker Databasetest = new Leave.DataClients.SqlTableTracker(Leave.Modules.Holiday_Database_Connector.connection_string2, "LeaveTypes"))
                {
                    Databasetest.Insert( "'"+LeaveType.Text+"'","LeaveType");
                }
                Status.Content = "Success";
                MainPage.page_container.Navigate(new leave_types());
           }
            catch (Exception ex) { Status.Content = "Failed"; }
        }
    }
}
