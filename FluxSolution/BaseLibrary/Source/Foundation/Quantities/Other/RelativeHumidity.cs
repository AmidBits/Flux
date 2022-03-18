namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this RelativeHumidityUnit unit, bool useFullName = false, bool preferUnicode = false)
      => unit switch
      {
        RelativeHumidityUnit.Percent => "\u0025",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum RelativeHumidityUnit
  {
    Percent,
  }

  /// <summary>Relative humidity is represented as a percentage value, e.g. 34.5 for 34.5%.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Humidity#Relative_humidity"/>
  public struct RelativeHumidity
    : System.IComparable<RelativeHumidity>, System.IConvertible, System.IEquatable<RelativeHumidity>, IUnitQuantifiable<double, RelativeHumidityUnit>
  {
    public const RelativeHumidityUnit DefaultUnit = RelativeHumidityUnit.Percent;

    private readonly double m_value;

    public RelativeHumidity(double value, RelativeHumidityUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        RelativeHumidityUnit.Percent => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string ToUnitString(RelativeHumidityUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    public double ToUnitValue(RelativeHumidityUnit unit = DefaultUnit)
      => unit switch
      {
        RelativeHumidityUnit.Percent => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(RelativeHumidity v)
      => v.m_value;
    public static explicit operator RelativeHumidity(double v)
      => new(v);

    public static bool operator <(RelativeHumidity a, RelativeHumidity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(RelativeHumidity a, RelativeHumidity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(RelativeHumidity a, RelativeHumidity b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(RelativeHumidity a, RelativeHumidity b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(RelativeHumidity a, RelativeHumidity b)
      => a.Equals(b);
    public static bool operator !=(RelativeHumidity a, RelativeHumidity b)
      => !a.Equals(b);

    public static RelativeHumidity operator -(RelativeHumidity v)
      => new(-v.m_value);
    public static RelativeHumidity operator +(RelativeHumidity a, double b)
      => new(a.m_value + b);
    public static RelativeHumidity operator +(RelativeHumidity a, RelativeHumidity b)
      => a + b.m_value;
    public static RelativeHumidity operator /(RelativeHumidity a, double b)
      => new(a.m_value / b);
    public static RelativeHumidity operator /(RelativeHumidity a, RelativeHumidity b)
      => a / b.m_value;
    public static RelativeHumidity operator *(RelativeHumidity a, double b)
      => new(a.m_value * b);
    public static RelativeHumidity operator *(RelativeHumidity a, RelativeHumidity b)
      => a * b.m_value;
    public static RelativeHumidity operator %(RelativeHumidity a, double b)
      => new(a.m_value % b);
    public static RelativeHumidity operator %(RelativeHumidity a, RelativeHumidity b)
      => a % b.m_value;
    public static RelativeHumidity operator -(RelativeHumidity a, double b)
      => new(a.m_value - b);
    public static RelativeHumidity operator -(RelativeHumidity a, RelativeHumidity b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(RelativeHumidity other)
      => m_value.CompareTo(other.m_value);

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

    // IEquatable
    public bool Equals(RelativeHumidity other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is RelativeHumidity o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
