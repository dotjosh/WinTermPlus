using System;
using System.Globalization;
using System.Windows.Data;
using WinTermPlus.Infrastructure;

namespace WinTermPlus.UI.Converters
{
    public class StringToPercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            return ((Percentage)value).ToInt().ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}