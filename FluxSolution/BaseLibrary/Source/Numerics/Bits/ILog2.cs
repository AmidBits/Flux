namespace Flux
{
  public static partial class Bits
  {
#if NET7_0_OR_GREATER

    /// <summary>Get the two integer log-2 nearest to value.</summary>
    /// <param name="value">The value for which the nearest integer log-2 will be found.</param>
    /// <param name="ilog2TowardsZero">Outputs the integer log-2 that is closer to zero.</param>
    /// <param name="ilog2AwayFromZero">Outputs the integer log-2 that is farther from zero.</param>
    /// <returns>The nearest two integer log-2 to value as out parameters.</returns>
    public static void LocateILog2<TSelf, TResult>(this TSelf value, out TResult ilog2TowardsZero, out TResult ilog2AwayFromZero)
     where TSelf : System.Numerics.INumber<TSelf>
     where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      if (TSelf.IsZero(value))
      {
        ilog2TowardsZero = TResult.Zero;
        ilog2AwayFromZero = TResult.Zero;
      }
      else if (TSelf.IsNegative(value))
      {
        LocateILog2(TSelf.Abs(value), out ilog2TowardsZero, out ilog2AwayFromZero);

        ilog2TowardsZero = -ilog2TowardsZero;
        ilog2AwayFromZero = -ilog2AwayFromZero;
      }
      else // The number is greater than zero here.
      {
        var quotient = TResult.CreateChecked(value.TruncMod(TSelf.One, out var remainder));

        ilog2TowardsZero = TResult.Log2(quotient);

        if (!TSelf.IsZero(remainder)) quotient++; // Any fraction could make log2 ceiling potentially one snap up.

        ilog2AwayFromZero = TResult.IsPow2(quotient) ? TResult.Log2(quotient) : TResult.Log2(quotient) + TResult.One;
      }
    }

    //public static TResult NearerILog2<TSelf, TResult>(this TSelf value, RoundingMode mode, out TResult ilog2TowardsZero, out TResult ilog2AwayFromZero)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //  where TResult : System.Numerics.IBinaryInteger<TResult>
    //{
    //  LocateILog2(value, out ilog2TowardsZero, out ilog2AwayFromZero);
    //}

    /// <summary>Computes the integer base 2 log away from zero (ceiling) of <paramref name="value"/>.</summary>
    public static TResult ILog2AwayFromZero<TSelf, TResult>(this TSelf value, out TResult ilog2AwayFromZero)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      LocateILog2(value, out _, out ilog2AwayFromZero);

      return ilog2AwayFromZero;
    }

    /// <summary>Computes the integer base 2 log towards zero (floor) of <paramref name="value"/>.</summary>
    public static TResult ILog2TowardsZero<TSelf, TResult>(this TSelf value, out TResult ilog2TowardsZero)
      where TSelf : System.Numerics.INumber<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      LocateILog2(value, out ilog2TowardsZero, out _);

      return ilog2TowardsZero;
    }

    /// <summary>Get the two integer log-2 nearest to value.</summary>
    /// <param name="value">The value for which the nearest integer log-2 will be found.</param>
    /// <param name="ilog2TowardsZero">Outputs the integer log-2 that is closer to zero.</param>
    /// <param name="ilog2AwayFromZero">Outputs the integer log-2 that is farther from zero.</param>
    /// <returns>The nearest two integer log-2 to value as out parameters.</returns>
    public static bool TryLocateILog<TSelf, TResult>(this TSelf value, out TResult ilog2TowardsZero, out TResult ilog2AwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      try
      {
        LocateILog2(value, out ilog2TowardsZero, out ilog2AwayFromZero);

        return true;
      }
      catch { }

      ilog2TowardsZero = TResult.Zero;
      ilog2AwayFromZero = TResult.Zero;

      return false;
    }

#endif
  }
}
