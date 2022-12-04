namespace Flux
{
  /// <summary>Rounds a value to the nearest boundary. The distance computation is a slight optimization for special cases, e.g. when rounding to multiple of. The mode specifies how to round when between two intervals.</summary>
  public class BoundaryRounding<TSelf, TBound>
    : INumberRoundable<TSelf, TBound>
    where TSelf : System.Numerics.INumber<TSelf>
    where TBound : System.Numerics.INumber<TBound>
  {
    private readonly RoundingMode m_mode;
    private readonly TBound m_boundaryTowardsZero;
    private readonly TBound m_boundaryAwayFromZero;

    public BoundaryRounding(RoundingMode mode, TBound boundaryTowardsZero, TBound boundaryAwayFromZero)
    {
      m_mode = mode;
      m_boundaryTowardsZero = boundaryTowardsZero;
      m_boundaryAwayFromZero = boundaryAwayFromZero;
    }

    #region Static methods
    /// <summary>Rounds a value to the nearest boundary. The distance computation is a slight optimization for special cases, e.g. when rounding to multiple of. The mode specifies how to round when between two intervals.</summary>
    public static TBound Round(TSelf x, TBound boundaryTowardsZero, TBound boundaryAwayFromZero, RoundingMode mode, TSelf distanceTowardsZero, TSelf distanceAwayFromZero)
    {
      return mode switch
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
    public static TBound Round(TSelf x, TBound boundaryTowardsZero, TBound boundaryAwayFromZero, RoundingMode mode)
    {
      var distanceTowardsZero = TSelf.Abs(x - TSelf.CreateChecked(boundaryTowardsZero)); // Distance from value to the boundary towardsZero.
      var distanceAwayFromZero = TSelf.Abs(TSelf.CreateChecked(boundaryAwayFromZero) - x); // Distance from value to the boundary awayFromZero;

      return Round(x, boundaryTowardsZero, boundaryAwayFromZero, mode, distanceTowardsZero, distanceAwayFromZero);
    }
    #endregion Static methods

    #region Implemented interfaces
    /// <summary>Rounds a value to the nearest boundary. The mode specifies how to round when between two intervals.</summary>
    public TBound RoundNumber(TSelf x)
      => Round(x, m_boundaryTowardsZero, m_boundaryAwayFromZero, m_mode);
    #endregion Implemented interfaces
  }
}
