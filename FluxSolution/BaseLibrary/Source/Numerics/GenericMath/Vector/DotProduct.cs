#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Vector
  {
    /// <summary>Returns the dot product of two 2D vectors.</summary>
    public static TSelf DotProduct<TSelf>(TSelf ax, TSelf ay, TSelf bx, TSelf by)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => ax * bx + ay * by;

    /// <summary>Returns the dot product of two 3D vectors.</summary>
    public static TSelf DotProduct<TSelf>(TSelf ax, TSelf ay, TSelf az, TSelf bx, TSelf by, TSelf bz)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => ax * bx + ay * by + az * bz;
  }
}
#endif
