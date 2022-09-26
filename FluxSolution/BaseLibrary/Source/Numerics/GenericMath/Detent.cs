#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Snaps the value to the nearest interval if it's within the specified distance of an interval, otherwise unaltered.</summary>
    public static TSelf DetentInterval<TSelf>(this TSelf value, TSelf interval, TSelf distance, HalfwayRounding mode)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (value % interval is var remainder && TSelf.IsZero(remainder))
        return value; // The number is already on an interval.

      var towardsZero = value - remainder;
      var awayFromZero = value < TSelf.Zero ? towardsZero - interval : towardsZero + interval;

      var tzDistance = TSelf.Abs(value - towardsZero);
      var afzDistance = TSelf.Abs(awayFromZero - value);

      if (tzDistance > distance && afzDistance > distance) // If neither is within distance of interval.
        return value;

      if (tzDistance < afzDistance) // If closer to towardsZero.
        return towardsZero;
      if (afzDistance < tzDistance) // If closer to awayFromZero.
        return awayFromZero;

      return mode switch // It's a tie, use halfway rounding to resolve.
      {
        HalfwayRounding.ToEven => TSelf.IsEvenInteger(towardsZero) ? towardsZero : awayFromZero,
        HalfwayRounding.AwayFromZero => awayFromZero,
        HalfwayRounding.TowardZero => towardsZero,
        HalfwayRounding.ToNegativeInfinity => value < TSelf.Zero ? awayFromZero : towardsZero,
        HalfwayRounding.ToPositiveInfinity => value < TSelf.Zero ? towardsZero : awayFromZero,
        HalfwayRounding.ToOdd => TSelf.IsOddInteger(awayFromZero) ? awayFromZero : towardsZero,
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
    }

    /// <summary>PREVIEW! Snaps the value to the position if it's within the specified distance of the position, otherwise unaltered.</summary>
    public static TSelf DetentPosition<TSelf>(this TSelf number, TSelf position, TSelf distance)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(position - number) <= TSelf.Abs(distance)
      ? position // Detent to position.
      : number;

    /// <summary>PREVIEW! Snaps the value to zero if it's within the specified distance of zero, otherwise unaltered.</summary>
    public static TSelf DetentZero<TSelf>(this TSelf number, TSelf distance)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(number) <= TSelf.Abs(distance)
      ? TSelf.Zero // Detent to zero.
      : number;
  }
}
#endif
