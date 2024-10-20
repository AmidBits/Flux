namespace Flux.Geometry
{
  /// <summary>
  /// <para></para>
  /// <see href="https://en.wikipedia.org/wiki/Circle"/>
  /// </summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct CircleGeometry
  {
    public static CircleGeometry Unit { get; } = new(1);

    private readonly double m_radius;

    public CircleGeometry(double radius) => m_radius = radius;

    /// <summary>
    /// <para>The radius of the circle.</para>
    /// </summary>
    public double Radius => m_radius;

    /// <summary>Returns the area of circle.</summary>
    public double Area => Coordinates.PolarCoordinate.SurfaceAreaOfCircle(m_radius);

    /// <summary>Returns the circumference of the circle.</summary>
    public double Circumference => Coordinates.PolarCoordinate.PerimeterOfCircle(m_radius);

    /// <summary>Returns whether a point is inside the circle.</summary>
    public bool Contains(double x, double y) => ContainsPoint(m_radius, x, y);

    /// <summary>
    /// <para>Creates a circle consisting of <paramref name="count"/> vertices transformed with <paramref name="resultSelector"/> starting at <paramref name="radOffset"/> and optional <paramref name="maxRandomness"/> (using <paramref name="rng"/>) unit interval (toward 0 = no random, toward 1 = total random).</para>
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
    /// <param name="count"></param>
    /// <param name="resultSelector">The selector that determines the result (<typeparamref name="TResult"/>) for each vector.</param>
    /// <param name="radOffset">The offset in radians to apply to each vector.</param>
    /// <param name="maxRandomness">The maximum randomness to allow for each vector. Must be in the range [0, 0.5].</param>
    /// <param name="rng">The random number generator to use, or default if null.</param>
    /// <returns>A new sequence of <typeparamref name="TResult"/>.</returns>
    public static System.Collections.Generic.IEnumerable<TResult> CreateVectors<TResult>(double count, System.Func<double, double, TResult> resultSelector, double radius = 1, double radOffset = 0, double maxRandomness = 0, System.Random? rng = null)
    {
      rng ??= System.Random.Shared;

      var circularArc = System.Math.Tau / count;

      for (var segment = 0; segment < count; segment++)
      {
        var angle = radOffset + segment * circularArc;

        if (maxRandomness > 0)
          angle += rng.NextDouble(0, circularArc * maxRandomness);

        var (x, y) = Coordinates.PolarCoordinate.ConvertPolarToCartesian2Ex(radius, angle);

        yield return resultSelector(x, y);
      }
    }

    #region Static methods

    /// <summary>Returns whether a point (<paramref name="x"/>, <paramref name="y"/>) is inside of a circle with the specified <paramref name="radius"/>.</summary>
    public static bool ContainsPoint(double radius, double x, double y) => System.Math.Pow(x, 2) + System.Math.Pow(y, 2) <= System.Math.Pow(radius, 2);

    #endregion // Static methods
  }
}
