namespace Flux
{
  public static partial class Vector
  {
    /// <summary>For 2D vectors, the cross product of two vectors, is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</summary>
    public static TSelf CrossProduct<TSelf>(TSelf ax, TSelf ay, TSelf bx, TSelf by)
      where TSelf : System.Numerics.INumber<TSelf>
      => ax * by - ay * bx;

    /// <summary>Returns the cross product of two 3D vectors.</summary>
    public static (TSelf x, TSelf y, TSelf z) CrossProduct<TSelf>(TSelf ax, TSelf ay, TSelf az, TSelf bx, TSelf by, TSelf bz)
      where TSelf : System.Numerics.INumber<TSelf>
      => (ay * bz - az * by, az * bx - ax * bz, ax * by - ay * bx);
  }
}
