using System;
using System.Globalization;
using Xamarin.Forms;

namespace Chat.Converters
{
    public class IsGreaterThanZeroConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            var result = ((int)value) > 0;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        
    }
}