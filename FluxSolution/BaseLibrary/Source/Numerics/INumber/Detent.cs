#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static TSelf DetentInterval<TSelf>(this TSelf value, TSelf interval, TSelf distance)
      where TSelf : System.Numerics.INumber<TSelf>
      => (value % interval is var rem && rem < TSelf.One ? value - rem : value - rem + interval) is var ni && TSelf.Abs(ni - value) <= TSelf.Abs(distance)
      ? ni // Detent to nearest interval.
      : value;

    /// <summary>PREVIEW! Snaps the value to the position if it's within the specified distance of the position.</summary>
    public static TSelf DetentPosition<TSelf>(this TSelf value, TSelf position, TSelf distance)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(position - value) <= TSelf.Abs(distance)
      ? position // Detent to position.
      : value;

    /// <summary>PREVIEW! Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static TSelf DetentZero<TSelf>(this TSelf value, TSelf distance)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(value) <= TSelf.Abs(distance)
      ? TSelf.Zero // Detent to zero.
      : value;
  }
}
#endif
