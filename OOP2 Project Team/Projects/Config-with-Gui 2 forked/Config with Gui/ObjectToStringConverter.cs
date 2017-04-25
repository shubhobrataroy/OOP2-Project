using System;
using System.Globalization;
using System.Windows.Data;

namespace Config_with_Gui
{
	class ObjectToStringConverter : IValueConverter
	{
		public object Convert( object value, Type type, object parameter, System.Globalization.CultureInfo cultureInfo )	{
			double num;
			if( value.ToString( ) == "!!" ) return "Red";
			if( double.TryParse( value.ToString( ), out num ) )
				return num < 0 ? "Red" : num == 0 ? "LightSeaGreen" : "DarkGreen";
			return "Black";
		}

		public object ConvertBack( object value, Type type, object parameter, System.Globalization.CultureInfo cultureInfo )	{
			throw new InvalidOperationException( "Cannot convert back!" );
		}
	}
}
