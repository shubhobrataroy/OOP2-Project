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
    /// Interaction logic for leave_types.xaml
    /// </summary>
    public partial class leave_types : Page
    {
        public leave_types()
        {
            InitializeComponent();
        }

        public Page getPage(double p1, double p2)
        {
            leave_types_page.Height = p1;
            leave_types_page.Width = p2;
            return leave_types_page;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
