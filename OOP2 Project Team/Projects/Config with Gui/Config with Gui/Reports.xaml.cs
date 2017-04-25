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
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Page
    {
        static bool init = false;
        public Reports()
        {
            InitializeComponent();
            init = true;
        }

        public Page getPage() { return reports_page; }

        public Page getPage(double p1, double p2)
        {
            this.Height = p1;
            this.Width = p2;
            return this;
        }

        private void defaultSelected(object sender, RoutedEventArgs e)
        {
            if (init)
            {
				employeeLabel.Visibility = Visibility.Collapsed;
                employeeTextBox.Visibility = Visibility.Collapsed;

                leaveTypeLabel.Visibility = Visibility.Collapsed;
                leaveTypeComboBox.Visibility = Visibility.Collapsed;

                fromLabel.Visibility = Visibility.Collapsed;
                fromComboBox.Visibility = Visibility.Collapsed;

                jobTitleLabel.Visibility = Visibility.Collapsed;
                jobTitleComboBox.Visibility = Visibility.Collapsed;

                locationLabel.Visibility = Visibility.Collapsed;
                locationComboBox.Visibility = Visibility.Collapsed;

                subUnitLabel.Visibility = Visibility.Collapsed;
                subUnitComboBox.Visibility = Visibility.Collapsed;

                checkBox.Visibility = Visibility.Collapsed;

                viewButton.Visibility = Visibility.Collapsed;
            }
        }

        private void leaveTypeSelected(object sender, RoutedEventArgs e)
        {

            employeeLabel.Visibility = Visibility.Collapsed;
            employeeTextBox.Visibility = Visibility.Collapsed;


            leaveTypeLabel.Visibility = 0;
            leaveTypeComboBox.Visibility = 0;

            fromLabel.Visibility = 0;
            fromComboBox.Visibility = 0;

            jobTitleLabel.Visibility = 0;
            jobTitleComboBox.Visibility = 0;

            locationLabel.Visibility = 0;
            locationComboBox.Visibility = 0;

            subUnitLabel.Visibility = 0;
            subUnitComboBox.Visibility = 0;

            checkBox.Visibility = 0;

            viewButton.Visibility = 0;

        }

        private void employeeSelected(object sender, RoutedEventArgs e)
        {
            leaveTypeLabel.Visibility = Visibility.Collapsed;
            leaveTypeComboBox.Visibility = Visibility.Collapsed;

            fromLabel.Visibility = Visibility.Collapsed;
            fromComboBox.Visibility = Visibility.Collapsed;

            jobTitleLabel.Visibility = Visibility.Collapsed;
            jobTitleComboBox.Visibility = Visibility.Collapsed;

            locationLabel.Visibility = Visibility.Collapsed;
            locationComboBox.Visibility = Visibility.Collapsed;

            subUnitLabel.Visibility = Visibility.Collapsed;
            subUnitComboBox.Visibility = Visibility.Collapsed;

            checkBox.Visibility = Visibility.Collapsed;


            employeeLabel.Visibility = 0;
            employeeTextBox.Visibility = 0;

            fromLabel.Visibility = 0;
            fromComboBox.Visibility = 0;

            viewButton.Visibility = 0;
        }
    }
}
