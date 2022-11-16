namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Snaps the value to zero if it's within the specified distance of zero, otherwise unaltered.</summary>
    public static TSelf DetentZero<TSelf>(this TSelf number, TSelf distance)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Abs(number) <= TSelf.Abs(distance)
      ? TSelf.Zero // Detent to zero.
      : number;
  }
}
