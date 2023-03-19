namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Computes the integer log floor and ceiling of <paramref name="x"/> using base <paramref name="b"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static void LocateIntegerLog<TSelf>(this TSelf value, TSelf radix, out TSelf ilogTowardsZero, out TSelf ilogAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsNegative(value))
      {
        LocateIntegerLog(TSelf.Abs(value), radix, out ilogTowardsZero, out ilogAwayFromZero);

        ilogTowardsZero = -ilogTowardsZero;
        ilogAwayFromZero = -ilogAwayFromZero;
      }
      else // The value is greater than or equal to zero here.
      {
        AssertRadix(radix);

        checked
        {
          ilogTowardsZero = TSelf.Zero;
          ilogAwayFromZero = TSelf.Zero;

          if (!TSelf.IsZero(value))
          {
            if (!IsIntegerPow(value, radix))
              ilogAwayFromZero++;

            while (value >= radix)
            {
              value /= radix;

              ilogTowardsZero++;
              ilogAwayFromZero++;
            }
          }
        }
      }
    }

    /// <summary>Computes the integer log ceiling of x using base b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static TSelf LocateIntegerLogAfz<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegative(value);
      AssertRadix(radix);

      var ilogAwayFromZero = TSelf.Zero;

      if (!TSelf.IsZero(value))
      {
        if (!IsIntegerPow(value, radix))
          ilogAwayFromZero++;

        while (value >= radix)
        {
          value /= radix;

          ilogAwayFromZero++;
        }
      }

      return ilogAwayFromZero;
    }

    /// <summary>Computes the integer log floor of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static TSelf LocateIntegerLogTz<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegative(value);
      AssertRadix(radix);

      var ilogTowardsZero = TSelf.Zero;

      if (!TSelf.IsZero(value))
        while (value >= radix)
        {
          value /= radix;

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
    public static bool TryLocateIntegerLog<TSelf>(this TSelf value, TSelf radix, out TSelf ilogTowardsZero, out TSelf ilogAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      try
      {
        LocateIntegerLog(value, radix, out ilogTowardsZero, out ilogAwayFromZero);

        return true;
      }
      catch { }

      ilogTowardsZero = TSelf.Zero;
      ilogAwayFromZero = TSelf.Zero;

      return false;
    }
  }
}
