using System.ComponentModel;
using System.Net.Http.Headers;

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
    public static T ConvertTo<T>(this MetricPrefix source, T value, MetricPrefix target, int dimensions)
      where T : System.Numerics.INumber<T>, System.Numerics.IPowerFunctions<T>
      => value * T.Pow(T.Pow(T.CreateChecked(10), T.CreateChecked((int)source) - T.CreateChecked((int)target)), T.CreateChecked(dimensions));

    public static T ConvertTo<T>(this MetricPrefix source, T value, MetricPrefix target)
      where T : System.Numerics.INumber<T>, System.Numerics.IPowerFunctions<T>
      => value * T.Pow(T.CreateChecked(10), T.CreateChecked((int)source) - T.CreateChecked((int)target));

    public static System.Runtime.Intrinsics.Vector256<double> ConvertTo(this MetricPrefix source, System.Runtime.Intrinsics.Vector256<double> value, MetricPrefix target, int dimensions)
      => value * double.Pow(double.Pow(10, (int)source - (int)target), dimensions);

    public static System.Runtime.Intrinsics.Vector256<double> ConvertTo(this MetricPrefix source, System.Runtime.Intrinsics.Vector256<double> value, MetricPrefix target)
      => value * double.Pow(10, (int)source - (int)target);

    /// <summary>
    /// <para>Derives the appropriate prefix for engineering notation.</para>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="forceTriples"></param>
    /// <returns></returns>
    public static MetricPrefix GetEngineeringNotationPrefix<TSource>(this TSource source, bool forceTriples = true)
      where TSource : System.Numerics.INumber<TSource>
    {
      if (TSource.IsZero(source))
        return MetricPrefix.Unprefixed;

      var log = int.CreateChecked(TSource.Abs(source).FastIntegerLog(10, UniversalRounding.WholeToNegativeInfinity, out var _));

      if (forceTriples && log > -3 && log < 3)
        return (MetricPrefix)log.Spread(-3, 3, HalfRounding.Random);

      if (TSource.IsNegative(source) && log.TruncRemSgn(3, out var r, out var rs) is var q)
        return (MetricPrefix)((q + rs) * 3);

      return (MetricPrefix)(log / 3 * 3);
    }

    /// <summary>
    /// <para>Find the infimum (the largest that is less than) and supremum (the smallest that is greater than) prefixes with adjusted value of the specified <paramref name="source"/> prefix and <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static (T TowardZeroValue, MetricPrefix TowardZeroPrefix, T AwayFromValue, MetricPrefix AwayFromPrefix) GetInfimumAndSupremum<T>(this MetricPrefix source, T value, bool proper)
      where T : System.Numerics.INumber<T>, System.Numerics.IPowerFunctions<T>
    {
      var metricPrefixes = System.Enum.GetValues<MetricPrefix>();

      var adjustedValue = source.ConvertTo(value, MetricPrefix.Unprefixed);

      var (InfimumIndex, InfimumItem, InfimumValue, SupremumIndex, SupremumItem, SupremumValue) = metricPrefixes.AsReadOnlySpan().InfimumAndSupremum(adjustedValue, e => T.CopySign(T.Pow(T.CreateChecked(10), T.Abs(T.CreateChecked((int)e))), T.CreateChecked((int)e)), proper);

      var ltValue = MetricPrefix.Unprefixed.ConvertTo(value, InfimumItem);
      var gtValue = MetricPrefix.Unprefixed.ConvertTo(value, SupremumItem);

      return (ltValue, InfimumItem, gtValue, SupremumItem);
    }

    public static string GetMetricPrefixName(this MetricPrefix source)
      => source != MetricPrefix.Unprefixed ? source.ToString() : string.Empty;

    public static string GetMetricPrefixSymbol(this MetricPrefix source, bool preferUnicode)
      => source switch
      {
        MetricPrefix.Unprefixed => string.Empty,
        MetricPrefix.Quetta => "Q",
        MetricPrefix.Ronna => "R",
        MetricPrefix.Yotta => "Y",
        MetricPrefix.Zetta => "Z",
        MetricPrefix.Exa => "E",
        MetricPrefix.Peta => "P",
        MetricPrefix.Tera => "T",
        MetricPrefix.Giga => "G",
        MetricPrefix.Mega => "M",
        MetricPrefix.Kilo => "k",
        MetricPrefix.Hecto => "h",
        MetricPrefix.Deca => preferUnicode ? "\u3372" : "da",
        MetricPrefix.Deci => "d",
        MetricPrefix.Centi => "c",
        MetricPrefix.Milli => "m",
        MetricPrefix.Micro => preferUnicode ? "\u03BC" : "\u00B5",
        MetricPrefix.Nano => "n",
        MetricPrefix.Pico => "p",
        MetricPrefix.Femto => "f",
        MetricPrefix.Atto => "a",
        MetricPrefix.Zepto => "z",
        MetricPrefix.Yocto => "y",
        MetricPrefix.Ronto => "r",
        MetricPrefix.Quecto => "q",
        _ => string.Empty,
      };

    #region Engineering notation

    public static (MetricPrefix prefix, double value) GetEngineeringNotationProperties<TSelf>(this TSelf source, bool restrictToTriplets = true)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      var prefix = MetricPrefix.Unprefixed;

      var number = double.CreateChecked(source);

      if (!TSelf.IsZero(source))
      {
        var abs = double.Abs(number);
        var log10 = double.Log10(abs);

        if (restrictToTriplets)
        {
          var div3 = log10 / 3;
          var floor = double.Floor(div3);
          var mul3 = floor * 3;

          prefix = (MetricPrefix)int.CreateChecked(mul3);
        }
        else
        {
          var floor = double.Floor(log10);
          var iands = System.Enum.GetValues<MetricPrefix>().GetInfimumAndSupremum(mp => (int)mp, int.CreateChecked(floor), true);

          prefix = iands.TowardZeroItem;
        }

        var exp = (int)prefix;
        var pow = double.Pow(10, -exp);

        number *= pow;
      }

      return (prefix, number);
    }

    /// <summary>
    /// <para>Creates a new string, representing the <paramref name="source"/> number in engineering notation, with options of <paramref name="spacing"/>, <paramref name="format"/> and <paramref name="formatProvider"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Engineering_notation"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="source"></param>
    /// <param name="spacing"></param>
    /// <returns></returns>
    public static string ToEngineeringNotationString<TSelf>(this TSelf source, string? unit = null, string? format = null, System.IFormatProvider? formatProvider = null, Unicode.UnicodeSpacing spacing = Unicode.UnicodeSpacing.ThinSpace, bool tripletsOnly = true)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (TSelf.IsZero(source)) return "0";

      var sm = new SpanMaker<char>();

      var number = double.CreateChecked(source);

      var (prefix, value) = source.GetEngineeringNotationProperties(tripletsOnly);

      var exp = (int)prefix;
      var pow = double.Pow(10, -exp);

      sm = sm.Append(value.ToString(format, formatProvider));

      var symbol = prefix.GetMetricPrefixSymbol(false);

      if (symbol.Length > 0 is var hasSymbol && !string.IsNullOrWhiteSpace(unit) is var hasUnit && (hasSymbol || hasUnit))
      {
        sm = sm.Append(spacing.ToSpacingString());

        if (hasSymbol)
          sm = sm.Append(symbol);

        if (hasUnit)
          sm = sm.Append(unit);
      }

      return sm.ToString();
    }

    //public static string GetNumberWithUnitPrefix(this double number, int power = 1)
    //{
    //  char[] incPrefixes = new[] { 'k', 'M', 'G', 'T', 'P', 'E', 'Z', 'Y' };
    //  char[] decPrefixes = new[] { 'm', 'µ', 'n', 'p', 'f', 'a', 'z', 'y' };

    //  var degree = (int)double.Floor(double.Log10(double.Abs(number)) / (3 * (double)power));
    //  var scaled = number * double.Pow(1000, -(degree * power));

    //  char? prefix = null;

    //  switch (double.Sign(degree))
    //  {
    //    case 1: prefix = incPrefixes[degree - 1]; break;
    //    case -1: prefix = decPrefixes[-degree - 1]; break;
    //  }

    //  return scaled.ToString() + ' ' + prefix;
    //}

    #endregion // Engineering notation
  }

  /// <summary>
  /// <para>The MetricPrefix enum represents the SI metrix prefix decimal (base 10) multiples.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Metric_prefix#SI_prefixes_table"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/International_System_of_Units#Prefixes"/></para>
  /// </summary>
  [DefaultValue(Unprefixed)]
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
