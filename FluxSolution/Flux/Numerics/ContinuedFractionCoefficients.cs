namespace Flux
{
  public static class ContinuedFractionCoefficients
  {
    /// <summary>
    /// <para></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Continued_fraction"/></para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="source"></param>
    /// <param name="maxCoefficients"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GenerateContinuedFractionCoefficients<TFloat>(this TFloat source, int maxCoefficients = 97)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(maxCoefficients);

      var a = new System.Collections.Generic.List<System.Numerics.BigInteger>();

      var count = 0;

      var previous = System.Numerics.BigInteger.Zero;

      while (TFloat.Truncate(source) is var wholePart && !TFloat.IsZero(wholePart) && --maxCoefficients >= 0 && !TFloat.IsInfinity(wholePart))
      {
        var current = System.Numerics.BigInteger.CreateChecked(wholePart);

        yield return current;

        if (current == previous && count > maxCoefficients)
          break;
        //if (a.AsReadOnlySpan().CommonSuffixLength(a.Last()) > 23)
        //  break;

        source = TFloat.One / (source - wholePart);

        previous = current;
      }
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
    /// <param name="maxTerms"></param>
    /// <param name="showAsIrrational"></param>
    /// <returns></returns>
    public static string ToContinuedFractionRepresentationString<TInteger>(this System.Collections.Generic.List<TInteger> source, UnicodeSpacing spacing = UnicodeSpacing.None, int maxTerms = 23)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var spacingString = spacing.ToSpacingString();

      var sb = new System.Text.StringBuilder();

      using var e = source.GetEnumerator();

      if (e.MoveNext())
      {
        sb.Append('[');

        sb.Append(e.Current);
        sb.Append(';');
        sb.Append(spacingString);

        var count = 1;

        while (e.MoveNext() && count <= maxTerms)
        {
          if (count++ >= 0)
          {
            sb.Append(',');
            sb.Append(spacingString);
          }

          sb.Append(e.Current);
        }

        if (count >= maxTerms)
        {
          sb.Append(',');
          sb.Append(spacingString);
          sb.Append('.', 3);
        }

        sb.Append(']');
      }

      return sb.ToString();
    }
  }
}
