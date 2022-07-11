using EnduranceJudge.Gateways.Desktop.Converters;
using System;

namespace EnduranceJudge.Gateways.Desktop.Core.Components.Converters;

public class TimeSpanToStringConverter : XamlConverterBase<TimeSpan, string>
{
    protected override string Convert(TimeSpan source)
        => ValueSerializer.FormatSpan(source);
}
