namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static MetricPrefix Create(this MetricPrefixUnit unit, double value)
      => new(value, unit);
    public static string GetUnitName(this MetricPrefixUnit unit)
      => unit == MetricPrefixUnit.None ? string.Empty : unit.ToString().ToLower();
    public static string GetUnitSymbol(this MetricPrefixUnit unit)
      => unit switch
      {
        MetricPrefixUnit.Yotta => "Y",
        MetricPrefixUnit.Zetta => "Z",
        MetricPrefixUnit.Exa => "E",
        MetricPrefixUnit.Peta => "P",
        MetricPrefixUnit.Tera => "T",
        MetricPrefixUnit.Giga => "G",
        MetricPrefixUnit.Mega => "M",
        MetricPrefixUnit.Kilo => "k",
        MetricPrefixUnit.Hecto => "h",
        MetricPrefixUnit.Deca => "da",
        MetricPrefixUnit.None => string.Empty,
        MetricPrefixUnit.Deci => "d",
        MetricPrefixUnit.Centi => "c",
        MetricPrefixUnit.Milli => "m",
        MetricPrefixUnit.Micro => "\u03bc",
        MetricPrefixUnit.Nano => "n",
        MetricPrefixUnit.Pico => "p",
        MetricPrefixUnit.Femto => "f",
        MetricPrefixUnit.Atto => "a",
        MetricPrefixUnit.Zepto => "z",
        MetricPrefixUnit.Yocto => "y",
        _ => string.Empty,
      };
  }

  public enum MetricPrefixUnit
  {
    Yotta = 24,
    Zetta = 21,
    Exa = 18,
    Peta = 15,
    Tera = 12,
    Giga = 9,
    Mega = 6,
    Kilo = 3,
    Hecto = 2,
    Deca = 1,
    None = 0,
    Deci = -1,
    Centi = -2,
    Milli = -3,
    Micro = -6,
    Nano = -9,
    Pico = -12,
    Femto = -15,
    Atto = -18,
    Zepto = -21,
    Yocto = -24,
  }

  /// <summary>Parts per notation. In science and engineering, the parts-per notation is a set of pseudo-units to describe small values of miscellaneous dimensionless quantities, e.g. mole fraction or mass fraction. Since these fractions are quantity-per-quantity measures, they are pure numbers with no associated units of measurement.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Parts-per_notation"/>
  public struct MetricPrefix
    : System.IComparable<MetricPrefix>, System.IConvertible, System.IEquatable<MetricPrefix>, IValueGeneralizedUnit<double>
  {
    public const MetricPrefixUnit DefaultUnit = MetricPrefixUnit.None;

    private readonly double m_value;

    /// <summary>Creates a new instance of this type.</summary>
    /// <param name="value">The parts in parts per notation.</param>
    /// <param name="unit">The notation in parts per notation.</param>
    public MetricPrefix(double value, MetricPrefixUnit unit = DefaultUnit)
      => m_value = value * System.Math.Pow(10, (double)unit);

    public double Value
      => m_value;

    public string ToUnitString(MetricPrefixUnit unit = DefaultUnit, string? format = null, bool useNameInsteadOfSymbol = true)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))} {(useNameInsteadOfSymbol ? unit.GetUnitName() : unit.GetUnitSymbol())}";
    public double ToUnitValue(MetricPrefixUnit unit = DefaultUnit)
      => m_value / System.Math.Pow(10, (double)unit);

    #region Static methods
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(MetricPrefix v)
      => v.Value;

    public static bool operator <(MetricPrefix a, MetricPrefix b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(MetricPrefix a, MetricPrefix b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(MetricPrefix a, MetricPrefix b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(MetricPrefix a, MetricPrefix b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(MetricPrefix a, MetricPrefix b)
      => a.Equals(b);
    public static bool operator !=(MetricPrefix a, MetricPrefix b)
      => !a.Equals(b);

    public static MetricPrefix operator -(MetricPrefix v)
      => new(-v.m_value);
    public static MetricPrefix operator +(MetricPrefix a, double b)
      => new(a.m_value + b);
    public static MetricPrefix operator +(MetricPrefix a, MetricPrefix b)
      => a + b.m_value;
    public static MetricPrefix operator /(MetricPrefix a, double b)
      => new(a.m_value / b);
    public static MetricPrefix operator /(MetricPrefix a, MetricPrefix b)
      => a / b.m_value;
    public static MetricPrefix operator *(MetricPrefix a, double b)
      => new(a.m_value * b);
    public static MetricPrefix operator *(MetricPrefix a, MetricPrefix b)
      => a * b.m_value;
    public static MetricPrefix operator %(MetricPrefix a, double b)
      => new(a.m_value % b);
    public static MetricPrefix operator %(MetricPrefix a, MetricPrefix b)
      => a % b.m_value;
    public static MetricPrefix operator -(MetricPrefix a, double b)
      => new(a.m_value - b);
    public static MetricPrefix operator -(MetricPrefix a, MetricPrefix b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(MetricPrefix other)
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
    public bool Equals(MetricPrefix other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is MetricPrefix o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_value);
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
