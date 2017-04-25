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
    /// Interaction logic for Work_week.xaml
    /// </summary>
    public partial class Work_week : Page
    {
        public Work_week()
        {
            InitializeComponent();
        }

        public Page getPage(double p1, double p2)
        {
            work_week_page.Height = p1;
            work_week_page.Width = p2;
            return work_week_page;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
