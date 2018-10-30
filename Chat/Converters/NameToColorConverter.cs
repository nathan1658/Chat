using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;

namespace Chat.Converters
{
    public class NameToColorConverter:IValueConverter
    {

        readonly List<string> colorList = new List<string>
        {
            "#1abc9c",
            "#16a085",
            "#2ecc71",
            "#27ae60",
            "#3498db",
            "#2980b9",
            "#9b59b6",
            "#8e44ad",
            "#34495e",
            "#2c3e50",
            "#f1c40f",
            "#f39c12",
            "#e67e22",
            "#d35400",
            "#e74c3c",
            "#c0392b",
            "#bdc3c7",
            "#95a5a6",
            "#7f8c8d"
         };


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string name = value.ToString();
            MD5 md5Hasher = MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(name));
            int ivalue = BitConverter.ToInt32(hashed, 0);
            if(ivalue<0)
            {
                ivalue *= -1;
            }
            int index = ivalue % (colorList.Count);

            return colorList[index];

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
