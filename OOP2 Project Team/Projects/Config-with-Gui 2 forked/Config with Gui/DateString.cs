using System;

namespace Leave
{
	class DateString
	{
		private DateTime date;

		public DateString( object obj )	{
			if( obj is DateTime )	{
				this.date = ( DateTime )obj;
			}	else if( obj is string )	{
				string dateString = obj.ToString( );
				string errorMessage = "Invalid Date Format.";
				if(dateString[2] != '-' || dateString[5] != '-') throw new FormatException( errorMessage );
				try	{
					date = new DateTime(int.Parse(dateString.Substring(6, 4)), int.Parse(dateString.Substring(0, 2)), int.Parse(dateString.Substring(3, 2)));
				}	catch(FormatException e)	{
					throw new FormatException( errorMessage );
				}
			}	else	{
				throw new ArgumentException( "Argument must be either a DateTime of a String" );
			}
		}

		public DateString( ) : this(DateTime.Today) { }

		public DateTime Date	{
			get { return date; }
			set { date = value; }
		}

		public DateTime ToDateTime( )	{
			return date;
		}

		public override string ToString()	{
			int day = date.Day;
			int month = date.Month;
			int year = date.Year;			return (date.Month < 10 ? "0" + date.Month : date.Month.ToString()) + "-" + (date.Day < 10 ? "0" + date.Day : date.Day.ToString()) + "-" + date.Year;		}

		public static implicit operator DateTime( DateString dateString )	{
			return dateString.date;
		}

		public static explicit operator string( DateString dateString )	{
			return dateString.ToString( );
		}

		public static implicit operator DateString( DateTime date )	{
			return new DateString( date );
		}
		
		public static implicit operator DateString( string dateString )	{
			return new DateString( dateString );
		}
	}
}