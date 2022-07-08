using System;
using System.Globalization;
using System.Windows.Data;

namespace EnduranceJudge.Gateways.Desktop.Core.Components.Converters;

[ValueConversion(typeof(TimeSpan?), typeof(bool))]
public class TimeSpanToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return ValueSerializer.DEFAULT_SPAN;
        }
        if (value is not TimeSpan timeSpan)
        {
            throw new InvalidOperationException($"Cannot convert '{value}' to string. Expected a TimeSpan object.");
        }
        return ValueSerializer.FormatSpan(timeSpan);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
