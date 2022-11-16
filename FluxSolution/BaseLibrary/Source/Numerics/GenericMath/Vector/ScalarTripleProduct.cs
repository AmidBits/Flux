namespace Flux
{
  public static partial class Vector
  {
    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static TSelf ScalarTripleProduct<TSelf>(TSelf ax, TSelf ay, TSelf az, TSelf bx, TSelf by, TSelf bz, TSelf cx, TSelf cy, TSelf cz)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      var (bcx, bcy, bcz) = CrossProduct(bx, by, bz, cx, cy, cz);

      return DotProduct(ax, ay, az, bcx, bcy, bcz);
    }
  }
}
