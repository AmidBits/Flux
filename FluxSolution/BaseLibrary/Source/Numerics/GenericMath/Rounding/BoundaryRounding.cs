namespace Flux
{
  /// <summary>Rounds a value to the nearest boundary. The distance computation is a slight optimization for special cases, e.g. when rounding to multiple of. The mode specifies how to round when between two intervals.</summary>
  public class BoundaryRounding<TSelf, TBound>
    : INumberRoundable<TSelf, TBound>
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
    {
      return mode switch // First we 
      {
        RoundingMode.Envelop => boundaryAwayFromZero,
        RoundingMode.Truncate => boundaryTowardsZero,
        RoundingMode.Floor => x < TSelf.Zero ? boundaryAwayFromZero : boundaryTowardsZero,
        RoundingMode.Ceiling => x < TSelf.Zero ? boundaryTowardsZero : boundaryAwayFromZero,
        _ => (distanceTowardsZero < distanceAwayFromZero) ? boundaryTowardsZero // It's a clear win for towardsZero.
          : (distanceAwayFromZero < distanceTowardsZero) ? boundaryAwayFromZero // It's a clear win for awayFromZero.
          : mode switch // Here it's exactly halfway, use appropriate rounding to resolve winner.
          {
            RoundingMode.HalfToEven => TBound.IsEvenInteger(boundaryTowardsZero) ? boundaryTowardsZero : boundaryAwayFromZero,
            RoundingMode.HalfAwayFromZero => boundaryAwayFromZero,
            RoundingMode.HalfTowardZero => boundaryTowardsZero,
            RoundingMode.HalfToNegativeInfinity => x < TSelf.Zero ? boundaryAwayFromZero : boundaryTowardsZero,
            RoundingMode.HalfToPositiveInfinity => x < TSelf.Zero ? boundaryTowardsZero : boundaryAwayFromZero,
            RoundingMode.HalfToOdd => TBound.IsOddInteger(boundaryAwayFromZero) ? boundaryAwayFromZero : boundaryTowardsZero,
            _ => throw new System.ArgumentOutOfRangeException(mode.ToString()),
          }
      };
    }

    /// <summary>Rounds a value to the nearest boundary. Computes the distance to both boundaries and then calls the alternate <see cref="Round(TSelf, TSelf, TSelf, RoundingMode, TSelf, TSelf)"/>.</summary>
    public static TBound Round(TSelf x, RoundingMode mode, TBound boundaryTowardsZero, TBound boundaryAwayFromZero)
    {
      MeasureDistanceToBoundaries(x, boundaryTowardsZero, boundaryAwayFromZero, out TSelf distanceTowardsZero, out TSelf distanceAwayFromZero);

      return Round(x, mode, boundaryTowardsZero, boundaryAwayFromZero, distanceTowardsZero, distanceAwayFromZero);
    }

    #endregion Static methods

    #region Implemented interfaces
    public TBound RoundNumber(TSelf x, RoundingMode mode) => Round(x, mode, m_boundaryTowardsZero, m_boundaryAwayFromZero);
    #endregion Implemented interfaces
  }
}
