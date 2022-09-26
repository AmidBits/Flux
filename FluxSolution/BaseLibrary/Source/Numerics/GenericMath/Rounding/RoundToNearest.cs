#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Rounds a value to the nearest specified interval. The mode specifies how to round when between two intervals.</summary>
    public static TBound RoundToNearest<TSelf, TBound>(this TSelf value, TBound boundTowardsZero, TBound boundAwayFromZero, HalfwayRounding mode)
      where TSelf : System.Numerics.INumber<TSelf>
      where TBound : System.Numerics.INumber<TBound>
    {
      var tzDistance = TSelf.Abs(value - TSelf.CreateChecked(boundTowardsZero)); // Distance from value to towardsZero.
      var afzDistance = TSelf.Abs(TSelf.CreateChecked(boundAwayFromZero) - value); // Distance from value to awayFromZero;

      if (tzDistance < afzDistance) // It's a clear win for towardsZero.
        return boundTowardsZero;
      if (afzDistance < tzDistance) // It's a clear win for awayFromZero.
        return boundAwayFromZero;

      return mode switch // It's exactly halfway, use appropriate rounding to resolve winner.
      {
        HalfwayRounding.ToEven => TBound.IsEvenInteger(boundTowardsZero) ? boundTowardsZero : boundAwayFromZero,
        HalfwayRounding.AwayFromZero => boundAwayFromZero,
        HalfwayRounding.TowardZero => boundTowardsZero,
        HalfwayRounding.ToNegativeInfinity => value < TSelf.Zero ? boundAwayFromZero : boundTowardsZero,
        HalfwayRounding.ToPositiveInfinity => value < TSelf.Zero ? boundTowardsZero : boundAwayFromZero,
        HalfwayRounding.ToOdd => TBound.IsOddInteger(boundAwayFromZero) ? boundAwayFromZero : boundTowardsZero,
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
    }
  }
}
#endif
