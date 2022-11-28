namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Computes the integer log floor and ceiling of <paramref name="x"/> using base <paramref name="b"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static void LocateNearestIntegerLog<TSelf, TRadix, TResult>(this TSelf number, TRadix radix, out TResult nearestTowardsZero, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      if (TSelf.IsNegative(number))
      {
        LocateNearestIntegerLog(TSelf.Abs(number), radix, out nearestTowardsZero, out nearestAwayFromZero);

        nearestTowardsZero = -nearestTowardsZero;
        nearestAwayFromZero = -nearestAwayFromZero;

        return;
      }

      nearestTowardsZero = TResult.Zero;
      nearestAwayFromZero = TResult.Zero;

      AssertNonNegative(number);
      AssertRadix(radix, out TSelf tradix);

      checked
      {
        if (!TSelf.IsZero(number))
        {
          if (!IsPow(number, radix))
            nearestAwayFromZero++;

          while (number >= tradix)
          {
            number /= tradix;

            nearestTowardsZero++;
            nearestAwayFromZero++;
          }
        }
      }
    }

    public static TSelf NearestIntegerLog<TSelf, TRadix, TResult>(this TSelf number, TRadix radix, RoundingMode mode, out TResult nearestTowardsZero, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      LocateNearestIntegerLog(number, radix, out nearestTowardsZero, out nearestAwayFromZero);

      return BoundaryRounding<TSelf>.Round(number, TSelf.CreateChecked(nearestTowardsZero), TSelf.CreateChecked(nearestAwayFromZero), mode);
    }

    /// <summary>Computes the integer log ceiling of x using base b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static TResult NearestIntegerLogAwayFromZero<TSelf, TRadix, TResult>(this TSelf number, TRadix radix, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      AssertNonNegative(number);
      AssertRadix(radix, out TSelf tradix);

      nearestAwayFromZero = TResult.Zero;

      if (!TSelf.IsZero(number))
      {
        if (!IsPow(number, radix))
          nearestAwayFromZero++;

        while (number >= tradix)
        {
          number /= tradix;

          nearestAwayFromZero++;
        }
      }

      return nearestAwayFromZero;
    }

    /// <summary>Computes the integer log floor of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static TResult NearestIntegerLogTowardsZero<TSelf, TRadix, TResult>(this TSelf number, TRadix radix, out TResult nearestTowardsZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      AssertNonNegative(number);
      AssertRadix(radix, out TSelf tradix);

      nearestTowardsZero = TResult.Zero;

      if (!TSelf.IsZero(number))
        while (number >= tradix)
        {
          number /= tradix;

          nearestTowardsZero++;
        }

      return nearestTowardsZero;
    }

    /// <summary>Attempt to compute the integer log floor and ceiling of <paramref name="number"/> using base <paramref name="radix"/> into the out parameters.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logarithm"/>
    public static bool TryNearestIntegerLog<TSelf, TRadix, TResult>(this TSelf number, TRadix radix, out TResult nearestTowardsZero, out TResult nearestAwayFromZero)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TResult : System.Numerics.IBinaryInteger<TResult>
    {
      try
      {
        LocateNearestIntegerLog(number, radix, out nearestTowardsZero, out nearestAwayFromZero);

        return true;
      }
      catch { }

      nearestTowardsZero = TResult.Zero;
      nearestAwayFromZero = TResult.Zero;

      return false;
    }
  }
}
