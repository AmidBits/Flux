namespace Flux.Quantities
{
  /// <summary>
  /// <para>Time. SI unit of second. This is a base quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Time"/></para>
  /// </summary>
  public readonly record struct Time
    : System.IComparable, System.IComparable<Time>, System.IFormattable, ISiUnitValueQuantifiable<double, TimeUnit>
  {
    ///// <summary>Amount of time in one picosecond.</summary>
    //public static readonly Time Picosecond = new(MetricPrefix.Pico, 1);
    ///// <summary>Amount of time in one nanosecond.</summary>
    //public static readonly Time Nanosecond = new(MetricPrefix.Nano, 1);
    ///// <summary>Amount of time in one microsecond.</summary>
    //public static readonly Time Microsecond = new(MetricPrefix.Micro, 1);
    ///// <summary>Amount of time in one millisecond.</summary>
    //public static readonly Time Millisecond = new(MetricPrefix.Milli, 1);
    /// <summary>Amount of time in one second.</summary>
    public static readonly Time Second = new(1);
    /// <summary>Amount of time in one minute.</summary>
    public static readonly Time Minute = new(1, TimeUnit.Minute);
    /// <summary>Amount of time in one hour.</summary>
    public static readonly Time Hour = new(1, TimeUnit.Hour);
    /// <summary>Amount of time in one day.</summary>
    public static readonly Time Day = new(1, TimeUnit.Day);

    private readonly double m_value;

    /// <summary>
    /// <para>Creates a new instance from the specified <paramref name="value"/> of <paramref name="unit"/>. The default <paramref name="unit"/> is <see cref="TimeUnit.Second"/></para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="unit"></param>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public Time(double value, TimeUnit unit = TimeUnit.Second) => m_value = ConvertFromUnit(unit, value);

    /// <summary>
    /// <para>Creates a new instance from the specified <see cref="MetricPrefix"/> (metric multiple) of <see cref="TimeUnit.Second"/>, e.g. <see cref="MetricPrefix.Milli"/> for milliseconds.</para>
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="prefix"></param>
    public Time(MetricPrefix prefix, double seconds) => m_value = prefix.ConvertTo(seconds, MetricPrefix.Unprefixed);

    /// <summary>Creates a new instance from the specified <paramref name="timeSpan"/>.</summary>
    public Time(System.TimeSpan timeSpan) : this(timeSpan.TotalSeconds, TimeUnit.Second) { }

    public System.TimeSpan ToTimeSpan() => System.TimeSpan.FromSeconds(m_value);

    #region Static methods

    #region Conversion methods

    /// <summary>
    /// <para>Converts a second value to sub-second parts, plus early terminations (using double) for milli-seconds and micro-seconds for functionality with less resolution.</para>
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static (long second, double seconds, int milliSecond, double milliSeconds, int microSecond, double microSeconds, int nanoSecond) ConvertSecondsToSubSecondParts(double seconds)
    {
      var ts = System.TimeSpan.FromSeconds(seconds);

      return (
        long.CreateChecked(double.Truncate(seconds)),
        seconds,
        ts.Milliseconds,
        ts.Milliseconds + (ts.Microseconds * 1000L + ts.Nanoseconds) / 1000000.0,
        ts.Microseconds,
        ts.Microseconds + ts.Nanoseconds / 1000.0,
        ts.Nanoseconds
      );
    }

    /// <summary>
    /// <para>Converts a sub-second value into sub-second integer components, plus early terminations (using double) for milli-seconds, micro-seconds and nano-seconds for functionality with less resolution.</para>
    /// </summary>
    /// <param name="subSecondTotalValue"></param>
    /// <returns></returns>
    /// <remarks>
    /// <para>Only handles sub-second parts and only down to <see cref="MetricPrefix.Nano"/>.</para>
    /// <para>Second is the implied base here, so it would be pointless to allow anything greater-or-equal-to <see cref="MetricPrefix.Unprefixed"/>.</para>
    /// </remarks>
    public static (long second, double seconds, int milliSecond, double milliSeconds, int microSecond, double microSeconds, int nanoSecond, double nanoSeconds) ConvertTotalSubSecondUnitToSubSecondParts(MetricPrefix subSecondUnit, long subSecondTotalValue)
    {
      if ((int)subSecondUnit >= (int)MetricPrefix.Unprefixed || (int)subSecondUnit < (int)MetricPrefix.Nano) throw new System.ArgumentOutOfRangeException(nameof(subSecondUnit)); // Only allow sub-second prefixes.

      checked
      {
        var value = (double)subSecondTotalValue;
        var unitValue = (long)System.Numerics.BigInteger.Pow(10, int.Abs((int)subSecondUnit));

        var pvs = new double[4];
        var svs = new long[4];

        for (var i = 0; i < 4; i++)
        {
          if (unitValue == 0) break;

          var pv = value / unitValue;
          var sv = (long)double.Truncate(pv);

          value -= (sv * unitValue);
          unitValue /= 1000;

          pvs[i] = pv;
          svs[i] = sv;
        }

        return (svs[0], pvs[0], (int)svs[1], pvs[1], (int)svs[2], pvs[2], (int)svs[3], pvs[3]);
      }
    }

    //public static (long second, double seconds, int milliSecond, double milliSeconds, int microSecond, double microSeconds, int nanoSecond, double nanoSeconds) ConvertSecondsToSubSecondParts(double seconds)
    //{
    //  checked
    //  {
    //    var second = (long)double.Truncate(seconds);

    //    var milliSeconds = (seconds - second) * 1000L;

    //    var milliSecond = (int)double.Truncate(milliSeconds);

    //    var microSeconds = seconds * 1000000L - (milliSecond * 1000L + second * 1000000L);

    //    var microSecond = (int)double.Truncate(microSeconds);

    //    var nanoSeconds = seconds * 1000000000L - (microSecond * 1000L + milliSecond * 1000000L + second * 1000000000L);

    //    var nanoSecond = (int)double.Truncate(nanoSeconds);

    //    return (second, seconds, milliSecond, milliSeconds, microSecond, microSeconds, nanoSecond, nanoSeconds);
    //  }
    //}

    #endregion // Conversion methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Time a, Time b) => a.CompareTo(b) < 0;
    public static bool operator >(Time a, Time b) => a.CompareTo(b) > 0;
    public static bool operator <=(Time a, Time b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Time a, Time b) => a.CompareTo(b) >= 0;

    public static Time operator -(Time v) => new(-v.m_value);
    public static Time operator *(Time a, Time b) => new(a.m_value * b.m_value);
    public static Time operator /(Time a, Time b) => new(a.m_value / b.m_value);
    public static Time operator %(Time a, Time b) => new(a.m_value % b.m_value);
    public static Time operator +(Time a, Time b) => new(a.m_value + b.m_value);
    public static Time operator -(Time a, Time b) => new(a.m_value - b.m_value);
    public static Time operator *(Time a, double b) => new(a.m_value * b);
    public static Time operator /(Time a, double b) => new(a.m_value / b);
    public static Time operator %(Time a, double b) => new(a.m_value % b);
    public static Time operator +(Time a, double b) => new(a.m_value + b);
    public static Time operator -(Time a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Time o ? CompareTo(o) : -1;

    // IComparable<T>
    public int CompareTo(Time other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(TimeUnit.Second, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(TimeUnit.Second, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(TimeUnit unit, double value)
      => unit switch
      {
        TimeUnit.Second => value,
        TimeUnit.BeatPerMinute => 60 / value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(TimeUnit unit, double value)
      => unit switch
      {
        TimeUnit.Second => value,
        TimeUnit.BeatPerMinute => 60 / value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, TimeUnit from, TimeUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(TimeUnit unit)
      => unit switch
      {
        TimeUnit.Second => 1,

        TimeUnit.Tick => 0.0000001,
        TimeUnit.Minute => 60,
        TimeUnit.Hour => 3600,
        TimeUnit.Day => 86400,
        TimeUnit.Week => 604800,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(TimeUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(TimeUnit unit, bool preferUnicode)
      => unit switch
      {
        TimeUnit.Second => "s",

        TimeUnit.Tick => "ticks",
        TimeUnit.Minute => "min",
        TimeUnit.Hour => "h",
        TimeUnit.Day => "d",
        TimeUnit.Week => "wk",
        TimeUnit.BeatPerMinute => "bpm",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(TimeUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(TimeUnit unit = TimeUnit.Second, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Time.Value"/> property is in <see cref="TimeUnit.Second"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
