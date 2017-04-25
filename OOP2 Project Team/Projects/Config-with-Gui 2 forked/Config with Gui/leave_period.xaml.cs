using Leave.Supplements;
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
    /// Interaction logic for leave_period.xaml
    /// </summary>
    public partial class leave_period : Page
    {
        public leave_period()
        {
            InitializeComponent();
            int i = 0;
        }

        public Page getPage(double p1, double p2)
        {
            leave_period_page.Height = p1;
            leave_period_page.Width = p2;
            return leave_period_page;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //DateTime? d1 = startdate.SelectedDate;
            //DateTime? d2 = enddate.SelectedDate;
            
           //LeavePeriods.AddLeavePeriod(d1.ToString(), d2.ToString());
            labelMain.Content = startdate.Text + " to " + enddate.Text;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        
    }
}
