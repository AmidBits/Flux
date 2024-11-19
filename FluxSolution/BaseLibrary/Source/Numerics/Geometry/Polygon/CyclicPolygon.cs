namespace Flux.Geometry
{
  public sealed record class CyclicPolygon
    : PolygonGeometry
  {
    private CyclicPolygon(System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector128<double>> vertices) : base(vertices) { }

    public override double Circumradius => m_vertices[0].EuclideanLength();

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
      => new(EllipseGeometry.Create(count, circumradius, circumradius, arcOffset, translateX, translateY, maxRandomness, rng));

    #endregion // Static methods

    #region Implemented interfaces

    //new public string ToString(string? format, IFormatProvider? formatProvider)
    //{
    //  format ??= "N3";

    //  var sb = new System.Text.StringBuilder();

    //  sb.Append($"{GetType().Name} {{ Vertices = {m_vertices.Count}");

    //  sb.Append($", Circumradius = {Circumradius.ToString(format, formatProvider)}");
    //  sb.Append($", Perimeter = {Perimeter.ToString(format, formatProvider)}");
    //  sb.Append($", SurfaceArea = {SurfaceArea.ToString(format, formatProvider)} ({SurfaceAreaSigned.ToString(format, formatProvider)})");

    //  sb.Append($" [{m_vertices.ToStringXY(format, formatProvider)}] }}");

    //  return sb.ToString();
    //}

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
