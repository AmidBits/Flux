namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitFactor(this Units.MetricMultiplicativePrefix source)
      => System.Math.Pow(10, (int)source);
    public static string GetUnitString(this Units.MetricMultiplicativePrefix source, bool preferUnicode, bool useFullName = false)
      => useFullName ? source.ToString() : source switch
      {
        Units.MetricMultiplicativePrefix.Yotta => "Y",
        Units.MetricMultiplicativePrefix.Zetta => "Z",
        Units.MetricMultiplicativePrefix.Exa => "E",
        Units.MetricMultiplicativePrefix.Peta => "P",
        Units.MetricMultiplicativePrefix.Tera => "T",
        Units.MetricMultiplicativePrefix.Giga => "G",
        Units.MetricMultiplicativePrefix.Mega => "M",
        Units.MetricMultiplicativePrefix.Kilo => "k",
        Units.MetricMultiplicativePrefix.Hecto => "h",
        Units.MetricMultiplicativePrefix.Deca => preferUnicode ? "\u3372" : "da",
        Units.MetricMultiplicativePrefix.One => string.Empty,
        Units.MetricMultiplicativePrefix.Deci => "d",
        Units.MetricMultiplicativePrefix.Centi => "c",
        Units.MetricMultiplicativePrefix.Milli => "m",
        Units.MetricMultiplicativePrefix.Micro => "\u00B5",
        Units.MetricMultiplicativePrefix.Nano => "n",
        Units.MetricMultiplicativePrefix.Pico => "p",
        Units.MetricMultiplicativePrefix.Femto => "f",
        Units.MetricMultiplicativePrefix.Atto => "a",
        Units.MetricMultiplicativePrefix.Zepto => "z",
        Units.MetricMultiplicativePrefix.Yocto => "y",
        _ => string.Empty,
      };
  }

  namespace Units
  {
    public enum MetricMultiplicativePrefix
    {
      Quetta = 30,
      Ronna = 27,
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
      Ronto = -27,
      Quecto = -30,
    }

    /// <summary>Parts per notation. In science and engineering, the parts-per notation is a set of pseudo-units to describe small values of miscellaneous dimensionless quantities, e.g. mole fraction or mass fraction. Since these fractions are quantity-per-quantity measures, they are pure numbers with no associated units of measurement.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Metric_prefix"/>
    public readonly record struct MetricMultiplicative
      : System.IComparable, System.IComparable<MetricMultiplicative>, System.IFormattable, IUnitQuantifiable<double, MetricMultiplicativePrefix>
    {
      private readonly double m_value;

      /// <summary>Creates a new instance of this type.</summary>
      /// <param name="value">The value to represent.</param>
      /// <param name="multiplicativePrefix">The metric multiplicative prefix of the specified value.</param>
      public MetricMultiplicative(double value, MetricMultiplicativePrefix multiplicativePrefix)
        => m_value = value * multiplicativePrefix.GetUnitFactor();

      #region Static methods

      public static MetricMultiplicativePrefix FindMetricMultiplicativePrefix(double value, out double outValue, MetricMultiplicativePrefix prefix = MetricMultiplicativePrefix.One)
      {
        var sourceFactor = (int)prefix;
        var target = (MetricMultiplicativePrefix)System.Convert.ToInt32(new System.Numerics.BigInteger(System.Math.Truncate(value)).DigitCount(10) / 3 * 3 + sourceFactor);
        var targetFactor = (int)target;

        outValue = value / System.Math.Pow(10, targetFactor - sourceFactor);

        return target;
      }

      #endregion // Static methods

      #region Overloaded operators
      public static explicit operator double(MetricMultiplicative v) => v.Value;
      public static explicit operator MetricMultiplicative(double v) => new(v, MetricMultiplicativePrefix.One);

      public static bool operator <(MetricMultiplicative a, MetricMultiplicative b) => a.CompareTo(b) < 0;
      public static bool operator <=(MetricMultiplicative a, MetricMultiplicative b) => a.CompareTo(b) <= 0;
      public static bool operator >(MetricMultiplicative a, MetricMultiplicative b) => a.CompareTo(b) > 0;
      public static bool operator >=(MetricMultiplicative a, MetricMultiplicative b) => a.CompareTo(b) >= 0;

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

      // IComparable
      public int CompareTo(object? other) => other is not null && other is MetricMultiplicative o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(MetricMultiplicative other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(MetricMultiplicativePrefix.One, format, preferUnicode, useFullName);
      public double Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(MetricMultiplicativePrefix multiplicativePrefix, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":format")}}}", ToUnitValue(multiplicativePrefix))}{(multiplicativePrefix.GetUnitString(preferUnicode, useFullName) is var prefix && prefix.Length > 0 ? $" {prefix}" : string.Empty)}";
      public double ToUnitValue(MetricMultiplicativePrefix multiplicativePrefix) => m_value / multiplicativePrefix.GetUnitFactor();

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
