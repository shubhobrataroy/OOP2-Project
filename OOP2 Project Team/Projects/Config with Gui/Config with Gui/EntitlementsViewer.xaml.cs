﻿using System;
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

namespace Config_with_Gui {
	/// <summary>
	/// Interaction logic for EntitlementsViewer.xaml
	/// </summary>
	public partial class EntitlementsViewer : Page
	{
		public EntitlementsViewer(double height, double width)	{
			InitializeComponent();
			this.Height = height;
			this.Width = width;
		}
	}
}
