using System;
using System.Globalization;
using System.Windows.Data;

namespace EnduranceJudge.Gateways.Desktop.Core.Components.Converters;

[ValueConversion(typeof(DateTime?), typeof(bool))]
public class DateTimeToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return ValueSerializer.DEFAULT_TIME;
        }
        if (value is not DateTime date)
        {
            throw new InvalidOperationException($"Cannot convert '{value}' to string. Expected a DateTime object.");
        }
        return ValueSerializer.FormatTime(date);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
