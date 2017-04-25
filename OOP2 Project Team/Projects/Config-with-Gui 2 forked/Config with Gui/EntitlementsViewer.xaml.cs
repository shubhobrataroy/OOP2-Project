using System;
using System.Data;
using System.IO;
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
using System.Windows.Automation.Peers;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Leave.Modules;
using Leave.Supplements;
using Leave.DataClients;

namespace Config_with_Gui	{
	/// <summary>
	/// Interaction logic for EntitlementsViewer.xaml
	/// </summary>
	public partial class EntitlementsViewer : Page
	{
		private UIElement baseControl;
		private DataManager dataManager;

		public EntitlementsViewer(UIElement baseControl)	{
			InitializeComponent();
			this.baseControl = baseControl;
			MainPage mainPage = baseControl as MainPage;
			this.Height = mainPage.Container.Height;
			this.Width	= mainPage.Container.Width;
			dataManager = new DataManager();
		}

		public UIElement BaseControl { get { return baseControl; } }

		private void Page_Loaded(object sender, RoutedEventArgs e)	{

		//	if user is not logged
		//	return to the login page
		//	else load required data
		//	and continue with the current view

			MainPage mainPage = baseControl as MainPage;
			if(!mainPage.UserIsLoggedIn)	{
				MainWindow mainWindow = mainPage.BaseControl as MainWindow;
				mainWindow.Frame.Navigate(new LoginPage(mainPage.BaseControl));
				return;
			}
			Stack<Page> jobStack = mainPage.JobStack;
			if( jobStack.Count != 0 && jobStack.Peek( ) != mainPage.LeaveAssignmentPage )
				jobStack.Clear( );
			string rowConstraints = "";
			if( mainPage.UserRole == "USER" )	{
				rowConstraints = "Entitlements.EmployeeID = " + mainPage.EmployeeID;
				icb_employees.IsEnabled = false;
				icb_employees.Text = Employees.GetSpecificEmployeeInfo( "EmployeeName", "EmployeeID = " + mainPage.EmployeeID ).Rows[0][0].ToString( );
				icb_employeeID.Text = mainPage.EmployeeID;
				if( icb_employeeID.Visibility == Visibility.Visible )
					icb_employeeID.Visibility = Visibility.Collapsed;
			}	else	{
				icb_employees.Data		= Entitlements.GetSpecificDistributedEntitlementInfo( "DISTINCT Employees.EmployeeName", rowConstraints );
			}
			icb_leaveTypes.Data			= Entitlements.GetSpecificEntitlementInfo( "DISTINCT LeaveType", rowConstraints );
			icb_entitlementType.Data	= Entitlements.GetSpecificEntitlementInfo( "DISTINCT EntitlementType", rowConstraints );
			icb_validFrom.Data			= Entitlements.GetSpecificEntitlementInfo( "DISTINCT ValidFrom", rowConstraints );
			icb_validTo.Data			= Entitlements.GetSpecificEntitlementInfo(" DISTINCT ValidTo", rowConstraints );
		}

		private void ComboBox_Drop_Down_Opened(object sender, EventArgs e)	{
			(sender as IntelComboBox).ViewMatches();
		}

		private void Element_Text_Changed(object sender, TextChangedEventArgs e)	{
			MainPage mainPage = baseControl as MainPage;
			if(sender is IntelComboBox)	{
				( sender as IntelComboBox ).ViewMatches();
				if( sender == icb_employees )	{
					icb_employeeID.Data = Entitlements.GetSpecificDistributedEntitlementInfo("DISTINCT Entitlements.EmployeeID", "EmployeeName = '" + icb_employees.Text + "'");
					if( icb_employeeID.Items.Count > 0 )	{
						icb_employeeID.SelectedIndex = 0;
					}
					if(icb_employeeID.Items.Count > 1)	{
						lb_employeeID.Visibility = System.Windows.Visibility.Visible;
						icb_employeeID.Visibility = System.Windows.Visibility.Visible;
					}	else	{
						lb_employeeID.Visibility = System.Windows.Visibility.Collapsed;
						icb_employeeID.Visibility = System.Windows.Visibility.Collapsed;
					}
				}
			}
			else if( sender == tb_balance )	{
				if( tb_balance.Text.Length == 0 )	{
					if( lb_message.Visibility == System.Windows.Visibility.Visible )
						lb_message.Visibility = System.Windows.Visibility.Collapsed;
					return;
				}
				double num;
				if( double.TryParse(tb_balance.Text, out num) )	{
					num *= 100;
					if(( int )num == num )	{
						if( lb_message.Visibility == System.Windows.Visibility.Visible )
							lb_message.Visibility = System.Windows.Visibility.Collapsed;
					}	else if( lb_message.Visibility == System.Windows.Visibility.Collapsed )	{
						lb_message.Visibility = System.Windows.Visibility.Visible;
					}
				}
			}
		}

		public void ViewEntitlements( )	{
			Button_View_Click( btn_View, new RoutedEventArgs( ));
		}
		
		private void Button_View_Click(object sender, RoutedEventArgs e) {
			if( lb_message.Visibility == System.Windows.Visibility.Visible )	{
				MessageBox.Show( lb_message.Content.ToString( ), "Invalid Balance" );
				return;
			}
			StringBuilder rowConstraints = new StringBuilder( );
			if( icb_employeeID.Text.Length > 0 )	{
				rowConstraints.Append( "Entitlements.EmployeeID = " + icb_employeeID.Text );
			}	else if( icb_employees.Text.Length > 0 )	{
				rowConstraints.Append("Entitlements.EmployeeID IS NULL");
			}	else	{
				rowConstraints.Append("Entitlements.EmployeeID IS NOT NULL");
			}
			if(icb_leaveTypes.Text.Length > 0)
				rowConstraints.Append(" AND LeaveType = '" + icb_leaveTypes.Text + "'");
			
			if(icb_entitlementType.Text.Length > 0)
				rowConstraints.Append(" AND EntitlementType = '" + icb_entitlementType.Text + "'");
			
			if(icb_validFrom.Text.Length > 0 && icb_validTo.Text.Length > 0)
				rowConstraints.Append(" AND (ValidFrom = '" + icb_validFrom.Text + "' OR ValidTo = '" + icb_validTo.Text + "')");
			
			else if(icb_validFrom.Text.Length > 0)
				rowConstraints.Append(" AND ValidFrom = '" + icb_validFrom.Text + "'");
			
			else if(icb_validTo.Text.Length > 0)
				rowConstraints.Append(" AND ValidTo = '" + icb_validTo.Text + "'");
			
			if(tb_balance.Text.Length > 0)
				rowConstraints.Append(" AND Balance = " + tb_balance.Text);
			
			FillDataGrid( dataManager.Data = Entitlements.GetAllDistributedEntitlementInfo( rowConstraints.ToString( )));
		}

		private void Button_Delete_Click(object sender, RoutedEventArgs e)	{
			if( ( baseControl as MainPage ).UserRole == "USER" )	{
				MessageBox.Show( "You are not allowed to delete data!", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Error );
			}
			else if(MessageBox.Show("Are you want to delete the selected data?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)	{
				StringBuilder rowConstraints = new StringBuilder();
				for(int i = 0; i < dataGrid.SelectedItems.Count; i++)	{
					if(i > 0) rowConstraints.Append(" OR ");
					rowConstraints.Append("(EmployeeID = " + (dataGrid.SelectedItems[i] as DataRowView)[0] + " AND LeaveType = '" + (dataGrid.SelectedItems[i] as DataRowView)[2] + "')");
				}
				try {
					Entitlements.DeleteEntitlementInfo(rowConstraints.ToString());
				}	catch( System.Data.SqlClient.SqlException exception )	{
					MessageBox.Show( "Data could not be deleted!", "Failure", MessageBoxButton.OK, MessageBoxImage.Asterisk );
				}
			}
			Button_View_Click(btn_View, new RoutedEventArgs());
		}

		private void Button_Save_Click( object sender, RoutedEventArgs e )	{
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

		private void FillDataGrid( DataTable data )	{
		//	Fill the DataGrid
			dataGrid.ItemsSource = data.AsDataView( );
		//	Make sure that the admin is able to edit only the entitlement balance
			int readOnlyColumnsCount = 0;
			if( ( baseControl as MainPage ).UserRole == "ADMIN" )	{
				readOnlyColumnsCount = 6;
			}	else	{
				readOnlyColumnsCount = 7;
			}
			for(int i = 0; i < readOnlyColumnsCount; i++)	{
				dataGrid.Columns[i].IsReadOnly = true;
			}
		//	Normalize the DataGridColumns
			foreach(DataGridColumn column in dataGrid.Columns)
				column.Width = new DataGridLength( 1.0, DataGridLengthUnitType.Star );
		}

		private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)	{
			try {
				DataRowView rowView = dataGrid.SelectedItems[0] as DataRowView;
				TextBox textBox = e.EditingElement as TextBox;
				string column = e.Column.Header.ToString();
				string value = textBox.Text;
				string changes = column + " = " + value;
				string rowConstraints = "EmployeeID = " + rowView[0] + " AND LeaveType = '" + rowView[2] + "'";
				Entitlements.UpdateEntitlementInfo( changes, rowConstraints );
				MessageBox.Show( "Changes saved!", "", MessageBoxButton.OK, MessageBoxImage.Information );
			}	catch( System.Data.SqlClient.SqlException exception )	{
				MessageBox.Show("Invalid Data!", "", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			MainPage mainPage = baseControl as MainPage;
			Stack<Page> jobStack = mainPage.JobStack;
			if( jobStack.Count > 0 )
				mainPage.Container.Navigate( jobStack.Pop( ) );
		}
	}
}
