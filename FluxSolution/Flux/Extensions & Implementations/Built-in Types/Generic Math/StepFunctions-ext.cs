namespace Flux
{
  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region Heavistep

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="TResult"></typeparam>
      /// <param name="value"></param>
      /// <param name="zeroValueByConvention"></param>
      /// <param name="result"></param>
      /// <returns></returns>
      public static TResult Heavistep<TResult>(TNumber value, TResult zeroValueByConvention, out TResult result)
        where TResult : System.Numerics.INumber<TResult>
        => result
        = TNumber.IsNegative(value)
        ? TResult.Zero
        : TNumber.IsZero(value)
        ? zeroValueByConvention
        : TResult.One;

      #endregion

      #region KroneckerDelta

      /// <summary>
      /// <para>The Kronecker delta is a function of two variables, usually just non-negative integers. The function is 1 if the variables are equal, and 0 otherwise.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Kronecker_delta"/></para>
      /// </summary>
      /// <param name="b"></param>
      /// <returns>Exactly <typeparamref name="TResult"/>.One (when <paramref name="value"/> == <paramref name="other"/>), otherwise <typeparamref name="TResult"/>.Zero.</returns>
      public static TResult KroneckerDelta<TResult>(TNumber value, TNumber other, out TResult kroneckerDelta)
        where TResult : System.Numerics.INumber<TResult>
        => kroneckerDelta
        = (value == other)
        ? TResult.One
        : TResult.Zero;

      #endregion

      #region Sign

      /// <summary>
      /// <para>Sign step function that guarantees one of {-<typeparamref name="TNumber"/>.One, <typeparamref name="TNumber"/>.Zero, <typeparamref name="TNumber"/>.One} for output (not just less-than-zero and greater-than-zero).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Sign_function"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Step_function"/></para>
      /// </summary>
      /// <returns>Exactly -<typeparamref name="TNumber"/>.One (when <paramref name="value"/> is negative), <typeparamref name="TNumber"/>.Zero (when <paramref name="value"/> is zero), otherwise <typeparamref name="TNumber"/>.One.</returns>
      public static TResult Sign<TResult>(TNumber value, out TResult sign)
        where TResult : System.Numerics.INumber<TResult>
      {
        if (TNumber.IsZero(value))
          return sign = TResult.Zero;

        return UnitSign(value, out sign);
      }

      #endregion

      #region UnitSign

      /// <summary>
      /// <para>The unit sign step function, i.e. zero is treated as a positive unit value of one.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Step_function"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Sign_function"/></para>
      /// </summary>
      /// <returns>Exactly -<typeparamref name="TResult"/>.One (when <paramref name="value"/> is negative) otherwise <typeparamref name="TResult"/>.One.</returns>
      public static TResult UnitSign<TResult>(TNumber value, out TResult unit)
        where TResult : System.Numerics.INumber<TResult>
        => unit
        = TNumber.IsNegative(value)
        ? -TResult.One
        : TResult.One;

      #endregion
    }
  }
}
