namespace Flux
{
#if NET7_0_OR_GREATER
  /// <summary>Cartesian 3D coordinate with real numbers.</summary>
  public interface IVector4<TSelf>
    : ICartesianCoordinate4<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>
  {
    /// <summary>Compute the Chebyshev length of the 3D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    TSelf ChebyshevLength(TSelf edgeLength);

    /// <summary>Compute the Euclidean length of the vector.</summary>
    TSelf EuclideanLength();

    /// <summary>Compute the length squared of the 3D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    TSelf EuclideanLengthSquared();

    /// <summary>Compute the Manhattan length of the 3D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    TSelf ManhattanLength(TSelf edgeLength);
  }
#else
  public interface IVector4
    : ICartesianCoordinate4
  {
    double X { get; }
    double Y { get; }
    double Z { get; }
    double W { get; }
  }
#endif
}
