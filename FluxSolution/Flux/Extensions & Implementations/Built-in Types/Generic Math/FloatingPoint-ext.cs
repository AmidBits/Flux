namespace Flux
{
  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region CompareToFractionMidpoint

      /// <summary>
      /// <para>Compare the fraction part of <paramref name="value"/> to it's midpoint (i.e. its .5).</para>
      /// </summary>
      /// <param name="x">The value to be compared.</param>
      /// <returns>
      /// <para>The result is similar to that of the Compare/CompareTo functionality, but exactly -1, 0, or 1 is always returned.</para>
      /// <para>-1 if <paramref name="x"/> is less-than 0.5.</para>
      /// <para>0 if <paramref name="x"/> is equal-to 0.5.</para>
      /// <para>+1 if <paramref name="x"/> is greater-than 0.5.</para>
      /// </returns>
      public static int CompareFractionToMidpoint(TFloat value)
        => CompareFractionToThreshold(value, TFloat.CreateChecked(0.5));

      #endregion

      #region CompareFractionToThreshold

      /// <summary>
      /// <para>Compares the fraction part of <paramref name="value"/> to the specified <paramref name="threshold"/> and returns the sign of the result (i.e. -1 means less-than, 0 means equal-to, and 1 means greater-than).</para>
      /// </summary>
      /// <param name="value">The value to be compared.</param>
      /// <param name="threshold">Must be in the unit interval [0, 1].</param>
      /// <returns>
      /// <para>The result is similar to that of the Compare/CompareTo functionality, but exactly -1, 0, or 1 is always returned.</para>
      /// <para>-1 when <paramref name="value"/> is less than <paramref name="threshold"/>.</para>
      /// <para>0 when <paramref name="value"/> is equal to <paramref name="threshold"/>.</para>
      /// <para>+1 when <paramref name="value"/> is greater than <paramref name="threshold"/>.</para>
      /// </returns>
      public static int CompareFractionToThreshold(TFloat value, TFloat threshold)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(threshold);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(threshold, TFloat.One);

        var floor = TFloat.Floor(value);

        return (value - floor).CompareTo(threshold);
      }

      #endregion

      #region GetParts

      /// <summary>
      /// <para>Gets the integral part and the fractional part of a <typeparamref name="TFloat"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Decimal"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Decimal_separator"/></para>
      /// <para><seealso href="https://stackoverflow.com/a/33996511/3178666"/></para>
      /// </summary>
      /// <returns>
      /// <para>The integral (integer) part and the fractional part of a 64-bit floating point value.</para>
      /// </returns>
      public static (TFloat IntegralPart, TFloat FractionalPart) GetParts(TFloat value)
      {
        var integral = TFloat.Truncate(value);
        var fractional = value - integral;

        return (integral, fractional);
      }

      #endregion

      #region Special functions

#if DECIMAL_SPECIAL_FUNCTIONS || DOUBLE_SPECIAL_FUNCTIONS
      public static TFloat Gamma(TFloat x)
        => x switch
        {
#if DECIMAL_SPECIAL_FUNCTIONS
          decimal dfp128 => TFloat.CreateChecked(decimal.Gamma(dfp128)),
#endif
#if DOUBLE_SPECIAL_FUNCTIONS
          double fp64 => TFloat.CreateChecked(double.Gamma(fp64)),
          float fp32 => TFloat.CreateChecked(double.Gamma(fp32)),
#endif
          _ => throw new System.NotImplementedException($"Type: {x.GetType()}"),
        };

      public static TFloat LogGamma(TFloat x)
        => x switch
        {
#if DECIMAL_SPECIAL_FUNCTIONS
          decimal dfp128 => TFloat.CreateChecked(decimal.LogGamma(dfp128)),
#endif
#if DOUBLE_SPECIAL_FUNCTIONS
          double fp64 => TFloat.CreateChecked(double.LogGamma(fp64)),
          float fp32 => TFloat.CreateChecked(double.LogGamma(fp32)),
#endif
          _ => throw new System.NotImplementedException($"Type: {x.GetType()}"),
        };
#endif

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IRootFunctions<TFloat>
    {
      #region HelmertsExpansionParameterK1

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Vincenty%27s_formulae"/></para>
      /// </summary>
      public static TFloat HelmertsExpansionParameterK1(TFloat x)
        => TFloat.Sqrt(TFloat.One + x * x) is var k ? (k - TFloat.One) / (k + TFloat.One) : throw new System.ArithmeticException();

      #endregion
    }
  }
}
