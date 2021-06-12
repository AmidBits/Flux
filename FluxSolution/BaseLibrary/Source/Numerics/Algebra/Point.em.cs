using System.Linq;

namespace Flux.Numerics
{
  public static class Point
  {
    /// <summary>Returns the cross product of the two 2D vectors.</summary>
    /// <remarks>For 2D vectors, this is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</remarks>
    public static double CrossProduct(double x1, double y1, double x2, double y2)
      => x1 * y2 - y1 * x2;
    /// <summary>Returns the cross product of the two 3D vectors.</summary>
    public static (double x, double y, double z) CrossProduct(double x1, double y1, double z1, double x2, double y2, double z2)
      => (y1 * z2 - z1 * y2, z1 * x2 - x1 * z2, x1 * y2 - y1 * x2);
    public static void CrossProduct(double x1, double y1, double z1, double x2, double y2, double z2, out double x3, out double y3, out double z3)
    {
      x3 = y1 * z2 - z1 * y2;
      y3 = z1 * x2 - x1 * z2;
      z3 = x1 * y2 - y1 * x2;
    }

    /// <summary>Returns the dot product of the two 2D vectors.</summary>
    public static double DotProduct(double x1, double y1, double x2, double y2)
      => x1 * x2 + y1 * y2;
    /// <summary>Returns the dot product of the two 3D vectors.</summary>
    public static double DotProduct(double x1, double y1, double z1, double x2, double y2, double z2)
      => x1 * x2 + y1 * y2 + z1 * z2;
  }
}
