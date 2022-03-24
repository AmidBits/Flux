namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static double GetUnitFactor(this MetricMultiplicativePrefix source)
      => System.Math.Pow(10, (int)source);
    public static string GetUnitString(this MetricMultiplicativePrefix source, bool useFullName = false, bool preferUnicode = false)
      => useFullName ? source.ToString() : source switch
      {
        MetricMultiplicativePrefix.Yotta => "Y",
        MetricMultiplicativePrefix.Zetta => "Z",
        MetricMultiplicativePrefix.Exa => "E",
        MetricMultiplicativePrefix.Peta => "P",
        MetricMultiplicativePrefix.Tera => "T",
        MetricMultiplicativePrefix.Giga => "G",
        MetricMultiplicativePrefix.Mega => "M",
        MetricMultiplicativePrefix.Kilo => "k",
        MetricMultiplicativePrefix.Hecto => "h",
        MetricMultiplicativePrefix.Deca => preferUnicode ? "\u3372" : "da",
        MetricMultiplicativePrefix.One => string.Empty,
        MetricMultiplicativePrefix.Deci => "d",
        MetricMultiplicativePrefix.Centi => "c",
        MetricMultiplicativePrefix.Milli => "m",
        MetricMultiplicativePrefix.Micro => preferUnicode ? "\u03bc" : "\u00b5",
        MetricMultiplicativePrefix.Nano => "n",
        MetricMultiplicativePrefix.Pico => "p",
        MetricMultiplicativePrefix.Femto => "f",
        MetricMultiplicativePrefix.Atto => "a",
        MetricMultiplicativePrefix.Zepto => "z",
        MetricMultiplicativePrefix.Yocto => "y",
        _ => string.Empty,
      };
  }

  public enum MetricMultiplicativePrefix
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
    One = 0,
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
  public struct MetricMultiplicative
    : System.IComparable, System.IComparable<MetricMultiplicative>, System.IConvertible, System.IEquatable<MetricMultiplicative>, System.IFormattable, IUnitQuantifiable<double, MetricMultiplicativePrefix>
  {
    private readonly double m_value;

    /// <summary>Creates a new instance of this type.</summary>
    /// <param name="value">The value to represent.</param>
    /// <param name="multiplicativePrefix">The metric multiplicative prefix of the specified value.</param>
    public MetricMultiplicative(double value, MetricMultiplicativePrefix multiplicativePrefix)
      => m_value = value / multiplicativePrefix.GetUnitFactor();

    [System.Diagnostics.Contracts.Pure]
    public double Value
      => m_value;

    [System.Diagnostics.Contracts.Pure]
    public string ToUnitString(MetricMultiplicativePrefix multiplicativePrefix, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":format")}}}", ToUnitValue(multiplicativePrefix))} {multiplicativePrefix.GetUnitString(useFullName, preferUnicode)}";
    [System.Diagnostics.Contracts.Pure]
    public double ToUnitValue(MetricMultiplicativePrefix multiplicativePrefix)
      => m_value / multiplicativePrefix.GetUnitFactor();

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static explicit operator double(MetricMultiplicative v) => v.Value;
    [System.Diagnostics.Contracts.Pure] public static explicit operator MetricMultiplicative(double v) => new(v, MetricMultiplicativePrefix.One);

    [System.Diagnostics.Contracts.Pure] public static bool operator <(MetricMultiplicative a, MetricMultiplicative b) => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator <=(MetricMultiplicative a, MetricMultiplicative b) => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >(MetricMultiplicative a, MetricMultiplicative b) => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure] public static bool operator >=(MetricMultiplicative a, MetricMultiplicative b) => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure] public static bool operator ==(MetricMultiplicative a, MetricMultiplicative b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(MetricMultiplicative a, MetricMultiplicative b) => !a.Equals(b);

    [System.Diagnostics.Contracts.Pure] public static MetricMultiplicative operator -(MetricMultiplicative v) => new(-v.m_value, MetricMultiplicativePrefix.One);
    [System.Diagnostics.Contracts.Pure] public static MetricMultiplicative operator +(MetricMultiplicative a, double b) => new(a.m_value + b, MetricMultiplicativePrefix.One);
    [System.Diagnostics.Contracts.Pure] public static MetricMultiplicative operator +(MetricMultiplicative a, MetricMultiplicative b) => a + b.m_value;
    [System.Diagnostics.Contracts.Pure] public static MetricMultiplicative operator /(MetricMultiplicative a, double b) => new(a.m_value / b, MetricMultiplicativePrefix.One);
    [System.Diagnostics.Contracts.Pure] public static MetricMultiplicative operator /(MetricMultiplicative a, MetricMultiplicative b) => a / b.m_value;
    [System.Diagnostics.Contracts.Pure] public static MetricMultiplicative operator *(MetricMultiplicative a, double b) => new(a.m_value * b, MetricMultiplicativePrefix.One);
    [System.Diagnostics.Contracts.Pure] public static MetricMultiplicative operator *(MetricMultiplicative a, MetricMultiplicative b) => a * b.m_value;
    [System.Diagnostics.Contracts.Pure] public static MetricMultiplicative operator %(MetricMultiplicative a, double b) => new(a.m_value % b, MetricMultiplicativePrefix.One);
    [System.Diagnostics.Contracts.Pure] public static MetricMultiplicative operator %(MetricMultiplicative a, MetricMultiplicative b) => a % b.m_value;
    [System.Diagnostics.Contracts.Pure] public static MetricMultiplicative operator -(MetricMultiplicative a, double b) => new(a.m_value - b, MetricMultiplicativePrefix.One);
    [System.Diagnostics.Contracts.Pure] public static MetricMultiplicative operator -(MetricMultiplicative a, MetricMultiplicative b) => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<>
    [System.Diagnostics.Contracts.Pure] public int CompareTo(MetricMultiplicative other) => m_value.CompareTo(other.m_value);
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(object? other) => other is not null && other is MetricMultiplicative o ? CompareTo(o) : -1;

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
    [System.Diagnostics.Contracts.Pure] public bool Equals(MetricMultiplicative other) => m_value == other.m_value;

    // IFormattable
    [System.Diagnostics.Contracts.Pure] public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is MetricMultiplicative o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_value);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Value = {ToUnitString(MetricMultiplicativePrefix.One, null, false, false)} }}";
    #endregion Object overrides
  }
}
