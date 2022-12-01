namespace Flux
{
  /// <summary>Cartesian 3D coordinate.</summary>
  public interface ICartesianCoordinate3<TSelf>
    : System.IFormattable
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf X { get; }
    TSelf Y { get; }
    TSelf Z { get; }

    /// <summary>Returns the cross product of two 3D vectors as out variables.</summary>
    static CartesianCoordinate3<TSelf> CrossProduct(ICartesianCoordinate3<TSelf> a, ICartesianCoordinate3<TSelf> b)
     => new(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);

    /// <summary>Returns the dot product of two normalized 3D vectors.</summary>
    static TSelf DotProduct(ICartesianCoordinate3<TSelf> a, ICartesianCoordinate3<TSelf> b)
     => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    static TSelf ScalarTripleProduct(ICartesianCoordinate3<TSelf> a, ICartesianCoordinate3<TSelf> b, ICartesianCoordinate3<TSelf> c)
      => DotProduct(a, CrossProduct(b, c));

    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    static CartesianCoordinate3<TSelf> VectorTripleProduct(ICartesianCoordinate3<TSelf> a, ICartesianCoordinate3<TSelf> b, ICartesianCoordinate3<TSelf> c)
     => CrossProduct(a, CrossProduct(b, c));

    string System.IFormattable.ToString(string? format, System.IFormatProvider? provider)
      => $"{GetType().Name} {{ X = {string.Format($"{{0:{format ?? "N6"}}}", X)}, Y = {string.Format($"{{0:{format ?? "N6"}}}", Y)}, Z = {string.Format($"{{0:{format ?? "N6"}}}", Z)} }}";
  }
}
