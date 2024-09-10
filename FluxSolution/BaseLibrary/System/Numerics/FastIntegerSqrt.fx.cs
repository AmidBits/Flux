namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number"></param>
    /// <param name="mode"></param>
    /// <param name="sqrt"></param>
    /// <returns>The resulting integer-sqrt.</returns>
    public static TNumber FastIntegerSqrt<TNumber>(this TNumber number, UniversalRounding mode, out double sqrt)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      checked
      {
        sqrt = double.Sqrt(double.CreateChecked(TNumber.Abs(number)));

        return TNumber.CopySign(TNumber.CreateChecked(sqrt.Round(mode)), number);
      }
    }

    /// <summary>
    /// <para>Attempts to compute the square-root of a <paramref name="value"/> and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="sqrt"/> (double) and <paramref name="integerSqrt"/> are returned as out parameters.</para>
    /// <para>This is a faster method, but is limited to integer input less-than-or-equal to 53 bits in size.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TSqrt"></typeparam>
    /// <param name="value">The square value to find the square-<paramref name="sqrt"/> of.</param>
    /// <param name="mode"></param>
    /// <param name="integerSqrt"></param>
    /// <param name="sqrt">The resulting square-root of <paramref name="value"/>.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryFastIntegerSqrt<TValue, TSqrt>(this TValue value, UniversalRounding mode, out TSqrt integerSqrt, out double sqrt)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TSqrt : System.Numerics.IBinaryInteger<TSqrt>
    {
      try
      {
        if (value.GetBitLengthEx() <= 53)
        {
          integerSqrt = TSqrt.CreateChecked(value.FastIntegerSqrt(mode, out sqrt));

          return true;
        }
      }
      catch { }

      integerSqrt = TSqrt.Zero;
      sqrt = 0.0;

      return false;
    }
  }
}
