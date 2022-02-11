namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitString(this MassUnit unit, bool useNameInstead = false, bool useUnicodeIfAvailable = false)
      => useNameInstead ? unit.ToString() : unit switch
      {
        MassUnit.Milligram => "mg",
        MassUnit.Gram => "g",
        MassUnit.Ounce => "oz",
        MassUnit.Pound => "lb",
        MassUnit.Kilogram => "kg",
        MassUnit.MetricTon => "t",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum MassUnit
  {
    Milligram,
    Gram,
    Ounce,
    Pound,
    Kilogram,
    MetricTon,
  }

  /// <summary>Mass. SI unit of kilogram. This is a base quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Mass"/>
  public struct Mass
    : System.IComparable<Mass>, System.IConvertible, System.IEquatable<Mass>, IMetricPrefixFormattable, ISiBaseUnitQuantifiable<double, MassUnit>
  {
    public const MassUnit DefaultUnit = MassUnit.Kilogram;

    public static Mass ElectronMass
      => new(9.1093837015e-31);

    private readonly double m_value;

    public Mass(double value, MassUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        MassUnit.Milligram => value / 1000000,
        MassUnit.Gram => value / 1000,
        MassUnit.Ounce => value / 35.27396195,
        MassUnit.Pound => value * 0.45359237,
        MassUnit.Kilogram => value,
        MassUnit.MetricTon => value * 1000,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public string GetMetricPrefixString(MetricMultiplicativePrefix prefix, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{new MetricMultiplicative(m_value, MetricMultiplicativePrefix.Kilo).ToUnitString(prefix, format, useFullName, preferUnicode)}{MassUnit.Gram.GetUnitString(useFullName, preferUnicode)}";

    public string ToUnitString(MassUnit unit = DefaultUnit, string? valueFormat = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0:{(valueFormat is null ? string.Empty : $":{valueFormat}")}}}", ToUnitValue(unit))} {unit.GetUnitString(useFullName, preferUnicode)}";
    public double ToUnitValue(MassUnit unit = DefaultUnit)
      => unit switch
      {
        MassUnit.Milligram => m_value * 1000000,
        MassUnit.Gram => m_value * 1000,
        MassUnit.Ounce => m_value * 35.27396195,
        MassUnit.Pound => m_value / 0.45359237,
        MassUnit.Kilogram => m_value,
        MassUnit.MetricTon => m_value / 1000,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Mass v)
      => v.m_value;
    public static explicit operator Mass(double v)
      => new(v);

    public static bool operator <(Mass a, Mass b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Mass a, Mass b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Mass a, Mass b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Mass a, Mass b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Mass a, Mass b)
      => a.Equals(b);
    public static bool operator !=(Mass a, Mass b)
      => !a.Equals(b);

    public static Mass operator -(Mass v)
      => new(-v.m_value);
    public static Mass operator +(Mass a, double b)
      => new(a.m_value + b);
    public static Mass operator +(Mass a, Mass b)
      => a + b.m_value;
    public static Mass operator /(Mass a, double b)
      => new(a.m_value / b);
    public static Mass operator /(Mass a, Mass b)
      => a / b.m_value;
    public static Mass operator *(Mass a, double b)
      => new(a.m_value * b);
    public static Mass operator *(Mass a, Mass b)
      => a * b.m_value;
    public static Mass operator %(Mass a, double b)
      => new(a.m_value % b);
    public static Mass operator %(Mass a, Mass b)
      => a % b.m_value;
    public static Mass operator -(Mass a, double b)
      => new(a.m_value - b);
    public static Mass operator -(Mass a, Mass b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Mass other)
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

    // IEquatable
    public bool Equals(Mass other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Mass o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
