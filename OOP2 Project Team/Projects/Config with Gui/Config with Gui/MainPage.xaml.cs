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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    /// 

    public partial class MainPage : Page
    {
        public static Frame page_container;

        public MainPage()
        {
            InitializeComponent();
            page_container = Container;
        }

        public void Leave_Entitlements_Event(object sender, RoutedEventArgs e)
        {
            Container.Navigate(new EntitlementsHandler(Container.Height, Container.Width));
        }

        public void Leave_Add_Entitlements_Event(object sender, RoutedEventArgs e)
        {
            Container.Navigate(new EntitlementsAdder(Container.Height, Container.Width));
        }

        public void Leave_Configure_Event(object sender, RoutedEventArgs e)
        {
            Page page = new config().getPage(Container.Height, Container.Width);
            Container.Navigate(page);
        }

        public void Leave_Leave_List_Event(object sender, RoutedEventArgs e)
        {
            Page page = new Leave_Lists().getPage(Container.Height, Container.Width);
            Container.Navigate(page);
        }

        public void Leave_Period_Event(object sender, RoutedEventArgs e)
        {
            Page page = new leave_period().getPage(Container.Height, Container.Width);
            Container.Navigate(page);
        }

        public void Leave_Types_Event(object sender, RoutedEventArgs e)
        {
            Page page = new leave_types().getPage(Container.Height, Container.Width);
            Container.Navigate(page);
        }

        public void Leave_Work_Week_Event(object sender, RoutedEventArgs e)
        {
            Page page = new Work_week().getPage(Container.Height, Container.Width);
            Container.Navigate(page);
        }

        public void Leave_Holiday_Event(object sender, RoutedEventArgs e)
        {
            Page page = new Holidays().getPage(Container.Height, Container.Width);
            Container.Navigate(page);
        }

        public void Add_Entitlements_Event(object sender, RoutedEventArgs e)
        {

            Page page = new EntitlementsAdder(Container.Height, Container.Width);
            Container.Navigate(page);
        }

        public void Leave_Reports_Event(object sender, RoutedEventArgs e)
        {

            try
            {
                Page page = new Reports().getPage(Container.Height, Container.Width);
                Container.Navigate(page);
            }
            catch (Exception excep) { MessageBox.Show(excep.ToString()); }
        }

        public void Assign_Leave_Event(object sender, RoutedEventArgs e)
        {
            Page page = new Assign_Leave().getPage(Container.Height, Container.Width);
            Container.Navigate(page);
        }
    }
}
