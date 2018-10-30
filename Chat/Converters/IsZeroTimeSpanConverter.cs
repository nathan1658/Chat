using System;
using System.Globalization;
using Xamarin.Forms;

namespace Chat.Converters
{
    public class IsZeroTimeSpanConverter:IValueConverter
    {
  
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeSpan = (TimeSpan) value;
            if (timeSpan != null)
            {
                if (timeSpan > new TimeSpan()) 
                {
                    return true;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
