namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns <paramref name="x"/> clamped, i.e. if out-of-bounds then clamp to the nearest boundary, within the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    /// <remarks>This is the same as the built-in clamp method.</remarks>
    public static TSelf Constrain<TSelf>(this TSelf x, TSelf min, TSelf max)
      where TSelf : System.Numerics.INumber<TSelf>
      => x < min ? min : x > max ? max : x;
  }
}
