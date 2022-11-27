namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Computes the integer log floor and ceiling of <paramref name="x"/> using base <paramref name="b"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static void IntegerLog<TSelf, TRadix, TResult>(this TSelf number, TRadix radix, out TResult logFloor, out TResult logCeiling)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      logFloor = TResult.Zero;
      logCeiling = TResult.Zero;

      AssertNonNegative(number);
      AssertRadix(radix, out TSelf tradix);

      checked
      {
        if (!TSelf.IsZero(number))
        {
          if (!IsIntegerPowOf(number, radix))
            logCeiling++;

          while (number >= tradix)
          {
            number /= tradix;

            logFloor++;
            logCeiling++;
          }
        }
      }
    }

    /// <summary>Computes the integer log ceiling of x using base b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static TSelf IntegerLogCeiling<TSelf, TRadix>(this TSelf number, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      AssertNonNegative(number);
      AssertRadix(radix, out TSelf tradix);

      var logCeiling = TSelf.Zero;

      if (!TSelf.IsZero(number))
      {
        if (!IsIntegerPowOf(number, radix))
          logCeiling++;

        while (number >= tradix)
        {
          number /= tradix;

          logCeiling++;
        }
      }

      return logCeiling;
    }

    /// <summary>Computes the integer log floor of <paramref name="x"/> using base <paramref name="radix"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static TSelf IntegerLogFloor<TSelf, TRadix>(this TSelf x, TRadix radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      AssertNonNegative(x);
      AssertRadix(radix, out TSelf tradix);

      var logFloor = TSelf.Zero;

      if (!TSelf.IsZero(x))
        while (x >= tradix)
        {
          x /= tradix;

          logFloor++;
        }

      return logFloor;
    }

    /// <summary>Attempt to compute the integer log floor and ceiling of <paramref name="x"/> using base <paramref name="radix"/> into the out parameters.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static bool TryIntegerLog<TSelf, TRadix, TResult>(this TSelf x, TRadix radix, out TResult logFloor, out TResult logCeiling)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      try
      {
        IntegerLog(x, radix, out logFloor, out logCeiling);

        return true;
      }
      catch { }

      logFloor = TResult.Zero;
      logCeiling = TResult.Zero;

      return false;
    }
  }
}
