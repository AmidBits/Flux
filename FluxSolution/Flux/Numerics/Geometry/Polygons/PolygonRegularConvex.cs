namespace Flux.Numerics.Geometry.Polygons
{
  /// <summary>
  /// <para>A regular polygon is a polygon that is direct equiangular (all angles are equal in measure) and equilateral (all sides have the same length).</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Regular_polygon"/></para>
  /// </summary>
  public record class PolygonRegularConvex
    : PolygonCyclic
  {
    private PolygonRegularConvex(System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector128<double>> vertices) : base(vertices) { }

    public double Apothem => SideLength / (2 * double.Tan(double.Pi / m_vertices.Count));

    public override double Circumradius => SideLength / (2 * double.Sin(double.Pi / m_vertices.Count));

    public Units.Angle ExteriorAngle => new(double.Tau / m_vertices.Count);

    public Units.Angle InteriorAngle => new((m_vertices.Count - 2) * (double.Pi / m_vertices.Count));

    public override double Perimeter => Units.Length.OfRegularPolygonPerimeter(Circumradius, m_vertices.Count);

    public double SideLength => (m_vertices[0] - m_vertices[1]).EuclideanLength();

    public override double SurfaceArea => Units.Area.OfRegularPolygon(Circumradius, m_vertices.Count);

    #region Static methods

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="count">The number of vertices to compute.</param>
    /// <param name="circumradius">The circumradius of the polygon.</param>
    /// <param name="arcOffset">The rotational offset in radians.</param>
    /// <param name="translateX">The translation X offset.</param>
    /// <param name="translateY">The translation Y offset.</param>
    /// <returns></returns>
    public static PolygonRegularConvex Create(int count, double circumradius, double arcOffset = 0, double translateX = 0, double translateY = 0)
      => new(Ellipses.EllipseGeometry.CreatePointsOnEllipse(count, circumradius, circumradius, arcOffset, translateX, translateY));

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="count">The number of vertices to compute.</param>
    /// <param name="circumradius">The circumradius of the polygon.</param>
    /// <param name="arcOffset">The rotational offset in radians.</param>
    /// <param name="translateX">The translation X offset.</param>
    /// <param name="translateY">The translation Y offset.</param>
    /// <returns></returns>
    public static PolygonRegularConvex CreateEx(int count, double circumradius, double arcOffset = 0, double translateX = 0, double translateY = 0)
      => new(Ellipses.EllipseGeometry.CreatePointsOnEllipseEx(count, circumradius, circumradius, arcOffset, translateX, translateY));

    #region Conversion methods

    public static double ConvertCircumradiusToInradius(double circumradius, int numberOfVertices) => circumradius * double.Cos(double.Pi / numberOfVertices);
    public static double ConvertInradiusToCircumradius(double inradius, int numberOfVertices) => inradius / double.Cos(double.Pi / numberOfVertices);

    public static double ConvertSideLengthToCircumradius(double sideLength, int numberOfVertices) => sideLength / (2 * double.Sin(double.Pi / numberOfVertices));
    public static double ConvertCircumradiusToSideLength(double circumradius, int numberOfVertices) => 2 * circumradius * double.Sin(double.Pi / numberOfVertices);

    public static double ConvertSideLengthToInradius(int sideLength, int numberOfVertices) => sideLength / (2 * double.Tan(double.Pi / numberOfVertices)); //0.5 * sideLength * Quantities.Angle.Cot(double.Pi / numberOfVertices);
    public static double ConvertInradiusToSideLength(double inradius, int numberOfVertices) => 2 * inradius * double.Tan(double.Pi / numberOfVertices);

    #endregion // Conversion methods

    public static double ExteriorAngleOfRegularPolygon(int numberOfSides)
      => double.Tau / numberOfSides;

    public static double InteriorAngleOfRegularPolygon(double numberOfSides)
      => (numberOfSides - 2) * double.Pi / numberOfSides;

    #endregion // Static methods

    #region Implemented interfaces

    public override string ToString(string? format, IFormatProvider? formatProvider)
    {
      format ??= "N3";

      var sb = new System.Text.StringBuilder();

      sb.Append(GetType().Name);

      sb.Append($" {{");

      sb.Append($" Apothem = {Apothem.ToString(format, formatProvider)}");
      sb.Append($", Centroid = {Centroid.ToStringXY(format, formatProvider)}");
      sb.Append($", Circumradius = {Circumradius.ToString(format, formatProvider)}");
      sb.Append($", ExteriorAngle = {ExteriorAngle.ToUnitString(Units.AngleUnit.Degree, format, formatProvider)}");
      sb.Append($", InteriorAngle = {InteriorAngle.ToUnitString(Units.AngleUnit.Degree, format, formatProvider)}");
      sb.Append($", IsConvex = {IsConvex}");
      sb.Append($", Perimeter = {Perimeter.ToString(format, formatProvider)}");
      sb.Append($", SurfaceArea = {SurfaceArea.ToString(format, formatProvider)}");
      sb.Append($", SurfaceAreaSigned = {SurfaceAreaSigned.ToString(format, formatProvider)}");

      sb.Append($", Vertices = {m_vertices.Count}");

      //sm = sm.Append($" [{m_vertices.ToStringXY(format, formatProvider)}]");

      sb.Append($" }}");

      return sb.ToString();
    }

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
