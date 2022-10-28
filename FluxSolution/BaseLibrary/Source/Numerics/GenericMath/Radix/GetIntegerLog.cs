#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Computes the integer log floor and ceiling of <paramref name="x"/> using base <paramref name="b"/> as an integer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static void GetIntegerLog<TSelf, TResult>(this TSelf number, TSelf radix, out TResult logFloor, out TResult logCeiling)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      logFloor = TResult.Zero;
      logCeiling = TResult.Zero;

      AssertNonNegative(number);
      AssertRadix(radix);

      checked
      {
        if (!TSelf.IsZero(number))
        {
          if (!IsIntegerPow(number, radix))
            logCeiling++;

          while (number >= radix)
          {
            number /= radix;

            logFloor++;
            logCeiling++;
          }
        }
      }
    }

    /// <summary>PREVIEW! Computes the integer log ceiling of <paramref name="x"/> using base <paramref name="b"/> as an integer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static int GetIntegerLogCeiling<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => int.CreateChecked(IntegerLogCeiling(number, radix));

    /// <summary>PREVIEW! Computes the integer log floor of <paramref name="x"/> using base <paramref name="b"/> as an integer.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static int GetIntegerLogFloor<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => int.CreateChecked(IntegerLogFloor(number, radix));

    /// <summary>PREVIEW! Attempt to compute the integer log floor and ceiling of <paramref name="x"/> using base <paramref name="b"/> into the out parameters.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static bool TryGetIntegerLog<TSelf, TResult>(this TSelf x, TSelf b, out TResult logFloor, out TResult logCeiling)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      try
      {
        GetIntegerLog(x, b, out logFloor, out logCeiling);

        return true;
      }
      catch { }

      logFloor = TResult.Zero;
      logCeiling = TResult.Zero;

      return false;
    }
  }
}
#endif
