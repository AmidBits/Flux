namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this TimeUnit unit, bool useFullName = false, bool preferUnicode = false)
      => useFullName ? unit.ToString() : unit switch
      {
        TimeUnit.Nanosecond => "ns",
        TimeUnit.Microsecond => "\u00B5s",
        TimeUnit.Millisecond => "ms",
        TimeUnit.Second => "s",
        TimeUnit.Minute => "min",
        TimeUnit.Hour => "h",
        TimeUnit.Day => "d",
        TimeUnit.Week => "week(s)",
        TimeUnit.Fortnight => "fortnight(s)",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum TimeUnit
  {
    Nanosecond,
    Microsecond,
    Millisecond,
    Second,
    Minute,
    Hour,
    Day,
    Week,
    Fortnight,
  }

  /// <summary>Time. SI unit of second. This is a base quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Time"/>
  public struct Time
    : System.IComparable, System.IComparable<Time>, System.IConvertible, System.IEquatable<Time>, System.IFormattable, IMetricOneQuantifiable, ISiBaseUnitQuantifiable<double, TimeUnit>
  {
    public const TimeUnit DefaultUnit = TimeUnit.Second;

    /// <see href="https://en.wikipedia.org/wiki/Flick_(time)"></see>
    public static Time Flick => new(1.0 / 705600000.0);

    private readonly double m_value;

    public Time(double value, TimeUnit unit = DefaultUnit)
      => m_value = unit switch
      {
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

    [System.Diagnostics.Contracts.Pure] public double Value => m_value;

    [System.Diagnostics.Contracts.Pure] public System.TimeSpan ToTimeSpan() => System.TimeSpan.FromSeconds(m_value);

    [System.Diagnostics.Contracts.Pure]
    public string ToMetricOneString(MetricMultiplicativePrefix prefix, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{new MetricMultiplicative(m_value, MetricMultiplicativePrefix.One).ToUnitString(prefix, format, useFullName, preferUnicode)}{DefaultUnit.GetUnitString(useFullName, preferUnicode)}";

    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(TimeUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(useFullName, preferUnicode)}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(TimeUnit unit = DefaultUnit)
      => unit switch
      {
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

    #region Static methods
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(Time v) => v.m_value;
    [System.Diagnostics.Contracts.Pure] public static explicit operator Time(double v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(Time a, Time b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(Time a, Time b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(Time a, Time b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(Time a, Time b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(Time a, Time b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(Time a, Time b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static Time operator -(Time v) => new(-v.m_value);
    [System.Diagnostics.Contracts.Pure] public static Time operator +(Time a, double b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static Time operator +(Time a, Time b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Time operator /(Time a, double b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static Time operator /(Time a, Time b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Time operator *(Time a, double b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static Time operator *(Time a, Time b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Time operator %(Time a, double b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static Time operator %(Time a, Time b) => a % b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Time operator -(Time a, double b) => new(a.m_value - b);
    [System.Diagnostics.Contracts.Pure] public static Time operator -(Time a, Time b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<T>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(Time other) => m_value.CompareTo(other.m_value);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is Time o ? CompareTo(o) : -1;

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
    #endregion IConvertible

    // IEquatable<T>
    [System.Diagnostics.Contracts.Pure] public bool Equals(Time other) => m_value == other.m_value;

    // IFormattable
    [System.Diagnostics.Contracts.Pure] public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is Time o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
