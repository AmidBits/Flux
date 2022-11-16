namespace Flux
{
  public static partial class Vector
  {
    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    public static (TSelf x, TSelf y, TSelf z) VectorTripleProduct<TSelf>(TSelf ax, TSelf ay, TSelf az, TSelf bx, TSelf by, TSelf bz, TSelf cx, TSelf cy, TSelf cz)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      var (bcx, bcy, bcz) = CrossProduct(bx, by, bz, cx, cy, cz);

      return CrossProduct(ax, ay, az, bcx, bcy, bcz);
    }
  }
}
