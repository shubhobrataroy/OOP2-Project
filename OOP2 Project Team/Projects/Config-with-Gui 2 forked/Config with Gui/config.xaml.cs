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
    /// Interaction logic for config.xaml
    /// </summary>
    public partial class config : Page
    {
        public config()
        {
            InitializeComponent();
        }

        public Page getPage() { return Config; } //The Name of my page is Config

        public Page getPage(double p1, double p2)
        {
            Config.Height = p1;
            Config.Width = p2;
            return Config;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page page = new leave_period().getPage(MainWindow.page_container.Height, MainWindow.page_container.Width);
            MainWindow.page_container.Navigate(page);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
