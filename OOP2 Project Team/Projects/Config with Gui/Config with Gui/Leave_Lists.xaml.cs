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
    /// Interaction logic for Leave_Lists.xaml
    /// </summary>
    public partial class Leave_Lists : Page
    {
        public Leave_Lists()
        {
            InitializeComponent();
        }

        public Page getPage() { return leave; } 

        public Page getPage(double p1, double p2)
        {
            leave.Height = p1;
            leave.Width = p2;
            return leave;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void gridSplitter_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {

        }

        private void dataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Container_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
