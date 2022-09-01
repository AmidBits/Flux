#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static TSelf DetentInterval<TSelf>(this TSelf value, TSelf interval, TSelf distance)
      where TSelf : System.Numerics.INumber<TSelf>
      => (value % interval is var rem && value - rem is var lo && lo + interval is var hi && rem < TSelf.One ? lo : hi) is var ni && TSelf.Abs(ni - value) is var nd && nd < TSelf.Abs(distance)
      ? ni
      : value;
    //{
    //  var nearestInterval = value % interval is var rem && value - rem is var lo && lo + interval is var hi && rem < TSelf.One ? lo : hi;
    //  var nearestDistance = TSelf.Abs(nearestInterval - value);

    //  return nearestDistance <= TSelf.Abs(distance) ? nearestInterval : value;

    //  //var vi = (value / interval);
    //  //if ((value / interval) * interval is var nearestInterval && TSelf.Abs(nearestInterval - value) < distance)
    //  //  return nearestInterval;
    //  //else
    //  //  return value;
    //}

    /// <summary>PREVIEW! Snaps the value to the position if it's within the specified distance of the position.</summary>
    public static TSelf DetentPosition<TSelf>(this TSelf value, TSelf position, TSelf distance)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(position - value) <= TSelf.Abs(distance)
      ? position
      : value;

    /// <summary>PREVIEW! Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static TSelf DetentZero<TSelf>(this TSelf value, TSelf distance)
      where TSelf : System.Numerics.INumber<TSelf>
      => value < -distance || value > distance
      ? value
      : TSelf.Zero;
  }
}
#endif
