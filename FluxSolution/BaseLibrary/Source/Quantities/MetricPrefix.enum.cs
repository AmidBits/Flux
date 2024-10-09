namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Convert <paramref name="value"/> from <paramref name="source"/> prefix to <paramref name="target"/> prefix with a choice of <paramref name="dimensions"/> (1 is default, 2 for squares and 3 for cubes).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="target"></param>
    /// <param name="dimensions"></param>
    /// <returns></returns>
    public static T ConvertTo<T>(this Quantities.MetricPrefix source, T value, Quantities.MetricPrefix target, int dimensions = 1)
      where T : System.Numerics.INumber<T>, System.Numerics.IPowerFunctions<T>
      => value * T.Pow(T.Pow(T.CreateChecked(10), (T.CreateChecked((int)source) - T.CreateChecked((int)target))), T.CreateChecked(dimensions));

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

      var adjustedValue = source.ConvertTo(value, Quantities.MetricPrefix.Unprefixed);

      var (InfimumIndex, InfimumItem, InfimumValue, SupremumIndex, SupremumItem, SupremumValue) = metricPrefixes.AsReadOnlySpan().GetInfimumAndSupremum(adjustedValue, e => T.CopySign(T.Pow(T.CreateChecked(10), T.Abs(T.CreateChecked((int)e))), T.CreateChecked((int)e)), proper);

      var ltValue = Quantities.MetricPrefix.Unprefixed.ConvertTo(value, InfimumItem);
      var gtValue = Quantities.MetricPrefix.Unprefixed.ConvertTo(value, SupremumItem);

      return (ltValue, InfimumItem, gtValue, SupremumItem);
    }

    public static string GetPrefixName(this Quantities.MetricPrefix source)
      => source != Quantities.MetricPrefix.Unprefixed ? source.ToString() : string.Empty;

    public static string GetPrefixSymbol(this Quantities.MetricPrefix source, bool preferUnicode)
      => source switch
      {
        Quantities.MetricPrefix.Unprefixed => string.Empty,
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

    public static double GetPrefixValue(this Quantities.MetricPrefix source) => System.Math.Pow(10, (int)source);
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
      Unprefixed = 0,
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
  }
}
