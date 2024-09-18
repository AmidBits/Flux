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
    public static T Convert<T>(this Quantities.MetricPrefix source, T value, Quantities.MetricPrefix target)
      where T : System.Numerics.INumber<T>, System.Numerics.IPowerFunctions<T>
      => value * T.Pow(T.CreateChecked(10), T.CreateChecked((int)source) - T.CreateChecked((int)target));

    public static decimal Convert(this Quantities.MetricPrefix source, decimal value, Quantities.MetricPrefix target)
      => value * (decimal)double.Pow(10, (int)source - (int)target);

    /// <summary>
    /// <para>Find the infimum (the largest that is less than) and supremum (the smallest that is greater than) prefixes with adjusted value of the specified <paramref name="source"/> prefix and <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static (T TowardZeroValue, Quantities.MetricPrefix TowardZeroPrefix, T AwayFromValue, Quantities.MetricPrefix AwayFromPrefix) GetInfimumAndSupremum<T>(this Quantities.MetricPrefix source, T value, bool proper)
      where T : System.Numerics.INumber<T>, System.Numerics.IPowerFunctions<T>
    {
      var metricPrefixes = System.Enum.GetValues<Quantities.MetricPrefix>();

      var adjustedValue = source.Convert(value, Quantities.MetricPrefix.NoPrefix);

      var (InfimumIndex, InfimumItem, InfimumValue, SupremumIndex, SupremumItem, SupremumValue) = metricPrefixes.AsReadOnlySpan().GetInfimumAndSupremum(adjustedValue, e => T.CopySign(T.Pow(T.CreateChecked(10), T.Abs(T.CreateChecked((int)e))), T.CreateChecked((int)e)), proper);

      var ltValue = Quantities.MetricPrefix.NoPrefix.Convert(value, InfimumItem);
      var gtValue = Quantities.MetricPrefix.NoPrefix.Convert(value, SupremumItem);

      return (ltValue, InfimumItem, gtValue, SupremumItem);
    }

    public static string GetUnitName(this Quantities.MetricPrefix source)
      => source != Quantities.MetricPrefix.NoPrefix ? source.ToString() : string.Empty;

    public static string GetUnitSymbol(this Quantities.MetricPrefix source, bool preferUnicode)
      => source switch
      {
        Quantities.MetricPrefix.NoPrefix => string.Empty,
        Quantities.MetricPrefix.Quetta => "Q",
        Quantities.MetricPrefix.Ronna => "R",
        Quantities.MetricPrefix.Yotta => "Y",
        Quantities.MetricPrefix.Zetta => "Z",
        Quantities.MetricPrefix.Exa => "E",
        Quantities.MetricPrefix.Peta => "P",
        Quantities.MetricPrefix.Tera => "T",
        Quantities.MetricPrefix.Giga => "G",
        Quantities.MetricPrefix.Mega => "M",
        Quantities.MetricPrefix.Kilo => "k",
        Quantities.MetricPrefix.Hecto => "h",
        Quantities.MetricPrefix.Deca => preferUnicode ? "\u3372" : "da",
        Quantities.MetricPrefix.Deci => "d",
        Quantities.MetricPrefix.Centi => "c",
        Quantities.MetricPrefix.Milli => "m",
        Quantities.MetricPrefix.Micro => preferUnicode ? "\u03BC" : "\u00B5",
        Quantities.MetricPrefix.Nano => "n",
        Quantities.MetricPrefix.Pico => "p",
        Quantities.MetricPrefix.Femto => "f",
        Quantities.MetricPrefix.Atto => "a",
        Quantities.MetricPrefix.Zepto => "z",
        Quantities.MetricPrefix.Yocto => "y",
        Quantities.MetricPrefix.Ronto => "r",
        Quantities.MetricPrefix.Quecto => "q",
        _ => string.Empty,
      };

    public static double GetUnitValue(this Quantities.MetricPrefix source) => System.Math.Pow(10, (int)source);
  }

  namespace Quantities
  {
    /// <summary>
    /// <para>The MetricPrefix enum represents the SI metrix prefix decimal (base 10) multiples.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Metric_prefix#SI_prefixes_table"/></para>
    /// <para><see href="https://en.wikipedia.org/wiki/International_System_of_Units#Prefixes"/></para>
    /// </summary>
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

    //public static partial class MetricMultiplicative
    //{
    //  public static double MetricUnit1PerUnit2<TUnit1, TUnit2>(double value, IUnitValueQuantifiable<double, TUnit1> m1, IUnitValueQuantifiable<double, TUnit2> m2)
    //  {

    //  }
    //}

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
