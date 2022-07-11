using EnduranceJudge.Gateways.Desktop.Converters;
using System;

namespace EnduranceJudge.Gateways.Desktop.Core.Components.Converters;

public class DateTimeToStringConverter : XamlConverterBase<DateTime, string>
{
    protected override string Convert(DateTime source)
        => ValueSerializer.FormatTime(source);
}
