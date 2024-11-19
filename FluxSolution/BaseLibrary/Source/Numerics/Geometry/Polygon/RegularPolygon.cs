namespace Flux.Geometry
{
  public sealed record class RegularPolygon
    : PolygonGeometry
  {
    private RegularPolygon(System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector128<double>> vertices) : base(vertices) { }

    public override double Circumradius => m_vertices[0].EuclideanLength();

    public double ExteriorAngle => double.RadiansToDegrees(ExteriorAngleOfRegularPolygon(m_vertices.Count));

    public override double Inradius => ConvertCircumradiusToInradius(Circumradius, m_vertices.Count);

    public double InteriorAngle => double.RadiansToDegrees(InteriorAngleOfRegularPolygon(ExteriorAngle));

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
    public static RegularPolygon Create(int count, double circumradius, double arcOffset = 0, double translateX = 0, double translateY = 0)
      => new(EllipseGeometry.Create(count, circumradius, circumradius, arcOffset, translateX, translateY));

    #region Conversion methods

    public static double ConvertCircumradiusToInradius(double circumradius, int numberOfVertices) => circumradius * double.Cos(double.Pi / numberOfVertices);
    public static double ConvertInradiusToCircumradius(double inradius, int numberOfVertices) => inradius / double.Cos(double.Pi / numberOfVertices);

    public static double ConvertSideLengthToCircumradius(double sideLength, int numberOfVertices) => sideLength / 2 * double.Sin(double.Pi / numberOfVertices);
    public static double ConvertCircumradiusToSideLength(double circumradius, int numberOfVertices) => 2 * circumradius * double.Sin(double.Pi / numberOfVertices);

    public static double ConvertSideLengthToInradius(int sideLength, int numberOfVertices) => 0.5 * sideLength * Quantities.Angle.Cot(double.Pi / numberOfVertices);
    public static double ConvertInradiusToSideLength(double inradius, int numberOfVertices) => 2 * inradius * double.Tan(double.Pi / numberOfVertices);

    #endregion // Conversion methods

    public static double AreaOfRegularPolygon(double sideLength, int numberOfSides)
      => numberOfSides * sideLength * sideLength * Quantities.Angle.Cot(double.Pi / numberOfSides) / 4;

    public static double ExteriorAngleOfRegularPolygon(int numberOfSides)
      => double.Tau / numberOfSides;

    public static double InradiusOfRegularPolygonByCircumradius(double circumradius, int numberOfSides)
      => circumradius * double.Cos(double.Pi / numberOfSides);

    public static double InradiusOfRegularPolygonBySideLength(double sideLength, int numberOfSides)
      => sideLength / 2 * double.Tan(double.Pi / numberOfSides);

    public static double InteriorAngleOfRegularPolygon(double numberOfSides)
      => double.Pi - double.Tau / numberOfSides;

    public static double PerimeterOfRegularPolygon(double sideLength, int numberOfSides)
      => numberOfSides * sideLength;

    #endregion // Static methods

    #region Implemented interfaces

    new public string ToString(string? format, IFormatProvider? formatProvider)
    {
      format ??= "N3";

      var sb = new System.Text.StringBuilder();

      sb.Append(GetType().Name);

      sb.Append($" {{ Vertices = {m_vertices.Count}");

      sb.Append($", Circumradius = {Circumradius.ToString(format, formatProvider)}");
      sb.Append($", ExteriorAngle = {ExteriorAngle.ToString(format, formatProvider)}");
      sb.Append($", Inradius = {Inradius.ToString(format, formatProvider)}");
      sb.Append($", InteriorAngle = {InteriorAngle.ToString(format, formatProvider)}");
      sb.Append($", Perimeter = {Perimeter.ToString(format, formatProvider)}");
      sb.Append($", SurfaceArea = {SurfaceArea.ToString(format, formatProvider)} ({SurfaceAreaSigned.ToString(format, formatProvider)})");

      sb.Append($" [{m_vertices.ToStringXY(format, formatProvider)}] }}");

      return sb.ToString();
    }

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
