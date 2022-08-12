#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static TSelf DetentInterval<TSelf>(this TSelf value, TSelf interval, TSelf distance)
      where TSelf : System.Numerics.IBinaryNumber<TSelf>
      => (value / interval) * interval is var nearestInterval && TSelf.Abs(nearestInterval - value) < distance
      ? nearestInterval
      : value;

    /// <summary>Snaps the value to the position if it's within the specified distance of the position.</summary>
    public static TSelf DetentPosition<TSelf>(this TSelf value, TSelf position, TSelf distance)
      where TSelf : System.Numerics.IBinaryNumber<TSelf>
      => TSelf.Abs(position - value) > TSelf.Abs(distance)
      ? value
      : position;

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static TSelf DetentZero<TSelf>(this TSelf value, TSelf distance)
      where TSelf : System.Numerics.IBinaryNumber<TSelf>
      => value < -distance || value > distance
      ? value
      : TSelf.Zero;
  }
}
#endif
