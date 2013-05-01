using System;
using System.Globalization;
using System.Windows.Data;

namespace Measure.Converters
{
    public class TimespanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(TimeSpan))
            {
                return string.Empty;
            }

            var timeSpan = (TimeSpan)value;

            return timeSpan.ToString(@"hh\:mm\:ss\.fff");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
