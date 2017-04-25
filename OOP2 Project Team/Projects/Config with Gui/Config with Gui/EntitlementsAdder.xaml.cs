using System;
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
using Leave.Modules;

namespace Config_with_Gui {
	/// <summary>
	/// Interaction logic for EntitlementAdder.xaml
	/// </summary>
	public partial class EntitlementsAdder : Page
	{
		public EntitlementsAdder(double height, double width)	{
			InitializeComponent();
			this.Height = height;
			this.Width = width;
		}

		private void Button_Click(object sender, RoutedEventArgs e)	{
			string values = "";
			string rowConstraints = "";
			if(id.Text.Length != 0)	{
				values += id.Text;
				rowConstraints += "EntitlementID";
			}
			if (leaveType.Text.Length != 0) {
				values += ", '" + leaveType.Text + "'";
				rowConstraints += ", " + "LeaveType";
			}
			if (entitlementType.Text.Length != 0) {
				values += ", '" + entitlementType.Text + "'";
				rowConstraints += ", " + "EntitlementType";
			}
			if (validFrom.Text.Length != 0) {
				values += ", '" + validFrom.Text + "'";
				rowConstraints += ", " + "ValidFrom";
			}
			if (validTo.Text.Length != 0) {
				values += ", '" + validTo.Text + "'";
				rowConstraints += ", " + "ValidTo";
			}
			if (balance.Text.Length != 0) {
				values += ", " + balance.Text;
				rowConstraints += ", " + "Balance";
			}
			
			try	{
				Entitlements.AddEntitlements(values, rowConstraints);
				MessageBox.Show("Entitlements added successfully!");
			}	catch(SqlException exception)	{
				MessageBox.Show(exception.Message + values + rowConstraints);
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e) {
			MainWindow.page_container.Navigate(new EntitlementsHandler(MainWindow.page_container.Height, MainWindow.page_container.Width));
		}
	}
}
