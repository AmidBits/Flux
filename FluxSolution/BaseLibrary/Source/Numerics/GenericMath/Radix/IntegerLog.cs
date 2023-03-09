namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Computes the integer log floor and ceiling of <paramref name="x"/> using base <paramref name="b"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static void LocateIntegerLog<TSelf, TRadix, TResult>(this TSelf value, TRadix radix, out TResult ilogTowardsZero, out TResult ilogAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      if (TSelf.IsNegative(value))
      {
        LocateIntegerLog(TSelf.Abs(value), radix, out ilogTowardsZero, out ilogAwayFromZero);

        ilogTowardsZero = -ilogTowardsZero;
        ilogAwayFromZero = -ilogAwayFromZero;
      }
      else // The value is greater than or equal to zero here.
      {
        var rdx = TSelf.CreateChecked(AssertRadix(radix));

        checked
        {
          ilogTowardsZero = TResult.Zero;
          ilogAwayFromZero = TResult.Zero;

          if (!TSelf.IsZero(value))
          {
            if (!IsIntegerPow(value, radix))
              ilogAwayFromZero++;

            while (value >= rdx)
            {
              value /= rdx;

              ilogTowardsZero++;
              ilogAwayFromZero++;
            }
          }
        }
      }
    }

    /// <summary>Computes the integer log ceiling of x using base b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static TResult LocateIntegerLogAfz<TSelf, TRadix, TResult>(this TSelf value, TRadix radix, out TResult ilogAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      AssertNonNegative(value);
      var rdx = TSelf.CreateChecked(AssertRadix(radix));

      ilogAwayFromZero = TResult.Zero;

      if (!TSelf.IsZero(value))
      {
        if (!IsIntegerPow(value, radix))
          ilogAwayFromZero++;

        while (value >= rdx)
        {
          value /= rdx;

          ilogAwayFromZero++;
        }
      }

      return ilogAwayFromZero;
    }

    /// <summary>Computes the integer log floor of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static TResult LocateIntegerLogTz<TSelf, TRadix, TResult>(this TSelf value, TRadix radix, out TResult ilogTowardsZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      AssertNonNegative(value);
      var rdx = TSelf.CreateChecked(AssertRadix(radix));

      ilogTowardsZero = TResult.Zero;

      if (!TSelf.IsZero(value))
        while (value >= rdx)
        {
          value /= rdx;

          ilogTowardsZero++;
        }

      return ilogTowardsZero;
    }

    //public static TSelf NearestIntegerLog<TSelf, TRadix, TResult>(this TSelf number, TRadix radix, RoundingMode mode, out TResult nearestTowardsZero, out TResult nearestAwayFromZero)
    //  where TSelf : System.Numerics.IBinaryInteger<TSelf>
    //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
    //  where TResult : System.Numerics.IBinaryInteger<TResult>
    //{
    //  LocateILog(number, radix, out nearestTowardsZero, out nearestAwayFromZero);

    //  return BoundaryRounding<TSelf, TSelf>.Round(number, TSelf.CreateChecked(nearestTowardsZero), TSelf.CreateChecked(nearestAwayFromZero), mode);
    //}

    /// <summary>Attempt to compute the integer log floor and ceiling of <paramref name="value"/> using base <paramref name="radix"/> into the out parameters.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static bool TryLocateIntegerLog<TSelf, TRadix, TResult>(this TSelf value, TRadix radix, out TResult ilogTowardsZero, out TResult ilogAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      try
      {
        LocateIntegerLog(value, radix, out ilogTowardsZero, out ilogAwayFromZero);

        return true;
      }
      catch { }

      ilogTowardsZero = TResult.Zero;
      ilogAwayFromZero = TResult.Zero;

      return false;
    }
  }
}
