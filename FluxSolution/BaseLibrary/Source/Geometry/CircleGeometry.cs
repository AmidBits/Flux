using Flux.Quantities;

namespace Flux
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct CircleGeometry
  {
    public static readonly CircleGeometry Empty;

    /// <summary>The radius of the circle.</summary>
    private readonly double m_radius;

    public CircleGeometry(double radius) => m_radius = radius;

    public double Radius => m_radius;

    /// <summary>Returns the area of an ellipse based on two semi-axes or radii a and b (the order of the arguments do not matter).</summary>
    public double Area => double.Pi * double.Pow(m_radius, 2);

    /// <summary>Returns the circumference of an ellipse based on the two semi-axis or radii a and b (the order of the arguments do not matter). Uses Ramanujans second approximation.</summary>
    public double Circumference => 2 * double.Pi * m_radius;

    /// <summary>Returns whether the point is inside a potentially tilted ellipse.</summary>
    public bool Contains(double x, double y) => double.Pow(x, 2) + double.Pow(y, 2) <= double.Pow(m_radius, 2);

    /// <summary>Creates a elliptical polygon with random vertices from the specified number of segments, width, height and an optional random variance unit interval (toward 0 = least random, toward 1 = most random).
    /// Flux.Media.Geometry.Ellipse.CreatePoints(3, 100, 100, 0); // triangle, horizontally pointy
    /// Flux.Media.Geometry.Ellipse.CreatePoints(3, 100, 100, Flux.Math.Pi.DivBy6); // triangle, vertically pointy
    /// Flux.Media.Geometry.Ellipse.CreatePoints(4, 100, 100, 0); // rectangle, horizontally and vertically pointy
    /// Flux.Media.Geometry.Ellipse.CreatePoints(4, 100, 100, Flux.Math.Pi.DivBy4); // rectangle, vertically and horizontally flat
    /// Flux.Media.Geometry.Ellipse.CreatePoints(5, 100, 100, 0); // pentagon, horizontally pointy
    /// Flux.Media.Geometry.Ellipse.CreatePoints(5, 100, 100, Flux.Math.Pi.DivBy6); // pentagon, vertically pointy
    /// Flux.Media.Geometry.Ellipse.CreatePoints(6, 100, 100, 0); // hexagon, vertically flat (or horizontally pointy)
    /// Flux.Media.Geometry.Ellipse.CreatePoints(6, 100, 100, Flux.Math.Pi.DivBy6); // hexagon, horizontally flat (or vertically pointy)
    /// Flux.Media.Geometry.Ellipse.CreatePoints(8, 100, 100, 0); // octagon, horizontally and vertically pointy
    /// Flux.Media.Geometry.Ellipse.CreatePoints(8, 100, 100, Flux.Math.Pi.DivBy8); // octagon, vertically and horizontally flat
    /// </summary>
    public System.Collections.Generic.IEnumerable<TResult> CreateCircularArcPoints<TResult>(double numberOfPoints, System.Func<double, double, TResult> resultSelector, double offsetRadians = 0, double maxRandomVariation = 0)
    {
      var circularArc = double.Tau / numberOfPoints;

      for (var segment = 0; segment < numberOfPoints; segment++)
      {
        var angle = offsetRadians + segment * circularArc;

        if (maxRandomVariation > GenericMath.Epsilon1E7)
          angle += Random.NumberGenerators.Crypto.NextDouble(0, circularArc * maxRandomVariation);

        var (x, y) = Flux.Convert.RotationAngleToCartesian2Ex(angle);

        yield return resultSelector(x * m_radius, y * m_radius);
      }
    }

    /// <summary></summary>
    public Numerics.CartesianCoordinate2<double> ToCartesianCoordinate2(double radRotationAngle = 0)
      => (Numerics.CartesianCoordinate2<double>)Flux.Convert.RotationAngleToCartesian2Ex(radRotationAngle) * m_radius;

    public override string ToString() => $"{GetType().Name} {{ {m_radius} }}";
  }
}
