namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Rounds a value to the nearest boundary. The mode specifies how to round when halfway between two boundaries.</summary>
    public static TBound RoundToBoundary<TValue, TBound, TDistance>(this TValue value, RoundingMode mode, TBound boundaryTowardsZero, TBound boundaryAwayFromZero, TDistance distanceTowardsZero, TDistance distanceAwayFromZero)
      where TValue : System.Numerics.INumber<TValue>
      where TBound : System.Numerics.INumber<TBound>
      where TDistance : System.Numerics.INumber<TDistance>
      => mode switch
      {
        // First we take care of the direct rounding cases.
        RoundingMode.AllAwayFromZero => boundaryAwayFromZero,
        RoundingMode.AllTowardZero => boundaryTowardsZero,
        RoundingMode.AllToNegativeInfinity => TValue.IsNegative(value) ? boundaryAwayFromZero : boundaryTowardsZero,
        RoundingMode.AllToPositiveInfinity => TValue.IsNegative(value) ? boundaryTowardsZero : boundaryAwayFromZero,
        // If not applicable, and since we're comparing a value against two boundaries, if the distances from the value to the two boundaries are not equal, we can avoid halfway checks.
        _ => (distanceTowardsZero < distanceAwayFromZero) ? boundaryTowardsZero // A clear win for towards-zero.
          : (distanceTowardsZero > distanceAwayFromZero) ? boundaryAwayFromZero // A clear win for away-from-zero.
          : mode switch // If the distances are equal, i.e. exactly halfway, we use the appropriate rounding strategy to resolve a winner.
          {
            RoundingMode.HalfToEven => TBound.IsEvenInteger(boundaryTowardsZero) ? boundaryTowardsZero : boundaryAwayFromZero,
            RoundingMode.HalfAwayFromZero => boundaryAwayFromZero,
            RoundingMode.HalfTowardZero => boundaryTowardsZero,
            RoundingMode.HalfToNegativeInfinity => TValue.IsNegative(value) ? boundaryAwayFromZero : boundaryTowardsZero,
            RoundingMode.HalfToPositiveInfinity => TValue.IsNegative(value) ? boundaryTowardsZero : boundaryAwayFromZero,
            RoundingMode.HalfToOdd => TBound.IsOddInteger(boundaryAwayFromZero) ? boundaryAwayFromZero : boundaryTowardsZero,
            _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
          }
      };

    /// <summary>Rounds a value to the nearest boundary. Computes the distance to both boundaries and then calls the alternate <see cref="Round(TSelf, TSelf, TSelf, RoundingMode, TSelf, TSelf)"/>.</summary>
    public static TBound RoundToBoundary<TValue, TBound>(this TValue value, RoundingMode mode, TBound boundaryTowardsZero, TBound boundaryAwayFromZero)
      where TValue : System.Numerics.INumber<TValue>
      where TBound : System.Numerics.INumber<TBound>
    {
      RoundToBoundaryDistances(value, boundaryTowardsZero, boundaryAwayFromZero, out TValue distanceTowardsZero, out TValue distanceAwayFromZero);

      return RoundToBoundary(value, mode, boundaryTowardsZero, boundaryAwayFromZero, distanceTowardsZero, distanceAwayFromZero);
    }

    /// <summary></summary>
    /// <param name="value">The value to consider, in relation to the boundaries.</param>
    /// <param name="boundaryTowardsZero">The boundary closer to zero (positive or negative).</param>
    /// <param name="boundaryAwayFromZero">The boundary farther from zero (positive or negative).</param>
    /// <param name="distanceTowardsZero">Out parameter with the distance between <paramref name="value"/> and <paramref name="boundaryTowardsZero"/>.</param>
    /// <param name="distanceAwayFromZero">Out parameter with the distance between <paramref name="value"/> to <paramref name="boundaryAwayFromZero"/>.</param>
    /// <param name="maxDistanceToBoundaries">The maximum distance considered to be within range of one or both of the boundaries. The indicator is returned by the method.</param>
    /// <returns>Whether <paramref name="distanceTowardsZero"/> or <paramref name="distanceAwayFromZero"/> are within <paramref name="maxDistanceToBoundaries"/>.</returns>
    public static void RoundToBoundaryDistances<TValue, TBound, TDistance>(this TValue value, TBound boundaryTowardsZero, TBound boundaryAwayFromZero, out TDistance distanceTowardsZero, out TDistance distanceAwayFromZero)
      where TValue : System.Numerics.INumber<TValue>
      where TBound : System.Numerics.INumber<TBound>
      where TDistance : System.Numerics.INumber<TDistance>
    {
      var origin = TDistance.CreateChecked(value);

      distanceTowardsZero = TDistance.Abs(origin - TDistance.CreateChecked(boundaryTowardsZero)); // Distance from value to the boundary towardsZero.
      distanceAwayFromZero = TDistance.Abs(TDistance.CreateChecked(boundaryAwayFromZero) - origin); // Distance from value to the boundary awayFromZero;
    }

#else

    /// <summary></summary>
    /// <param name="x">The value to consider, in relation to the boundaries.</param>
    /// <param name="boundaryTowardsZero">The boundary closer to zero (positive or negative).</param>
    /// <param name="boundaryAwayFromZero">The boundary farther from zero (positive or negative).</param>
    /// <param name="distanceTowardsZero">Out parameter with the distance between <paramref name="x"/> and <paramref name="boundaryTowardsZero"/>.</param>
    /// <param name="distanceAwayFromZero">Out parameter with the distance between <paramref name="x"/> to <paramref name="boundaryAwayFromZero"/>.</param>
    /// <param name="maxDistanceToBoundaries">The maximum distance considered to be within range of one or both of the boundaries. The indicator is returned by the method.</param>
    /// <returns>Whether <paramref name="distanceTowardsZero"/> or <paramref name="distanceAwayFromZero"/> are within <paramref name="maxDistanceToBoundaries"/>.</returns>
    public static void RoundToBoundaryDistances(this double x, double boundaryTowardsZero, double boundaryAwayFromZero, out double distanceTowardsZero, out double distanceAwayFromZero)
    {
      var origin = x;

      distanceTowardsZero = System.Math.Abs(origin - boundaryTowardsZero); // Distance from value to the boundary towardsZero.
      distanceAwayFromZero = System.Math.Abs(boundaryAwayFromZero - origin); // Distance from value to the boundary awayFromZero;
    }

    /// <summary>Rounds a value to the nearest boundary. The distance computation is a slight optimization for special cases, e.g. when rounding to multiple of. The mode specifies how to round when between two intervals.</summary>
    public static double RoundToBoundary(this double x, RoundingMode mode, double boundaryTowardsZero, double boundaryAwayFromZero, double distanceTowardsZero, double distanceAwayFromZero)
      => mode switch
      {
        // First we take care of the direct rounding cases.
        RoundingMode.AllAwayFromZero => boundaryAwayFromZero,
        RoundingMode.AllTowardZero => boundaryTowardsZero,
        RoundingMode.AllToNegativeInfinity => x < 0 ? boundaryAwayFromZero : boundaryTowardsZero,
        RoundingMode.AllToPositiveInfinity => x < 0 ? boundaryTowardsZero : boundaryAwayFromZero,
        // If not applicable, and since we're comparing a value against two boundaries, if the distances from the value to the two boundaries are not equal, we can avoid halfway checks.
        _ => (distanceTowardsZero < distanceAwayFromZero) ? boundaryTowardsZero // A clear win for towards-zero.
          : (distanceTowardsZero > distanceAwayFromZero) ? boundaryAwayFromZero // A clear win for away-from-zero.
          : mode switch // If the distances are equal, i.e. exactly halfway, we use the appropriate rounding strategy to resolve a winner.
          {
            RoundingMode.HalfToEven => (System.Convert.ToInt64(boundaryTowardsZero) & 1) == 0 ? boundaryTowardsZero : boundaryAwayFromZero,
            RoundingMode.HalfAwayFromZero => boundaryAwayFromZero,
            RoundingMode.HalfTowardZero => boundaryTowardsZero,
            RoundingMode.HalfToNegativeInfinity => x < 0 ? boundaryAwayFromZero : boundaryTowardsZero,
            RoundingMode.HalfToPositiveInfinity => x < 0 ? boundaryTowardsZero : boundaryAwayFromZero,
            RoundingMode.HalfToOdd => (System.Convert.ToInt64(boundaryAwayFromZero) & 1) == 1 ? boundaryAwayFromZero : boundaryTowardsZero,
            _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
          }
      };

    /// <summary>Rounds a value to the nearest boundary. Computes the distance to both boundaries and then calls the alternate <see cref="Round(TSelf, TSelf, TSelf, RoundingMode, TSelf, TSelf)"/>.</summary>
    public static double RoundToBoundary(this double x, RoundingMode mode, double boundaryTowardsZero, double boundaryAwayFromZero)
    {
      RoundToBoundaryDistances(x, boundaryTowardsZero, boundaryAwayFromZero, out double distanceTowardsZero, out double distanceAwayFromZero);

      return RoundToBoundary(x, mode, boundaryTowardsZero, boundaryAwayFromZero, distanceTowardsZero, distanceAwayFromZero);
    }

#endif
  }
}
