namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Continued_fraction"/></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="source"></param>
    /// <param name="maxCount"></param>
    /// <returns></returns>
    public static System.Collections.Generic.List<System.Numerics.BigInteger> GenerateContinuedFractionCoefficients<TNumber>(this TNumber source, int maxCount)
      where TNumber : System.Numerics.IFloatingPoint<TNumber>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(maxCount);

      var a = new System.Collections.Generic.List<System.Numerics.BigInteger>();

      while (TNumber.Truncate(source) is var wholePart && !TNumber.IsZero(wholePart) && --maxCount >= 0 && !TNumber.IsInfinity(wholePart))
      {
        a.Add(System.Numerics.BigInteger.CreateChecked(wholePart));

        if (a.AsReadOnlySpan().CommonSuffixLength(a.Last()) > 23)
          break;

        source = TNumber.One / (source - wholePart);
      }

      return a;
    }

    /// <summary>
    /// <para></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Continued_fraction"/></para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Numerics.BigRational ParseContinuedFractionCoefficients<TInteger>(this System.Collections.Generic.List<TInteger> source)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var br = Numerics.BigRational.Zero;

      for (var i = source.Count - 1; i >= 0; i--)
      {
        var c = new Numerics.BigRational(System.Numerics.BigInteger.CreateChecked(source[i]));

        br = Numerics.BigRational.IsZero(br) ? c : c + Numerics.BigRational.One / br;
      }

      return br;
    }

    //public static Numerics.BigRational ToBigRational<TSelf>(this TSelf value, int maxApproximationIterations = 101)
    //  where TSelf : System.Numerics.IFloatingPoint<TSelf>
    //{
    //  if (TSelf.IsZero(value)) return Numerics.BigRational.Zero;
    //  if (TSelf.IsInteger(value)) return new Numerics.BigRational(System.Numerics.BigInteger.CreateChecked(value));

    //  var Am = (Item1: System.Numerics.BigInteger.Zero, Item2: System.Numerics.BigInteger.One);
    //  var Bm = (Item1: System.Numerics.BigInteger.One, Item2: System.Numerics.BigInteger.Zero);

    //  System.Numerics.BigInteger A;
    //  System.Numerics.BigInteger B;

    //  var a = System.Numerics.BigInteger.Zero;
    //  var b = System.Numerics.BigInteger.Zero;

    //  if (value > TSelf.One)
    //  {
    //    var xW = TSelf.Truncate(value);

    //    var ar = ToBigRational(value - xW, maxApproximationIterations);

    //    return ar + System.Numerics.BigInteger.CreateChecked(xW);
    //  }

    //  for (var counter = 0; counter < maxApproximationIterations && !TSelf.IsZero(value); counter++)
    //  {
    //    var r = TSelf.One / value;
    //    var rR = TSelf.Round(r);

    //    var rT = System.Numerics.BigInteger.CreateChecked(rR);

    //    A = Am.Item2 + rT * Am.Item1;
    //    B = Bm.Item2 + rT * Bm.Item1;

    //    if (double.IsInfinity(double.CreateChecked(A)) || double.IsInfinity(double.CreateChecked(B)))
    //      break;

    //    a = A;
    //    b = B;

    //    Am = (A, Am.Item1);
    //    Bm = (B, Bm.Item1);

    //    value = r - rR;
    //  }

    //  return new(a, b);
    //}

    /// <summary>
    /// <para></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Continued_fraction"/></para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="source"></param>
    /// <param name="spacing"></param>
    /// <param name="showAsIrrational"></param>
    /// <returns></returns>
    public static string ToContinuedFractionRepresentationString<TInteger>(this System.Collections.Generic.List<TInteger> source, Unicode.UnicodeSpacing spacing = Unicode.UnicodeSpacing.Space, bool showAsIrrational = false)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var spacingString = spacing.ToSpacingString();

      var sb = new System.Text.StringBuilder();

      sb.Append('[');
      sb.Append(source[0]);
      sb.Append(';');
      sb.Append(spacingString);
      sb.AppendJoin(',' + spacingString, source.Skip(1));
      if (showAsIrrational)
      {
        sb.Append(',');
        sb.Append(spacingString);
        sb.Append('.', 3);
      }
      sb.Append(']');

      return sb.ToString();
    }
  }
}
