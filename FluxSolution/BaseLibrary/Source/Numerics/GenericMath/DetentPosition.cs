#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Snaps the value to the position if it's within the specified distance of the position, otherwise unaltered.</summary>
    public static TSelf DetentPosition<TSelf>(this TSelf number, TSelf position, TSelf distance)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(position - number) <= TSelf.Abs(distance)
      ? position // Detent to position.
      : number;
  }
}
#endif
