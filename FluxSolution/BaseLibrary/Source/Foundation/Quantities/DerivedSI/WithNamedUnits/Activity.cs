namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this ActivityUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        ActivityUnit.Becquerel => preferUnicode ? "\u33C3" : "Bq",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum ActivityUnit
  {
    /// <summary>Becquerel.</summary>
    Becquerel,
  }

  /// <summary>Activity, unit of becquerel.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Specific_activity"/>
  public readonly struct Activity
    : System.IComparable, System.IComparable<Activity>, System.IConvertible, System.IEquatable<Activity>, System.IFormattable, IUnitQuantifiable<double, ActivityUnit>
  {
    public const ActivityUnit DefaultUnit = ActivityUnit.Becquerel;

    private readonly double m_value;

    public Activity(double value, ActivityUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        ActivityUnit.Becquerel => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(Activity v) => v.m_value;
    [System.Diagnostics.Contracts.Pure] public static explicit operator Activity(double v) => new(v);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(Activity a, Activity b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(Activity a, Activity b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(Activity a, Activity b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(Activity a, Activity b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(Activity a, Activity b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(Activity a, Activity b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static Activity operator -(Activity v) => new(-v.m_value);
    [System.Diagnostics.Contracts.Pure] public static Activity operator +(Activity a, double b) => new(a.m_value + b);
    [System.Diagnostics.Contracts.Pure] public static Activity operator +(Activity a, Activity b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Activity operator /(Activity a, double b) => new(a.m_value / b);
    [System.Diagnostics.Contracts.Pure] public static Activity operator /(Activity a, Activity b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Activity operator *(Activity a, double b) => new(a.m_value * b);
    [System.Diagnostics.Contracts.Pure] public static Activity operator *(Activity a, Activity b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Activity operator %(Activity a, double b) => new(a.m_value % b);
    [System.Diagnostics.Contracts.Pure] public static Activity operator %(Activity a, Activity b) => a % b.m_value;
    [System.Diagnostics.Contracts.Pure] public static Activity operator -(Activity a, double b) => new(a.m_value - b);
    [System.Diagnostics.Contracts.Pure] public static Activity operator -(Activity a, Activity b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(Activity other) => m_value.CompareTo(other.m_value);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is Activity o ? CompareTo(o) : -1;

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => m_value != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_value);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_value);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_value);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_value);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_value);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_value);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_value);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_value);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_value);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_value);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_value, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_value);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_value);
    #endregion IConvertible

    // IEquatable<>
    [System.Diagnostics.Contracts.Pure] public bool Equals(Activity other) => m_value == other.m_value;

    // IFormattable
    [System.Diagnostics.Contracts.Pure] public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    // IQuantifiable<>
    [System.Diagnostics.Contracts.Pure] public double Value { get => m_value; init => m_value = value; }
    // IUnitQuantifiable<>
    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(ActivityUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(ActivityUnit unit = DefaultUnit)
      => unit switch
      {
        ActivityUnit.Becquerel => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is Activity o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => m_value.GetHashCode();
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}