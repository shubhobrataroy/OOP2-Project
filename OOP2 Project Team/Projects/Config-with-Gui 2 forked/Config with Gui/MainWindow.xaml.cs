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


namespace Config_with_Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public static Frame page_container;

        public MainWindow()
        {
            InitializeComponent();
            string connectionString = @"Data Source=SHUBHO\SQLEXPRESS;Initial Catalog=Leave;Integrated Security=True";
			Leave.Modules.Holiday_Database_Connector.connection_string2 = connectionString;
			Leave.Modules.Entitlements.connectionString = connectionString;
            page_container = Frame;
            //	Frame.Navigate(new MainPage());
            Frame.Navigate(new LoginPage(this));
            
         
        }
    }
}
