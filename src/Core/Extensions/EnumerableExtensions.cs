using System;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Core.Extensions;

public static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (var item in enumerable)
        {
            action(item);
        }
    }

    public static void ForEach<T>(this IEnumerable<T> enumerable, Func<T, object> action)
    {
        foreach (var item in enumerable)
        {
            action(item);
        }
    }

    public static TimeSpan SumSpan(this IEnumerable<TimeSpan?> enumerable)
    {
        var result = TimeSpan.Zero;
        foreach (var span in enumerable.Where(x => x.HasValue))
        {
            result += span!.Value;
        }
        return result;
    }
}
