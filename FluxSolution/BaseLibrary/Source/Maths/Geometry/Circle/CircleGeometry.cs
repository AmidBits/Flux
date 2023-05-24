namespace Flux.Geometry
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct CircleGeometry
  {
    /// <summary>The radius of the circle.</summary>
    private readonly double m_radius;

    public CircleGeometry(double radius) => m_radius = radius;

    public double Radius => m_radius;

    /// <summary>Returns the area of circle.</summary>
    public double Area => System.Math.PI * m_radius * m_radius;

    /// <summary>Returns the circumference of the circle.</summary>
    public double Circumference => 2 * System.Math.PI * m_radius;

    /// <summary>Returns whether a point is inside the circle.</summary>
    public bool Contains(double x, double y) => System.Math.Pow(x, 2) + System.Math.Pow(y, 2) <= System.Math.Pow(m_radius, 2);

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
    /// <typeparam name="TResult"></typeparam>
    /// <param name="resultSelector">The selector that determines the result (<typeparamref name="TResult"/>) for each vector.</param>
    /// <param name="radOffset">The offset in radians to apply to each vector.</param>
    /// <param name="maxRandomness">The maximum randomness to allow for each vector. Must be in the range [0, 0.5].</param>
    /// <param name="rng">The random number generator to use, or default if null.</param>
    /// <returns>A new sequence of <typeparamref name="TResult"/>.</returns>
    public System.Collections.Generic.IEnumerable<TResult> CreateVectors<TResult>(double count, System.Func<double, double, TResult> resultSelector, double radOffset = 0, double maxRandomness = 0, System.Random? rng = null)
    {
      rng ??= Random.NumberGenerators.Crypto;

      var circularArc = System.Math.Tau / count;

      for (var segment = 0; segment < count; segment++)
      {
        var angle = radOffset + segment * circularArc;

        if (maxRandomness > 0)
          angle += rng.NextDouble(0, circularArc * maxRandomness);

        var (x, y) = Convert.RotationAngleToCartesian2Ex(angle);

        yield return resultSelector(x * m_radius, y * m_radius);
      }
    }

#if NET7_0_OR_GREATER
    /// <summary></summary>
    public CartesianCoordinate2<double> ToCartesianCoordinate2(double rotationAngle = 0)
      => new(
        System.Math.Cos(rotationAngle) * m_radius,
        System.Math.Sin(rotationAngle) * m_radius
      );
#endif

    public HexagonGeometry ToHexagonGeometry() => new(m_radius);

    public EllipseGeometry ToEllipseGeometry() => new(m_radius, m_radius);
  }
}
