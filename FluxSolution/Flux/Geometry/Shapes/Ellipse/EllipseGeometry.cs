namespace Flux.Geometry.Shapes.Ellipse
{
  /// <summary>
  /// <para></para>
  /// <see href="https://en.wikipedia.org/wiki/Ellipse"/>
  /// </summary>
  public readonly record struct EllipseGeometry
    : System.IFormattable
  {
    public static EllipseGeometry Unit { get; } = new(1, 1);

    private readonly double m_a;
    private readonly double m_b;

    public EllipseGeometry(double a, double b)
    {
      m_a = a;
      m_b = b;
    }

    public void Deconstruct(out double a, out double b) { a = m_a; b = m_b; }

    /// <summary>The semi-major or a-axis of the ellipse.</summary>
    public double A => m_a;
    /// <summary>The semi-minor or b-axis of the ellipse.</summary>
    public double B => m_b;

    /// <summary>Returns the approximate circumference of an ellipse based on the two semi-axis or radii a and b (the order of the arguments do not matter). Uses Ramanujans second approximation.</summary>
    public double Perimeter => Units.Length.PerimeterOfEllipse(m_a, m_b);

    /// <summary>Returns the area of an ellipse based on two semi-axes or radii a and b (the order of the arguments do not matter).</summary>
    public double SurfaceArea => Units.Area.OfEllipse(m_a, m_b);

    /// <summary>Returns whether a point (<paramref name="x"/>, <paramref name="y"/>) is inside the optionally rotated (<paramref name="rotationAngle"/> in radians, the default 0 equals no rotation) ellipse.</summary>
    public bool Contains(double x, double y, double rotationAngle = 0) => PointInEllipse(m_a, m_b, x, y, rotationAngle);

    /// <summary>
    /// <para>The linear eccentricity of an ellipse or hyperbola, denoted c (or sometimes f or e), is the distance between its center and either of its two foci.</para>
    /// <see href="https://en.wikipedia.org/wiki/Eccentricity_(mathematics)"/>
    /// <see href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    /// <remarks>In the case of ellipses and hyperbolas the linear eccentricity is sometimes called the half-focal separation.</remarks>
    public double GetLinearEccentricity() => double.Sqrt((m_a * m_a) - (m_b * m_b));

    /// <summary>
    /// <para>First eccentricity.</para>
    /// <para>The eccentricity of a conic section is a non-negative real number that uniquely characterizes its shape. One can think of the eccentricity as a measure of how much a conic section deviates from being circular. The eccentricity of an ellipse which is not a circle is greater than zero but less than 1.</para>
    /// <see href="https://en.wikipedia.org/wiki/Eccentricity_(mathematics)"/>
    /// <see href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    public double GetFirstEccentricity() => double.Sqrt(1 - (m_b * m_b) / (m_a * m_a));

    /// <summary>
    /// <para>Second eccentricity.</para>
    /// <see href="https://en.wikipedia.org/wiki/Eccentricity_(mathematics)"/>
    /// <see href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    public double GetSecondEccentricity() => double.Sqrt((m_a * m_a) / (m_b * m_b) - 1);

    /// <summary>
    /// <para>Third eccentricity.</para>
    /// <see href="https://en.wikipedia.org/wiki/Eccentricity_(mathematics)"/>
    /// <see href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    public double GetThirdEccentricity()
    {
      var a2 = m_a * m_a;
      var b2 = m_b * m_b;

      return double.Sqrt(a2 - b2) / double.Sqrt(a2 + b2);
    }

    /// <summary>
    /// <para>First flattening.</para>
    /// <see href="https://en.wikipedia.org/wiki/Flattening"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    public double GetFirstFlattening() => (m_a - m_b) / m_a;

    /// <summary>
    /// <para>Second flattening.</para>
    /// <see href="https://en.wikipedia.org/wiki/Flattening"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    public double GetSecondFlattening() => (m_a - m_b) / m_b;

    /// <summary>
    /// <para>Third flattening.</para>
    /// <see href="https://en.wikipedia.org/wiki/Flattening"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    public double GetThirdFlattening() => (m_a - m_b) / (m_a + m_b);

    #region Static methods

    #region Conversion methods

    /// <summary>Returns an ellipse geometry from the specified cartesian coordinates. The angle (radians) is derived as starting at a 90 degree angle (i.e. 3 o'clock), so not at the "top" as may be expected.</summary>
    public static (double a, double b) ConvertCartesian2ToEllipse(double x, double y)
      => (
        System.Math.Sqrt(x * x + y * y),
        System.Math.Atan2(y, x)
      );

    /// <summary></summary>
    public static (double x, double y) ConvertEllipseToCartesian2(double a, double b, double rotationAngle = 0)
      => (
        System.Math.Cos(rotationAngle) * a,
        System.Math.Sin(rotationAngle) * b
      );

    #endregion // Conversion methods

    /// <summary>
    /// <para>Creates <paramref name="count"/> vertices along the perimeter of the ellipse [<paramref name="a"/>, <paramref name="b"/>] with the <paramref name="arcOffset"/>, <paramref name="translateX"/>, <paramref name="translateY"/> and randomized by <paramref name="maxRandomness"/> using the <paramref name="rng"/>.</para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="a">Correspond to the X-axis. If X and Y are both equal to 1, the result follows the circumradius of the unit circle.</param>
    /// <param name="b">Correspond to the Y-axis.</param>
    /// <param name="arcOffset"></param>
    /// <param name="translateX"></param>
    /// <param name="translateY"></param>
    /// <param name="maxRandomness"></param>
    /// <param name="rng"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Runtime.Intrinsics.Vector128<double>> CreatePointsOnEllipse(int count, double a = 1, double b = 1, double arcOffset = 0, double translateX = 0, double translateY = 0, double maxRandomness = 0, System.Random? rng = null)
    {
      rng ??= System.Random.Shared;

      var arc = System.Math.Tau / count;

      for (var index = 0; index < count; index++)
      {
        var angle = arcOffset + index * arc;

        if (maxRandomness > 0)
          angle += rng.NextDouble(0, arc * maxRandomness);

        var (x, y) = Geometry.CoordinateSystems.PolarCoordinate.ConvertPolarToCartesian2Ex(1, angle);

        yield return System.Runtime.Intrinsics.Vector128.Create(x * a + translateX, y * b + translateY);
      }
    }

    /// <summary>
    /// <para>Returns whether a point (<paramref name="x"/>, <paramref name="y"/>) is inside the optionally rotated (<paramref name="rotationAngle"/> in radians, the default 0 equals no rotation) ellipse with the the two specified semi-axes or radii (<paramref name="a"/>, <paramref name="b"/>). The ellipse <paramref name="a"/> and <paramref name="b"/> correspond to same axes as <paramref name="x"/> and <paramref name="y"/> of the point, respectively.</para>
    /// </summary>
    public static bool PointInEllipse(double a, double b, double x, double y, double rotationAngle = 0)
      => double.Cos(rotationAngle) is var cos && double.Sin(rotationAngle) is var sin && double.Pow(cos * x + sin * y, 2) / (a * a) + double.Pow(sin * x - cos * y, 2) / (b * b) <= 1;

    #endregion // Static methods

    #region Implemented interfaces

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
      format ??= "N3";

      return $"{GetType().Name} {{ A = {m_a.ToString(format, formatProvider)}, B = {m_b.ToString(format, formatProvider)}, Perimeter = {Perimeter.ToString(format, formatProvider)}, SurfaceArea = {SurfaceArea.ToString(format, formatProvider)} }}";
    }

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
