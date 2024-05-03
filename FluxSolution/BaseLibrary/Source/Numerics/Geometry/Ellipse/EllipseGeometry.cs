namespace Flux.Geometry
{
  /// <summary>
  /// <para></para>
  /// <see href="https://en.wikipedia.org/wiki/Ellipse"/>
  /// </summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct EllipseGeometry
  {
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

    /// <summary>Returns the area of an ellipse based on two semi-axes or radii a and b (the order of the arguments do not matter).</summary>
    public double Area => SurfaceAreaOfEllipse(m_a, m_b);

    /// <summary>Returns the approximate circumference of an ellipse based on the two semi-axis or radii a and b (the order of the arguments do not matter). Uses Ramanujans second approximation.</summary>
    public double Perimeter => PerimeterOfEllipse(m_a, m_b);

    /// <summary>Returns whether a point (<paramref name="x"/>, <paramref name="y"/>) is inside the optionally rotated (<paramref name="rotationAngle"/> in radians, the default 0 equals no rotation) ellipse.</summary>
    public bool Contains(double x, double y, double rotationAngle = 0) => EllipseContainsPoint(m_a, m_b, x, y, rotationAngle);

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
      rng ??= System.Random.Shared;

      var arc = System.Math.Tau / numberOfPoints;

      for (var index = 0; index < numberOfPoints; index++)
      {
        var angle = radOffset + index * arc;

        if (maxRandomness > 0)
          angle += rng.NextDouble(0, arc * maxRandomness);

        var (x, y) = Coordinates.PolarCoordinate.ConvertPolarToCartesian2Ex(1, angle);

        // Looking to add rotation angle to the struct. ;)
        //var xy = System.Numerics.Vector2.Transform(new System.Numerics.Vector2((float)x, (float)y), new System.Numerics.Quaternion(System.Numerics.Vector3.Zero, (float)System.Math.PI));

        yield return resultSelector(x * m_a, y * m_b);
      }
    }

    /// <summary>
    /// <para>The linear eccentricity of an ellipse or hyperbola, denoted c (or sometimes f or e), is the distance between its center and either of its two foci.</para>
    /// <see href="https://en.wikipedia.org/wiki/Eccentricity_(mathematics)"/>
    /// <see href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    /// <remarks>In the case of ellipses and hyperbolas the linear eccentricity is sometimes called the half-focal separation.</remarks>
    public double LinearEccentricity => System.Math.Sqrt(System.Math.Pow(m_a, 2) - System.Math.Pow(m_b, 2));

    /// <summary>
    /// <para>First eccentricity.</para>
    /// <para>The eccentricity of a conic section is a non-negative real number that uniquely characterizes its shape. One can think of the eccentricity as a measure of how much a conic section deviates from being circular. The eccentricity of an ellipse which is not a circle is greater than zero but less than 1.</para>
    /// <see href="https://en.wikipedia.org/wiki/Eccentricity_(mathematics)"/>
    /// <see href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    public double FirstEccentricity => System.Math.Sqrt(1 - System.Math.Pow(m_b, 2) / System.Math.Pow(m_a, 2));

    /// <summary>
    /// <para>Second eccentricity.</para>
    /// <see href="https://en.wikipedia.org/wiki/Eccentricity_(mathematics)"/>
    /// <see href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    public double SecondEccentricity => System.Math.Sqrt(System.Math.Pow(m_a, 2) / System.Math.Pow(m_b, 2) - 1);

    /// <summary>
    /// <para>Third eccentricity.</para>
    /// <see href="https://en.wikipedia.org/wiki/Eccentricity_(mathematics)"/>
    /// <see href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    public double ThirdEccentricity
    {
      get
      {
        var a2 = System.Math.Pow(m_a, 2);
        var b2 = System.Math.Pow(m_b, 2);

        return System.Math.Sqrt(a2 - b2) / System.Math.Sqrt(a2 + b2);
      }
    }

    /// <summary>
    /// <para>First flattening.</para>
    /// <see href="https://en.wikipedia.org/wiki/Flattening"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    public double FirstFlattening => (m_a - m_b) / m_a;

    /// <summary>
    /// <para>Second flattening.</para>
    /// <see href="https://en.wikipedia.org/wiki/Flattening"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    public double SecondFlattening => (m_a - m_b) / m_b;

    /// <summary>
    /// <para>Third flattening.</para>
    /// <see href="https://en.wikipedia.org/wiki/Flattening"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Focus_(geometry)"/>
    /// </summary>
    public double ThirdFlattening => (m_a - m_b) / (m_a + m_b);

    #region Static methods

    /// <summary>Returns whether a point (<paramref name="x"/>, <paramref name="y"/>) is inside the optionally rotated (<paramref name="rotationAngle"/> in radians, the default 0 equals no rotation) ellipse with the the two specified semi-axes or radii (<paramref name="a"/>, <paramref name="b"/>). The ellipse <paramref name="a"/> and <paramref name="b"/> correspond to same axes as <paramref name="x"/> and <paramref name="y"/> of the point, respectively.</summary>
    public static bool EllipseContainsPoint(double a, double b, double x, double y, double rotationAngle = 0)
      => System.Math.Cos(rotationAngle) is var cos && System.Math.Sin(rotationAngle) is var sin && System.Math.Pow(cos * x + sin * y, 2) / (a * a) + System.Math.Pow(sin * x - cos * y, 2) / (b * b) <= 1;

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

    /// <summary>
    /// <para>This is a common recurring (unnamed, other than "H", AFAIK) formula in terms of ellipses. The parameters <paramref name="a"/> and <paramref name="b"/> are the lengths of the semi-major and semi-minor axes, respectively.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Ellipse#Circumference"/></para>
    /// </summary>
    /// <param name="a">The semi-major axis.</param>
    /// <param name="b">The semi-minor axis.</param>
    /// <returns>pow(a - b, 2) / pow(a + b, 2)</returns>
    public static double H(double a, double b) => System.Math.Pow(a - b, 2) / System.Math.Pow(a + b, 2);

    /// <summary>Returns the approximate circumference of an ellipse with the two semi-axis or radii <paramref name="a"/> and <paramref name="b"/> (the order of the arguments do not matter). Uses Ramanujans second approximation.</summary>
    public static double PerimeterOfEllipse(double a, double b)
    {
      var circle = System.Math.PI * (a + b); // (2 * PI * radius)

      if (a == b) // For a circle, use (PI * diameter);
        return circle;

      var h3 = 3 * H(a, b);

      return circle * (1 + h3 / (10 + System.Math.Sqrt(4 - h3)));
    }

    /// <summary>Returns the area of an ellipse with the two specified semi-axes or radii <paramref name="a"/> and <paramref name="b"/> (the order of the arguments do not matter).</summary>
    public static double SurfaceAreaOfEllipse(double a, double b) => System.Math.PI * a * b;

    #endregion Static methods
  }
}
