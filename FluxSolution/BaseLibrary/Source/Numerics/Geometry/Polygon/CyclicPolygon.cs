namespace Flux.Geometry
{
  public sealed record class CyclicPolygon
    : PolygonGeometry
  {
    private CyclicPolygon(System.Collections.Generic.List<System.Runtime.Intrinsics.Vector256<double>> vertices) : base(vertices) { }

    new public double Circumradius => m_vertices[0].EuclideanLength();

    #region Static methods

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="vertexCount">The number of vertices to compute.</param>
    /// <param name="circumradius">The circumradius of the polygon.</param>
    /// <param name="rotateRad">The rotational offset in radians.</param>
    /// <param name="translateX">The translation X offset.</param>
    /// <param name="translateY">The translation Y offset.</param>
    /// <param name="maxRandomness">The max amount of randomness to apply to each vertex.</param>
    /// <param name="rng">The random-number-generator to use. Default if null.</param>
    /// <returns></returns>
    public static CyclicPolygon Create(double vertexCount, double circumradius, double rotateRad = 0, double translateX = 0, double translateY = 0, double maxRandomness = 0, System.Random? rng = null)
    {
      rng ??= System.Random.Shared;

      var arc = double.Tau / vertexCount;

      var vertices = new System.Collections.Generic.List<System.Runtime.Intrinsics.Vector256<double>>();

      for (var segment = 0; segment < vertexCount; segment++)
      {
        var angle = rotateRad + segment * arc;

        if (maxRandomness > 0) angle += rng.NextDouble(0, arc * maxRandomness);

        var (x, y) = Coordinates.PolarCoordinate.ConvertPolarToCartesian2Ex(circumradius, angle);

        vertices.Add(System.Runtime.Intrinsics.Vector256.Create(x + translateX, y + translateY, 0, 0));
      }

      return new(vertices);
    }

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

      sb.Append(" [");
      sb.Append(string.Join(@", ", m_vertices.Select(v => $"<{v[0].ToString(format, formatProvider)}, {v[1].ToString(format, formatProvider)}>")));
      sb.Append("] }}");

      return sb.ToString();
    }

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
