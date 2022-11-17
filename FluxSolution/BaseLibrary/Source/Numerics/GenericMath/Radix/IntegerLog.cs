namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Computes the integer log floor and ceiling of <paramref name="x"/> using base <paramref name="b"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static void IntegerLog<TSelf, TResult>(this TSelf number, TSelf radix, out TResult logFloor, out TResult logCeiling)
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
          if (!IsIntegerPowOf(number, radix))
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

    /// <summary>Computes the integer log ceiling of x using base b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static TSelf IntegerLogCeiling<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegative(number);
      AssertRadix(radix);

      var logCeiling = TSelf.Zero;

      if (!TSelf.IsZero(number))
      {
        if (!IsIntegerPowOf(number, radix))
          logCeiling++;

        while (number >= radix)
        {
          number /= radix;

          logCeiling++;
        }
      }

      return logCeiling;
    }

    /// <summary>Computes the integer log floor of <paramref name="x"/> using base <paramref name="b"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static TSelf IntegerLogFloor<TSelf>(this TSelf x, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegative(x);
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

    /// <summary>Attempt to compute the integer log floor and ceiling of <paramref name="x"/> using base <paramref name="b"/> into the out parameters.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static bool TryIntegerLog<TSelf, TResult>(this TSelf x, TSelf b, out TResult logFloor, out TResult logCeiling)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      try
      {
        IntegerLog(x, b, out logFloor, out logCeiling);

        return true;
      }
      catch { }

      logFloor = TResult.Zero;
      logCeiling = TResult.Zero;

      return false;
    }
  }
}
