#if NET7_0_OR_GREATER
namespace Flux
{
  public class BoundaryRounding<TSelf>
    : INumberRoundable<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    private readonly RoundingMode m_mode;
    private readonly TSelf m_boundaryTowardsZero;
    private readonly TSelf m_boundaryAwayFromZero;

    public BoundaryRounding(RoundingMode mode, TSelf boundaryTowardsZero, TSelf boundaryAwayFromZero)
    {
      m_mode = mode;
      m_boundaryTowardsZero = boundaryTowardsZero;
      m_boundaryAwayFromZero = boundaryAwayFromZero;
    }

    /// <summary>PREVIEW! Rounds a value to the nearest boundary. The distance computation is a slight optimization for special cases, e.g. when rounding to multiple of. The mode specifies how to round when between two intervals.</summary>
    public static TSelf Round(TSelf x, TSelf boundaryTowardsZero, TSelf boundaryAwayFromZero, RoundingMode mode, TSelf distanceTowardsZero, TSelf distanceAwayFromZero)
    {
      if (distanceTowardsZero < distanceAwayFromZero) // It's a clear win for towardsZero.
        return boundaryTowardsZero;
      if (distanceAwayFromZero < distanceTowardsZero) // It's a clear win for awayFromZero.
        return boundaryAwayFromZero;

      return mode switch // It's exactly halfway, use appropriate rounding to resolve winner.
      {
        RoundingMode.HalfwayToEven => TSelf.IsEvenInteger(boundaryTowardsZero) ? boundaryTowardsZero : boundaryAwayFromZero,
        RoundingMode.HalfwayAwayFromZero => boundaryAwayFromZero,
        RoundingMode.HalfwayTowardZero => boundaryTowardsZero,
        RoundingMode.HalfwayToNegativeInfinity => x < TSelf.Zero ? boundaryAwayFromZero : boundaryTowardsZero,
        RoundingMode.HalfwayToPositiveInfinity => x < TSelf.Zero ? boundaryTowardsZero : boundaryAwayFromZero,
        RoundingMode.HalfwayToOdd => TSelf.IsOddInteger(boundaryAwayFromZero) ? boundaryAwayFromZero : boundaryTowardsZero,
        RoundingMode.Envelop => boundaryAwayFromZero,
        RoundingMode.Truncate => boundaryTowardsZero,
        RoundingMode.Floor => x < TSelf.Zero ? boundaryAwayFromZero : boundaryTowardsZero,
        RoundingMode.Ceiling => x < TSelf.Zero ? boundaryTowardsZero : boundaryAwayFromZero,
        _ => throw new System.ArgumentOutOfRangeException(mode.ToString()),
      };
    }

    /// <summary>PREVIEW! Rounds a value to the nearest boundary. Computes the distance to both boundaries and then calls the alternate <see cref="Round(TSelf, TSelf, TSelf, RoundingMode, TSelf, TSelf)"/>.</summary>
    public static TSelf Round(TSelf x, TSelf boundaryTowardsZero, TSelf boundaryAwayFromZero, RoundingMode mode)
    {
      var distanceTowardsZero = TSelf.Abs(x - TSelf.CreateChecked(boundaryTowardsZero)); // Distance from value to the boundary towardsZero.
      var distanceAwayFromZero = TSelf.Abs(TSelf.CreateChecked(boundaryAwayFromZero) - x); // Distance from value to the boundary awayFromZero;

      return Round(x, boundaryTowardsZero, boundaryAwayFromZero, mode, distanceTowardsZero, distanceAwayFromZero);
    }

#region Implemented interfaces
    /// <summary>PREVIEW! Rounds a value to the nearest boundary. The mode specifies how to round when between two intervals.</summary>
    public TSelf RoundNumber(TSelf x)
      => Round(x, m_boundaryTowardsZero, m_boundaryAwayFromZero, m_mode);
#endregion Implemented interfaces
  }
}
#endif
