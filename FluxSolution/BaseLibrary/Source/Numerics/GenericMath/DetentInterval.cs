namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval, otherwise unaltered.</summary>
    public static TSelf DetentInterval<TSelf>(this TSelf number, TSelf interval, TSelf distance, RoundingMode mode)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (number % interval is var remainder && TSelf.IsZero(remainder))
        return number; // The number is already on an interval.

      var boundaryTowardsZero = number - remainder;
      var boundaryAwayFromZero = number < TSelf.Zero ? boundaryTowardsZero - interval : boundaryTowardsZero + interval;

      var distanceTowardsZero = TSelf.Abs(number - boundaryTowardsZero);
      var distanceAwayFromZero = TSelf.Abs(boundaryAwayFromZero - number);

      if (distanceTowardsZero > distance && distanceAwayFromZero > distance) // If neither is within distance of interval.
        return number;

      return new BoundaryRounding<TSelf, TSelf>(mode, boundaryTowardsZero, boundaryAwayFromZero).RoundNumber(number);
    }
  }
}
