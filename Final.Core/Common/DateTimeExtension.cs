

namespace Final.Core.Common;

public static class DateTimeExtension
{
    public static DateTime GetFirstDay(this DateTime date, int ? year = null, int? mount = null)
        => new(year ?? date.Year, mount ?? date.Month, 1);

    public static DateTime GetLastDay(this DateTime date, int? year = null, int? mount = null)
        => new DateTime(year ?? date.Year, mount ?? date.Month, 1).AddMonths(1).AddDays(-1);
}
