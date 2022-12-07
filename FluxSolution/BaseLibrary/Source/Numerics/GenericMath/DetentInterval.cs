namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval, otherwise unaltered.</summary>
    public static TSelf DetentInterval<TSelf>(this TSelf value, TSelf interval, TSelf distance, RoundingMode mode)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (value % interval is var remainder && TSelf.IsZero(remainder))
        return value; // The number is already on an interval.

      var boundaryTowardsZero = value - remainder;
      var boundaryAwayFromZero = value < TSelf.Zero ? boundaryTowardsZero - interval : boundaryTowardsZero + interval;

      var distanceTowardsZero = TSelf.Abs(value - boundaryTowardsZero);
      var distanceAwayFromZero = TSelf.Abs(boundaryAwayFromZero - value);

      if (distanceTowardsZero > distance && distanceAwayFromZero > distance) // If neither is within distance of interval.
        return value;

      return new BoundaryRounding<TSelf, TSelf>(mode, boundaryTowardsZero, boundaryAwayFromZero).RoundNumber(value);
    }
  }
}
