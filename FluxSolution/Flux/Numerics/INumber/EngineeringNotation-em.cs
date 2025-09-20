namespace Flux
{
  public static partial class EngineeringNotation
  {
    /// <summary>
    /// <para>Gets a prefix and value representing the engineering notation of <paramref name="value"/> and optionally whether to <paramref name="restrictToTriplets"/>.</para>
    /// <example><code>var (p, v) = (1803).GetEngineeringNotationProperties(); // (p: "k", v: 1.803)</code></example>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="restrictToTriplets"></param>
    /// <returns></returns>
    public static (Units.MetricPrefix EngineeringNotationPrefix, double EngineeringNotationValue) GetEngineeringNotationProperties<TNumber>(this TNumber value, bool restrictToTriplets = true)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      var prefix = Units.MetricPrefix.Unprefixed;

      var number = double.CreateChecked(value);

      if (!TNumber.IsZero(value))
      {
        prefix = double.Log10(double.Abs(number)) is var log10 && restrictToTriplets
        ? (Units.MetricPrefix)int.CreateChecked(double.Floor(log10 / 3) * 3)
        : System.Enum.GetValues<Units.MetricPrefix>().GetInfimumAndSupremum(mp => (int)mp, int.CreateChecked(double.Floor(log10)), true).InfimumItem;

        number *= double.Pow(10, -(int)prefix);
      }

      return (prefix, number);
    }

    /// <summary>
    /// <para>Creates a string representation of the <paramref name="value"/> number in engineering notation, with options of <paramref name="format"/>, <paramref name="formatProvider"/>, <paramref name="spacing"/> and <paramref name="restrictToTriplets"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Engineering_notation"/></para>
    /// <example><code>var v = (1803).ToEngineeringNotationString("g"); // v == "1.803 kg"</code></example>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="unit"></param>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <param name="spacing"></param>
    /// <param name="restrictToTriplets"></param>
    /// <returns></returns>
    public static string ToEngineeringNotationString<TNumber>(this TNumber value, string? unit = null, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.ThinSpace, bool restrictToTriplets = true)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      if (TNumber.IsZero(value))
        return '0'.ToString();

      var sm = new SpanMaker<char>();

      var (enp, env) = value.GetEngineeringNotationProperties(restrictToTriplets);

      sm = sm.Append(env.ToString(format, formatProvider));

      var symbol = enp.GetMetricPrefixSymbol(false);

      var hasSymbol = symbol.Length > 0;
      var hasUnit = !string.IsNullOrWhiteSpace(unit);

      if (hasSymbol || hasUnit)
      {
        sm = sm.Append(spacing.ToSpacingString());

        if (hasSymbol)
          sm = sm.Append(symbol);

        if (hasUnit)
          sm = sm.Append(unit);
      }

      return sm.ToString();
    }

    ///// <summary>
    ///// <para>Derives the appropriate prefix for engineering notation.</para>
    ///// </summary>
    ///// <typeparam name="TSource"></typeparam>
    ///// <param name="source"></param>
    ///// <param name="forceTriples"></param>
    ///// <returns></returns>
    //public static Units.MetricPrefix GetEngineeringNotationPrefix<TSource>(this TSource source, bool forceTriples = true)
    //  where TSource : System.Numerics.INumber<TSource>
    //{
    //  if (TSource.IsZero(source))
    //    return Units.MetricPrefix.Unprefixed;

    //  var log = int.CreateChecked(TSource.Abs(source).FastIntegerLog(10, UniversalRounding.WholeToNegativeInfinity, out var _));

    //  if (forceTriples && log > -3 && log < 3)
    //    return (Units.MetricPrefix)log.Spread(-3, 3, HalfRounding.Random);

    //  if (TSource.IsNegative(source) && log.TruncRem(3) is var (tq, r))
    //    return (Units.MetricPrefix)((tq + int.Sign(r)) * 3);

    //  return (Units.MetricPrefix)(log / 3 * 3);
    //}

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
  }
}
