using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Diot.Views.Converters
{
	/// <summary>
	///		This class allows for multiple converters to be applied at once.
	///		
	///		Usage:
	///		&lt;c:ValueConverterGroup x:Key="InvertAndVisibilitate"&gt;
	///			&lt;c:BooleanInverterConverter/&gt;
	///			&lt;c:BooleanToVisibilityConverter/&gt;
	///		&lt;/c:ValueConverterGroup&gt;
	/// </summary>
	/// <seealso cref="List{IValueConverter}" />
	/// <seealso cref="IValueConverter" />
	public class ValueConverterGroup : List<IValueConverter>, IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return this.Aggregate(value, (current, converter) => converter.Convert(current, targetType, parameter, culture));
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
