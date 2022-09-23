#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Snaps the value to the nearest interval if it's within the specified distance of an interval, otherwise unaltered.</summary>
    public static TSelf DetentInterval<TSelf>(this TSelf number, TSelf interval, TSelf distance)
      where TSelf : System.Numerics.INumber<TSelf>
      => (number % interval is var rem && rem < TSelf.One ? number - rem : number - rem + interval) is var ni && TSelf.Abs(ni - number) <= TSelf.Abs(distance)
      ? ni // Detent to nearest interval.
      : number;

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
