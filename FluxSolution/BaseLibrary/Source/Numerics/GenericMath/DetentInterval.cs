namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval, otherwise unaltered.</summary>
    public static TSelf DetentInterval<TSelf>(this TSelf value, TSelf interval, TSelf distance, RoundingMode mode)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      LocateMultiple(value, interval, false, out var multipleTowardsZero, out var multipleAwayFromZero);

      BoundaryRounding<TSelf, TSelf>.MeasureDistanceToBoundaries(value, multipleTowardsZero, multipleAwayFromZero, out TSelf distanceTowardsZero, out TSelf distanceAwayFromZero);

      if (BoundaryRounding<TSelf, TSelf>.IsWithinDistanceToBoundaries(distanceTowardsZero, distanceAwayFromZero, distance))
        return BoundaryRounding<TSelf, TSelf>.Round(value, mode, multipleTowardsZero, multipleAwayFromZero);

      return value; // If it gets here, value was not within the distance from either multipleTowardsZero or multipleAwayFromZero, so we return the value unchanged.
    }
  }
}
