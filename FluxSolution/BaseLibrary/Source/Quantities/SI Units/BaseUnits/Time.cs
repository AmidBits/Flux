namespace Flux.Quantities
{
  public enum TimeUnit
  {
    /// <summary>This is the default unit for <see cref="Time"/>.</summary>
    Second,
    Picosecond,
    Nanosecond,
    /// <summary>
    /// <para>The unit of .NET ticks.</para>
    /// <para><see href=""/></para>
    /// </summary>
    Ticks,
    Microsecond,
    Millisecond,
    Minute,
    Hour,
    Day,
    Week,
    /// <summary>This represents two weeks.</summary>
    Fortnight,
    /// <summary>Represents the musical BPM.</summary>
    BeatsPerMinute
  }

  /// <summary>
  /// <para>Time. SI unit of second. This is a base quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Time"/></para>
  /// </summary>
  public readonly record struct Time
    : System.IComparable, System.IComparable<Time>, System.IFormattable, ISiPrefixValueQuantifiable<double, TimeUnit>
  {
    ///// <see href="https://en.wikipedia.org/wiki/Flick_(time)"></see>
    //public static readonly Time Flick = new(1.0 / 705600000.0);
    ///// <summary>Amount of time in one millisecond.</summary>
    //public static readonly Time Millisecond = new(0.001);
    ///// <summary>Amount of time in one second.</summary>
    //public static readonly Time Second = new(1);
    ///// <summary>Amount of time in one minute.</summary>
    //public static readonly Time Minute = new(60);
    ///// <summary>Amount of time in one hour.</summary>
    //public static readonly Time Hour = new(3600);
    ///// <summary>Amount of time in one day.</summary>
    //public static readonly Time Day = new(86400);

    private readonly double m_value;

    /// <summary>
    /// <para>Creates a new instance from the specified <paramref name="value"/> of <paramref name="unit"/>. The default <paramref name="unit"/> is <see cref="TimeUnit.Second"/></para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="unit"></param>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public Time(double value, TimeUnit unit = TimeUnit.Second)
      => m_value = unit switch
      {
        TimeUnit.Second => value,
        TimeUnit.Picosecond => value / 1000000000000,
        TimeUnit.Nanosecond => ConvertNanosecondToSecond(value),
        TimeUnit.Ticks => ConvertTickToSecond(value),
        TimeUnit.Microsecond => ConvertMicrosecondToSecond(value),
        TimeUnit.Millisecond => ConvertMillisecondToSecond(value),
        TimeUnit.Minute => value * 60,
        TimeUnit.Hour => value * 3600,
        TimeUnit.Day => ConvertDayToSecond(value),
        TimeUnit.Week => ConvertWeekToSecond(value),
        TimeUnit.Fortnight => ConvertFortnightToSecond(value),
        TimeUnit.BeatsPerMinute => ConvertBpmToSecond(value),
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    /// <summary>
    /// <para>Creates a new instance from the specified <see cref="MetricPrefix"/> (metric multiple) of <see cref="TimeUnit.Second"/>, e.g. <see cref="MetricPrefix.Milli"/> for milliseconds.</para>
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="prefix"></param>
    public Time(double seconds, MetricPrefix prefix) => m_value = prefix.Convert(seconds, MetricPrefix.NoPrefix);

    /// <summary>Creates a new instance from the specified <paramref name="timeSpan"/>.</summary>
    public Time(System.TimeSpan timeSpan) : this(timeSpan.TotalSeconds, TimeUnit.Second) { }

    public System.TimeSpan ToTimeSpan() => System.TimeSpan.FromSeconds(m_value);

    #region Static methods

    #region Conversion methods

    /// <summary>
    /// <para>Convert beats-per-minute to seconds.</para>
    /// </summary>
    /// <param name="bpm"></param>
    /// <returns></returns>
    public static double ConvertBpmToSecond(double bpm) => 60 / bpm;

    public static double ConvertDayToSecond(double days) => days * 86400;

    public static double ConvertFortnightToSecond(double fortnights) => fortnights * 1209600;

    public static double ConvertMicrosecondToSecond(double microseconds) => microseconds / 1000000;

    public static double ConvertMicrosecondToMillisecond(double microseconds) => microseconds / 1000;

    public static double ConvertMicrosecondToNanosecond(double microseconds) => microseconds * 1000;

    public static double ConvertMillisecondToMicrosecond(double milliseconds) => milliseconds * 1000;

    public static double ConvertMillisecondToNanosecond(double milliseconds) => milliseconds * 1000000;

    public static double ConvertMillisecondToSecond(double milliseconds) => milliseconds / 1000;

    public static double ConvertNanosecondToMicrosecond(double nanoseconds) => nanoseconds / 1000;

    public static double ConvertNanosecondToMillisecond(double nanoseconds) => nanoseconds / 1000000;

    public static double ConvertNanosecondToSecond(double nanoseconds) => nanoseconds / 1000000000;

    /// <summary>
    /// <para>Convert seconds to beats-per-minute.</para>
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static double ConvertSecondToBpm(double seconds) => 60 / seconds;

    public static double ConvertSecondToDay(double seconds) => seconds / 86400;

    public static double ConvertSecondToFortnight(double seconds) => seconds / 1209600;

    public static double ConvertSecondToMicrosecond(double seconds) => seconds * 1000000;

    public static double ConvertSecondToMillisecond(double seconds) => seconds * 1000;

    public static double ConvertSecondToNanosecond(double seconds) => seconds * 1000000000;

    /// <summary>
    /// <para>Convert seconds to .NET ticks. There are 10,000,000 ticks per second.</para>
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static double ConvertSecondToTick(double seconds) => seconds * 10000000;

    public static double ConvertSecondToWeek(double seconds) => seconds / 604800;

    /// <summary>
    /// <para>Convert .NET ticks to seconds. There are 10,000,000 ticks per second.</para>
    /// </summary>
    /// <param name="ticks"></param>
    /// <returns></returns>
    public static double ConvertTickToSecond(double ticks) => ticks / 10000000;

    public static double ConvertWeekToSecond(double weeks) => weeks * 604800;

    #endregion // Conversion methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Time a, Time b) => a.CompareTo(b) < 0;
    public static bool operator <=(Time a, Time b) => a.CompareTo(b) <= 0;
    public static bool operator >(Time a, Time b) => a.CompareTo(b) > 0;
    public static bool operator >=(Time a, Time b) => a.CompareTo(b) >= 0;

    public static Time operator -(Time v) => new(-v.m_value, TimeUnit.Second);
    public static Time operator +(Time a, double b) => new(a.m_value + b, TimeUnit.Second);
    public static Time operator +(Time a, Time b) => a + b.m_value;
    public static Time operator /(Time a, double b) => new(a.m_value / b, TimeUnit.Second);
    public static Time operator /(Time a, Time b) => a / b.m_value;
    public static Time operator *(Time a, double b) => new(a.m_value * b, TimeUnit.Second);
    public static Time operator *(Time a, Time b) => a * b.m_value;
    public static Time operator %(Time a, double b) => new(a.m_value % b, TimeUnit.Second);
    public static Time operator %(Time a, Time b) => a % b.m_value;
    public static Time operator -(Time a, double b) => new(a.m_value - b, TimeUnit.Second);
    public static Time operator -(Time a, Time b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Time o ? CompareTo(o) : -1;

    // IComparable<T>
    public int CompareTo(Time other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitValueSymbolString(TimeUnit.Second, format, formatProvider);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Time.Value"/> property is in <see cref="TimeUnit.Second"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetUnitName() + GetUnitName(TimeUnit.Second, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetUnitSymbol(preferUnicode) + GetUnitSymbol(TimeUnit.Second, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

    public string ToSiPrefixValueNameString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = true)
      => GetSiPrefixValue(prefix).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetSiPrefixName(prefix, preferPlural);

    public string ToSiPrefixValueSymbolString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetSiPrefixValue(prefix).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetSiPrefixSymbol(prefix, preferUnicode);

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitQuantifiable<>

    public string GetUnitName(TimeUnit unit, bool preferPlural) => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(TimeUnit unit, bool preferUnicode)
      => unit switch
      {
        TimeUnit.Second => "s",
        TimeUnit.Picosecond => preferUnicode ? "\u33B0" : "ps",
        TimeUnit.Nanosecond => preferUnicode ? "\u33B1" : "ns",
        TimeUnit.Ticks => "tick" + GetUnitValue(unit).PluralStringSuffix(),
        TimeUnit.Microsecond => preferUnicode ? "\u33B2" : "\u00B5s",
        TimeUnit.Millisecond => preferUnicode ? "\u33B3" : "ms",
        TimeUnit.Minute => "min",
        TimeUnit.Hour => "h",
        TimeUnit.Day => "d",
        TimeUnit.Week => "week" + GetUnitValue(unit).PluralStringSuffix(),
        TimeUnit.Fortnight => "fortnight" + GetUnitValue(unit).PluralStringSuffix(),
        TimeUnit.BeatsPerMinute => "bpm",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(TimeUnit unit)
        => unit switch
        {
          TimeUnit.Picosecond => m_value * 1000000000000,
          TimeUnit.Nanosecond => ConvertSecondToNanosecond(m_value),
          TimeUnit.Ticks => ConvertSecondToTick(m_value),
          TimeUnit.Microsecond => ConvertSecondToMicrosecond(m_value),
          TimeUnit.Millisecond => ConvertSecondToMillisecond(m_value),
          TimeUnit.Second => m_value,
          TimeUnit.Minute => m_value / 60,
          TimeUnit.Hour => m_value / 3600,
          TimeUnit.Day => ConvertSecondToDay(m_value),
          TimeUnit.Week => ConvertSecondToWeek(m_value),
          TimeUnit.Fortnight => ConvertSecondToFortnight(m_value),
          TimeUnit.BeatsPerMinute => ConvertSecondToBpm(m_value),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

    public string ToUnitValueNameString(TimeUnit unit = TimeUnit.Second, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitName(unit, preferPlural);

    public string ToUnitValueSymbolString(TimeUnit unit = TimeUnit.Second, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + unitSpacing.ToSpacingString() + GetUnitSymbol(unit, preferUnicode);

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
