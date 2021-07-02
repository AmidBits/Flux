namespace Flux.Numerics
{
  public static class VectorEx3
  {
    /// <summary>Returns the cross product of two 2D vectors.</summary>
    /// <remarks>For 2D vectors, this is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</remarks>
    public static double CrossProduct(double x1, double y1, double x2, double y2)
      => x1 * y2 - y1 * x2;
    /// <summary>Returns the cross product of two 3D vectors as out variables.</summary>
    public static void CrossProduct(double x1, double y1, double z1, double x2, double y2, double z2, out double x, out double y, out double z)
    {
      x = y1 * z2 - z1 * y2;
      y = z1 * x2 - x1 * z2;
      z = x1 * y2 - y1 * x2;
    }

    /// <summary>Returns the dot product of two 2D vectors.</summary>
    public static double DotProduct(double x1, double y1, double x2, double y2)
      => x1 * x2 + y1 * y2;
    /// <summary>Returns the dot product of two 3D vectors.</summary>
    public static double DotProduct(double x1, double y1, double z1, double x2, double y2, double z2)
      => x1 * x2 + y1 * y2 + z1 * z2;

    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static double ScalarTripleProduct(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3)
    {
      CrossProduct(x2, y2, z2, x3, y3, z3, out var cp23x, out var cp23y, out var cp23z);

      return DotProduct(x1, y1, z1, cp23x, cp23y, cp23z);
    }

    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    public static void VectorTripleProduct(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3, out double x, out double y, out double z)
    {
      CrossProduct(x2, y2, z2, x3, y3, z3, out var cp23x, out var cp23y, out var cp23z);

      CrossProduct(x1, y1, z1, cp23x, cp23y, cp23z, out x, out y, out z);
    }
  }
}
