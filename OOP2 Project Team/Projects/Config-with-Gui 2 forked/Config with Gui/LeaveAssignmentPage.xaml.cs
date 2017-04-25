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
	/// Interaction logic for LeaveAssignmentPage.xaml
	/// </summary>
	public partial class LeaveAssignmentPage : Page
	{
		private UIElement baseControl;
		private double lostBalance = 0;

		public LeaveAssignmentPage(UIElement baseControl)	{
			InitializeComponent();
			this.baseControl = baseControl;
			MainPage mainPage = baseControl as MainPage;
			this.Height = mainPage.Container.Height;
			this.Width	= mainPage.Container.Width;
		}

		public UIElement BaseControl { get { return baseControl; } }

		private void Page_Loaded(object sender, RoutedEventArgs e)	{

			//	if user is not logged
			//	return to the login page
			//	else load required data
			//	and continue with the current view

			MainPage mainPage = baseControl as MainPage;
			if( ! mainPage.UserIsLoggedIn )	{
				( mainPage.BaseControl as MainWindow ).Frame.Navigate( new LoginPage( mainPage.BaseControl ) );
				return;
			}
			Stack<Page> jobStack = mainPage.JobStack;
			if( jobStack.Count != 0 && jobStack.Peek( ) != mainPage.ViewApplication )
				jobStack.Clear( );
			icb_employees.Data	= Entitlements.GetSpecificDistributedEntitlementInfo("DISTINCT Employees.EmployeeName");
			icb_leaveTypes.Data	= Entitlements.GetSpecificEntitlementInfo("DISTINCT LeaveType");
			lb_balance.Content = GetBalance( );
		}

		public void Load( )	{
			Page_Loaded( this, new RoutedEventArgs( ) );
		}

		private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)	{
			if( dp_leavingDate.SelectedDate != null && dp_joiningDate.SelectedDate != null && dp_leavingDate.SelectedDate.Value >= dp_joiningDate.SelectedDate.Value )	{
				lb_message.Visibility = System.Windows.Visibility.Visible;
				lb_from.Visibility = System.Windows.Visibility.Collapsed;
				lb_to.Visibility = System.Windows.Visibility.Collapsed;
				cb_from.Visibility = System.Windows.Visibility.Collapsed;
				cb_to.Visibility = System.Windows.Visibility.Collapsed;
			}	else	{
				lb_message.Visibility = System.Windows.Visibility.Collapsed;
				lb_from.Visibility = System.Windows.Visibility.Visible;
				lb_to.Visibility = System.Windows.Visibility.Visible;
				cb_from.Visibility = System.Windows.Visibility.Visible;
				cb_to.Visibility = System.Windows.Visibility.Visible;
			}
			lb_balance.Content = GetBalance( );
		}
		
		private void ComboBox_SelectionChanged( object sender, SelectionChangedEventArgs e )	{
			lb_balance.Content = GetBalance( );
		}

		private void ComboBox_Text_Changed(object sender, TextChangedEventArgs e)	{
			( sender as IntelComboBox ).ViewMatches( );
			icb_employeeID.Data = Entitlements.GetSpecificDistributedEntitlementInfo("Entitlements.EmployeeID", "EmployeeName = '" + icb_employees.Text + "' AND LeaveType = '" + icb_leaveTypes.Text + "'");
			if( icb_employeeID.Data.Rows.Count > 0 )
				icb_employeeID.SelectedIndex = 0;
			if (icb_employeeID.Data.Rows.Count > 1) {
				lb_employeeID.Visibility  = System.Windows.Visibility.Visible;
				icb_employeeID.Visibility = System.Windows.Visibility.Visible;
			}	else	{
				lb_employeeID.Visibility  = System.Windows.Visibility.Collapsed;
				icb_employeeID.Visibility = System.Windows.Visibility.Collapsed;
			}
			lb_balance.Content = GetBalance( );
		}

		private object GetBalance( )	{
			if( lb_message.Visibility == System.Windows.Visibility.Visible || icb_leaveTypes.Text.Length == 0 || dp_leavingDate.SelectedDate == null || dp_joiningDate.SelectedDate == null || icb_employeeID.Text.Length == 0 ) return "";
			object balance = null;
			DataTable data = Entitlements.GetSpecificEntitlementInfo( "Balance, ValidFrom, ValidTo", "EmployeeID = " + icb_employeeID.Text + " AND LeaveType = '" + icb_leaveTypes.Text + "'" );
			DateTime leavingDate = dp_leavingDate.SelectedDate.Value;
			DateTime joiningDate = dp_joiningDate.SelectedDate.Value;
			if( leavingDate < new DateString( data.Rows[0][1].ToString( )) || joiningDate.AddDays( -1 ) > new DateString( data.Rows[0][2].ToString( ))) return "!!";
			balance = data.Rows[0][0];
			lostBalance = 0;
			DataTable holidays = WorkHolidays.GetAllHolidayInfo( );
			DataTable workingDays = WorkDays.GetWorkDayInfo( "WorkDayType NOT LIKE 'Non%'" );
			for(DateTime date = leavingDate; date < joiningDate; date = date.AddDays( 1 ))	{
				bool dayIsHoliday = false;
				foreach(DataRow row in holidays.Rows)	{
					if( date == ( DateTime )row[1] && row[2].ToString( ) == "Half Day" )	{
						lostBalance += .5;
						dayIsHoliday = true;
						break;
					}
				}
				if( dayIsHoliday ) continue;
				foreach(DataRow row in workingDays.Rows)	{
					if( row[1].ToString( ) == date.DayOfWeek.ToString( ))	{
						if( row[2].ToString( ) == "Half Day" ) lostBalance += .5;
						else lostBalance++;
						break;
					}
				}
			}
			lostBalance = ( lostBalance * ( cb_to.SelectedIndex - cb_from.SelectedIndex + 1 ) * .25 ) / 8;
		//	It was a tiresome balance calculation
		//	Alhamdu Lillaah!
			return (double.Parse( "0" + balance ) - lostBalance).ToString( );
		}

		public void AssignLeave( string employeeID, string leaveType, DateTime leavingDate, DateTime joiningDate )	{
			Page_Loaded(this, new RoutedEventArgs( ));
			icb_employeeID.Text = employeeID;
			icb_leaveTypes.Text = leaveType;
			dp_leavingDate.SelectedDate = leavingDate;
			dp_joiningDate.SelectedDate = joiningDate;
			Button_Click( button, new RoutedEventArgs( ));
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			MainPage mainPage = baseControl as MainPage;
			Stack<Page> jobStack = mainPage.JobStack;
			if( icb_employeeID.Text.Length == 0 || icb_leaveTypes.Text.Length == 0 || dp_leavingDate.SelectedDate == null || dp_joiningDate.SelectedDate == null )	{
				MessageBox.Show("Please fill in the required fields.", "Incomplete", MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			if( lb_message.Visibility == System.Windows.Visibility.Visible )	{
				MessageBox.Show( lb_message.Content.ToString( ) + "!", "Invalid Interval", MessageBoxButton.OK, MessageBoxImage.Error );
				return;
			}
			if( dp_leavingDate.SelectedDate.Value < DateTime.Today )	{
				MessageBox.Show( "Leaving date cannot be a past date.", "Date Past", MessageBoxButton.OK, MessageBoxImage.Error );
				return;
			}
			DataTable data = LeavePeriods.GetLeavePeriods( );
			string startingDate = "";
			string endingDate = "";
			bool leavingDateIsValid = false;
			bool joiningDateIsValid = false;
			foreach(DataRow row in data.Rows)	{
				if( dp_leavingDate.SelectedDate.Value >= new DateString( row[0] ) )	{
					startingDate = row[0].ToString( );
					leavingDateIsValid = true;
					break;
				}
			}
			foreach(DataRow row in data.Rows)	{
				if( dp_joiningDate.SelectedDate.Value.AddDays( -1 ) <= new DateString( row[1] ) )	{
					endingDate = row[1].ToString( );
					joiningDateIsValid = true;
					break;
				}
			}
			if( ! leavingDateIsValid || ! joiningDateIsValid )	{
				MessageBox.Show( "The leave period is not valid. Please enter a valid one or reconfigure.", "Invalid Leave Period", MessageBoxButton.OK, MessageBoxImage.Exclamation );
				return;
			}
			if( lb_balance.Content.ToString( ) == "!!" )	{
				MessageBox.Show( "The interval you entered exceeds the bounds!", "Too Big Interval", MessageBoxButton.OK, MessageBoxImage.Error );
				return;
			}
			if( lb_balance.Content.ToString( ).Length == 0 )	{
				if( MessageBox.Show( "No balance available. Do you wish to entitle?", "No Balance", MessageBoxButton.YesNo, MessageBoxImage.Exclamation ) == MessageBoxResult.Yes)	{
					EntitlementsAdder entitlementsAdder = mainPage.EntitlementsAdder;
					jobStack.Push( this );
					mainPage.Container.Navigate( entitlementsAdder );
					entitlementsAdder.icb_employees.Text = icb_employees.Text;
					entitlementsAdder.icb_employeeID.Text = icb_employeeID.Text;
					entitlementsAdder.icb_leaveTypes.Text = icb_leaveTypes.Text;
					entitlementsAdder.icb_validFrom.Text = startingDate;
					entitlementsAdder.icb_validTo.Text = endingDate;
				}	else	{
					MessageBox.Show( "Leave Not Assigned!", "Failure", MessageBoxButton.OK, MessageBoxImage.Information );
				}
				return;
			}
			if( double.Parse( lb_balance.Content.ToString( )) < 0 )	{
				MessageBoxResult result = MessageBox.Show( "Balance is insufficient! Do you wish to update entitlement?", "Insufficient Balance", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation );
				if( result != MessageBoxResult.No )	{
					if( result == MessageBoxResult.Yes )	{
						EntitlementsViewer entitlementsViewer = mainPage.EntitlementsViewer;
						mainPage.JobStack.Push( this );
						mainPage.Container.Navigate( entitlementsViewer );
						entitlementsViewer.icb_employeeID.Text = icb_employeeID.Text;
						entitlementsViewer.icb_employees.Text = icb_employees.Text;
						entitlementsViewer.icb_leaveTypes.Text = icb_leaveTypes.Text;
						entitlementsViewer.icb_validFrom.Text = startingDate;
						entitlementsViewer.icb_validTo.Text = endingDate;
					}
					return;
				}
			}
			string values = icb_employeeID.Text + ", '" + icb_leaveTypes.Text + "', '" + new DateString( dp_leavingDate.SelectedDate.Value ) + "', '" + new DateString( dp_joiningDate.SelectedDate.Value ) + "', " + lostBalance + ", '" + tb_commentBox.Text.Replace("'", "''") + "'";
			string changes = "Balance = " + lb_balance.Content;
			StringBuilder rowConstraints  = new StringBuilder( "EmployeeID = " + icb_employeeID.Text + " AND LeaveType = '" + icb_leaveTypes.Text + "'" );
			StringBuilder dateConstraints = new StringBuilder( );
			try	{
				Entitlements.UpdateEntitlementInfo( changes, rowConstraints.ToString( ));
				LeaveList.AssignLeave( values );
				data = LeaveApplications.FetchDetails( "LeavingDate, JoiningDate", rowConstraints.ToString( ));
				foreach(DataRow row in data.Rows)	{
					if( new DateString( row[0].ToString( )) >= dp_leavingDate.SelectedDate && new DateString( row[1].ToString( )) <= dp_joiningDate.SelectedDate )	{
						dateConstraints.Append( "LeavingDate = '" + row[0] + "' AND JoiningDate = '" + row[1] + "' OR " );
					}
				}
				dateConstraints.Append( "1 = 2" );
				rowConstraints.Append( " AND (" + dateConstraints.ToString( ) + ")" );
				LeaveApplications.RejectApplications( rowConstraints.ToString( ));
				MessageBox.Show( "Leave Assigned Successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information );
			}	catch(System.Data.SqlClient.SqlException exception)	{
				MessageBox.Show(exception.Message);
			}
			if( jobStack.Count > 0 )
				mainPage.Container.Navigate( jobStack.Pop( ) );
		}
	}
}
