namespace Flux
{
#if NET7_0_OR_GREATER

  /// <summary>Rounds a value to the nearest boundary. The mode specifies how to round when halfway between two boundaries.</summary>
  public class BoundaryRounding<TSelf, TBound>
    : INumberRoundable<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
    where TBound : System.Numerics.INumber<TBound>
  {
    private readonly TBound m_boundaryTowardsZero;
    private readonly TBound m_boundaryAwayFromZero;

    public BoundaryRounding(TBound boundaryTowardsZero, TBound boundaryAwayFromZero)
    {
      m_boundaryTowardsZero = boundaryTowardsZero;
      m_boundaryAwayFromZero = boundaryAwayFromZero;
    }

    #region Static methods
    public static bool IsWithinDistanceToBoundaries<TDistance>(TDistance distanceTowardsZero, TDistance distanceAwayFromZero, TDistance maxDistanceToBoundaries)
      where TDistance : System.Numerics.INumber<TDistance>
      => maxDistanceToBoundaries <= TDistance.Zero || distanceTowardsZero <= maxDistanceToBoundaries || distanceAwayFromZero <= maxDistanceToBoundaries;

    /// <summary></summary>
    /// <param name="x">The value to consider, in relation to the boundaries.</param>
    /// <param name="boundaryTowardsZero">The boundary closer to zero (positive or negative).</param>
    /// <param name="boundaryAwayFromZero">The boundary farther from zero (positive or negative).</param>
    /// <param name="distanceTowardsZero">Out parameter with the distance between <paramref name="x"/> and <paramref name="boundaryTowardsZero"/>.</param>
    /// <param name="distanceAwayFromZero">Out parameter with the distance between <paramref name="x"/> to <paramref name="boundaryAwayFromZero"/>.</param>
    /// <param name="maxDistanceToBoundaries">The maximum distance considered to be within range of one or both of the boundaries. The indicator is returned by the method.</param>
    /// <returns>Whether <paramref name="distanceTowardsZero"/> or <paramref name="distanceAwayFromZero"/> are within <paramref name="maxDistanceToBoundaries"/>.</returns>
    public static void MeasureDistanceToBoundaries<TResult>(TSelf x, TBound boundaryTowardsZero, TBound boundaryAwayFromZero, out TResult distanceTowardsZero, out TResult distanceAwayFromZero)
      where TResult : System.Numerics.INumber<TResult>
    {
      var origin = TResult.CreateChecked(x);

      distanceTowardsZero = TResult.Abs(origin - TResult.CreateChecked(boundaryTowardsZero)); // Distance from value to the boundary towardsZero.
      distanceAwayFromZero = TResult.Abs(TResult.CreateChecked(boundaryAwayFromZero) - origin); // Distance from value to the boundary awayFromZero;
    }

    /// <summary>Rounds a value to the nearest boundary. The distance computation is a slight optimization for special cases, e.g. when rounding to multiple of. The mode specifies how to round when between two intervals.</summary>
    public static TBound Round(TSelf x, RoundingMode mode, TBound boundaryTowardsZero, TBound boundaryAwayFromZero, TSelf distanceTowardsZero, TSelf distanceAwayFromZero)
      => mode switch
      {
        // First we take care of the direct rounding cases.
        RoundingMode.Envelop => boundaryAwayFromZero,
        RoundingMode.Truncate => boundaryTowardsZero,
        RoundingMode.Floor => TSelf.IsNegative(x) ? boundaryAwayFromZero : boundaryTowardsZero,
        RoundingMode.Ceiling => TSelf.IsNegative(x) ? boundaryTowardsZero : boundaryAwayFromZero,
        // If not applicable, and since we're comparing a value against two boundaries, if the distances from the value to the two boundaries are not equal, we can avoid halfway checks.
        _ => (distanceTowardsZero < distanceAwayFromZero) ? boundaryTowardsZero // A clear win for towards-zero.
          : (distanceTowardsZero > distanceAwayFromZero) ? boundaryAwayFromZero // A clear win for away-from-zero.
          : mode switch // If the distances are equal, i.e. exactly halfway, we use the appropriate rounding strategy to resolve a winner.
          {
            RoundingMode.HalfToEven => TBound.IsEvenInteger(boundaryTowardsZero) ? boundaryTowardsZero : boundaryAwayFromZero,
            RoundingMode.HalfAwayFromZero => boundaryAwayFromZero,
            RoundingMode.HalfTowardZero => boundaryTowardsZero,
            RoundingMode.HalfToNegativeInfinity => TSelf.IsNegative(x) ? boundaryAwayFromZero : boundaryTowardsZero,
            RoundingMode.HalfToPositiveInfinity => TSelf.IsNegative(x) ? boundaryTowardsZero : boundaryAwayFromZero,
            RoundingMode.HalfToOdd => TBound.IsOddInteger(boundaryAwayFromZero) ? boundaryAwayFromZero : boundaryTowardsZero,
            _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
          }
      };

    /// <summary>Rounds a value to the nearest boundary. Computes the distance to both boundaries and then calls the alternate <see cref="Round(TSelf, TSelf, TSelf, RoundingMode, TSelf, TSelf)"/>.</summary>
    public static TBound Round(TSelf x, RoundingMode mode, TBound boundaryTowardsZero, TBound boundaryAwayFromZero)
    {
      MeasureDistanceToBoundaries(x, boundaryTowardsZero, boundaryAwayFromZero, out TSelf distanceTowardsZero, out TSelf distanceAwayFromZero);

      return Round(x, mode, boundaryTowardsZero, boundaryAwayFromZero, distanceTowardsZero, distanceAwayFromZero);
    }

    #endregion Static methods

    #region Implemented interfaces
    public TSelf RoundNumber(TSelf x, RoundingMode mode) => TSelf.CreateChecked(Round(x, mode, m_boundaryTowardsZero, m_boundaryAwayFromZero));
    #endregion Implemented interfaces
  }

#else

  /// <summary>Rounds a value to the nearest boundary. The mode specifies how to round when halfway between two boundaries.</summary>
  public class BoundaryRounding<Bogus1, Bogus2>
    : INumberRoundable
  {
    private readonly double m_boundaryTowardsZero;
    private readonly double m_boundaryAwayFromZero;

    public BoundaryRounding(double boundaryTowardsZero, double boundaryAwayFromZero)
    {
      m_boundaryTowardsZero = boundaryTowardsZero;
      m_boundaryAwayFromZero = boundaryAwayFromZero;
    }

    #region Static methods
    public static bool IsWithinDistanceToBoundaries(double distanceTowardsZero, double distanceAwayFromZero, double maxDistanceToBoundaries)
      => maxDistanceToBoundaries <= 0 || distanceTowardsZero <= maxDistanceToBoundaries || distanceAwayFromZero <= maxDistanceToBoundaries;

    /// <summary></summary>
    /// <param name="x">The value to consider, in relation to the boundaries.</param>
    /// <param name="boundaryTowardsZero">The boundary closer to zero (positive or negative).</param>
    /// <param name="boundaryAwayFromZero">The boundary farther from zero (positive or negative).</param>
    /// <param name="distanceTowardsZero">Out parameter with the distance between <paramref name="x"/> and <paramref name="boundaryTowardsZero"/>.</param>
    /// <param name="distanceAwayFromZero">Out parameter with the distance between <paramref name="x"/> to <paramref name="boundaryAwayFromZero"/>.</param>
    /// <param name="maxDistanceToBoundaries">The maximum distance considered to be within range of one or both of the boundaries. The indicator is returned by the method.</param>
    /// <returns>Whether <paramref name="distanceTowardsZero"/> or <paramref name="distanceAwayFromZero"/> are within <paramref name="maxDistanceToBoundaries"/>.</returns>
    public static void MeasureDistanceToBoundaries(double x, double boundaryTowardsZero, double boundaryAwayFromZero, out double distanceTowardsZero, out double distanceAwayFromZero)
    {
      var origin = x;

      distanceTowardsZero = System.Math.Abs(origin - boundaryTowardsZero); // Distance from value to the boundary towardsZero.
      distanceAwayFromZero = System.Math.Abs(boundaryAwayFromZero - origin); // Distance from value to the boundary awayFromZero;
    }

    /// <summary>Rounds a value to the nearest boundary. The distance computation is a slight optimization for special cases, e.g. when rounding to multiple of. The mode specifies how to round when between two intervals.</summary>
    public static double Round(double x, RoundingMode mode, double boundaryTowardsZero, double boundaryAwayFromZero, double distanceTowardsZero, double distanceAwayFromZero)
      => mode switch
      {
        // First we take care of the direct rounding cases.
        RoundingMode.Envelop => boundaryAwayFromZero,
        RoundingMode.Truncate => boundaryTowardsZero,
        RoundingMode.Floor => x < 0 ? boundaryAwayFromZero : boundaryTowardsZero,
        RoundingMode.Ceiling => x < 0 ? boundaryTowardsZero : boundaryAwayFromZero,
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
    public static double Round(double x, RoundingMode mode, double boundaryTowardsZero, double boundaryAwayFromZero)
    {
      MeasureDistanceToBoundaries(x, boundaryTowardsZero, boundaryAwayFromZero, out double distanceTowardsZero, out double distanceAwayFromZero);

      return Round(x, mode, boundaryTowardsZero, boundaryAwayFromZero, distanceTowardsZero, distanceAwayFromZero);
    }

    #endregion Static methods

    #region Implemented interfaces
    public double RoundNumber(double x, RoundingMode mode) => Round(x, mode, m_boundaryTowardsZero, m_boundaryAwayFromZero);
    #endregion Implemented interfaces
  }

#endif
}
