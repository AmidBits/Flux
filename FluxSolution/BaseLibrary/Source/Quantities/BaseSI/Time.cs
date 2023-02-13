namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this Quantities.TimeUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.TimeUnit.Picosecond => preferUnicode ? "\u33B0" : "ps",
        Quantities.TimeUnit.Nanosecond => preferUnicode ? "\u33B1" : "ns",
        Quantities.TimeUnit.Microsecond => preferUnicode ? "\u33B2" : "\u00B5s",
        Quantities.TimeUnit.Millisecond => preferUnicode ? "\u33B3" : "ms",
        Quantities.TimeUnit.Second => "s",
        Quantities.TimeUnit.Minute => "min",
        Quantities.TimeUnit.Hour => "h",
        Quantities.TimeUnit.Day => "d",
        Quantities.TimeUnit.Week => "week(s)",
        Quantities.TimeUnit.Fortnight => "fortnight(s)",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum TimeUnit
    {
      /// <summary>This is the default unit for time.</summary>
      Second,
      Picosecond,
      Nanosecond,
      Microsecond,
      Millisecond,
      Minute,
      Hour,
      Day,
      Week,
      Fortnight,
    }

    /// <summary>Time. SI unit of second. This is a base quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Time"/>
    public readonly record struct Time
      : System.IComparable, System.IComparable<Time>, System.IConvertible, System.IFormattable, IUnitQuantifiable<double, TimeUnit>
    {
      public const TimeUnit DefaultUnit = TimeUnit.Second;

      /// <see href="https://en.wikipedia.org/wiki/Flick_(time)"></see>
      public static Time Flick => new(1.0 / 705600000.0);

      private readonly double m_value;

      public Time(double value, TimeUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          TimeUnit.Picosecond => value / 1000000000000,
          TimeUnit.Nanosecond => value / 1000000000,
          TimeUnit.Microsecond => value / 1000000,
          TimeUnit.Millisecond => value / 1000,
          TimeUnit.Second => value,
          TimeUnit.Minute => value * 60,
          TimeUnit.Hour => value * 3600,
          TimeUnit.Day => value * 86400,
          TimeUnit.Week => value * 604800,
          TimeUnit.Fortnight => value * 1209600,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      /// <summary>Creates a new Time instance from the specified <paramref name="timeSpan"/>.</summary>
      public Time(System.TimeSpan timeSpan)
        : this(timeSpan.TotalSeconds)
      { }

      public System.TimeSpan ToTimeSpan() => System.TimeSpan.FromSeconds(m_value);

      #region Static methods
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Time v) => v.m_value;
      public static explicit operator Time(double v) => new(v);

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
      // IComparable<T>
      public int CompareTo(Time other) => m_value.CompareTo(other.m_value);
      // IComparable
      public int CompareTo(object? other) => other is not null && other is Time o ? CompareTo(o) : -1;

      #region IConvertible
      public System.TypeCode GetTypeCode() => System.TypeCode.Object;
      public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
      public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
      public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
      public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
      public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
      public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
      public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
      public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
      public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
      [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
      public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
      public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
      public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
      [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
      [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
      [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
      #endregion IConvertible

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public double Value { get => m_value; init => m_value = value; }
      // IUnitQuantifiable<>

      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
        => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public string ToUnitString(TimeUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      public double ToUnitValue(TimeUnit unit = DefaultUnit)
        => unit switch
        {
          TimeUnit.Picosecond => m_value * 1000000000000,
          TimeUnit.Nanosecond => m_value * 1000000000,
          TimeUnit.Microsecond => m_value * 1000000,
          TimeUnit.Millisecond => m_value * 1000,
          TimeUnit.Second => m_value,
          TimeUnit.Minute => m_value / 60,
          TimeUnit.Hour => m_value / 3600,
          TimeUnit.Day => m_value / 86400,
          TimeUnit.Week => m_value / 604800,
          TimeUnit.Fortnight => m_value / 1209600,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      #endregion Implemented interfaces

      #region Object overrides
      public override string ToString() => $"{GetType().Name} {{ {ToQuantityString()} }}";
      #endregion Object overrides
    }
  }
}
