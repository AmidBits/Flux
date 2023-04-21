namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval, otherwise unaltered.</summary>
    public static TSelf DetentInterval<TSelf>(this TSelf value, TSelf interval, TSelf distance, RoundingMode mode)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      LocateMultipleOf(value, interval, false, out var multipleTowardsZero, out var multipleAwayFromZero);

      BoundaryRounding<TSelf, TSelf>.MeasureDistanceToBoundaries(value, multipleTowardsZero, multipleAwayFromZero, out TSelf distanceTowardsZero, out TSelf distanceAwayFromZero);

      if (BoundaryRounding<TSelf, TSelf>.IsWithinDistanceToBoundaries(distanceTowardsZero, distanceAwayFromZero, distance))
        return BoundaryRounding<TSelf, TSelf>.Round(value, mode, multipleTowardsZero, multipleAwayFromZero);

      return value; // If it gets here, value was not within the distance from either multipleTowardsZero or multipleAwayFromZero, so we return the value unchanged.
    }

#else

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static System.Numerics.BigInteger DetentInterval(this System.Numerics.BigInteger value, in System.Numerics.BigInteger interval, in System.Numerics.BigInteger distance)
      => (value / interval) * interval is var nearestInterval && System.Numerics.BigInteger.Abs(nearestInterval - value) < distance
      ? nearestInterval
      : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static decimal DetentInterval(this decimal value, in decimal interval, in decimal distance)
      => System.Math.Round(value / interval) * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance
      ? nearestInterval
      : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static float DetentInterval(this float value, in float interval, in float distance)
      => (float)System.Math.Round(value / interval) * interval is var nearestInterval && (float)System.Math.Abs(nearestInterval - value) < distance
      ? nearestInterval
      : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static double DetentInterval(this double value, in double interval, in double distance)
      => System.Math.Round(value / interval) * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance
      ? nearestInterval
      : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static int DetentInterval(this int value, int interval, int distance)
      => value / interval * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance
      ? nearestInterval
      : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static long DetentInterval(this long value, long interval, long distance)
      => value / interval * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance
      ? nearestInterval
      : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    [System.CLSCompliant(false)]
    public static uint DetentInterval(this uint value, uint interval, uint distance)
      => (value / interval) * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance
      ? nearestInterval
      : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an ixxnterval.</summary>
    [System.CLSCompliant(false)]
    public static ulong DetentInterval(this ulong value, ulong interval, ulong distance)
      => (value / interval) * interval is var nearestInterval && (ulong)System.Math.Abs((long)(nearestInterval - value)) < distance
      ? nearestInterval
      : value;

#endif
  }
}
