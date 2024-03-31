namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Convert <paramref name="value"/> from <paramref name="source"/> prefix to <paramref name="target"/> prefix.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static double Convert(this Units.MetricPrefix source, double value, Units.MetricPrefix target) => value * System.Math.Pow(10, (int)source - (int)target);

    /// <summary>
    /// <para>Find the infimum (the largest that is less than) prefix and value from <paramref name="source"/> prefix and <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static (double TargetValue, Units.MetricPrefix TargetPrefix) FindInfimum(this Units.MetricPrefix source, double value)
    {
      var targetDigits = Units.Radix.DigitCount(new System.Numerics.BigInteger(System.Math.Truncate(value)), 10) + (int)source - 1;

      var targetPrefix = (Units.MetricPrefix)System.Enum.GetValues<Units.MetricPrefix>().Select(p => (int)p).Order().Last(p => targetDigits >= p);
      var targetValue = source.Convert(value, targetPrefix);

      return (targetValue, targetPrefix);
    }

    public static double GetUnitFactor(this Units.MetricPrefix source) => System.Math.Pow(10, (int)source);

    public static string GetUnitString(this Units.MetricPrefix source, bool preferUnicode, bool useFullName)
      => useFullName ? source.ToString() : source switch
      {
        Units.MetricPrefix.NoPrefix => string.Empty,
        Units.MetricPrefix.Quetta => "Q",
        Units.MetricPrefix.Ronna => "R",
        Units.MetricPrefix.Yotta => "Y",
        Units.MetricPrefix.Zetta => "Z",
        Units.MetricPrefix.Exa => "E",
        Units.MetricPrefix.Peta => "P",
        Units.MetricPrefix.Tera => "T",
        Units.MetricPrefix.Giga => "G",
        Units.MetricPrefix.Mega => "M",
        Units.MetricPrefix.Kilo => "k",
        Units.MetricPrefix.Hecto => "h",
        Units.MetricPrefix.Deca => preferUnicode ? "\u3372" : "da",
        Units.MetricPrefix.Deci => "d",
        Units.MetricPrefix.Centi => "c",
        Units.MetricPrefix.Milli => "m",
        Units.MetricPrefix.Micro => preferUnicode ? "\u03BC" : "\u00B5",
        Units.MetricPrefix.Nano => "n",
        Units.MetricPrefix.Pico => "p",
        Units.MetricPrefix.Femto => "f",
        Units.MetricPrefix.Atto => "a",
        Units.MetricPrefix.Zepto => "z",
        Units.MetricPrefix.Yocto => "y",
        Units.MetricPrefix.Ronto => "r",
        Units.MetricPrefix.Quecto => "q",
        _ => string.Empty,
      };
  }

  namespace Units
  {
    public enum MetricPrefix
    {
      /// <summary>Represents a value that is not a metric multiple. A.k.a. one.</summary>
      NoPrefix = 0,
      /// <summary></summary>
      Quetta = 30,
      /// <summary></summary>
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
      /// <summary></summary>
      Ronto = -27,
      /// <summary></summary>
      Quecto = -30,
    }

    ///// <summary>Parts per notation. In science and engineering, the parts-per notation is a set of pseudo-units to describe small values of miscellaneous dimensionless quantities, e.g. mole fraction or mass fraction. Since these fractions are quantity-per-quantity measures, they are pure numbers with no associated units of measurement.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Metric_prefix"/>
    //public readonly record struct MetricMultiplicative
    //  : System.IComparable, System.IComparable<MetricMultiplicative>, System.IFormattable, IUnitValueQuantifiable<double, MetricMultiplicativePrefix>
    //{
    //  private readonly double m_value;

    //  /// <summary>Creates a new instance of this type.</summary>
    //  /// <param name="value">The value to represent.</param>
    //  /// <param name="multiplicativePrefix">The metric multiplicative prefix of the specified value.</param>
    //  public MetricMultiplicative(double value, MetricMultiplicativePrefix multiplicativePrefix) => m_value = value * multiplicativePrefix.GetUnitFactor();

    //  #region Static methods

    //  public static double Convert(double value, MetricMultiplicativePrefix from, MetricMultiplicativePrefix to) => value * System.Math.Pow(10, (int)from - (int)to);

    //  public static MetricMultiplicativePrefix FindMetricMultiplicativePrefix(double value, out double outValue, MetricMultiplicativePrefix prefix = MetricMultiplicativePrefix.One)
    //  {
    //    var sourceFactor = (int)prefix;
    //    var target = (MetricMultiplicativePrefix)System.Convert.ToInt64((long)Radix.DigitCount(new System.Numerics.BigInteger(System.Math.Truncate(value)), 10) / 3 * 3 + sourceFactor);
    //    var targetFactor = (int)target;

    //    outValue = value / System.Math.Pow(10, targetFactor - sourceFactor);

    //    return target;
    //  }

    //  #endregion // Static methods

    //  #region Overloaded operators
    //  public static explicit operator double(MetricMultiplicative v) => v.Value;
    //  public static explicit operator MetricMultiplicative(double v) => new(v, MetricMultiplicativePrefix.One);

    //  public static bool operator <(MetricMultiplicative a, MetricMultiplicative b) => a.CompareTo(b) < 0;
    //  public static bool operator <=(MetricMultiplicative a, MetricMultiplicative b) => a.CompareTo(b) <= 0;
    //  public static bool operator >(MetricMultiplicative a, MetricMultiplicative b) => a.CompareTo(b) > 0;
    //  public static bool operator >=(MetricMultiplicative a, MetricMultiplicative b) => a.CompareTo(b) >= 0;

    //  public static MetricMultiplicative operator -(MetricMultiplicative v) => new(-v.m_value, MetricMultiplicativePrefix.One);
    //  public static MetricMultiplicative operator +(MetricMultiplicative a, double b) => new(a.m_value + b, MetricMultiplicativePrefix.One);
    //  public static MetricMultiplicative operator +(MetricMultiplicative a, MetricMultiplicative b) => a + b.m_value;
    //  public static MetricMultiplicative operator /(MetricMultiplicative a, double b) => new(a.m_value / b, MetricMultiplicativePrefix.One);
    //  public static MetricMultiplicative operator /(MetricMultiplicative a, MetricMultiplicative b) => a / b.m_value;
    //  public static MetricMultiplicative operator *(MetricMultiplicative a, double b) => new(a.m_value * b, MetricMultiplicativePrefix.One);
    //  public static MetricMultiplicative operator *(MetricMultiplicative a, MetricMultiplicative b) => a * b.m_value;
    //  public static MetricMultiplicative operator %(MetricMultiplicative a, double b) => new(a.m_value % b, MetricMultiplicativePrefix.One);
    //  public static MetricMultiplicative operator %(MetricMultiplicative a, MetricMultiplicative b) => a % b.m_value;
    //  public static MetricMultiplicative operator -(MetricMultiplicative a, double b) => new(a.m_value - b, MetricMultiplicativePrefix.One);
    //  public static MetricMultiplicative operator -(MetricMultiplicative a, MetricMultiplicative b) => a - b.m_value;
    //  #endregion Overloaded operators

    //  #region Implemented interfaces

    //  // IComparable
    //  public int CompareTo(object? other) => other is not null && other is MetricMultiplicative o ? CompareTo(o) : -1;

    //  // IComparable<>
    //  public int CompareTo(MetricMultiplicative other) => m_value.CompareTo(other.m_value);

    //  // IFormattable
    //  public string ToString(string? format, System.IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    //  // IQuantifiable<>
    //  public string ToValueString(TextOptions options = default)
    //    => ToUnitValueString(MetricMultiplicativePrefix.One, options);

    //  public double Value => m_value;

    //  // IUnitQuantifiable<>
    //  public double GetUnitValue(MetricMultiplicativePrefix multiplicativePrefix)
    //    => m_value / multiplicativePrefix.GetUnitFactor();

    //  public string ToUnitValueString(MetricMultiplicativePrefix multiplicativePrefix, TextOptions options = default)
    //    => $"{string.Format($"{{0{(options.Format is null ? string.Empty : $":format")}}}", GetUnitValue(multiplicativePrefix))}{(multiplicativePrefix.GetUnitString(true, false) is var prefix && prefix.Length > 0 ? $" {prefix}" : string.Empty)}";

    //  #endregion Implemented interfaces

    //  public override string ToString() => ToValueString();
    //}
  }
}
