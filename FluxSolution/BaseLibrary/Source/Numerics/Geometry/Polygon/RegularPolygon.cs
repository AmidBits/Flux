namespace Flux.Geometry
{
  public sealed record class RegularPolygon
    : PolygonGeometry
  {
    private RegularPolygon(System.Collections.Generic.List<System.Runtime.Intrinsics.Vector256<double>> vertices) : base(vertices) { }

    new public double Circumradius => m_vertices[0].EuclideanLength();

    public double Inradius => ConvertCircumradiusToInradius(Circumradius, m_vertices.Count);

    #region Static methods

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="vertexCount">The number of vertices to compute.</param>
    /// <param name="circumradius">The circumradius of the polygon.</param>
    /// <param name="rotateRad">The rotational offset in radians.</param>
    /// <param name="translateX">The translation X offset.</param>
    /// <param name="translateY">The translation Y offset.</param>
    /// <returns></returns>
    public static RegularPolygon Create(int vertexCount, double circumradius, double rotateRad = 0, double translateX = 0, double translateY = 0)
    {
      var arc = double.Tau / vertexCount;

      var vertices = new System.Collections.Generic.List<System.Runtime.Intrinsics.Vector256<double>>();

      for (var segment = 0; segment < vertexCount; segment++)
      {
        var angle = rotateRad + segment * arc;

        var (x, y) = Coordinates.PolarCoordinate.ConvertPolarToCartesian2Ex(circumradius, angle);

        vertices.Add(System.Runtime.Intrinsics.Vector256.Create(x + translateX, y + translateY, 0, 0));
      }

      return new(vertices);
    }

    public static double ConvertCircumradiusToInradius(double circumradius, int vertices) => circumradius * double.Cos(double.Pi / vertices);
    public static double ConvertInradiusToCircumradius(double inradius, int vertices) => inradius / double.Cos(double.Pi / vertices);

    public static double ConvertSideLengthToCircumradius(double sideLength, int vertices) => sideLength / 2 * double.Sin(double.Pi / vertices);
    public static double ConvertCircumradiusToSideLength(double circumradius, int vertices) => 2 * circumradius * double.Sin(double.Pi / vertices);

    public static double ConvertSideLengthToInradius(int sideLength, int vertices) => 0.5 * sideLength * Quantities.Angle.Cot(double.Pi / vertices);
    public static double ConvertInradiusToSideLength(double inradius, int vertices) => 2 * inradius * double.Tan(double.Pi / vertices);

    #endregion // Static methods

    #region Implemented interfaces

    new public string ToString(string? format, IFormatProvider? formatProvider)
    {
      format ??= "N4";

      var sb = new System.Text.StringBuilder();

      sb.Append($"{GetType().Name} {{ Vertices = {m_vertices.Count}");

      sb.Append($", Area = {Area.ToString(format, formatProvider)}");
      sb.Append($" ({AreaSigned.ToString(format, formatProvider)})");
      sb.Append($", Perimeter = {Perimeter.ToString(format, formatProvider)}");
      sb.Append($", Circumradius = {Circumradius.ToString(format, formatProvider)}");
      sb.Append($", Inradius = {Inradius.ToString(format, formatProvider)}");

      sb.Append(" [");
      sb.Append(string.Join(@", ", m_vertices.Select(v => $"<{v[0].ToString(format, formatProvider)}, {v[1].ToString(format, formatProvider)}>")));
      sb.Append("] }}");

      return sb.ToString();
    }

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
