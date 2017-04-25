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
    /// Interaction logic for Assign_Leave.xaml
    /// </summary>
    public partial class Assign_Leave : Page
    {
        public Assign_Leave()
        {
            InitializeComponent();
        }

        public Page getPage() { return Aleave; }

        public Page getPage(double p1, double p2)
        {
            Aleave.Height = p1;
            Aleave.Width = p2;
            return Aleave;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
