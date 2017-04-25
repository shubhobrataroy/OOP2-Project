using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config_with_Gui
{
	class IntToBooleanConverter : System.Windows.Data.IValueConverter
	{
		public object Convert(object value, Type type, object parameter, CultureInfo cultureInfo)		{
			return (int)value != 0;
		}

		public object ConvertBack(object value, Type type, object parameter, CultureInfo cultureInfo)	{
			switch((bool)value)	{
				case false:
					return 0;
				default:
					return 1;
			}
		}
	}
}
