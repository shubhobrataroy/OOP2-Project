using System;
using System.Data;
using System.Data.SqlClient;
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


namespace Config_with_Gui {
	/// <summary>
	/// Interaction logic for EntitlementAdder.xaml
	/// </summary>
	public partial class EntitlementsAdder : Page
	{
		private UIElement baseControl;

		public EntitlementsAdder(UIElement baseControl)	{
			InitializeComponent();
			this.baseControl = baseControl;
			MainPage mainPage = baseControl as MainPage;
			this.Height = mainPage.Container.Height;
			this.Width	= mainPage.Container.Width;
		}

		public UIElement BaseControl { get { return baseControl; } }

		private void Page_Loaded(object sender, RoutedEventArgs e)	{
			if( ( baseControl as MainPage ).UserRole == "USER" )	{
				mainGrid.Visibility = System.Windows.Visibility.Collapsed;
				lb_message2.Visibility = System.Windows.Visibility.Visible;
				return;
			}
			MainPage mainPage = baseControl as MainPage;
			Stack<Page> jobStack = mainPage.JobStack;
			if( !mainPage.UserIsLoggedIn )	{
				(mainPage.BaseControl as MainWindow).Frame.Navigate(new LoginPage(baseControl));
			}
			if( jobStack.Count != 0 && jobStack.Peek( ) != mainPage.LeaveAssignmentPage )
				jobStack.Clear( );
			icb_employees.Data			 = Employees.GetSpecificEmployeeInfo("DISTINCT EmployeeName");
			icb_leaveTypes.Data			 = LeaveTypes.GetLeaveTypes( );
			icb_entitlementType.Data	 = EntitlementTypes.GetEntitlementTypes( );
			icb_validFrom.Data			 = LeavePeriods.GetStartingDate( );
			icb_validTo.Data			 = LeavePeriods.GetEndingDate( );
		}

		private void Element_Text_Changed(object sender, TextChangedEventArgs e)	{
			if(sender is IntelComboBox)	{
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
			else if( sender == tb_balance )	{
				if( tb_balance.Text.Length == 0 )	{
					if( lb_message.Visibility == System.Windows.Visibility.Visible )
						lb_message.Visibility = System.Windows.Visibility.Collapsed;
					return;
				}
				double num;
				if( double.TryParse(tb_balance.Text, out num) )	{
					for (int i = 0; i < tb_balance.Text.Length; i++ )	{
						if( tb_balance.Text[i] == '.' )	{
							if( i < tb_balance.Text.Length - 3 )	{
								lb_message.Visibility = Visibility.Visible;
								break;
							}
						}
						else	{
							lb_message.Visibility = Visibility.Collapsed;
						}
					}
				}	else if( lb_message.Visibility == System.Windows.Visibility.Collapsed )	{
					lb_message.Visibility = System.Windows.Visibility.Visible;
				}
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)	{
			MainPage mainPage = baseControl as MainPage;
			Stack<Page> jobStack = mainPage.JobStack;

			if(lb_message.Visibility == System.Windows.Visibility.Visible)	{
				MessageBox.Show( lb_message.ToString( ), "Error", MessageBoxButton.OK, MessageBoxImage.Error );
				return;
			}
			if(lb_message2.Visibility == System.Windows.Visibility.Visible)	{
				MessageBox.Show( lb_message2.ToString( ), "Error", MessageBoxButton.OK, MessageBoxImage.Error );
				return;
			}
			if( icb_employees.Text.Length == 0 || icb_leaveTypes.Text.Length == 0 || tb_balance.Text.Length == 0 || icb_validFrom.Text.Length == 0 || icb_validTo.Text.Length == 0 )	{
				MessageBox.Show( "Please enter the required fields." );
				return;
			}
			if( icb_employeeID.Text.Length == 0 )	{
				MessageBox.Show( "No employee named '" + icb_employees.Text + "' found!", "Invalid name", MessageBoxButton.OK, MessageBoxImage.Error );
				return;
			}
			StringBuilder values = new StringBuilder( icb_employeeID.Text + ", '" + icb_leaveTypes.Text + "', '" + icb_validFrom + "', '" + icb_validTo + "', " + tb_balance.Text );
			StringBuilder columns = new StringBuilder( "EmployeeID, LeaveType, ValidFrom, ValidTo, Balance" );

			if( icb_entitlementType.Text.Length != 0 ) {
				values.Append( ", '" + icb_entitlementType.Text + "'" );
				columns.Append( ", EntitlementType" );
			}
			try	{
				Entitlements.AddEntitlementInfo( values.ToString( ), columns.ToString( ));
				MessageBox.Show( "Entitlements added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information );
			}	catch(SqlException exception)	{
				if( exception.Message.ToLower( ).Contains( "primary key" ) )	{
					if( MessageBox.Show( "An entitlement of type " + icb_leaveTypes.Text + " already exists for " + icb_employees.Text + ".\nDo you want to update the balance?", "Entitlements exists", MessageBoxButton.OKCancel, MessageBoxImage.Question ) == MessageBoxResult.OK )	{
						EntitlementsViewer entitlementsViewer = mainPage.EntitlementsViewer;
						mainPage.Container.Navigate( entitlementsViewer );
						entitlementsViewer.icb_employees.Text = icb_employees.Text;
						entitlementsViewer.icb_employeeID.Text = icb_employeeID.Text;
						entitlementsViewer.icb_entitlementType.Text = icb_entitlementType.Text;
						entitlementsViewer.icb_validFrom.Text = icb_validFrom.Text;
						entitlementsViewer.icb_validTo.Text = icb_validTo.Text;
						entitlementsViewer.tb_balance.Text = tb_balance.Text;
						return;
					}
				}
				else	{
					MessageBox.Show( "Entitlements could not be added.\nAdditional information: " + exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error );
				}
			}
			if( jobStack.Count > 0 )
				mainPage.Container.Navigate( jobStack.Pop( ) );
		}

		private void Element_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if( sender == icb_validFrom || sender == icb_validTo )	{
				if( icb_validFrom.SelectedIndex >= 0 && icb_validTo.SelectedIndex >= 0 )	{
					MessageBox.Show( "Message" );
					DateTime validFrom = new DateString( icb_validFrom.Items[icb_validFrom.SelectedIndex] );
					DateTime validTo = new DateString( icb_validTo.Items[icb_validTo.SelectedIndex] );
					if( validTo <= validFrom )	{
						lb_message3.Visibility = System.Windows.Visibility.Visible;
					}	else	{
						lb_message3.Visibility = System.Windows.Visibility.Collapsed;
					}
				 }
			}
		}
	}
}
