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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
       

        public Login()
        {
            InitializeComponent();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username=userName.Text;
            string password = Password.Password;

            if(login_connector.GetAuthentication(username,password)==true)
            {
               MainWindow.page_container.Navigate(new MainPage().getPage());
            }

            else { MessageBox.Show("Invalid Username / Password"); }
        }
    }
}
