#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    public static void IntegerLog<TSelf>(this TSelf x, TSelf b, out TSelf logFloor, out TSelf logCeiling)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      logFloor = TSelf.Zero;
      logCeiling = TSelf.Zero;

      AssertNonNegativeValue(x);
      AssertRadix(b);

      if (!TSelf.IsZero(x))
      {
        if (!IsIntegerPow(x, b))
          logCeiling++;

        while (x >= b)
        {
          x /= b;

          logFloor++;
          logCeiling++;
        }
      }
    }

    /// <summary>PREVIEW! Computes the integer log ceiling of x using base b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static TSelf IntegerLogCeiling<TSelf>(this TSelf x, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegativeValue(x);
      AssertRadix(b);

      var logCeiling = TSelf.Zero;

      if (!TSelf.IsZero(x))
      {
        if (!x.IsIntegerPow(b))
          logCeiling++;

        while (x >= b)
        {
          x /= b;

          logCeiling++;
        }
      }

      return logCeiling;
    }

    /// <summary>PREVIEW! Computes the integer log floor of <paramref name="x"/> using base <paramref name="b"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static TSelf IntegerLogFloor<TSelf>(this TSelf x, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegativeValue(x);
      AssertRadix(b);

      var logFloor = TSelf.Zero;

      if (!TSelf.IsZero(x))
        while (x >= b)
        {
          x /= b;

          logFloor++;
        }

      return logFloor;
    }

    /// <summary>PREVIEW! Attempt to compute the integer log floor and ceiling of <paramref name="x"/> using base <paramref name="b"/> into the out parameters.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static bool TryIntegerLog<TSelf>(this TSelf x, TSelf b, out TSelf logFloor, out TSelf logCeiling)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      try
      {
        IntegerLog(x, b, out logFloor, out logCeiling);

        return true;
      }
      catch { }

      logFloor = TSelf.Zero;
      logCeiling = TSelf.Zero;

      return false;
    }
  }
}
#endif
