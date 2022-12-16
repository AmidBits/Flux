namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static double GetUnitFactor(this Quantities.MetricMultiplicativePrefix source)
      => System.Math.Pow(10, (int)source);
    public static string GetUnitString(this Quantities.MetricMultiplicativePrefix source, bool preferUnicode, bool useFullName = false)
      => useFullName ? source.ToString() : source switch
      {
        Quantities.MetricMultiplicativePrefix.Yotta => "Y",
        Quantities.MetricMultiplicativePrefix.Zetta => "Z",
        Quantities.MetricMultiplicativePrefix.Exa => "E",
        Quantities.MetricMultiplicativePrefix.Peta => "P",
        Quantities.MetricMultiplicativePrefix.Tera => "T",
        Quantities.MetricMultiplicativePrefix.Giga => "G",
        Quantities.MetricMultiplicativePrefix.Mega => "M",
        Quantities.MetricMultiplicativePrefix.Kilo => "k",
        Quantities.MetricMultiplicativePrefix.Hecto => "h",
        Quantities.MetricMultiplicativePrefix.Deca => preferUnicode ? "\u3372" : "da",
        Quantities.MetricMultiplicativePrefix.One => string.Empty,
        Quantities.MetricMultiplicativePrefix.Deci => "d",
        Quantities.MetricMultiplicativePrefix.Centi => "c",
        Quantities.MetricMultiplicativePrefix.Milli => "m",
        Quantities.MetricMultiplicativePrefix.Micro => "\u00B5",
        Quantities.MetricMultiplicativePrefix.Nano => "n",
        Quantities.MetricMultiplicativePrefix.Pico => "p",
        Quantities.MetricMultiplicativePrefix.Femto => "f",
        Quantities.MetricMultiplicativePrefix.Atto => "a",
        Quantities.MetricMultiplicativePrefix.Zepto => "z",
        Quantities.MetricMultiplicativePrefix.Yocto => "y",
        _ => string.Empty,
      };
  }

  namespace Quantities
  {
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
    public readonly struct MetricMultiplicative
      : System.IComparable, System.IComparable<MetricMultiplicative>, System.IConvertible, System.IEquatable<MetricMultiplicative>, System.IFormattable, IUnitQuantifiable<double, MetricMultiplicativePrefix>
    {
      private readonly double m_value;

      /// <summary>Creates a new instance of this type.</summary>
      /// <param name="value">The value to represent.</param>
      /// <param name="multiplicativePrefix">The metric multiplicative prefix of the specified value.</param>
      public MetricMultiplicative(double value, MetricMultiplicativePrefix multiplicativePrefix)
        => m_value = value / multiplicativePrefix.GetUnitFactor();

      #region Overloaded operators
      public static explicit operator double(MetricMultiplicative v) => v.Value;
      public static explicit operator MetricMultiplicative(double v) => new(v, MetricMultiplicativePrefix.One);

      public static bool operator <(MetricMultiplicative a, MetricMultiplicative b) => a.CompareTo(b) < 0;
      public static bool operator <=(MetricMultiplicative a, MetricMultiplicative b) => a.CompareTo(b) <= 0;
      public static bool operator >(MetricMultiplicative a, MetricMultiplicative b) => a.CompareTo(b) > 0;
      public static bool operator >=(MetricMultiplicative a, MetricMultiplicative b) => a.CompareTo(b) >= 0;

      public static bool operator ==(MetricMultiplicative a, MetricMultiplicative b) => a.Equals(b);
      public static bool operator !=(MetricMultiplicative a, MetricMultiplicative b) => !a.Equals(b);

      public static MetricMultiplicative operator -(MetricMultiplicative v) => new(-v.m_value, MetricMultiplicativePrefix.One);
      public static MetricMultiplicative operator +(MetricMultiplicative a, double b) => new(a.m_value + b, MetricMultiplicativePrefix.One);
      public static MetricMultiplicative operator +(MetricMultiplicative a, MetricMultiplicative b) => a + b.m_value;
      public static MetricMultiplicative operator /(MetricMultiplicative a, double b) => new(a.m_value / b, MetricMultiplicativePrefix.One);
      public static MetricMultiplicative operator /(MetricMultiplicative a, MetricMultiplicative b) => a / b.m_value;
      public static MetricMultiplicative operator *(MetricMultiplicative a, double b) => new(a.m_value * b, MetricMultiplicativePrefix.One);
      public static MetricMultiplicative operator *(MetricMultiplicative a, MetricMultiplicative b) => a * b.m_value;
      public static MetricMultiplicative operator %(MetricMultiplicative a, double b) => new(a.m_value % b, MetricMultiplicativePrefix.One);
      public static MetricMultiplicative operator %(MetricMultiplicative a, MetricMultiplicative b) => a % b.m_value;
      public static MetricMultiplicative operator -(MetricMultiplicative a, double b) => new(a.m_value - b, MetricMultiplicativePrefix.One);
      public static MetricMultiplicative operator -(MetricMultiplicative a, MetricMultiplicative b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
      public int CompareTo(MetricMultiplicative other) => m_value.CompareTo(other.m_value);
      // IComparable
      public int CompareTo(object? other) => other is not null && other is MetricMultiplicative o ? CompareTo(o) : -1;

      #region IConvertible
      public System.TypeCode GetTypeCode() => System.TypeCode.Object;
      public bool ToBoolean(System.IFormatProvider? provider) => m_value != 0;
      public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_value);
      public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_value);
      public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_value);
      public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_value);
      public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_value);
      public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_value);
      public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_value);
      public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_value);
      [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_value);
      public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_value);
      public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_value);
      public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_value, conversionType, provider);
      [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_value);
      [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_value);
      [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_value);
      #endregion IConvertible

      // IEquatable<>
      public bool Equals(MetricMultiplicative other) => m_value == other.m_value;

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
        => ToUnitString(MetricMultiplicativePrefix.One, format, preferUnicode, useFullName);

      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>

      public string ToUnitString(MetricMultiplicativePrefix multiplicativePrefix, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":format")}}}", ToUnitValue(multiplicativePrefix))}{(multiplicativePrefix.GetUnitString(preferUnicode, useFullName) is var prefix && prefix.Length > 0 ? $" {prefix}" : string.Empty)}";

      public double ToUnitValue(MetricMultiplicativePrefix multiplicativePrefix)
        => m_value / multiplicativePrefix.GetUnitFactor();
      #endregion Implemented interfaces

      #region Object overrides
      public override bool Equals(object? obj) => obj is MetricMultiplicative o && Equals(o);
      public override int GetHashCode() => System.HashCode.Combine(m_value);
      public override string ToString() => $"{GetType().Name} {{ {ToQuantityString()} }}";
      #endregion Object overrides
    }
  }
}
