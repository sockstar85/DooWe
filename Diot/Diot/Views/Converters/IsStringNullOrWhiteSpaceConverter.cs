using System;
using System.Globalization;
using Xamarin.Forms;

namespace Diot.Views.Converters
{
	public class IsStringNullOrWhiteSpaceConverter : IValueConverter
	{
		/// <summary>
		///		Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="targetType">The type to which to convert the value.</param>
		/// <param name="parameter">A parameter to use during the conversion.</param>
		/// <param name="culture">The culture to use during the conversion.</param>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string val = value as string;

			return string.IsNullOrWhiteSpace(val?.Trim());
		}

		/// <summary>
		///		Implement this method to convert <paramref name="value" /> back from <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="targetType">The type to which to convert the value.</param>
		/// <param name="parameter">A parameter to use during the conversion.</param>
		/// <param name="culture">The culture to use during the conversion.</param>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//cannot convert back
			return value;
		}
	}
}
