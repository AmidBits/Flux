namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Time Create(this TimeUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this TimeUnit unit)
      => unit switch
      {
        TimeUnit.Nanosecond => @" ns",
        TimeUnit.Microsecond => " \u00B5s",
        TimeUnit.Millisecond => @" ms",
        TimeUnit.Second => @" s",
        TimeUnit.Minute => @" min",
        TimeUnit.Hour => @" h",
        TimeUnit.Day => @" d",
        TimeUnit.Week => @" week(s)",
        TimeUnit.Fortnight => @" fortnight(s)",
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
    : System.IComparable<Time>, System.IConvertible, System.IEquatable<Time>, IValueSiBaseUnit<double>, IValueGeneralizedUnit<double>
  {
    public const TimeUnit DefaultUnit = TimeUnit.Second;

    /// <see href="https://en.wikipedia.org/wiki/Flick_(time)"></see>
    public static Time Flick
      => new(1.0 / 705600000.0);

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

    public double Value
      => m_value;

    public System.TimeSpan ToTimeSpan()
      => System.TimeSpan.FromSeconds(m_value);

    public string ToUnitString(TimeUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
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
    public static explicit operator double(Time v)
      => v.m_value;
    public static explicit operator Time(double v)
      => new(v);

    public static bool operator <(Time a, Time b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Time a, Time b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Time a, Time b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Time a, Time b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Time a, Time b)
      => a.Equals(b);
    public static bool operator !=(Time a, Time b)
      => !a.Equals(b);

    public static Time operator -(Time v)
      => new(-v.m_value);
    public static Time operator +(Time a, double b)
      => new(a.m_value + b);
    public static Time operator +(Time a, Time b)
      => a + b.m_value;
    public static Time operator /(Time a, double b)
      => new(a.m_value / b);
    public static Time operator /(Time a, Time b)
      => a / b.m_value;
    public static Time operator *(Time a, double b)
      => new(a.m_value * b);
    public static Time operator *(Time a, Time b)
      => a * b.m_value;
    public static Time operator %(Time a, double b)
      => new(a.m_value % b);
    public static Time operator %(Time a, Time b)
      => a % b.m_value;
    public static Time operator -(Time a, double b)
      => new(a.m_value - b);
    public static Time operator -(Time a, Time b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<T>
    public int CompareTo(Time other)
      => m_value.CompareTo(other.m_value);

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

    // IEquatable<T>
    public bool Equals(Time other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Time o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
