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
    /// Interaction logic for View_Application.xaml
    /// </summary>
    public partial class ViewApplication : Page
    {
		private UIElement baseControl;
		private DataTable data;
		private int index;

        public ViewApplication( UIElement baseControl )
        {
            InitializeComponent();
			this.baseControl = baseControl;
			this.Height = ( baseControl as MainPage ).Container.Height;
			this.Width	= ( baseControl as MainPage ).Container.Width;
			data = new DataTable( );
        }

		private void Page_Loaded(object sender, RoutedEventArgs e)	{
			Reload( );
		}

		private void Reload( )	{
			string rowConstraints = "";
			MainPage mainPage =	baseControl as MainPage;
			if( mainPage.UserRole == "USER" )	{
				rowConstraints = "EmployeeID = " + mainPage.EmployeeID;
				btn_accept.Visibility = btn_reject.Visibility = Visibility.Collapsed;
			}
 			data = LeaveApplications.FetchApplications( rowConstraints );
			if( data.Rows.Count == 0 )	{
				lb_message.Visibility = Visibility.Visible;
				subGrid.Visibility = Visibility.Collapsed;
			}	else	{
				lb_message.Visibility = Visibility.Collapsed;
				subGrid.Visibility = Visibility.Visible;
				tb_employeeID.Text = data.Rows[index][0].ToString( );
				tb_employeeName.Text = Employees.GetSpecificEmployeeInfo( "EmployeeName", "EmployeeID = " + tb_employeeID.Text ).Rows[0][0].ToString( );
				tb_leaveType.Text = data.Rows[index][1].ToString( );
				tb_leavingDate.Text = data.Rows[index][2].ToString( );
				tb_joiningDate.Text = data.Rows[index][3].ToString( );
				tb_description.Text = data.Rows[index][4].ToString( );
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if( sender == btn_next )	{
				index = index + 1 >= data.Rows.Count ? 0 : index + 1;
			}	else if( sender == btn_prev )	{
				index = index <= 0 ? data.Rows.Count - 1 : index - 1;
			}	else if( sender == btn_reject )	{
				string rowConstraints = "EmployeeID = " + tb_employeeID.Text + " AND LeaveType = '" + tb_leaveType.Text.Replace( "'" , "''" ) + "' AND LeavingDate = '" + tb_leavingDate.Text + "' AND JoiningDate = '" + tb_joiningDate.Text + "' AND LeaveDescription = '" + tb_description.Text.Replace( "'", "''" ) + "'";
				LeaveApplications.RejectApplications( rowConstraints );
				if( index + 1 == data.Rows.Count ) index--;
			}	else	{
				MainPage mainPage = baseControl as MainPage;
				LeaveAssignmentPage leaveAssignmentPage = mainPage.LeaveAssignmentPage;
				mainPage.JobStack.Push( this );
				mainPage.Container.Navigate( leaveAssignmentPage );
				leaveAssignmentPage.icb_employees.Text  = tb_employeeName.Text;
				leaveAssignmentPage.icb_employeeID.Text = tb_employeeID.Text;
				leaveAssignmentPage.icb_leaveTypes.Text = tb_leaveType.Text;
				leaveAssignmentPage.dp_leavingDate.Text = tb_leavingDate.Text;
				leaveAssignmentPage.dp_joiningDate.Text = tb_joiningDate.Text;
			//	(baseControl as MainPage).LeaveAssignmentPage.AssignLeave( tb_employeeID.Text, tb_leaveType.Text, new DateString( tb_leavingDate.Text ).ToDateTime( ), new DateString( tb_joiningDate.Text ).ToDateTime( ));
				index = 0;
				return;
			}
			Reload( );
		}
    }
}