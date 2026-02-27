namespace Flux
{
  public static partial class DateTimeExtensions
  {
    private const double Lb = 1.55051976772e-08;
    private const double Lg = 6.969290134e-10;

    extension(System.DateTime)
    {
      #region CoordinateTimeDifference

      /// <summary>
      /// <para>A clock that performs exactly the same movements as the Solar System but is outside the system's (the Solar System's) gravity well.</para>
      /// <see href="https://en.wikipedia.org/wiki/Barycentric_Coordinate_Time"/>
      /// </summary>
      public static double GetBarycentricCoordinateTimeDifference(System.DateTime source)
        => Lb * (source.ToJulianDate(Temporal.TemporalCalendar.GregorianCalendar).Value - 2443144.5003725) * 86400;

      /// <summary>
      /// <para>A clock that performs exactly the same movements as the Earth but is outside the system's (Earth's) gravity well.</para>
      /// <see href="https://en.wikipedia.org/wiki/Geocentric_Coordinate_Time"/>
      /// </summary>
      public static double GetGeocentricCoordinateTimeDifference(System.DateTime source)
      => Lg * (source.ToJulianDate(Temporal.TemporalCalendar.GregorianCalendar).Value - 2443144.5003725) * 86400;

      #endregion

      #region OAEpock (OLE Automation)

      public static System.DateTime OAEpoch
        => new(1899, 12, 31, 0, 0, 0);

      #endregion

      #region UnixTime

      public static System.DateTime UnixEpoch
        => new(1970, 1, 1, 0, 0, 0);

      public static System.DateTime FromUnixTimestamp(long unixTimestamp)
        => get_UnixEpoch().AddSeconds(unixTimestamp);

      public static System.DateTime FromUnixUltraTimestamp(long unixUltraTimestamp)
        => get_UnixEpoch().AddMilliseconds(unixUltraTimestamp);

      public static long ToUnixTimestamp(System.DateTime source)
        => (long)(source - get_UnixEpoch()).TotalSeconds;

      public static long ToUnixUltraTimestamp(System.DateTime source)
        => (long)(source - get_UnixEpoch()).TotalMilliseconds;

      #endregion

      /// <summary>Determines the number of days in the quarter of the source.</summary>
      public static int DaysInQuarter(int year, int quarter)
        => EndOfQuarter(year, quarter).Subtract(StartOfQuarter(year, quarter)).Days + 1;

      /// <summary>Determines the number of days in the year of a <see cref="System.DateTime"/>.</summary>
      public static int DaysInYear(int year)
        => System.DateTime.IsLeapYear(year) ? 366 : 365;

      /// <summary>Determines the last day of the month in the source.</summary>
      public static System.DateTime EndOfMonth(int year, int month)
        => new(year, month, System.DateTime.DaysInMonth(year, month));

      /// <summary>Determines the last day of the specified quarter in a <see cref="System.DateTime"/>.</summary>
      public static System.DateTime EndOfQuarter(int year, int quarter)
      {
        return quarter switch
        {
          1 => new(year, 3, 31),
          2 => new(year, 6, 30),
          3 => new(year, 9, 30),
          4 => new(year, 12, 31),
          _ => throw new System.ArgumentOutOfRangeException(nameof(quarter)),
        };
      }

      /// <summary>Determines the last day of the week in the source, based on the current DateTimeFormatInfo.</summary>
      public static System.DateTime EndOfWeek(int year, int week, System.Globalization.CultureInfo cultureInfo)
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThan(week, 1);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(week, 53);

        var startOfWeek = StartOfWeek(year, week, cultureInfo);

        return startOfWeek.AddDays(6);
      }

      /// <summary>Determines the last day of the year in the source.</summary>
      public static System.DateTime EndOfYear(int year)
        => new(year, 12, 31);

      /// <summary>Determines the first day of the month in the source.</summary>
      public static System.DateTime StartOfMonth(int year, int month)
        => new(year, month, 1);

      /// <summary>Determines the first day of the specified quarter in the source.</summary>
      public static System.DateTime StartOfQuarter(int year, int quarter)
        => quarter switch
        {
          1 => new(year, 1, 1),
          2 => new(year, 4, 1),
          3 => new(year, 7, 1),
          4 => new(year, 10, 1),
          _ => throw new System.ArgumentOutOfRangeException(nameof(quarter)),
        };

      public static DateTime StartOfWeek(int year, int week, System.Globalization.CultureInfo cultureInfo)
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThan(week, 1);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(week, 53);

        var firstDayOfWeekInYear = System.DateTime.StartOfYear(year);

        if (WeekOfYear(firstDayOfWeekInYear, cultureInfo) is var weekOfYear && weekOfYear != 1)
          firstDayOfWeekInYear = firstDayOfWeekInYear.AddDays(1);

        var startOfWeek = firstDayOfWeekInYear.AddDays((int)cultureInfo.DateTimeFormat.FirstDayOfWeek - (int)firstDayOfWeekInYear.DayOfWeek);

        return startOfWeek.AddWeeks(week - 1);
      }

      /// <summary>Determines the first day of the year in the source.</summary>
      public static System.DateTime StartOfYear(int year)
        => new(year, 1, 1);

      public static int WeekOfYear(System.DateTime source, System.Globalization.CultureInfo culture)
        => culture.Calendar.GetWeekOfYear(source, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
    }

    extension(System.DateTime source)
    {
      #region Quarter

      /// <summary>Returns the current calendar quarter in the range [1, 4] of the <paramref name="source"/>.</summary>
      public int Quarter
        => (source.Month - 1) / 3 + 1;

      #endregion

      #region Add..

      public System.DateTime AddFortnights(int count)
        => source.AddDays(count * 14);

      public System.DateTime AddQuarters(int count)
        => source.AddMonths(count * 3);

      public System.DateTime AddWeeks(int count)
        => source.AddDays(count * 7);

      #endregion

      #region Age

      /// <summary>
      /// <para>Determines the age in years (with a decimal point) between the <paramref name="source"/> (e.g. a birth) and the <paramref name="target"/> (e.g. a birthday). The fractional portion of the resulting age-in-total-years is the percentage of the number of days that have passed since the last "birthday" toward the "birthday" that has not yet occured.</para>
      /// </summary>
      /// <remarks>
      /// <para>Note that this is the common or typical western way of calculating age. There are a multitude of scenarios where this way of calulcating would not be applicable.</para>
      /// <para>The method works in both direction of time. When using it backwards, i.e. <paramref name="source"/> is greater than <paramref name="target"/>, the resulting age-in-years will be negative, for distinction.</para>
      /// </remarks>
      public double AgeInTotalYears(System.DateTime target)
      {
        var ageInYears = AgeInYears(source, target); // Age in years (signed depending on source/target).

        var sign = source <= target ? 1 : -1; // The unit sign of age direction, based on source/target, either 1 (when >= 0) or -1 (when < 0).

        var lastBirthday = source.AddYears(ageInYears); // This is the most recent "birthday" (either direction).
        var nextBirthday = lastBirthday.AddYears(sign); // This is the upcoming "birthday" (either direction).

        var spanSinceLast = (target - lastBirthday).Ticks; // The span of time since the last birthday to the target.
        var spanUntilNext = (nextBirthday - target).Ticks; // The span of time until the next birthday from the target.

        var additionalAge = (double)spanSinceLast / (double)(spanSinceLast + spanUntilNext) * sign; // The quotient of target date and next "birthday" spans to the last "birtday".

        return ageInYears + additionalAge;
      }

      /// <summary>
      /// <para>Determines the age in integer years between the <paramref name="source"/> (e.g. a birth) and the <paramref name="target"/> (e.g. a birthday).</para>
      /// </summary>
      /// <remarks>
      /// <para>Note that this is the common or typical western way of calculating age. There are a multitude of scenarios where this way of calulcating would not be applicable.</para>
      /// <para>The method works in both direction of time. When using it backwards, i.e. <paramref name="source"/> is greater than <paramref name="target"/>, the resulting age-in-years will be negative, for distinction.</para>
      /// </remarks>
      public int AgeInYears(System.DateTime target)
        => target.Year - source.Year is int age && age > 0 && source.AddYears(age) > target ? age - 1 : age < 0 && source.AddYears(age) < target ? age + 1 : age;

      #endregion

      #region DayOfWeek

      /// <summary>Determines the two closest DayOfWeek dates before and after the source.</summary>
      public (System.DateTime closest, System.DateTime secondClosest) ClosestDayOfWeek(System.DayOfWeek dayOfWeek, bool unequal)
      {
        var last = LastDayOfWeek(source, dayOfWeek, unequal);
        var next = NextDayOfWeek(source, dayOfWeek, unequal);

        return next.Subtract(source) < source.Subtract(last) ? (next, last) : (last, next);
      }

      /// <summary>Yields the <see cref="System.DateTime"/> of the previous specified <paramref name="dayOfWeek"/> relative to the <paramref name="source"/>. Use <paramref name="unequal"/> to (false = include, true = exclude) <paramref name="source"/> as a result for the past <see cref="System.DayOfWeek"/>.</summary>
      public System.DateTime LastDayOfWeek(System.DayOfWeek dayOfWeek, bool unequal)
        => unequal && source.DayOfWeek == dayOfWeek
        ? source.AddDays(-7)
        : source.AddDays(((int)dayOfWeek - (int)source.DayOfWeek - 7) % 7);

      /// <summary>Yields the <see cref="System.DateTime"/> of the next specified <paramref name="dayOfWeek"/> relative to the <paramref name="source"/>. Use <paramref name="unequal"/> to include/exclude <paramref name="source"/> as a result for the future <see cref="System.DayOfWeek"/>.</summary>
      public System.DateTime NextDayOfWeek(System.DayOfWeek dayOfWeek, bool unequal)
        => unequal && source.DayOfWeek == dayOfWeek
        ? source.AddDays(7)
        : source.AddDays(((int)dayOfWeek - (int)source.DayOfWeek + 7) % 7);

      #endregion

      #region FirstDayOf..

      /// <summary>Determines the first day of the week in the source, based on the current DateTimeFormatInfo.</summary>
      public System.DateTime FirstDayOfWeek(System.Globalization.DateTimeFormatInfo? dateTimeFormatInfo = null)
      {
        dateTimeFormatInfo ??= System.Globalization.DateTimeFormatInfo.CurrentInfo;

        return source.FirstDayOfWeek(dateTimeFormatInfo.FirstDayOfWeek);
      }

      /// <summary>Determines the first day of the week in the source, based on the specified DateTimeFormatInfo.</summary>
      public System.DateTime FirstDayOfWeek(System.DayOfWeek firstDayOfWeek)
        => source.LastDayOfWeek(firstDayOfWeek, true);

      #endregion

      #region GetDates

      /// <summary>Yields a sequence of dates between the source and the specified number of days.</summary>
      /// <param name="source">Start date.</param>
      /// <param name="count">The number of days to include. Can be negative.</param>
      public System.Collections.Generic.IEnumerable<System.DateTime> GetDates(int count)
      {
        yield return source;

        var sign = int.IsNegative(count) ? -1 : 1;

        for (var index = sign; index != count; index += sign)
          yield return source.AddDays(index);
      }

      /// <summary>Yields the dates in the month of the source.</summary>
      public System.Collections.Generic.List<System.DateTime> GetDatesInMonth()
        => [.. System.DateTime.StartOfMonth(source.Year, source.Month).GetDates(System.DateTime.DaysInMonth(source.Year, source.Month))];

      /// <summary>Yields the dates in the current calendar quarter of the source.</summary>
      public System.Collections.Generic.List<System.DateTime> GetDatesInQuarter()
        => [.. System.DateTime.StartOfQuarter(source.Year, source.Quarter).GetDatesTo(System.DateTime.EndOfQuarter(source.Year, source.Quarter), true)];

      /// <summary>Yields the dates in the week of the source.</summary>
      public System.Collections.Generic.List<System.DateTime> GetDatesInWeek()
        => [.. source.FirstDayOfWeek().GetDates(7)];

      /// <summary>Yields the dates in the year of the source.</summary>
      public System.Collections.Generic.List<System.DateTime> GetDatesInYear()
        => [.. System.DateTime.StartOfYear(source.Year).GetDates(System.DateTime.DaysInYear(source.Year))];

      /// <summary>Yields a sequence of dates between the source and the specified target, which can be included or not.</summary>
      public System.Collections.Generic.IEnumerable<System.DateTime> GetDatesTo(System.DateTime target, bool includeTarget)
        => source.GetDates((target - source).Days is var numberOfDays && includeTarget ? numberOfDays + Number.Sign(numberOfDays) : numberOfDays);

      #endregion

      #region Week

      /// <summary>
      /// <para>Returns the current calendar week of year in the range [1, 53] of a <see cref="System.DateTime"/>.</para>
      /// </summary>
      public int Week(System.Globalization.CultureInfo? cultureInfo = null)
        => System.DateTime.WeekOfYear(source, cultureInfo ?? System.Globalization.CultureInfo.CurrentCulture);

      #endregion

      #region LastDayOf..

      /// <summary>Determines the last day of the week in the source, based on the current DateTimeFormatInfo.</summary>
      public System.DateTime LastDayOfWeek(System.Globalization.DateTimeFormatInfo? dateTimeFormatInfo = null)
      {
        dateTimeFormatInfo ??= System.Globalization.DateTimeFormatInfo.CurrentInfo;

        return LastDayOfWeek(source, (System.DayOfWeek)(((int)(dateTimeFormatInfo.FirstDayOfWeek + 6) % 7)));
      }

      /// <summary>Determines the last day of the week in the source, based on the specified DateTimeFormatInfo.</summary>
      public System.DateTime LastDayOfWeek(System.DayOfWeek lastDayOfWeek)
        => NextDayOfWeek(source, lastDayOfWeek, true);

      #endregion

      #region Ranges

      public static System.TimeSpan OverlapOfRanges(System.DateTime startA, System.DateTime endA, System.DateTime startB, System.DateTime endB)
        => (endA < endB ? endA : endB) - (startA > startB ? startA : startB);

      public static System.TimeSpan OverlapOfRanges(System.DateTime startA, System.TimeSpan lengthA, System.DateTime startB, System.TimeSpan lengthB)
        => OverlapOfRanges(startA, startA.Add(lengthA), startB, startB.Add(lengthB));

      public static bool AreRangesOverlapping(System.DateTime startA, System.DateTime endA, System.DateTime startB, System.DateTime endB)
        => (startA > startB ? startA : startB) <= (endA < endB ? endA : endB);

      public static bool AreRangesOverlapping(System.DateTime startA, System.TimeSpan lengthA, System.DateTime startB, System.TimeSpan lengthB)
        => AreRangesOverlapping(startA, startA.Add(lengthA), startB, startB.Add(lengthB));

      #endregion

      #region ToString

      /// <summary>Returns the source as a string formatted for use in a file name using "yyyyMMdd HHmmss fffffff".</summary>
      public string ToFileNameFriendlyString()
        => source.ToString("yyyyMMdd HHmmss fffffff", System.Globalization.CultureInfo.InvariantCulture);

      /// <summary>Returns the System.DateTime 'kind' part only as a string using the format "zzz". This is the <see cref="System.DateTimeKind"/> value used when the value was created.</summary>
      public string ToKindString()
        => source.ToString("zzz", System.Globalization.CultureInfo.InvariantCulture);

      /// <summary>Returns the System.DateTime as a string that complies with ISO 8601. The native .ToString("o") is used internally.</summary>
      /// <see href="https://en.wikipedia.org/wiki/ISO_8601"/>
      //[System.Obsolete(@"Please use the built-in [datetime].ToString(@""o"") instead.")]
      public string ToIso8601String()
        => source.ToString("o", System.Globalization.CultureInfo.InvariantCulture);

      /// <summary>Returns the System.DateTime date part only as a string using the ISO 8601 format "YYYY-MM-DD".</summary>
      /// <see href="https://en.wikipedia.org/wiki/ISO_8601"/>
      public string ToIso8601StringDate()
        => source.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

      /// <summary></summary>
      /// <see href="https://en.wikipedia.org/wiki/Ordinal_date"/>
      /// <seealso cref="https://en.wikipedia.org/wiki/ISO_8601#Week_dates"/>
      public string ToIso8601StringOrdinalDate()
        => $"{System.Globalization.ISOWeek.GetYear(source)}-{source.DayOfYear}";

      /// <summary>Returns the System.DateTime time part only as a string in a dynamic ISO 8601 output "hh:mm[[:ss].sss]" (seconds and fractional seconds are omitted if zero).</summary>
      /// <see href="https://en.wikipedia.org/wiki/ISO_8601"/>
      public string ToIso8601StringTime()
        => source.Millisecond > 0 ? ToIso8601StringTimeHhMmSsSss(source) : source.Second > 0 ? ToIso8601StringTimeHhMmSs(source) : ToIso8601StringTimeHhMm(source);

      /// <summary>Returns the System.DateTime time part only as a string using the ISO 8601 format hh:mm".</summary>
      /// <see href="https://en.wikipedia.org/wiki/ISO_8601"/>
      public string ToIso8601StringTimeHhMm()
        => source.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture);

      /// <summary>Returns the System.DateTime time part only as a string using the ISO 8601 format hh:mm:ss".</summary>
      /// <see href="https://en.wikipedia.org/wiki/ISO_8601"/>
      public string ToIso8601StringTimeHhMmSs()
        => source.ToString("HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

      /// <summary>Returns the System.DateTime time part only as a string using the ISO 8601 format hh:mm:ss.sss".</summary>
      /// <see href="https://en.wikipedia.org/wiki/ISO_8601"/>
      public string ToIso8601StringTimeHhMmSsSss()
        => source.ToString("HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);

      /// <summary></summary>
      /// <see href="https://en.wikipedia.org/wiki/ISO_week_date"/>
      /// <seealso cref="https://en.wikipedia.org/wiki/ISO_8601#Ordinal_dates"/>
      public string ToIso8601StringWeekDate()
        => $"{System.Globalization.ISOWeek.GetYear(source)}-W{System.Globalization.ISOWeek.GetWeekOfYear(source)}-{(int)source.DayOfWeek}";

      /// <summary>Returns a string in SQL Server parsable format, with dynamic precision based on zero values for fractional seconds.</summary>
      public string ToSqlString()
        => source.Millisecond >= 1000 ? source.ToSqlStringYyyyMmDdHhMmSsFffffff() : source.Millisecond >= 1 ? source.ToSqlStringYyyyMmDdHhMmSsFff() : source.ToSqlStringYyyyMmDdHhMmSs();

      /// <summary>Returns a string in SQL Server parsable format, precision includes seconds.</summary>
      public string ToSqlStringYyyyMmDdHhMmSs()
        => source.ToString(@"yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

      /// <summary>Returns a string in SQL Server parsable format, precision includes milliseconds.</summary>
      public string ToSqlStringYyyyMmDdHhMmSsFff()
        => source.ToString(@"yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);

      /// <summary>Returns a string in SQL Server parsable format, precision includes ticks (one more digit than microseconds).</summary>
      public string ToSqlStringYyyyMmDdHhMmSsFffffff()
        => source.ToString(@"yyyy-MM-dd HH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture);

      #endregion

      #region WeekOfYear

      /// <summary>
      /// <para>Determines the current week of year of the <paramref name="source"/>. Uses the specified <paramref name="dateTimeFormatInfo"/>, or <see cref="System.Globalization.DateTimeFormatInfo.CurrentInfo"/> if null.</para>
      /// </summary>
      public int WeekOfYear(System.Globalization.DateTimeFormatInfo? dateTimeFormatInfo = null)
      {
        dateTimeFormatInfo ??= System.Globalization.DateTimeFormatInfo.CurrentInfo;

        return dateTimeFormatInfo.Calendar.GetWeekOfYear(source, dateTimeFormatInfo.CalendarWeekRule, dateTimeFormatInfo.FirstDayOfWeek);
      }

      #endregion
    }
  }
}
