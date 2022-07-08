using System;
using System.Globalization;
using System.Windows.Data;

namespace EnduranceJudge.Gateways.Desktop.Core.Components.Converters;

[ValueConversion(typeof(double?), typeof(bool))]
public class DoubleToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return ValueSerializer.DEFAULT_DOUBLE;
        }
        if (value is not double doubleValue)
        {
            throw new InvalidOperationException($"Cannot convert '{value}' to string. Expected a 'double' object.");
        }
        return ValueSerializer.FormatDouble(doubleValue);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
