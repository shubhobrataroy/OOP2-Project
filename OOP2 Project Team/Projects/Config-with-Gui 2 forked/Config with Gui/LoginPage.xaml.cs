using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Leave.DataClients;
using Leave.Supplements;

namespace Config_with_Gui {
	/// <summary>
	/// Interaction logic for Page1.xaml
	/// </summary>
	public partial class LoginPage : Page
	{
		private UIElement baseControl;

		public LoginPage(UIElement baseControl)	{
			InitializeComponent();
			this.baseControl =  baseControl;
			MainWindow mainWindow = baseControl as MainWindow;
			this.Height = mainWindow.Frame.Height;
			this.Width	= mainWindow.Frame.Width;
			tb_username.Focus();
            (BaseControl as MainWindow).Frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
		}

		public UIElement BaseControl { get { return baseControl; } }

	//	Hide lable "Username Here ..." when user is typing
		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)	{
			if((sender as TextBox).Text.Length > 0) lb_username.Visibility = System.Windows.Visibility.Hidden;
			else lb_username.Visibility = System.Windows.Visibility.Visible;
		}

	//	Hide lable "Password Here ..." when user is typing
		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)	{
			if((sender as PasswordBox).Password.Length > 0) lb_password.Visibility = System.Windows.Visibility.Hidden;
			else lb_password.Visibility = System.Windows.Visibility.Visible;
		}

	//	Automate the Login by allowing the user to press ENTER
		private void Box_KeyDown(object sender, KeyEventArgs e) {
			if(e.Key == Key.Enter)	{
				if(sender is TextBox && pb_password.Password.Length == 0)	{
					pb_password.Focus();
				}	else	{
					Button_Click(btn_Login, new RoutedEventArgs());
				}
			}
		}

	/*
	 *	Button_Click Event Handler
	 *	Works when user presses "Login"
	 *	Automatically called when user presses ENTER
	 */

		private void Button_Click(object sender, RoutedEventArgs e)	{
		//	Specify user
			DataRowCollection userInfo = Users.GetAllUserInfo("UserName = '" + tb_username.Text + "'").Rows;
		//	Check username
			if(userInfo.Count == 1 && userInfo[0][0] as string == tb_username.Text)	{
			//	Check password
				if(userInfo[0][1] as string == pb_password.Password)	{
				//	Navigate to the MainPage when authentication is complete
					(baseControl as MainWindow).Frame.Navigate( new MainPage( baseControl, tb_username.Text, pb_password.Password, userInfo[0][2] as string, userInfo[0][3].ToString( )));
				}	else	{
				//	Alert for wrong password
					MessageBox.Show("Invalid password for " + tb_username.Text + "!");
				}
			}	else	{
				//	Alert for wrong username
					MessageBox.Show("Invalid username!");
			}
		}
	}
}
