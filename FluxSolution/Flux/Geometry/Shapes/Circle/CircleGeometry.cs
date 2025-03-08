namespace Flux.Geometry.Shapes.Circle
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
    public double Circumference => Units.Length.PerimeterOfCircle(m_radius);

    /// <summary>
    /// <para>The radius of the circle.</para>
    /// </summary>
    public double Radius => m_radius;

    /// <summary>The surface area of circle.</summary>
    public double SurfaceArea => Units.Area.OfCircle(m_radius);

    /// <summary>Returns whether a point is inside the circle.</summary>
    public bool Contains(double x, double y) => PointInCircle(m_radius, x, y);

    public CoordinateSystems.PolarCoordinate ToPolarCoordinate(double azimuthValue, Units.AngleUnit azimuthUnit) => new(m_radius, Units.LengthUnit.Meter, azimuthValue, azimuthUnit);

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
    public static System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector128<double>> CreatePointsOnCircle(int count, double radius = 1, double arcOffset = 0, double translateX = 0, double translateY = 0)
      => Ellipse.EllipseGeometry.CreatePointsOnEllipse(count, radius, radius, arcOffset, translateX, translateY);

    /// <summary>
    /// <para>Returns whether a point (<paramref name="x"/>, <paramref name="y"/>) is inside of a circle with the specified <paramref name="radius"/>.</para>
    /// </summary>
    public static bool PointInCircle(double radius, double x, double y)
      => double.Pow(x, 2) + double.Pow(y, 2) <= double.Pow(radius, 2);

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
