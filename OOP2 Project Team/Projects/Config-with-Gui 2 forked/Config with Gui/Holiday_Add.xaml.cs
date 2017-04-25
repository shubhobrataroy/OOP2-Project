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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace Config_with_Gui
{
    /// <summary>
    /// Interaction logic for Holiday_Add.xaml
    /// </summary>
    public partial class Holiday_Add : Window
    {
        private String connection_string;
        public Holiday_Add(string connection_string)
        {
            InitializeComponent();
            this.connection_string = connection_string;
            ObservableCollection<string> list = new ObservableCollection<string>();

            for (int i = 2016; i <= 2065; i++)
                list.Add(i.ToString());
            Year.ItemsSource = list;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Name.Text==null || Day.SelectedIndex<=-1 || Month.SelectedIndex<=-1 || Year.SelectedIndex<=-1|| HoliDayType.SelectedIndex<=-1 || RepeatsAnnually.SelectedIndex<=-1)
            {
                MessageBox.Show("Sorry Wrong Input. Please recheck and try again");
                
            }

            else
            {
                try
                {
                    //string connection_string2 = @"Data Source=Shubho,1234;Initial Catalog=Leave;User ID=group_leave;Password=oop2";

                    SqlConnection myConnection = new SqlConnection(connection_string);
                    myConnection.Open();

                    int month = Month.SelectedIndex, day = Day.SelectedIndex, year = 2016 + Year.SelectedIndex;

                    SqlCommand command = new SqlCommand("insert into Holidays values ('" + Name.Text + "','" + (month + 1) + "-" + (day + 1) + "-" + (year + 1) + "','" + HoliDayType.Text + "','" + RepeatsAnnually.Text + "')", myConnection);

                    command.ExecuteNonQuery();
                    myConnection.Close();
                    Status.Content = "Added Successfully";
                }
                catch (Exception er) { MessageBox.Show("Wrong input please check the date and month / Connection Problem"); Status.Content = "Failed"; }
            }
        }
    }
}
