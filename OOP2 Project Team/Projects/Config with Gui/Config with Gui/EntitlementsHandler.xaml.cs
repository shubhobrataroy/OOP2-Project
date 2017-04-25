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
	/// Interaction logic for Entitlements.xaml
	/// </summary>
	public partial class EntitlementsHandler : Page
	{
		public EntitlementsHandler(double height, double width) {
			InitializeComponent();
			this.Height = height;
			this.Width = width;
		}

		private void Button_Click(object sender, RoutedEventArgs e)	{
			MainWindow.page_container.Navigate(new EntitlementsAdder(MainWindow.page_container.Height, MainWindow.page_container.Width));
		}

		private void Button_Click_1(object sender, RoutedEventArgs e) {
			MainWindow.page_container.Navigate(new EntitlementsViewer(MainWindow.page_container.Height, MainWindow.page_container.Width));
		}
	}
}
