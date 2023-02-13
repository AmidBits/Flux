namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Snaps the value to the position if it's within the specified distance of the position, otherwise unaltered.</summary>
    public static TSelf DetentPosition<TSelf>(this TSelf value, TSelf position, TSelf distance)
      where TSelf : System.Numerics.INumber<TSelf>
      => ApproximateEquality.ByAbsoluteTolerance<TSelf>.IsApproximatelyEqual(position, value, distance)
      //=> TSelf.Abs(position - value) <= TSelf.Abs(distance)
      ? position // Detent to position.
      : value;
  }
}
