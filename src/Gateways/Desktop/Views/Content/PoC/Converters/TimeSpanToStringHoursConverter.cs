using EnduranceJudge.Gateways.Desktop.Converters;
using EnduranceJudge.Gateways.Desktop.Core;
using System;
using static EnduranceJudge.Localization.Strings;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.PoC.Converters;

public class TimeSpanToStringHoursConverter : XamlConverterBase<TimeSpan, string>
{
    protected override string Convert(TimeSpan source)
    {
        var value = ValueSerializer.FormatSpan(source);
        return $"{value} {HOURS_SYMBOL}";
    }
}
