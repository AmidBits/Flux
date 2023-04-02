namespace Flux.Geometry
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct EllipseGeometry
  {
    private readonly double m_x;
    private readonly double m_y;

    public EllipseGeometry(double x, double y)
    {
      m_x = x;
      m_y = y;
    }

    public void Deconstruct(out double x, out double y) { x = m_x; y = m_y; }

    /// <summary>The x-axis (width) of the ellipse.</summary>
    public double X => m_x;
    /// <summary>The y-axis (height) of the ellipse.</summary>
    public double Y => m_y;

    /// <summary>The semimajor axis of the ellipse.</summary>
    public double SemiMajorAxis => double.Max(m_x, m_y);
    /// <summary>The semiminor axis of the ellipse.</summary>
    public double SemiMinorAxis => double.Min(m_x, m_y);

    /// <summary>Returns the area of an ellipse based on two semi-axes or radii a and b (the order of the arguments do not matter).</summary>
    public double Area => double.Pi * SemiMajorAxis * SemiMinorAxis;

    /// <summary>Returns the approxate circumference of an ellipse based on the two semi-axis or radii a and b (the order of the arguments do not matter). Uses Ramanujans second approximation.</summary>
    public double Circumference
    {
      get
      {
        var circle = double.Pi * (m_x + m_y); // (2 * PI * radius)

        if (m_x == m_y) // For a circle, use (PI * diameter);
          return circle;

        var h3 = 3 * H(SemiMajorAxis, SemiMinorAxis);

        return circle * (1.0 + h3 / (10.0 + double.Sqrt(4.0 - h3)));
      }
    }

    /// <summary>Returns whether a point (<paramref name="x"/>, <paramref name="y"/>) is inside the optionally rotated (<paramref name="rotationAngle"/> in radians, the default 0 equals no rotation) ellipse.</summary>
    public bool Contains(double x, double y, double rotationAngle = 0)
      => double.Cos(rotationAngle) is var cos && double.Sin(rotationAngle) is var sin && double.Pow(cos * x + sin * y, 2) / (m_x * m_x) + double.Pow(sin * x - cos * y, 2) / (m_y * m_y) <= 1;

    /// <summary>
    /// <para>Creates a elliptical polygon with random vertices from the specified number of segments, width, height and an optional random variance unit interval (toward 0 = least random, toward 1 = most random).</para>
    /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(3, 100, 100, 0); // triangle, top pointy</para>
    /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(3, 100, 100, double.Tau / 6); // triangle, bottom pointy</para>
    /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(4, 100, 100, 0); // rectangle, horizontally and vertically pointy</para>
    /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(4, 100, 100, double.Tau / 8); // rectangle, vertically and horizontally flat</para>
    /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(5, 100, 100, 0); // pentagon, horizontally pointy</para>
    /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(5, 100, 100, double.Tau / 10); // pentagon, vertically pointy</para>
    /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(6, 100, 100, 0); // hexagon, vertically flat (or horizontally pointy)</para>
    /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(6, 100, 100, double.Tau / 12); // hexagon, horizontally flat (or vertically pointy)</para>
    /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(8, 100, 100, 0); // octagon, horizontally and vertically pointy</para>
    /// <para>Flux.Media.Geometry.Ellipse.CreatePoints(8, 100, 100, double.Tau / 16); // octagon, vertically and horizontally flat</para>
    /// </summary>
    /// <param name="resultSelector">The selector that determines the result (<typeparamref name="TResult"/>) for each vector.</param>
    /// <param name="radOffset">The offset in radians to apply to each vector.</param>
    /// <param name="maxRandomness">The maximum randomness to allow for each vector. Must be in the range [0, 0.5].</param>
    /// <param name="rng">The random number generator to use, or default if null.</param>
    /// <returns>A new sequence of <typeparamref name="TResult"/>.</returns>
    public System.Collections.Generic.IEnumerable<TResult> CreateVectors<TResult>(double numberOfPoints, System.Func<double, double, TResult> resultSelector, double radOffset = 0, double maxRandomness = 0, System.Random? rng = null)
    {
      rng ??= Random.NumberGenerators.Crypto;

      var circularArc = double.Tau / numberOfPoints;

      for (var segment = 0; segment < numberOfPoints; segment++)
      {
        var angle = radOffset + segment * circularArc;

        if (maxRandomness > 0)
          angle += rng.NextDouble(0, circularArc * maxRandomness);

        var (x, y) = Convert.RotationAngleToCartesian2Ex(angle);

        yield return resultSelector(x * m_x, y * m_y);
      }
    }

    /// <summary>
    /// <para>The eccentricity of a conic section is a non-negative real number that uniquely characterizes its shape. One can think of the eccentricity as a measure of how much a conic section deviates from being circular. The eccentricity of an ellipse which is not a circle is greater than zero but less than 1.</para>
    /// <see href="https://en.wikipedia.org/wiki/Eccentricity_(mathematics)"/>
    /// <see href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    public double Eccentricity => LinearEccentricity / SemiMajorAxis;

    /// <summary>
    /// <para>The eccentricity can be expressed in terms of the flattening f (defined as 1-b/a for semimajor axis a and semiminor axis b).</para>
    /// <see href="https://en.wikipedia.org/wiki/Eccentricity_(mathematics)"/>
    /// <see href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    public double Flattening => 1 - SemiMinorAxis / SemiMajorAxis;

    /// <summary>
    /// <para>First eccentricity.</para>
    /// <see href="https://en.wikipedia.org/wiki/Eccentricity_(mathematics)"/>
    /// <see href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    public double FirstEccentricity
      => double.Sqrt(1 - double.Pow(SemiMinorAxis, 2) / double.Pow(SemiMajorAxis, 2));

    /// <summary>
    /// <para>The linear eccentricity of an ellipse or hyperbola, denoted c (or sometimes f or e), is the distance between its center and either of its two foci.</para>
    /// <see href="https://en.wikipedia.org/wiki/Eccentricity_(mathematics)"/>
    /// <see href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    /// <remarks>In the case of ellipses and hyperbolas the linear eccentricity is sometimes called the half-focal separation.</remarks>
    public double LinearEccentricity => double.Sqrt(double.Pow(SemiMajorAxis, 2) - double.Pow(SemiMinorAxis, 2));

    /// <summary>
    /// <para>Second eccentricity.</para>
    /// <see href="https://en.wikipedia.org/wiki/Eccentricity_(mathematics)"/>
    /// <see href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    /// <param name="a">The semimajor axis.</param>
    /// <param name="b">The semiminor axis.</param>
    public double SecondEccentricity => double.Sqrt(double.Pow(SemiMajorAxis, 2) / double.Pow(SemiMinorAxis, 2) - 1);

    /// <summary>
    /// <para>Third eccentricity.</para>
    /// <see href="https://en.wikipedia.org/wiki/Eccentricity_(mathematics)"/>
    /// <see href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    /// <param name="a">The semimajor axis.</param>
    /// <param name="b">The semiminor axis.</param>
    public double ThirdEccentricity
    {
      get
      {
        var a2 = double.Pow(SemiMajorAxis, 2);
        var b2 = double.Pow(SemiMinorAxis, 2);

        return double.Sqrt(a2 - b2) / double.Sqrt(a2 + b2);
      }
    }

    /// <summary></summary>
    public Numerics.CartesianCoordinate2<double> ToCartesianCoordinate2(double rotationAngle = 0)
      => new(
        double.Cos(rotationAngle) * m_x,
        double.Sin(rotationAngle) * m_y
      );

    #region Static methods

    /// <summary>Returns an Ellipse from the specified cartesian coordinates. The angle (radians) is derived as starting at a 90 degree angle (i.e. 3 o'clock), so not at the "top" as may be expected.</summary>
    public static EllipseGeometry FromCartesian(double x, double y) => new(double.Sqrt(x * x + y * y), double.Atan2(y, x));

    /// <summary>This seem to be a common recurring (unnamed, other than "H", AFAIK) formula in terms of ellipses.</summary>
    public static double H(double a, double b)
      => double.Pow(a - b, 2) / double.Pow(a + b, 2);

    #endregion Static methods
  }
}
