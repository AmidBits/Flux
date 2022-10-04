#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Rounds a value to the nearest boundary. The distance computation is a slight optimization for special cases, e.g. when rounding to multiple of. The mode specifies how to round when between two intervals.</summary>
    public static TBound RoundToNearest<TSelf, TBound>(this TSelf x, TBound boundaryTowardsZero, TBound boundaryAwayFromZero, TSelf distanceTowardsZero, TSelf distanceAwayFromZero, HalfwayRounding mode)
      where TSelf : System.Numerics.INumber<TSelf>
      where TBound : System.Numerics.INumber<TBound>
    {
      if (distanceTowardsZero < distanceAwayFromZero) // It's a clear win for towardsZero.
        return boundaryTowardsZero;
      if (distanceAwayFromZero < distanceTowardsZero) // It's a clear win for awayFromZero.
        return boundaryAwayFromZero;

      return mode switch // It's exactly halfway, use appropriate rounding to resolve winner.
      {
        HalfwayRounding.ToEven => TBound.IsEvenInteger(boundaryTowardsZero) ? boundaryTowardsZero : boundaryAwayFromZero,
        HalfwayRounding.AwayFromZero => boundaryAwayFromZero,
        HalfwayRounding.TowardZero => boundaryTowardsZero,
        HalfwayRounding.ToNegativeInfinity => x < TSelf.Zero ? boundaryAwayFromZero : boundaryTowardsZero,
        HalfwayRounding.ToPositiveInfinity => x < TSelf.Zero ? boundaryTowardsZero : boundaryAwayFromZero,
        HalfwayRounding.ToOdd => TBound.IsOddInteger(boundaryAwayFromZero) ? boundaryAwayFromZero : boundaryTowardsZero,
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
    }

    /// <summary>PREVIEW! Rounds a value to the nearest boundary. The mode specifies how to round when between two intervals.</summary>
    public static TBound RoundToNearest<TSelf, TBound>(this TSelf x, TBound boundaryTowardsZero, TBound boundaryAwayFromZero, HalfwayRounding mode)
    where TSelf : System.Numerics.INumber<TSelf>
    where TBound : System.Numerics.INumber<TBound>
    {
      var distanceTowardsZero = TSelf.Abs(x - TSelf.CreateChecked(boundaryTowardsZero)); // Distance from value to the boundary towardsZero.
      var distanceAwayFromZero = TSelf.Abs(TSelf.CreateChecked(boundaryAwayFromZero) - x); // Distance from value to the boundary awayFromZero;

      return RoundToNearest(x, boundaryTowardsZero, boundaryAwayFromZero, distanceTowardsZero, distanceAwayFromZero, mode);
    }
  }
}
#endif
