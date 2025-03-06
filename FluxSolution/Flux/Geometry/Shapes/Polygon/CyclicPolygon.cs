namespace Flux.Geometry.Shapes.Polygon
{
  /// <summary>
  /// <para>A set of points are said to be concyclic (or cocyclic) if they lie on a common circle. A polygon whose vertices are concyclic is called a cyclic polygon, and the circle is called its circumscribing circle or circumcircle. All concyclic points are equidistant from the center of the circle.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Concyclic_points"/></para>
  /// </summary>
  public sealed record class CyclicPolygon
    : PolygonGeometry
  {
    private CyclicPolygon(System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector128<double>> vertices) : base(vertices) { }

    public override double Circumradius => m_vertices[0].EuclideanLength();

    public override bool IsConvex => true;

    #region Static methods

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="count">The number of vertices to compute.</param>
    /// <param name="circumradius">The circumradius of the polygon.</param>
    /// <param name="arcOffset">The rotational offset in radians.</param>
    /// <param name="translateX">The translation X offset.</param>
    /// <param name="translateY">The translation Y offset.</param>
    /// <param name="maxRandomness">The max amount of randomness to apply to each vertex.</param>
    /// <param name="rng">The random-number-generator to use. Default if null.</param>
    /// <returns></returns>
    public static CyclicPolygon Create(int count, double circumradius, double arcOffset = 0, double translateX = 0, double translateY = 0, double maxRandomness = 0, System.Random? rng = null)
      => new(Ellipse.EllipseGeometry.CreatePointsOfEllipse(count, circumradius, circumradius, arcOffset, translateX, translateY, maxRandomness, rng));

    #endregion // Static methods

    #region Implemented interfaces

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
