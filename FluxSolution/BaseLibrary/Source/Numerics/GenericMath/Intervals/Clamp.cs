#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns <paramref name="x"/> clamped, i.e. if out-of-bounds then clamp to the nearest boundary, within the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static TSelf Clamp<TSelf>(TSelf x, TSelf min, TSelf max)
      where TSelf : System.Numerics.INumber<TSelf>
      => x < min ? min : x > max ? max : x;
  }
}
#endif
