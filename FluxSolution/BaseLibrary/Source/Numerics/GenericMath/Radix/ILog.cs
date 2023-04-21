namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Computes the integer log floor and ceiling of <paramref name="x"/> using base <paramref name="b"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static void LocateILog<TSelf>(this TSelf value, TSelf radix, out TSelf ilogTowardsZero, out TSelf ilogAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsNegative(value))
      {
        LocateILog(TSelf.Abs(value), radix, out ilogTowardsZero, out ilogAwayFromZero);

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
            if (!IsPowOf(value, radix))
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
    public static TSelf LocateILogAfz<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegative(value);
      AssertRadix(radix);

      var ilogAwayFromZero = TSelf.Zero;

      if (!TSelf.IsZero(value))
      {
        if (!IsPowOf(value, radix))
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
    public static TSelf LocateILogTz<TSelf>(this TSelf value, TSelf radix)
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
    public static bool TryLocateILog<TSelf>(this TSelf value, TSelf radix, out TSelf ilogTowardsZero, out TSelf ilogAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      try
      {
        LocateILog(value, radix, out ilogTowardsZero, out ilogAwayFromZero);

        return true;
      }
      catch { }

      ilogTowardsZero = TSelf.Zero;
      ilogAwayFromZero = TSelf.Zero;

      return false;
    }

#else


    /// <summary>Computes the integer log floor and ceiling of <paramref name="x"/> using base <paramref name="b"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static void LocateILog(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix, out System.Numerics.BigInteger ilogTowardsZero, out System.Numerics.BigInteger ilogAwayFromZero)
    {
      if (value < 0)
      {
        LocateILog(System.Numerics.BigInteger.Abs(value), radix, out ilogTowardsZero, out ilogAwayFromZero);

        ilogTowardsZero = -ilogTowardsZero;
        ilogAwayFromZero = -ilogAwayFromZero;
      }
      else // The value is greater than or equal to zero here.
      {
        AssertRadix(radix);

        checked
        {
          ilogTowardsZero = System.Numerics.BigInteger.Zero;
          ilogAwayFromZero = System.Numerics.BigInteger.Zero;

          if (!value.IsZero)
          {
            if (!IsIPowOf(value, radix))
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
    public static System.Numerics.BigInteger LocateILogAfz(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix)
    {
      AssertNonNegative(value);
      AssertRadix(radix);

      var ilogAwayFromZero = System.Numerics.BigInteger.Zero;

      if (!value.IsZero)
      {
        if (!IsIPowOf(value, radix))
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
    public static System.Numerics.BigInteger LocateILogTz(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix)
    {
      AssertNonNegative(value);
      AssertRadix(radix);

      var ilogTowardsZero = System.Numerics.BigInteger.Zero;

      if (!value.IsZero)
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
    public static bool TryLocateILog(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix, out System.Numerics.BigInteger ilogTowardsZero, out System.Numerics.BigInteger ilogAwayFromZero)
    {
      try
      {
        LocateILog(value, radix, out ilogTowardsZero, out ilogAwayFromZero);

        return true;
      }
      catch { }

      ilogTowardsZero = System.Numerics.BigInteger.Zero;
      ilogAwayFromZero = System.Numerics.BigInteger.Zero;

      return false;
    }

#endif
  }
}
