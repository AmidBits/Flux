namespace Flux
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct CircleGeometry
    : System.IComparable<CircleGeometry>, System.IEquatable<CircleGeometry>, ISurfaceArea, ISurfaceContains, ISurfacePerimeter
  {
    public static readonly CircleGeometry Empty;

    /// <summary>The radius of the circle.</summary>
    private readonly double m_radius;

    public CircleGeometry(double radius)
    {
      m_radius = radius;
    }

    public double Radius
      => m_radius;

    public double GetSurfaceArea()
      => SurfaceArea(m_radius);
    public bool GetSurfaceContains(CartesianCoordinateR2 cc2)
      => SurfaceContains(cc2.X, cc2.Y, m_radius);
    public double GetSurfacePerimeter()
      => System.Math.PI * System.Math.Pow(m_radius, 2);

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
    public static System.Collections.Generic.IEnumerable<TResult> CreateCircularArcPoints<TResult>(double numberOfPoints, double radius, System.Func<double, double, TResult> resultSelector, double offsetRadians = 0, double maxRandomVariation = 0)
    {
      var circularArc = Maths.PiX2 / numberOfPoints;

      for (var segment = 0; segment < numberOfPoints; segment++)
      {
        var angle = offsetRadians + segment * circularArc;

        if (maxRandomVariation > Flux.Maths.Epsilon1E7)
          angle += Randomization.NumberGenerator.Crypto.NextDouble(0, circularArc * maxRandomVariation);

        var (x, y) = Flux.Angle.ConvertRotationAngleToCartesian2Ex(angle);

        yield return resultSelector(x * radius, y * radius);
      }
    }

    /// <summary>Returns the area of an ellipse based on two semi-axes or radii a and b (the order of the arguments do not matter).</summary>
    public static double SurfaceArea(double radius)
      => System.Math.PI * System.Math.Pow(radius, 2);
    /// <summary>Returns whether the point is inside a potentially tilted ellipse.</summary>
    public static bool SurfaceContains(double x, double y, double radius)
      => System.Math.Pow(x, 2) + System.Math.Pow(y, 2) <= System.Math.Pow(radius, 2);
    /// <summary>Returns the circumference of an ellipse based on the two semi-axis or radii a and b (the order of the arguments do not matter). Uses Ramanujans second approximation.</summary>
    public static double SurfacePerimeter(double radius)
      => System.Math.PI * radius * 2;
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(CircleGeometry a, CircleGeometry b)
      => a.Equals(b);
    public static bool operator !=(CircleGeometry a, CircleGeometry b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable<T>
    public int CompareTo(CircleGeometry other)
      => m_radius.CompareTo(other.m_radius);

    // IEquatable<T>
    public bool Equals(CircleGeometry other)
      => m_radius == other.m_radius;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is CircleGeometry o && Equals(o);
    public override int GetHashCode()
      => m_radius.GetHashCode();
    public override string? ToString()
      => $"{GetType().Name} {{ Radius = {m_radius} }}";
    #endregion Object overrides
  }
}
