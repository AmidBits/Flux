namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Computes the integer log floor and ceiling of <paramref name="x"/> using base <paramref name="b"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static void LocateILog<TSelf, TRadix, TResult>(this TSelf value, TRadix radix, out TResult ilogTowardsZero, out TResult ilogAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      if (TSelf.IsNegative(value))
      {
        LocateILog(TSelf.Abs(value), radix, out ilogTowardsZero, out ilogAwayFromZero);

        ilogTowardsZero = -ilogTowardsZero;
        ilogAwayFromZero = -ilogAwayFromZero;
      }
      else // The value is greater than or equal to zero here.
      {
        var tradix = TSelf.CreateChecked(AssertRadix(radix));

        checked
        {
          ilogTowardsZero = TResult.Zero;
          ilogAwayFromZero = TResult.Zero;

          if (!TSelf.IsZero(value))
          {
            if (!IsPow(value, radix))
              ilogAwayFromZero++;

            while (value >= tradix)
            {
              value /= tradix;

              ilogTowardsZero++;
              ilogAwayFromZero++;
            }
          }
        }
      }
    }

    //public static TSelf NearestIntegerLog<TSelf, TRadix, TResult>(this TSelf number, TRadix radix, RoundingMode mode, out TResult nearestTowardsZero, out TResult nearestAwayFromZero)
    //  where TSelf : System.Numerics.IBinaryInteger<TSelf>
    //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
    //  where TResult : System.Numerics.IBinaryInteger<TResult>
    //{
    //  LocateILog(number, radix, out nearestTowardsZero, out nearestAwayFromZero);

    //  return BoundaryRounding<TSelf, TSelf>.Round(number, TSelf.CreateChecked(nearestTowardsZero), TSelf.CreateChecked(nearestAwayFromZero), mode);
    //}

    /// <summary>Computes the integer log ceiling of x using base b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static TResult ILogAwayFromZero<TSelf, TRadix, TResult>(this TSelf value, TRadix radix, out TResult ilogAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      AssertNonNegative(value);
      var tradix = TSelf.CreateChecked(AssertRadix(radix));

      ilogAwayFromZero = TResult.Zero;

      if (!TSelf.IsZero(value))
      {
        if (!IsPow(value, radix))
          ilogAwayFromZero++;

        while (value >= tradix)
        {
          value /= tradix;

          ilogAwayFromZero++;
        }
      }

      return ilogAwayFromZero;
    }

    /// <summary>Computes the integer log floor of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static TResult ILogTowardsZero<TSelf, TRadix, TResult>(this TSelf value, TRadix radix, out TResult ilogTowardsZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      AssertNonNegative(value);
      var tradix = TSelf.CreateChecked(AssertRadix(radix));

      ilogTowardsZero = TResult.Zero;

      if (!TSelf.IsZero(value))
        while (value >= tradix)
        {
          value /= tradix;

          ilogTowardsZero++;
        }

      return ilogTowardsZero;
    }

    /// <summary>Attempt to compute the integer log floor and ceiling of <paramref name="value"/> using base <paramref name="radix"/> into the out parameters.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static bool TryLocateILog<TSelf, TRadix, TResult>(this TSelf value, TRadix radix, out TResult ilogTowardsZero, out TResult ilogAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      try
      {
        LocateILog(value, radix, out ilogTowardsZero, out ilogAwayFromZero);

        return true;
      }
      catch { }

      ilogTowardsZero = TResult.Zero;
      ilogAwayFromZero = TResult.Zero;

      return false;
    }
  }
}
