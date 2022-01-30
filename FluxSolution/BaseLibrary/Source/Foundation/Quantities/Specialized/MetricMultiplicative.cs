namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static double GetPrefixFactor(this MetricMultiplicativePrefix source)
      => System.Math.Pow(10, (int)source);
    public static string GetPrefixString(this MetricMultiplicativePrefix source, bool useNameInsteadOfSymbol, bool useUnicodeIfAvailable)
      => useNameInsteadOfSymbol ? source.ToString() : source switch
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
        MetricMultiplicativePrefix.Deca => useUnicodeIfAvailable ? "\u3372" : "da",
        MetricMultiplicativePrefix.None => string.Empty,
        MetricMultiplicativePrefix.Deci => "d",
        MetricMultiplicativePrefix.Centi => "c",
        MetricMultiplicativePrefix.Milli => "m",
        MetricMultiplicativePrefix.Micro => useUnicodeIfAvailable ? "\u03bc" : "\u00b5",
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
  public struct MetricMultiplicative
    : System.IComparable<MetricMultiplicative>, System.IConvertible, System.IEquatable<MetricMultiplicative>, IValueGeneralizedUnit<double>
  {
    private readonly double m_value;

    /// <summary>Creates a new instance of this type.</summary>
    /// <param name="value">The value to represent.</param>
    /// <param name="multiplicativePrefix">The metric multiplicative prefix of the specified value.</param>
    public MetricMultiplicative(double value, MetricMultiplicativePrefix multiplicativePrefix)
      => m_value = value * multiplicativePrefix.GetPrefixFactor();

    public double Value
      => m_value;

    public string ToPrefixString(MetricMultiplicativePrefix multiplicativePrefix, string? format = null, bool useNameInsteadOfSymbol = false, bool useUnicodeIfAvailable = false)
      => $"{string.Format($"{{0:{(format is null ? string.Empty : $":format")}}}", ToPrefixValue(multiplicativePrefix))} {multiplicativePrefix.GetPrefixString(useNameInsteadOfSymbol, useUnicodeIfAvailable)}";
    public double ToPrefixValue(MetricMultiplicativePrefix multiplicativePrefix)
      => m_value / multiplicativePrefix.GetPrefixFactor();

    #region Overloaded operators
    public static explicit operator double(MetricMultiplicative v)
      => v.Value;
    public static explicit operator MetricMultiplicative(double v)
      => new(v, MetricMultiplicativePrefix.None);

    public static bool operator <(MetricMultiplicative a, MetricMultiplicative b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(MetricMultiplicative a, MetricMultiplicative b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(MetricMultiplicative a, MetricMultiplicative b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(MetricMultiplicative a, MetricMultiplicative b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(MetricMultiplicative a, MetricMultiplicative b)
      => a.Equals(b);
    public static bool operator !=(MetricMultiplicative a, MetricMultiplicative b)
      => !a.Equals(b);

    public static MetricMultiplicative operator -(MetricMultiplicative v)
      => new(-v.m_value, MetricMultiplicativePrefix.None);
    public static MetricMultiplicative operator +(MetricMultiplicative a, double b)
      => new(a.m_value + b, MetricMultiplicativePrefix.None);
    public static MetricMultiplicative operator +(MetricMultiplicative a, MetricMultiplicative b)
      => a + b.m_value;
    public static MetricMultiplicative operator /(MetricMultiplicative a, double b)
      => new(a.m_value / b, MetricMultiplicativePrefix.None);
    public static MetricMultiplicative operator /(MetricMultiplicative a, MetricMultiplicative b)
      => a / b.m_value;
    public static MetricMultiplicative operator *(MetricMultiplicative a, double b)
      => new(a.m_value * b, MetricMultiplicativePrefix.None);
    public static MetricMultiplicative operator *(MetricMultiplicative a, MetricMultiplicative b)
      => a * b.m_value;
    public static MetricMultiplicative operator %(MetricMultiplicative a, double b)
      => new(a.m_value % b, MetricMultiplicativePrefix.None);
    public static MetricMultiplicative operator %(MetricMultiplicative a, MetricMultiplicative b)
      => a % b.m_value;
    public static MetricMultiplicative operator -(MetricMultiplicative a, double b)
      => new(a.m_value - b, MetricMultiplicativePrefix.None);
    public static MetricMultiplicative operator -(MetricMultiplicative a, MetricMultiplicative b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(MetricMultiplicative other)
      => m_value.CompareTo(other.m_value);

    #region IConvertible
    public System.TypeCode GetTypeCode()
      => System.TypeCode.Object;
    public bool ToBoolean(System.IFormatProvider? provider)
      => Value != 0;
    public byte ToByte(System.IFormatProvider? provider)
      => System.Convert.ToByte(Value);
    public char ToChar(System.IFormatProvider? provider)
      => System.Convert.ToChar(Value);
    public System.DateTime ToDateTime(System.IFormatProvider? provider)
      => System.Convert.ToDateTime(Value);
    public decimal ToDecimal(System.IFormatProvider? provider)
      => System.Convert.ToDecimal(Value);
    public double ToDouble(System.IFormatProvider? provider)
      => System.Convert.ToDouble(Value);
    public short ToInt16(System.IFormatProvider? provider)
      => System.Convert.ToInt16(Value);
    public int ToInt32(System.IFormatProvider? provider)
      => System.Convert.ToInt32(Value);
    public long ToInt64(System.IFormatProvider? provider)
      => System.Convert.ToInt64(Value);
    [System.CLSCompliant(false)]
    public sbyte ToSByte(System.IFormatProvider? provider)
      => System.Convert.ToSByte(Value);
    public float ToSingle(System.IFormatProvider? provider)
      => System.Convert.ToSingle(Value);
    public string ToString(System.IFormatProvider? provider)
      => string.Format(provider, "{0}", Value);
    public object ToType(System.Type conversionType, System.IFormatProvider? provider)
      => System.Convert.ChangeType(Value, conversionType, provider);
    [System.CLSCompliant(false)]
    public ushort ToUInt16(System.IFormatProvider? provider)
      => System.Convert.ToUInt16(Value);
    [System.CLSCompliant(false)]
    public uint ToUInt32(System.IFormatProvider? provider)
      => System.Convert.ToUInt32(Value);
    [System.CLSCompliant(false)]
    public ulong ToUInt64(System.IFormatProvider? provider)
      => System.Convert.ToUInt64(Value);
    #endregion IConvertible

    // IEquatable
    public bool Equals(MetricMultiplicative other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is MetricMultiplicative o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_value);
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToPrefixString(MetricMultiplicativePrefix.None, null, false, false)} }}";
    #endregion Object overrides
  }
}
