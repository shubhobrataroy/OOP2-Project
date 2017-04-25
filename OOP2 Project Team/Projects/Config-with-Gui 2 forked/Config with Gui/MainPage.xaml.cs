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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    /// 

    public partial class MainPage : Page
    {
        public static Frame page_container;

		private UIElement baseControl;
		private string username;
		private string password;
		private string userrole;
		private string employeeID;
		private bool userIsLoggedIn;
		private EntitlementsViewer entitlementsViewer;
		private EntitlementsAdder entitlementsAdder;
		private LeaveAssignmentPage leaveAssignmentPage;
		private SendApplication sendApplication;
		private ViewApplication viewApplication;
		private Stack<Page> jobStack;

		public MainPage(UIElement baseControl, string username, string password, string userrole, string employeeID)
        {
            InitializeComponent();
            page_container = Container;
			entitlementsViewer	= new EntitlementsViewer( this );
			entitlementsAdder	= new EntitlementsAdder( this );
			leaveAssignmentPage = new LeaveAssignmentPage( this );
			sendApplication = new SendApplication( this );
			viewApplication = new ViewApplication( this );
			jobStack = new Stack<Page>( );
			this.baseControl = baseControl;
			this.username = username;
			this.password = password;
			this.userrole = userrole;
			this.employeeID = employeeID;
			this.userIsLoggedIn = true;
			MainWindow mainWindow = baseControl as MainWindow;
			this.Height = mainWindow.Frame.Height;
			this.Width = mainWindow.Frame.Width;
            (baseControl as MainWindow).Frame.NavigationUIVisibility = NavigationUIVisibility.Visible;
        }

		private void Page_Loaded(object sender, RoutedEventArgs e)	{
			
		//	if user is not logged in
		//	view the login page
		//	else continue with current view

			if(!userIsLoggedIn)	{
				MainWindow mainWindow = baseControl as MainWindow;
				mainWindow.Frame.Navigate(new LoginPage(mainWindow));
				return;
			}
			tb_welcome.Text = "Welcome " + username + "!";
			tb_loggedInAs.Text = "You're now logged in as " + userrole;
		}

		public UIElement BaseControl { get { return baseControl; } }

		public string UserName { get { return username; } }
		
		public string Password { get { return password; } }
		
		public string UserRole { get { return userrole; } }

		public string EmployeeID { get { return employeeID; } }
		
		public bool UserIsLoggedIn { get { return userIsLoggedIn; } }

		public EntitlementsAdder EntitlementsAdder { get { return entitlementsAdder; } }

		public EntitlementsViewer EntitlementsViewer { get { return entitlementsViewer; } }

		public LeaveAssignmentPage LeaveAssignmentPage { get { return leaveAssignmentPage; } }

		public ViewApplication ViewApplication { get { return viewApplication; } }

		public SendApplication SendApplication { get { return sendApplication; } }

		public Stack<Page> JobStack { get { return jobStack; } }

		public void CommitNavigation( Page page )	{
			if( jobStack.Count == 100 ) jobStack.Clear( );
			jobStack.Push( page );
		}

        public void Leave_Add_Entitlements_Event(object sender, RoutedEventArgs e)
        {
            Container.Navigate(entitlementsAdder);
        }

        private void Leave_View_Entitlements_Event(object sender, RoutedEventArgs e) {
			Container.Navigate(entitlementsViewer);
        }

        public void Leave_Configure_Event(object sender, RoutedEventArgs e)
        {
            Page page = new config().getPage(Container.Height, Container.Width);
            Container.Navigate(page);
        }

        public void Leave_Leave_List_Event(object sender, RoutedEventArgs e)
        {
			Page page = new LeaveListsViewer(this);
            Container.Navigate(page);
        }

        public void Leave_Period_Event(object sender, RoutedEventArgs e)
        {
            Page page = new leave_period().getPage(Container.Height, Container.Width);
            Container.Navigate(page);
        }

        public void Leave_Types_Event(object sender, RoutedEventArgs e)
        {
            Page page = new leave_types().getPage(Container.Height, Container.Width);
            Container.Navigate(page);
        }

        public void Leave_Work_Week_Event(object sender, RoutedEventArgs e)
        {
            Page page = new Work_week().getPage(Container.Height, Container.Width);
            Container.Navigate(page);
        }

        public void Leave_Holiday_Event(object sender, RoutedEventArgs e)
        {
            Page page = new Holidays().getPage(Container.Height, Container.Width);
            Container.Navigate(page);
        }

        public void Leave_Reports_Event(object sender, RoutedEventArgs e)
        {

            //try
            //{
                Page page = new Reports().getPage(Container.Height, Container.Width);
                Container.Navigate(page);
            //}
            //catch (Exception excep) { MessageBox.Show(excep.ToString()); }
        }

        public void Leave_AssignLeave_Event(object sender, RoutedEventArgs e)
        {
        /*	Page page = new Assign_Leave().getPage(Container.Height, Container.Width);
            Container.Navigate(page);	*/
			Container.Navigate(leaveAssignmentPage);
        }

		private void Button_Click(object sender, RoutedEventArgs e) {
			username = password = userrole = "";
			userIsLoggedIn = false;
			(baseControl as MainWindow).Frame.Navigate(new LoginPage(baseControl));
		}

		private void Leave_Application_Event( object sender, RoutedEventArgs e )	{
			if( userrole == "ADMIN" )	{
				Container.Navigate( viewApplication );
			}	else	{
				Container.Navigate( sendApplication );
			}
		}

		private void Leave_Send_Application_Event( object sender, RoutedEventArgs e )	{
			Container.Navigate( sendApplication );
		}

		private void Leave_View_Application_Event(object sender, MouseButtonEventArgs e)	{
			Container.Navigate( viewApplication );
		}
    }
}