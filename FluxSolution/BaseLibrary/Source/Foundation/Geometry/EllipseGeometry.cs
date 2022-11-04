namespace Flux
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public record struct EllipseGeometry
    : System.IComparable<EllipseGeometry>, ISurfaceArea, ISurfaceContains, ISurfacePerimeter
  {
    public static readonly EllipseGeometry Empty;

    public readonly double m_semiMinorAxis;
    public readonly double m_semiMajorAxis;

    public EllipseGeometry(double semiMinorAxis, double semiMajorAxis)
    {
      m_semiMinorAxis = System.Math.Min(semiMinorAxis, semiMajorAxis);
      m_semiMajorAxis = System.Math.Max(semiMinorAxis, semiMajorAxis);
    }

    /// <summary>The height (Y axis) of the ellipse.</summary>
    public double SemiMinorAxis
      => m_semiMinorAxis;
    /// <summary>The width (X axis) of the ellipse.</summary>
    public double SemiMajorAxis
      => m_semiMajorAxis;

    public double GetSurfaceArea()
      => SurfaceArea(m_semiMajorAxis, m_semiMinorAxis);
    public bool GetSurfaceContains(CartesianCoordinate2R point)
      => SurfaceContains(point.X, point.Y, m_semiMajorAxis, m_semiMinorAxis, 0);
    /// <summary>Returns the circumference of an ellipse based on the two semi-axis or radii a and b (the order of the arguments do not matter). Uses Ramanujans second approximation.</summary>
    public double GetSurfacePerimeter()
      => SurfacePerimeter(m_semiMajorAxis, m_semiMinorAxis);

    #region Static methods
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
    public static System.Collections.Generic.IEnumerable<TResult> CreateCircularArcPoints<TResult>(double numberOfPoints, double radiusX, double radiusY, System.Func<double, double, TResult> resultSelector, double offsetRadians = 0, double maxRandomVariation = 0)
    {
      var circularArc = Maths.PiX2 / numberOfPoints;

      for (var segment = 0; segment < numberOfPoints; segment++)
      {
        var angle = offsetRadians + segment * circularArc;

        if (maxRandomVariation > Flux.Maths.Epsilon1E7)
          angle += Random.NumberGenerators.Crypto.NextDouble(0, circularArc * maxRandomVariation);

        var (x, y) = Flux.Angle.ConvertRotationAngleToCartesian2Ex(angle);

        yield return resultSelector(x * radiusX, y * radiusY);
      }
    }

    /// <summary>Returns the eccentricity of the ellipse. The order of radii does not matter.</summary>
    public static double Eccentricity(double a, double b)
      => System.Math.Sqrt(1 - System.Math.Pow(System.Math.Min(a, b) / System.Math.Max(a, b), 2));

    /// <summary>Returns an Ellipse from the specified cartesian coordinates. The angle (radians) is derived as starting at a 90 degree angle (i.e. 3 o'clock), so not at the "top" as may be expected.</summary>
    public static EllipseGeometry FromCartesian(double x, double y)
      => new(System.Math.Sqrt(x * x + y * y), System.Math.Atan2(y, x));
    public static CartesianCoordinate2R ToCartesianCoordinate2(double semiMajorAxis, double semiMinorAxis, double angle)
      => new(System.Math.Cos(angle) * semiMajorAxis, System.Math.Sin(angle) * semiMinorAxis);

    /// <summary>This seem to be a common recurring (unnamed, other than "H", AFAIK) formula in terms of ellipses.</summary>
    public static double H(double a, double b)
      => System.Math.Pow(a - b, 2) / System.Math.Pow(a + b, 2);

    /// <summary>Returns the area of an ellipse based on two semi-axes or radii a and b (the order of the arguments do not matter).</summary>
    public static double SurfaceArea(double a, double b)
      => System.Math.PI * a * b;
    /// <summary>Returns whether the point is inside a potentially tilted ellipse.</summary>
    public static bool SurfaceContains(double pointX, double pointY, double radiusX, double radiusY, double angle)
      => System.Math.Cos(angle) is var cos && System.Math.Sin(angle) is var sin && System.Math.Pow(cos * pointX + sin * pointY, 2) / (radiusX * radiusX) + System.Math.Pow(sin * pointX - cos * pointY, 2) / (radiusY * radiusY) <= 1;
    /// <summary>Returns the circumference of an ellipse based on the two semi-axis or radii a and b (the order of the arguments do not matter). Uses Ramanujans second approximation.</summary>
    public static double SurfacePerimeter(double a, double b)
    {
      var circle = System.Math.PI * (a + b);

      if (a == b) // For a circle, use (PI * diameter);
        return circle;

      var h3 = H(a, b) * 3;

      return circle * (1.0 + h3 / (10.0 + System.Math.Sqrt(4.0 - h3)));
    }
    #endregion Static methods

    #region Implemented interfaces
    // IComparable<T>
    public int CompareTo(EllipseGeometry other)
      => (m_semiMajorAxis + m_semiMinorAxis).CompareTo(other.m_semiMajorAxis + other.m_semiMinorAxis);
    #endregion Implemented interfaces
  }
}
