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
using Leave.Modules;

namespace Config_with_Gui
{
    /// <summary>
    /// Interaction logic for Work_week.xaml
    /// </summary>
    public partial class Work_week : Page
    {
        private List<ComboBox> changedItems;
        private List<int> changedItemsID;
        public Work_week()
        {
            InitializeComponent();
            changedItems = new List<ComboBox>();
            changedItemsID = new List<int>();
            Sat.Text = Work_Week_Database_Connector.GetCurrentSatusByID(0,false);
            Sun.Text = Work_Week_Database_Connector.GetCurrentSatusByID(1,false);
            Mon.Text = Work_Week_Database_Connector.GetCurrentSatusByID(2,false);
            Tue.Text = Work_Week_Database_Connector.GetCurrentSatusByID(3,false);
            Wed.Text = Work_Week_Database_Connector.GetCurrentSatusByID(4,false);
            Thu.Text = Work_Week_Database_Connector.GetCurrentSatusByID(5,false);
            Fri.Text = Work_Week_Database_Connector.GetCurrentSatusByID(6,true);
        }

        public Page getPage(double p1, double p2)
        {
            work_week_page.Height = p1;
            work_week_page.Width = p2;
            return work_week_page;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                foreach (int i in changedItemsID)
                {
                    Work_Week_Database_Connector.update(changedItemsID[i], changedItems[i].Text);
                }
                Status.Content = "Saved Successfully";
            }
            catch (Exception ex) { Status.Content = "Failed"; }
        }

        private void Button_Focus_Lost(object sender, RoutedEventArgs e)
        {
            Status.Content = "";
        }

        private void Sat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changedItems.Add((ComboBox)sender);
            changedItemsID.Add(0);
        }

        private void Sun_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changedItems.Add((ComboBox)sender);
            changedItemsID.Add(1);
        }

        private void Mon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changedItems.Add((ComboBox)sender);
            changedItemsID.Add(2);
        }

        private void Tue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changedItems.Add((ComboBox)sender);
            changedItemsID.Add(3);
        }

        private void Wed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changedItems.Add((ComboBox)sender);
            changedItemsID.Add(4);
        }

        private void Thu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changedItems.Add((ComboBox)sender);
            changedItemsID.Add(5);

        }

        private void Fri_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changedItems.Add((ComboBox)sender);
            changedItemsID.Add(6);
        }
    }
}
