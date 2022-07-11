using System;
using System.Globalization;
using System.Windows.Data;

namespace EnduranceJudge.Gateways.Desktop.Converters;

public abstract class XamlConverterBase<TSource, TDestination> : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return default(TDestination);
        }
        if (value is not TSource source)
        {
            var message = $"Invalid XAML value conversion." +
                $" Cannot convert type '{value.GetType()}'. Expected type '{typeof(TSource)}'";
            throw new Exception(message);
        }
        return this.Convert(source);
    }

    protected abstract TDestination Convert(TSource source);
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
