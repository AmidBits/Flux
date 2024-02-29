namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.TimeUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.TimeUnit.Second => "s",
        Units.TimeUnit.Picosecond => preferUnicode ? "\u33B0" : "ps",
        Units.TimeUnit.Nanosecond => preferUnicode ? "\u33B1" : "ns",
        Units.TimeUnit.Ticks => "ticks",
        Units.TimeUnit.Microsecond => preferUnicode ? "\u33B2" : "\u00B5s",
        Units.TimeUnit.Millisecond => preferUnicode ? "\u33B3" : "ms",
        Units.TimeUnit.Minute => "min",
        Units.TimeUnit.Hour => "h",
        Units.TimeUnit.Day => "d",
        Units.TimeUnit.Week => "week(s)",
        Units.TimeUnit.Fortnight => "fortnight(s)",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum TimeUnit
    {
      /// <summary>This is the default unit for <see cref="Time"/>.</summary>
      Second,
      Picosecond,
      Nanosecond,
      /// <summary>The unit of .NET ticks.</summary>
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
      : System.IComparable, System.IComparable<Time>, System.IFormattable, IMetricMultiplicable<double>, IUnitValueQuantifiable<double, TimeUnit>
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

      public Time(double value, TimeUnit unit = TimeUnit.Second)
        => m_value = unit switch
        {
          TimeUnit.Second => value,
          TimeUnit.Picosecond => value / 1000000000000,
          TimeUnit.Nanosecond => value / 1000000000,
          TimeUnit.Ticks => value / 10000000,
          TimeUnit.Microsecond => value / 1000000,
          TimeUnit.Millisecond => value / 1000,
          TimeUnit.Minute => value * 60,
          TimeUnit.Hour => value * 3600,
          TimeUnit.Day => value * 86400,
          TimeUnit.Week => value * 604800,
          TimeUnit.Fortnight => value * 1209600,
          TimeUnit.BeatsPerMinute => ConvertBpmToSeconds(value),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      /// <summary>Creates a new Time instance from the specified <paramref name="timeSpan"/>.</summary>
      public Time(System.TimeSpan timeSpan)
        : this(timeSpan.TotalSeconds)
      { }

      public System.TimeSpan ToTimeSpan() => System.TimeSpan.FromSeconds(m_value);

      #region Static methods

      #region Conversions

      /// <summary>
      /// <para>Convert beats-per-minute to seconds.</para>
      /// </summary>
      /// <param name="bpm"></param>
      /// <returns></returns>
      public static double ConvertBpmToSeconds(double bpm) => 60 / bpm;

      /// <summary>
      /// <para>Convert seconds to beats-per-minute.</para>
      /// </summary>
      /// <param name="seconds"></param>
      /// <returns></returns>
      public static double ConvertSecondsToBpm(double seconds) => 60 / seconds;

      #endregion // Conversions

      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(Time a, Time b) => a.CompareTo(b) < 0;
      public static bool operator <=(Time a, Time b) => a.CompareTo(b) <= 0;
      public static bool operator >(Time a, Time b) => a.CompareTo(b) > 0;
      public static bool operator >=(Time a, Time b) => a.CompareTo(b) >= 0;

      public static Time operator -(Time v) => new(-v.m_value);
      public static Time operator +(Time a, double b) => new(a.m_value + b);
      public static Time operator +(Time a, Time b) => a + b.m_value;
      public static Time operator /(Time a, double b) => new(a.m_value / b);
      public static Time operator /(Time a, Time b) => a / b.m_value;
      public static Time operator *(Time a, double b) => new(a.m_value * b);
      public static Time operator *(Time a, Time b) => a * b.m_value;
      public static Time operator %(Time a, double b) => new(a.m_value % b);
      public static Time operator %(Time a, Time b) => a % b.m_value;
      public static Time operator -(Time a, double b) => new(a.m_value - b);
      public static Time operator -(Time a, Time b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Time o ? CompareTo(o) : -1;

      // IComparable<T>
      public int CompareTo(Time other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(TimeUnit.Second, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      //IMetricMultiplicable<>
      public double ToMetricValue(MetricPrefix prefix) => MetricPrefix.Count.Convert(m_value, prefix);

      public string ToMetricValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.NarrowNoBreakSpace)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(ToMetricValue(prefix).ToString(format, formatProvider));
        sb.Append(spacing.ToSpacingString());
        sb.Append(prefix.GetUnitString(true, false));
        sb.Append(TimeUnit.Second.GetUnitString(false, false));
        return sb.ToString();
      }

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Time.Value"/> property is in <see cref="TimeUnit.Second"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(TimeUnit unit)
        => unit switch
        {
          TimeUnit.Picosecond => m_value * 1000000000000,
          TimeUnit.Nanosecond => m_value * 1000000000,
          TimeUnit.Ticks => m_value * 10000000,
          TimeUnit.Microsecond => m_value * 1000000,
          TimeUnit.Millisecond => m_value * 1000,
          TimeUnit.Second => m_value,
          TimeUnit.Minute => m_value / 60,
          TimeUnit.Hour => m_value / 3600,
          TimeUnit.Day => m_value / 86400,
          TimeUnit.Week => m_value / 604800,
          TimeUnit.Fortnight => m_value / 1209600,
          TimeUnit.BeatsPerMinute => ConvertSecondsToBpm(m_value),
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(TimeUnit unit, UnitValueStringOptions options = default)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(options.Format, options.FormatProvider));
        sb.Append(options.UnitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(options.PreferUnicode, options.UseFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
