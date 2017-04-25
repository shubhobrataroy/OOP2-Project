using System;
using System.Data;
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
using Leave;
using Leave.Modules;
using Leave.Supplements;

namespace Config_with_Gui
{
    /// <summary>
    /// Interaction logic for SendApplication.xaml
    /// </summary>
    public partial class SendApplication : Page
    {
		private UIElement baseControl;

        public SendApplication( UIElement baseControl )
        {
            InitializeComponent();
			this.baseControl = baseControl;
			this.Height = ( baseControl as MainPage ).Container.Height;
			this.Width = ( baseControl as MainPage ).Container.Width;
        }

		private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)	{
			if( dp_leavingDate.SelectedDate == null || dp_joiningDate.SelectedDate == null ) return;
			if( dp_joiningDate.SelectedDate.Value <= dp_leavingDate.SelectedDate.Value )	{
				lb_message.Visibility = System.Windows.Visibility.Visible;
			}	else	{
				lb_message.Visibility = System.Windows.Visibility.Collapsed;
			}
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)	{
			icb_leaveType.Data = LeaveTypes.GetLeaveTypes( );
		}

		private void Button_Click(object sender, RoutedEventArgs e)	{
			if( icb_leaveType.Text.Length == 0 || dp_leavingDate.SelectedDate == null || dp_joiningDate.SelectedDate == null )	{
				MessageBox.Show("Please fill in the required details.", "Incomplete", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}
			if( lb_message.Visibility == System.Windows.Visibility.Visible )	{
				MessageBox.Show( lb_message.Content + "!", "Error", MessageBoxButton.OK, MessageBoxImage.Error );
				return;
			}
			DateTime appliedLeavingDate = dp_leavingDate.SelectedDate.Value;
			DateTime appliedJoiningDate = dp_joiningDate.SelectedDate.Value;
			if( appliedLeavingDate < DateTime.Today )	{
				MessageBox.Show( "Leaving date cannot be a past date.", "Date Past", MessageBoxButton.OK, MessageBoxImage.Error );
				return;
			}
			MainPage mainPage = baseControl as MainPage;
			string columns = "LeavingDate, JoiningDate";
			string rowConstraints = "EmployeeID = " + mainPage.EmployeeID;
			DataTable data = LeaveList.GetSpecificLeaveInfo( columns, rowConstraints );
			string clashInfo = "";
			DateString currentLeavingDate = new DateString( );
			DateString currentJoiningDate = new DateString( );
			foreach(  DataRow row in data.Rows )	{
				currentLeavingDate = row[0].ToString( );
				currentJoiningDate = row[1].ToString( );

				if(	appliedLeavingDate >= currentLeavingDate && appliedLeavingDate <= currentJoiningDate	||
					appliedJoiningDate >= currentLeavingDate && appliedJoiningDate <= currentJoiningDate	||
					appliedLeavingDate <  currentLeavingDate && appliedJoiningDate >  currentJoiningDate	)
				{
					clashInfo = "You are already assigned a leaving date of " + currentLeavingDate + " and a joining date of " + currentJoiningDate;
					break;
				}
			}
			if( clashInfo.Length > 0 )	{
				MessageBox.Show( clashInfo );
				return;
			}
			mainPage.LeaveAssignmentPage.Load( );
			mainPage.LeaveAssignmentPage.icb_employeeID.Text = mainPage.EmployeeID;
			mainPage.LeaveAssignmentPage.icb_leaveTypes.Text = icb_leaveType.Text;
			mainPage.LeaveAssignmentPage.dp_leavingDate.SelectedDate = dp_leavingDate.SelectedDate;
			mainPage.LeaveAssignmentPage.dp_joiningDate.SelectedDate = dp_joiningDate.SelectedDate;
			object balance = mainPage.LeaveAssignmentPage.lb_balance.Content;
			if( balance.ToString( ).Length == 0 || balance.ToString( ) == "!!" || double.Parse( balance.ToString( )) < 0 )	{
				if( MessageBox.Show( "Application might not be granted due to balance issues.\nDo you still wish to continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning ) == MessageBoxResult.No )	{
					return;
				}
			}
			LeaveApplications.SendApplication( mainPage.EmployeeID, icb_leaveType.Text.Replace( "'", "''" ), dp_leavingDate.SelectedDate.Value, dp_joiningDate.SelectedDate.Value, tb_description.Text.Replace( "'", "''" ) );
			MessageBox.Show( "Application sent!", "Success", MessageBoxButton.OK, MessageBoxImage.Information );
		}
    }
}