using System;
using System.Globalization;
using System.Windows.Data;
using WinTermPlus.Infrastructure;

namespace WinTermPlus.UI.Converters
{
    public class RangeBaseToPercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            return ((Percentage)value).ToInt();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            return new Percentage((int)Math.Floor((double)value));
        }
    }
}