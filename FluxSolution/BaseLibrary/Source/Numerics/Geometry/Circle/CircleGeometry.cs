namespace Flux.Geometry
{
  /// <summary>
  /// <para></para>
  /// <see href="https://en.wikipedia.org/wiki/Circle"/>
  /// </summary>
  public readonly record struct CircleGeometry
    : System.IFormattable
  {
    public static CircleGeometry Unit { get; } = new(1);

    private readonly double m_radius;

    public CircleGeometry(double radius) => m_radius = radius;

    /// <summary>
    /// <para>The circumference (perimeter) of the circle.</para>
    /// </summary>
    public double Circumference => Coordinates.PolarCoordinate.PerimeterOfCircle(m_radius);

    /// <summary>
    /// <para>The radius of the circle.</para>
    /// </summary>
    public double Radius => m_radius;

    /// <summary>The surface area of circle.</summary>
    public double SurfaceArea => Coordinates.PolarCoordinate.SurfaceAreaOfCircle(m_radius);

    /// <summary>Returns whether a point is inside the circle.</summary>
    public bool Contains(double x, double y) => Coordinates.PolarCoordinate.PointInCircle(m_radius, x, y);

    #region Static methods

    /// <summary>
    /// <para>Creates <paramref name="count"/> vertices along the circumference of the circle [<paramref name="radius"/>] with the <paramref name="arcOffset"/>, <paramref name="translateX"/>, <paramref name="translateY"/> and randomized by <paramref name="maxRandomness"/> using the <paramref name="rng"/>.</para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="radius">When scaleRadius equals 1, the result follows the circumradius of the unit circle.</param>
    /// <param name="arcOffset"></param>
    /// <param name="translateX"></param>
    /// <param name="translateY"></param>
    /// <param name="maxRandomness"></param>
    /// <param name="rng"></param>
    /// <returns></returns>
    public static RegularPolygon Create(int count, double radius = 1, double arcOffset = 0, double translateX = 0, double translateY = 0)
      => RegularPolygon.Create(count, radius, arcOffset, translateX, translateY);

    /// <summary>
    /// <para>Computes the perimeter (circumference) of a circle with the specified <paramref name="radius"/>.</para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Perimeter"/></para>
    /// </summary>
    public static double PerimeterOfCircle(double radius)
      => 2 * double.Pi * radius;

    /// <summary>
    /// <para>Computes the perimeter of a semicircle with the specified <paramref name="radius"/>.</para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Perimeter"/></para>
    /// </summary>
    public static double PerimeterOfSemicircle(double radius)
      => (double.Pi + 2) * radius;

    /// <summary>
    /// <para>Returns whether a point (<paramref name="x"/>, <paramref name="y"/>) is inside of a circle with the specified <paramref name="radius"/>.</para>
    /// </summary>
    public static bool PointInCircle(double radius, double x, double y)
      => double.Pow(x, 2) + double.Pow(y, 2) <= double.Pow(radius, 2);

    /// <summary>
    /// <para>Computes the surface area of a circle with the specified <paramref name="radius"/>.</para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Surface_area"/></para>
    /// </summary>
    public static double SurfaceAreaOfCircle(double radius)
      => double.Pi * radius * radius;

    /// <summary>
    /// <para>Computes the surface area of a semicircle with the specified <paramref name="radius"/>.</para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Surface_area"/></para>
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static double SurfaceAreaOfSemicircle(double radius)
      => SurfaceAreaOfCircle(radius) / 2;

    #endregion // Static methods

    #region Implemented interfaces

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
      format ??= "N3";

      return $"{GetType().Name} {{ Radius = {m_radius.ToString(format, formatProvider)}, Circumference = {Circumference.ToString(format, formatProvider)}, SurfaceArea = {SurfaceArea.ToString(format, formatProvider)} }}";
    }

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
