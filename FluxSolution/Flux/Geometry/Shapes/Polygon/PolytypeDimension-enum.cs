namespace Flux.Geometry.Shapes.Polygon
{
  /// <summary>
  /// <para>A polytope is a broad term that represent different approaches to generalizing the convex polytopes to include other objects with similar properties.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Polytope"/></para>
  /// </summary>
  public enum PolytopeDimension
  {
    /// <summary>
    /// <para>The empty set or null polytope.</para>
    /// </summary>
    Nullitope = -1,
    /// <summary>
    /// <para>A 0-polytope, i.e. a 0-dimensional polytope.</para>
    /// </summary>
    Monon,
    /// <summary>
    /// <para>A 1-polytope, i.e. a 1-dimensional polytope.</para>
    /// </summary>
    Dion,
    /// <summary>
    /// <para>A 2-polytope, i.e. a 2-dimensional polytope.</para>
    /// </summary>
    Polygon,
    /// <summary>
    /// <para>A 3-polytope, i.e. a 3-dimensional polytope.</para>
    /// </summary>
    Polyhedron,
    /// <summary>
    /// <para>a 4-polytopem i.e. a 4-dimensional polytope.</para>
    /// </summary>
    Polychoron,
  }
}
