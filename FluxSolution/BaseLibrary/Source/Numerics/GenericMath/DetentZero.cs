namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Snaps the value to zero if it's within the specified distance of zero, otherwise unaltered.</summary>
    public static TSelf DetentZero<TSelf>(this TSelf value, TSelf distance)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(value) <= TSelf.Abs(distance)
      ? TSelf.Zero // Detent to zero.
      : value;
  }
}
