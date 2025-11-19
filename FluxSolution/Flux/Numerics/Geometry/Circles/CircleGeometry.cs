namespace Flux.Numerics.Geometry.Circles
{
  /// <summary>
  /// <para></para>
  /// <see href="https://en.wikipedia.org/wiki/Circle"/>
  /// </summary>
  public readonly record struct CircleGeometry
    : IFormattable, IFigurable
  {
    public static CircleGeometry Unit { get; } = new(1);

    private readonly double m_radius;

    public CircleGeometry(double radius) => m_radius = radius;

    /// <summary>
    /// <para>The radius of the circle.</para>
    /// </summary>
    public double Radius => m_radius;

    /// <summary>
    /// <para>The circumference (perimeter) of the circle.</para>
    /// </summary>
    public double Circumference => Units.Length.OfCirclePerimeter(m_radius);

    /// <summary>
    /// <para>The perimeter (circumference) of the circle.</para>
    /// </summary>
    public double Perimeter => Units.Length.OfCirclePerimeter(m_radius);

    /// <summary>The surface area of circle.</summary>
    public double SurfaceArea => Units.Area.OfCircle(m_radius);

    /// <summary>Returns whether a point is inside the circle.</summary>
    public bool Contains(double x, double y) => CircleContainsPoint(m_radius, x, y);

    public Numerics.Geometry.Ellipses.EllipseGeometry ToEllipseFigure() => new(m_radius);

    public Numerics.Geometry.Hexagons.HexagonGeometry ToHexagonFigure() => new(m_radius);

    public CoordinateSystems.PolarCoordinate ToPolarCoordinate(double azimuthValue, Units.AngleUnit azimuthUnit) => new(m_radius, Units.LengthUnit.Meter, azimuthValue, azimuthUnit);

    #region Static methods

    public static double ConvertAreaToCircumference(double area) => double.Sqrt(2 * area / double.Tau) * double.Tau;
    public static double ConvertAreaToRadius(double area) => double.Sqrt(2 * area / double.Tau);
    public static double ConvertCircumferenceToArea(double circumference) => circumference * circumference / (2 * double.Tau);
    public static double ConvertCircumferenceToRadius(double circumference) => circumference / double.Tau;
    public static double ConvertRadiusToArea(double radius) => radius * radius * double.Pi;
    public static double ConvertRadiusToCircumference(double radius) => radius * double.Tau;

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
    public static IEnumerable<System.Runtime.Intrinsics.Vector128<double>> CreatePointsOnCircle(int count, double radius = 1, double arcOffset = 0, double translateX = 0, double translateY = 0)
      => Ellipses.EllipseGeometry.CreatePointsOnEllipse(count, radius, radius, arcOffset, translateX, translateY);

    /// <summary>
    /// <para>Intersection of circle at 0, 1 or 2 points with line ABC.</para>
    /// <para>The circle and line are assumed to be at (0, 0).</para>
    /// </summary>
    /// <param name="r"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static (int IntersectCount, double X0, double Y0, double X1, double Y1) CircleIntersectWithLine(double r, double a, double b, double c)
    {
      if (c * c > r * r * (a * a + b * b) + 1E-6) // No points:
        return (0, 0, 0, 0, 0);

      var x0 = -a * c / (a * a + b * b);
      var y0 = -b * c / (a * a + b * b);

      if (double.Abs(c * c - r * r * (a * a + b * b)) < 1E-6) // 1 point:
        return (1, x0, y0, 0, 0);

      // 2 points:

      var d = r * r - c * c / (a * a + b * b);
      var m = double.Sqrt(d / (a * a + b * b));

      var x1 = x0 - b * m;
      var y1 = y0 + a * m;

      x0 += b * m;
      y0 -= a * m;

      return (2, x0, y0, x1, y1);
    }

    /// <summary>
    /// <para>Returns whether a point (<paramref name="x"/>, <paramref name="y"/>) is inside of a circle with the specified <paramref name="radius"/>.</para>
    /// </summary>
    public static bool CircleContainsPoint(double radius, double x, double y)
      => double.Pow(x, 2) + double.Pow(y, 2) <= double.Pow(radius, 2);

    #endregion // Static methods

    #region Implemented interfaces

    public string ToString(string? format, IFormatProvider? provider)
    {
      format ??= "N3";

      return $"{GetType().Name} {{ Radius = {m_radius.ToString(format, provider)}, Circumference = {Perimeter.ToString(format, provider)}, SurfaceArea = {SurfaceArea.ToString(format, provider)} }}";
    }

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
