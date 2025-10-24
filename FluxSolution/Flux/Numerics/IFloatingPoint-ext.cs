using System.Globalization;

namespace Flux
{
  public static partial class IFloatingPoint
  {
    /// <summary>Represents the Champernowne constant. A transcendental real constant whose decimal expansion has important properties.</summary>
    public const double C10 = 0.123456789101112131415161718192021222324252627282930313233343536373839404142434445464748495051525354555657585960;

    /// <summary>Represents the cube root of 2.</summary>
    public const double DeliansConstant = 1.2599210498948731647672106072782;

    /// <summary>Represents mathematical constants.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Euler%27s_constant"/>
    public const double EulersConstant = 0.57721566490153286060651209008240243;

    /// <summary>Represents the ratio of two quantities being the same as the ratio of their sum to their maximum. (~1.618)</summary>
    /// <see href="https://en.wikipedia.org/wiki/Golden_ratio"/>
    public const double GoldenRatio = 1.6180339887498948482045868343656381177203091798057628621354486227052604628189024497072072041893911374;

    public const double HalfPi = double.Pi / 2;

    /// <summary>Represents the square root of 2.</summary>
    public const double PythagorasConstant = 1.414213562373095048801688724209698078569671875376948073176679737990732478462;

    /// <summary>Represents the square root of 3.</summary>
    public const double TheodorusConstant = 1.732050807568877293527446341505872366942805253810380628055806979451933016909;

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPointConstants<TFloat>
    {
      /// <summary>Represents the Champernowne constant. A transcendental real constant whose decimal expansion has important properties.</summary>
      public static TFloat C10 => TFloat.CreateChecked(C10);

      /// <summary>Represents the cube root of 2.</summary>
      public static TFloat DeliansConstant => TFloat.CreateChecked(DeliansConstant);

      /// <summary>Represents mathematical constants.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Euler%27s_constant"/>
      public static TFloat EulersConstant => TFloat.CreateChecked(EulersConstant);

      /// <summary>Represents the ratio of two quantities being the same as the ratio of their sum to their maximum. (~1.618)</summary>
      /// <see href="https://en.wikipedia.org/wiki/Golden_ratio"/>
      public static TFloat GoldenRatio => TFloat.CreateChecked(GoldenRatio);

      public static TFloat HalfPi => TFloat.CreateChecked(HalfPi);

      /// <summary>Represents the square root of 2.</summary>
      public static TFloat PythagorasConstant => TFloat.CreateChecked(PythagorasConstant);

      /// <summary>Represents the square root of 3.</summary>
      public static TFloat TheodorusConstant => TFloat.CreateChecked(TheodorusConstant);
    }

    //extension<TFloat>(TFloat)
    //  where TFloat : System.Numerics.IFloatingPoint<TFloat>
    //{
    //  /// <summary>
    //  /// <para>Equivalent to the opposite effect of the Truncate() functionality, i.e. instead of truncating the fraction and essentially executing a round-toward-zero, envelop the fraction and essentially execute a round-away-from-zero.</para>
    //  /// <para>It can also be seen as a companion function to truncate(). Unlike truncate() which calls floor() for positive numbers and ceiling() for negative; envelope() calls ceiling() for positive numbers and floor() for negative.</para>
    //  /// </summary>
    //  /// <remarks>Like truncate, envelop is a symmetric biased around 0 type rounding.</remarks>
    //  /// <typeparam name="TFloat"></typeparam>
    //  /// <param name="x"></param>
    //  /// <returns></returns>
    //  public static TFloat Envelop(TFloat x)
    //  => TFloat.IsNegative(x) ? TFloat.Floor(x) : TFloat.Ceiling(x);
    //}

    extension<TFloat>(TFloat x)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      /// <summary>
      /// <para>Compare the fraction part of <paramref name="value"/> to it's midpoint (i.e. its .5).</para>
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="value">The value to be compared.</param>
      /// <returns>
      /// <para>The result is similar to that of the Compare/CompareTo functionality, but exactly -1, 0, or 1 is always returned.</para>
      /// <para>-1 if <paramref name="value"/> is less-than 0.5.</para>
      /// <para>0 if <paramref name="value"/> is equal-to 0.5.</para>
      /// <para>1 if <paramref name="value"/> is greater-than 0.5.</para>
      /// </returns>
      public int CompareToFractionMidpoint()
        => x.CompareToFractionPercent(TFloat.CreateChecked(0.5));

      /// <summary>
      /// <para>Compares the fraction part of <paramref name="value"/> to the specified <paramref name="percent"/> and returns the sign of the result (i.e. -1 means less-than, 0 means equal-to, and 1 means greater-than).</para>
      /// </summary>
      /// <typeparam name="Float"></typeparam>
      /// <param name="value">The value to be compared.</param>
      /// <param name="percent">Percent in the range [0, 1].</param>
      /// <returns>
      /// <para>The result is similar to that of the Compare/CompareTo functionality, but exactly -1, 0, or 1 is always returned.</para>
      /// <para>-1 when <paramref name="value"/> is less than <paramref name="percent"/>.</para>
      /// <para>0 when <paramref name="value"/> is equal to <paramref name="percent"/>.</para>
      /// <para>1 when <paramref name="value"/> is greater than <paramref name="percent"/>.</para>
      /// </returns>
      public int CompareToFractionPercent(TFloat percent)
        => (x - TFloat.Floor(x)).CompareTo(percent);

      /// <summary>
      /// <para>Equivalent to the opposite effect of the Truncate() functionality, i.e. instead of truncating the fraction and essentially executing a round-toward-zero, envelop the fraction and essentially execute a round-away-from-zero.</para>
      /// <para>It can also be seen as a companion function to truncate(). Unlike truncate() which calls floor() for positive numbers and ceiling() for negative; envelope() calls ceiling() for positive numbers and floor() for negative.</para>
      /// </summary>
      /// <remarks>Like truncate, envelop is a symmetric biased around 0 type rounding.</remarks>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="x"></param>
      /// <returns></returns>
      public TFloat Envelop()
        => TFloat.IsNegative(x) ? TFloat.Floor(x) : TFloat.Ceiling(x);

      /// <summary>
      /// <para>Equivalent to the opposite effect of the Truncate() functionality, i.e. instead of truncating the fraction and essentially executing a round-toward-zero, envelop the fraction and essentially execute a round-away-from-zero.</para>
      /// <para>It can also be seen as a companion function to truncate(). Unlike truncate() which calls floor() for positive numbers and ceiling() for negative; envelope() calls ceiling() for positive numbers and floor() for negative.</para>
      /// </summary>
      /// <remarks>Like truncate, envelop is a symmetric biased around 0 type rounding.</remarks>
      /// <typeparam name="TFloat"></typeparam>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="x"></param>
      /// <param name="digits"></param>
      /// <returns></returns>
      public TFloat Envelop(int digits)
      {
        var m = TFloat.CreateChecked(System.Numerics.BigInteger.Pow(10, digits));

        return Envelop(x * m) / m;
      }

      public Numerics.BigRational ToBigRational(int maxApproximationIterations = 101)
      {
        if (TFloat.IsZero(x)) return Numerics.BigRational.Zero;
        if (TFloat.IsInteger(x)) return new Numerics.BigRational(System.Numerics.BigInteger.CreateChecked(x));

        var Am = (Item1: System.Numerics.BigInteger.Zero, Item2: System.Numerics.BigInteger.One);
        var Bm = (Item1: System.Numerics.BigInteger.One, Item2: System.Numerics.BigInteger.Zero);

        System.Numerics.BigInteger A;
        System.Numerics.BigInteger B;

        var a = System.Numerics.BigInteger.Zero;
        var b = System.Numerics.BigInteger.Zero;

        if (x > TFloat.One)
        {
          var xW = TFloat.Truncate(x);

          var ar = ToBigRational(x - xW, maxApproximationIterations);

          return ar + System.Numerics.BigInteger.CreateChecked(xW);
        }

        for (var counter = 0; counter < maxApproximationIterations && !TFloat.IsZero(x); counter++)
        {
          var r = TFloat.One / x;
          var rR = TFloat.Round(r);

          var rT = System.Numerics.BigInteger.CreateChecked(rR);

          A = Am.Item2 + rT * Am.Item1;
          B = Bm.Item2 + rT * Bm.Item1;

          if (double.IsInfinity(double.CreateChecked(A)) || double.IsInfinity(double.CreateChecked(B)))
            break;

          a = A;
          b = B;

          Am = (A, Am.Item1);
          Bm = (B, Bm.Item1);

          x = r - rR;
        }

        return new(a, b);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <typeparam name="TFloat"></typeparam>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="x"></param>
      /// <param name="digits"></param>
      /// <returns></returns>
      public TFloat Truncate(int digits)
      {
        var m = TFloat.CreateChecked(System.Numerics.BigInteger.Pow(10, digits));

        return TFloat.Truncate(x * m) / m;
      }
    }

    extension<TFloat>(TFloat x)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IRootFunctions<TFloat>
    {
      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Vincenty%27s_formulae"/></para>
      /// </summary>
      public TFloat HelmertsExpansionParameterK1()
        => TFloat.Sqrt(TFloat.One + x * x) is var k ? (k - TFloat.One) / (k + TFloat.One) : throw new System.ArithmeticException();
    }

  }
}
