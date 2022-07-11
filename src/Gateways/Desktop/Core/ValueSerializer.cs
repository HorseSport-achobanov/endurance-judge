using EnduranceJudge.Application.Core.Exceptions;
using System;
using System.Globalization;
using static EnduranceJudge.Gateways.Desktop.DesktopConstants;
using static EnduranceJudge.Localization.Strings;

namespace EnduranceJudge.Gateways.Desktop.Core;

public class ValueSerializer
{
    public const string DEFAULT_TIME = "--:--:--";
    public const string DEFAULT_SPAN = "-:-:-.-";
    public const string DEFAULT_DOUBLE = "-.---";

    public static DateTime? ParseTime(string value)
    {
        if (value is null or DEFAULT_TIME)
        {
            return null;
        }
        var hasParsed = DateTime.TryParseExact(
            value,
            TIME_FORMAT,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None, out var result);
        if (!hasParsed)
        {
            var message = string.Format(INVALID_DATE_FORMAT_MESSAGE, value, TIME_FORMAT);
            throw new AppException(message);
        }
        return result;
    }
    public static TimeSpan? ParseSpan(string value)
    {
        if (value == null)
        {
            return null;
        }
        if (TimeSpan.TryParse(value, out var timeSpan))
        {
            return timeSpan;
        }
        return null;
    }
    public static string FormatSpan(TimeSpan? span)
    {
        var spanString = span?.ToString(TIME_SPAN_FORMAT) ?? DEFAULT_SPAN;
        if (spanString.EndsWith('.'))
        {
            spanString += "00";
        }
        return spanString;
    }
    public static string FormatTime(DateTime? time)
    {
        var timeString = time?.ToString(TIME_FORMAT) ?? DEFAULT_TIME;
        return timeString;
    }
    public static string FormatDouble(double? value)
    {
        var doubleString = value?.ToString(DOUBLE_FORMAT) ?? DEFAULT_DOUBLE;
        return doubleString;
    }
    public static double? ParseDouble(string value)
    {
        if (double.TryParse(value, out var number))
        {
            return number;
        }
        return null;
    }
}
