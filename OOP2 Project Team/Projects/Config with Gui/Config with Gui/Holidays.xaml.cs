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
    /// Interaction logic for Holidays.xaml
    /// </summary>
    public partial class Holidays : Page
    {
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

        }
    }
}
