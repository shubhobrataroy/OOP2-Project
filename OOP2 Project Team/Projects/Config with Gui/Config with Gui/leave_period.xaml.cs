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
        }

        public Page getPage(double p1, double p2)
        {
            leave_period_page.Height = p1;
            leave_period_page.Width = p2;
            return leave_period_page;
        }
    }
}
