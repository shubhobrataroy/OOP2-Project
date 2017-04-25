using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config_with_Gui
{
	class IndexToVisibilityConverter : System.Windows.Data.IValueConverter
	{
		public object Convert(object value, Type type, object parameter, CultureInfo cultureInfo)		{
			if( ( int )value  == 0 )
				return "Collapsed";
			else
				return "Visible";
		}

		public object ConvertBack(object value, Type type, object parameter, CultureInfo cultureInfo)	{
			throw new InvalidOperationException( "Cannot convert back!" );
		}
	}
}
