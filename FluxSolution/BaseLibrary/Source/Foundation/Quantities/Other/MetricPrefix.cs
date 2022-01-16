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

    public static PartsPerNotationUnit ToPartsPerNotationUnit(this MetricPrefixUnit unit)
    {
      return unit switch
      {
        MetricPrefixUnit.Hecto => PartsPerNotationUnit.Hundred,
        MetricPrefixUnit.Kilo => PartsPerNotationUnit.Thousand,
        MetricPrefixUnit.Mega => PartsPerNotationUnit.Million,
        MetricPrefixUnit.Giga => PartsPerNotationUnit.Billion,
        MetricPrefixUnit.Tera => PartsPerNotationUnit.Trillion,
        MetricPrefixUnit.Peta => PartsPerNotationUnit.Quadrillion,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
    }
  }

  public enum MetricPrefixUnit
  {
    /// <summary>A.k.a. septillion/quadrillion.</summary>
    Yotta = 24,
    /// <summary>A.k.a. sextillion/trilliard.</summary>
    Zetta = 21,
    /// <summary>A.k.a. quintillion/trillion.</summary>
    Exa = 18,
    /// <summary>A.k.a. quadrillion/billiard.</summary>
    Peta = 15,
    /// <summary>A.k.a. trillion/billion.</summary>
    Tera = 12,
    /// <summary>A.k.a. billion/milliard.</summary>
    Giga = 9,
    /// <summary>A.k.a. million.</summary>
    Mega = 6,
    /// <summary>A.k.a. thousand.</summary>
    Kilo = 3,
    /// <summary>A.k.a. hundred.</summary>
    Hecto = 2,
    /// <summary>A.k.a. ten.</summary>
    Deca = 1,
    /// <summary>A.k.a. one.</summary>
    None = 0,
    /// <summary>A.k.a. tenth.</summary>
    Deci = -1,
    /// <summary>A.k.a. hundredth.</summary>
    Centi = -2,
    /// <summary>A.k.a. thousandth.</summary>
    Milli = -3,
    /// <summary>A.k.a. millionth.</summary>
    Micro = -6,
    /// <summary>A.k.a. billionth/milliardth.</summary>
    Nano = -9,
    /// <summary>A.k.a. trillionth/billionth.</summary>
    Pico = -12,
    /// <summary>A.k.a. quadrillionth/billiardth.</summary>
    Femto = -15,
    /// <summary>A.k.a. quintillionth/trillionth.</summary>
    Atto = -18,
    /// <summary>A.k.a. sextillionth/trilliardth.</summary>
    Zepto = -21,
    /// <summary>A.k.a. septillionth/quadrillionth.</summary>
    Yocto = -24,
  }

  /// <summary>Parts per notation. In science and engineering, the parts-per notation is a set of pseudo-units to describe small values of miscellaneous dimensionless quantities, e.g. mole fraction or mass fraction. Since these fractions are quantity-per-quantity measures, they are pure numbers with no associated units of measurement.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Metric_prefix"/>
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
