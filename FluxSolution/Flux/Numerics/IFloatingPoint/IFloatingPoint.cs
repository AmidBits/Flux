//namespace Flux
//{
//  public static partial class IFloatingPoint
//  {
//    extension<TFloat>(TFloat value)
//      where TFloat : System.Numerics.IFloatingPoint<TFloat>
//    {
//      /// <summary>
//      /// <para>Compare the fraction part of <paramref name="value"/> to it's midpoint (i.e. its .5).</para>
//      /// </summary>
//      /// <typeparam name="TFloat"></typeparam>
//      /// <param name="value">The value to be compared.</param>
//      /// <returns>
//      /// <para>The result is similar to that of the Compare/CompareTo functionality, but exactly -1, 0, or 1 is always returned.</para>
//      /// <para>-1 if <paramref name="value"/> is less-than 0.5.</para>
//      /// <para>0 if <paramref name="value"/> is equal-to 0.5.</para>
//      /// <para>1 if <paramref name="value"/> is greater-than 0.5.</para>
//      /// </returns>
//      public int CompareToFractionMidpoint()
//        => value.CompareToFractionPercent(TFloat.CreateChecked(0.5));

//      /// <summary>
//      /// <para>Compares the fraction part of <paramref name="value"/> to the specified <paramref name="percent"/> and returns the sign of the result (i.e. -1 means less-than, 0 means equal-to, and 1 means greater-than).</para>
//      /// </summary>
//      /// <typeparam name="TFloat"></typeparam>
//      /// <param name="value">The value to be compared.</param>
//      /// <param name="percent">Percent in the range [0, 1].</param>
//      /// <returns>
//      /// <para>The result is similar to that of the Compare/CompareTo functionality, but exactly -1, 0, or 1 is always returned.</para>
//      /// <para>-1 when <paramref name="value"/> is less than <paramref name="percent"/>.</para>
//      /// <para>0 when <paramref name="value"/> is equal to <paramref name="percent"/>.</para>
//      /// <para>1 when <paramref name="value"/> is greater than <paramref name="percent"/>.</para>
//      /// </returns>
//      public int CompareToFractionPercent(TFloat percent)
//        => (value - TFloat.Floor(value)).CompareTo(percent);

//      /// <summary>
//      /// <para>Equivalent to the opposite effect of the Truncate() functionality, i.e. instead of truncating the fraction and essentially executing a round-toward-zero, envelop the fraction and essentially execute a round-away-from-zero.</para>
//      /// <para>It can also be seen as a companion function to truncate(). Unlike truncate() which calls floor() for positive numbers and ceiling() for negative; envelope() calls ceiling() for positive numbers and floor() for negative.</para>
//      /// </summary>
//      /// <typeparam name="TFloat"></typeparam>
//      /// <param name="value"></param>
//      /// <returns></returns>
//      /// <remarks>Like truncate, envelop is a symmetric biased around 0 type rounding.</remarks>
//      public TFloat Envelop()
//        => TFloat.IsNegative(value) ? TFloat.Floor(value) : TFloat.Ceiling(value);

//      public Numerics.BigRational ToBigRational(int maxApproximationIterations = 101)
//      {
//        if (TFloat.IsZero(value)) return Numerics.BigRational.Zero;
//        if (TFloat.IsInteger(value)) return new Numerics.BigRational(System.Numerics.BigInteger.CreateChecked(value));

//        var Am = (Item1: System.Numerics.BigInteger.Zero, Item2: System.Numerics.BigInteger.One);
//        var Bm = (Item1: System.Numerics.BigInteger.One, Item2: System.Numerics.BigInteger.Zero);

//        System.Numerics.BigInteger A;
//        System.Numerics.BigInteger B;

//        var a = System.Numerics.BigInteger.Zero;
//        var b = System.Numerics.BigInteger.Zero;

//        if (value > TFloat.One)
//        {
//          var xW = TFloat.Truncate(value);

//          var ar = ToBigRational(value - xW, maxApproximationIterations);

//          return ar + System.Numerics.BigInteger.CreateChecked(xW);
//        }

//        for (var counter = 0; counter < maxApproximationIterations && !TFloat.IsZero(value); counter++)
//        {
//          var r = TFloat.One / value;
//          var rR = TFloat.Round(r);

//          var rT = System.Numerics.BigInteger.CreateChecked(rR);

//          A = Am.Item2 + rT * Am.Item1;
//          B = Bm.Item2 + rT * Bm.Item1;

//          if (double.IsInfinity(double.CreateChecked(A)) || double.IsInfinity(double.CreateChecked(B)))
//            break;

//          a = A;
//          b = B;

//          Am = (A, Am.Item1);
//          Bm = (B, Bm.Item1);

//          value = r - rR;
//        }

//        return new(a, b);
//      }

//      public string ToDecimalFormattedNumberString(int decimalCount = 339)
//        => value.ToString("0." + new string('#', decimalCount), null);

//      public (System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator) ToRationalNumber(int maxApproximationIterations = 101)
//      {
//        var br = ToBigRational(value, maxApproximationIterations);

//        return (br.Numerator, br.Denominator);
//      }
//    }
//  }
//}
