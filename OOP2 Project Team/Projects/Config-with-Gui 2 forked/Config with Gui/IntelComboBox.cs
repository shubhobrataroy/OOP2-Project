using System;
using System.Threading;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading.Tasks;

namespace Config_with_Gui
{
	class IntelComboBox : ComboBox
	{
		private DataTable data = new DataTable();

		public IntelComboBox()	{
			IsEditable = true;
			IsTextSearchEnabled = false;
		}

		public DataTable Data	{
			get { return data; }
			set {
				data = value.Copy();
				LoadData(data.Select());
			}
		}

		protected override void OnPreviewKeyDown(KeyEventArgs e)	{
			if(e.Key == Key.Down)	{
				if(IsDropDownOpen == false)
					IsDropDownOpen = true;
			}
			else if(e.Key == Key.Up)	{
				if(IsDropDownOpen == true)
					IsDropDownOpen = false;
			}
		}

		public void ViewMatches()	{
			if(data.Columns.Count <= 0) return;
			string columnName = data.Columns[0].ColumnName;
			DataRow[] rows = null;
			try {
				rows = data.Select(columnName + " LIKE '%" + Text + "%'", columnName);
			}	catch(EvaluateException e)	{
				double result;
				rows = data.Select(columnName + (Text.Length == 0 ? " IS NOT NULL" : (double.TryParse(Text, out result) == true ? " = " + result : " IS NULL")), columnName);
			}
			LoadData( rows );
			if(rows.Length > 0 && Text.Length > 0 && !IsDropDownOpen)	{
				IsDropDownOpen = true;
				if( this.IsEditable )	{
					TextBox textbox = ( TextBox )Template.FindName( "PART_EditableTextBox", this );
					textbox.Select(Text.Length, 0);
				}
			}
		//	string text = Text;
		//	Items.Clear();
		//	ComboBoxItem item = null;
		//	foreach(DataRow row in rows)	{
		//		item = new ComboBoxItem();
		//		item.Content = row[0];
		//		Items.Add(item);
		//	}
		//	Text = text;
		}

		private void LoadData( DataRow[] rows )	{
			Items.Clear( );
			ComboBoxItem item = null;
			foreach( DataRow row in rows )	{
				item = new ComboBoxItem();
				item.Content = row[0];
				Items.Add(item);
			}
		}

		protected override void OnSelectionChanged(SelectionChangedEventArgs e)	{
			base.OnSelectionChanged(e);
			if( IsDropDownOpen )
				IsDropDownOpen = false;
		//	Text = "" + Text;
		}
	}
}
