namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string? ConvertToShortScale(this Units.MetricPrefix source)
      => Globalization.En.NumeralComposition.ShortScaleDictionary.TryGetValue(source.GetMetricPrefixValue(), out var shortScaleName) ? shortScaleName : null;

    /// <summary>
    /// <para>Convert <paramref name="value"/> from <paramref name="source"/> prefix to <paramref name="target"/> prefix with a choice of <paramref name="dimensions"/> (1 is default, 2 for squares and 3 for cubes).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="target"></param>
    /// <param name="dimensions"></param>
    /// <returns></returns>
    public static T ConvertTo<T>(this Units.MetricPrefix source, T value, Units.MetricPrefix target, int dimensions)
      where T : System.Numerics.INumber<T>, System.Numerics.IPowerFunctions<T>
      => value * T.Pow(T.Pow(T.CreateChecked(10), T.CreateChecked((int)source) - T.CreateChecked((int)target)), T.CreateChecked(dimensions));

    public static T ConvertTo<T>(this Units.MetricPrefix source, T value, Units.MetricPrefix target)
      where T : System.Numerics.INumber<T>, System.Numerics.IPowerFunctions<T>
      => value * T.Pow(T.CreateChecked(10), T.CreateChecked((int)source) - T.CreateChecked((int)target));

    public static System.Runtime.Intrinsics.Vector256<double> ConvertTo(this Units.MetricPrefix source, System.Runtime.Intrinsics.Vector256<double> value, Units.MetricPrefix target, int dimensions)
      => value * double.Pow(double.Pow(10, (int)source - (int)target), dimensions);

    public static System.Runtime.Intrinsics.Vector256<double> ConvertTo(this Units.MetricPrefix source, System.Runtime.Intrinsics.Vector256<double> value, Units.MetricPrefix target)
      => value * double.Pow(10, (int)source - (int)target);

    /// <summary>
    /// <para>Find the infimum (the largest that is less than) and supremum (the smallest that is greater than) prefixes with adjusted value of the specified <paramref name="source"/> prefix and <paramref name="value"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static (T TowardZeroValue, Units.MetricPrefix TowardZeroPrefix, T AwayFromValue, Units.MetricPrefix AwayFromPrefix) GetInfimumAndSupremum<T>(this Units.MetricPrefix source, T value, bool proper)
      where T : System.Numerics.INumber<T>, System.Numerics.IPowerFunctions<T>
    {
      var metricPrefixes = System.Enum.GetValues<Units.MetricPrefix>();

      var adjustedValue = source.ConvertTo(value, Units.MetricPrefix.Unprefixed);

      var (_, InfimumItem, _, _, SupremumItem, _) = metricPrefixes.AsReadOnlySpan().GetInfimumAndSupremum(adjustedValue, e => T.CopySign(T.Pow(T.CreateChecked(10), T.Abs(T.CreateChecked((int)e))), T.CreateChecked((int)e)), proper);

      var ltValue = source.ConvertTo(value, InfimumItem);
      var gtValue = source.ConvertTo(value, SupremumItem);

      return (ltValue, InfimumItem, gtValue, SupremumItem);
    }

    /// <summary>
    /// <para>Gets the name of a <see cref="Units.MetricPrefix"/> by means of the [enum-constant].ToString() method.</para>
    /// </summary>
    /// <remarks>The <see cref="Units.MetricPrefix.Unprefixed"/> is an exception and returns <see cref="string.Empty"/>.</remarks>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string GetMetricPrefixName(this Units.MetricPrefix source)
      => (source != Units.MetricPrefix.Unprefixed) ? source.ToString() : string.Empty;

    /// <summary>
    /// <para>Gets the standardized symbol of a <see cref="Units.MetricPrefix"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="preferUnicode">When true, use Unicode codepoints when available, otherwise regular letters are used.</param>
    /// <returns></returns>
    public static string GetMetricPrefixSymbol(this Units.MetricPrefix source, bool preferUnicode)
      => source switch
      {
        Units.MetricPrefix.Unprefixed => string.Empty,
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

    /// <summary>
    /// <para>Gets the numerical value of a <see cref="Units.MetricPrefix"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Numerics.BigInteger GetMetricPrefixValue(this Units.MetricPrefix source)
      => System.Numerics.BigInteger.Pow(10, (int)source);
  }
}
