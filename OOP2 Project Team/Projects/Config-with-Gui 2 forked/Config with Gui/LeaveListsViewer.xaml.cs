using System;
using System.IO;
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
using Leave.DataClients;
using Leave.Modules;
using Leave.Supplements;

namespace Config_with_Gui
{
    /// <summary>
    /// Interaction logic for Leave_Lists.xaml
    /// </summary>
    public partial class LeaveListsViewer : Page
    {
		private UIElement baseControl;
		private DataManager dataManager;

        public LeaveListsViewer(UIElement baseControl)
        {
            InitializeComponent();
			this.baseControl = baseControl;
			this.Height = (baseControl as MainPage).Container.Height;
			this.Width	= (baseControl as MainPage).Container.Width;
			dataManager = new DataManager( );
        }

		public UIElement BaseControl { get { return baseControl; } }

		private void Page_Loaded(object sender, RoutedEventArgs e)	{
			MainPage mainPage = baseControl as MainPage;
			string rowConstraints = "";
			if( mainPage.UserRole == "USER" )	{
				rowConstraints = "Employees.EmployeeID = " + mainPage.EmployeeID;
				icb_employees.IsEnabled = false;
				icb_subUnit.IsEnabled = false;
			}
			icb_employees.Data = LeaveList.GetSpecificDistributedLeaveInfo( "DISTINCT Employees.EmployeeName", rowConstraints );
			icb_subUnit.Data = Employees.GetSpecificEmployeeInfo( "DISTINCT Department", rowConstraints );
			if( mainPage.UserRole == "USER" )	{
				icb_employees.SelectedIndex = 0;
				icb_subUnit.SelectedIndex = 0;
			}
		}

		private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)	{
			if(dp_From.Text.Length == 0 || dp_To.Text.Length == 0) return;

			if( dp_To.SelectedDate.Value <= dp_From.SelectedDate.Value )	{
				lb_message.Visibility = System.Windows.Visibility.Visible;
			}	else	{
				lb_message.Visibility = System.Windows.Visibility.Collapsed;
			}
		}

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)	{
			(sender as IntelComboBox).ViewMatches();
			if(sender == icb_employees)	{
				icb_employeeID.Data = Employees.GetSpecificEmployeeInfo("EmployeeID", "EmployeeName = '" + icb_employees.Text + "'");
				if( icb_employeeID.Items.Count > 0 )
					icb_employeeID.SelectedIndex = 0;
				if(icb_employeeID.Items.Count > 1)	{
					lb_employeeID.Visibility = System.Windows.Visibility.Visible;
					icb_employeeID.Visibility = System.Windows.Visibility.Visible;
				}	else	{
					lb_employeeID.Visibility = System.Windows.Visibility.Collapsed;
					icb_employeeID.Visibility = System.Windows.Visibility.Collapsed;
				}
			}
        }

		private void Button_Search_Click(object sender, RoutedEventArgs e)	{
			if(lb_message.Visibility == System.Windows.Visibility.Visible)	{
				MessageBox.Show(lb_message.Content.ToString());
				return;
			}
			StringBuilder rowConstraints = new StringBuilder( );
			if( icb_employeeID.Text.Length > 0 )	{
				rowConstraints.Append( "Employees.EmployeeID = " + icb_employeeID.Text );
			}	else if( icb_employees.Text.Length > 0 )	{
				rowConstraints.Append( "Employees.EmployeeID IS NULL" );
			}	else	{
				rowConstraints.Append( "Employees.EmployeeID IS NOT NULL" );
			}
			if( icb_subUnit.Text.Length > 0 )	{
				rowConstraints.Append( " AND Employees.Department = '" + icb_subUnit.Text + "'" );
			}
			if( ! checkBox.IsChecked.Value )	{
				rowConstraints.Append( " AND Employees.PastEmployee = 'No'" );
			}
			DataTable data = LeaveList.GetAllDistributedLeaveInfo( rowConstraints.ToString( ) );
			if( dp_From.SelectedDate != null )
				for(int i = data.Rows.Count - 1; i >= 0; i--)
					if( dp_From.SelectedDate.Value > new DateString( data.Rows[i][3] ) )
						data.Rows.Remove( data.Rows[i] );
			if( dp_To.SelectedDate != null )
				for(int i = data.Rows.Count - 1; i >= 0; i--)
					if( dp_To.SelectedDate.Value < new DateString( data.Rows[i][4] ) )
						data.Rows.Remove( data.Rows[i] );
			FillDataGrid( dataManager.Data = data );
		}

		private void Button_Reset_Click( object sender, RoutedEventArgs e )	{
			if( ( baseControl as MainPage ).UserRole == "USER" )	{
				MessageBox.Show( "You are not allowed to reset the list!", "Access denied", MessageBoxButton.OK, MessageBoxImage.Exclamation );
				return;
			}
			if( MessageBox.Show( "Are you sure you want to trancate the leave list?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes )	{
				LeaveList.Delete( );
			}
		}

		private void FillDataGrid( DataTable data )	{
		//	Fill the DataGrid
			dataGrid.ItemsSource = data.AsDataView( );
		//	Normalize the DataGridColumns
			foreach( DataGridColumn column in dataGrid.Columns )
				column.Width = new DataGridLength( 1.0, DataGridLengthUnitType.Star );
		}

		private void Button_Save_Click(object sender, RoutedEventArgs e)	{
			try {
				MainPage mainPage = baseControl as MainPage;
				Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog( );
				saveFileDialog.FileName = mainPage.UserName + ".csv";
				saveFileDialog.Filter = "CSV (*.csv) | *.csv";
				if( saveFileDialog.ShowDialog( ) == true )	{
					string fileName = saveFileDialog.FileName;
					using(StreamWriter writer = new StreamWriter( fileName ))	{
						writer.WriteLine( dataManager.ExtractColumns( ));
						writer.WriteLine( dataManager.ExtractData( ));
					}
					MessageBox.Show("File Saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}	catch( IOException exception )	{
				MessageBox.Show( "An unexpected error occured while saving data!", "Failure", MessageBoxButton.OK, MessageBoxImage.Asterisk );
			}
		}
    }
}
