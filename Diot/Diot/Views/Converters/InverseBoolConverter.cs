using System;
using System.Globalization;
using Xamarin.Forms;

namespace Diot.Views.Converters
{
    /// <summary>
    ///     Converter that inverses a boolean value.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.IValueConverter" />
    public class InverseBoolConverter : IValueConverter
    {
        /// <summary>
        ///     Inverses the boolean value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as bool?;

            if (val.HasValue && val is bool?)
            {
                return !val.Value;
            }

            return value;
        }

        /// <summary>
        ///     Inverses the boolean value back.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as bool?;

            if (val.HasValue && val is bool?)
            {
                return !val.Value;
            }

            return value;
        }
    }
}
