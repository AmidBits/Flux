namespace Flux.Geometry
{
  public struct Ellipse
    : System.IEquatable<Ellipse>
  {
    public static readonly Ellipse Empty;
    public bool IsEmpty => Equals(Empty);

    /// <summary>The width (X axis) of the ellipse.</summary>
    public readonly double m_width;
    /// <summary>The height (Y axis) of the ellipse.</summary>
    public readonly double m_height;
    /// <summary>The angle (in radians) of rotational tilt.</summary>
    public readonly double m_angle;

    public Ellipse(double width, double height, double angle)
    {
      m_width = width;
      m_height = height;
      m_angle = angle;
    }
    public Ellipse(Size2 size, double angle)
      : this(size.Width, size.Height, angle)
    { }
    /// <summary>As a circle.</summary>
    public Ellipse(double radius, double angle)
      : this(radius * 2, radius * 2, angle)
    { }

    public static Ellipse ToEllipse(System.Numerics.Vector2 vector2)
      => new Ellipse(System.Math.Sqrt(vector2.X * vector2.X + vector2.Y * vector2.Y), System.Math.Atan2(vector2.Y, vector2.X));
    public static System.Numerics.Vector2 FromEllipse(Ellipse ellipse)
      => new System.Numerics.Vector2((float)(System.Math.Cos(ellipse.m_angle) * ellipse.m_width), (float)(System.Math.Sin(ellipse.m_angle) * ellipse.m_height));

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
    public static System.Collections.Generic.IEnumerable<TResult> CreateCircularArcPoints<TResult>(double numberOfPoints, double radiusX, double radiusY, System.Func<double, double, TResult> resultSelector, double offsetRadians = 0, double probabilityOfRandom = 0)
    {
      var circularArc = Maths.PiX2 / numberOfPoints;

      for (var segment = 0; segment < numberOfPoints; segment++)
      {
        var angle = offsetRadians + segment * circularArc;

        if (probabilityOfRandom > Flux.Maths.Epsilon1E7)
          angle += Randomization.NumberGenerator.Crypto.NextDouble(0, circularArc * probabilityOfRandom);

        Quantity.Angle.ConvertRotationAngleToCartesianEx(angle, out var x, out var y);

        yield return resultSelector(x * radiusX, y * radiusY);
      }
    }

    /// <summary>Returns the eccentricity of the ellipse. The order of radii does not matter.</summary>
    public static double EccentricityEx(double a, double b)
      => Eccentricity(System.Math.Max(a, b), System.Math.Min(a, b));
    /// <summary>Returns the eccentricity of the ellipse. Note that the axes radii matter.</summary>
    /// <param name="majorRadius"></param>
    /// <param name="minorRadius"></param>
    public static double Eccentricity(double majorRadius, double minorRadius)
      => System.Math.Sqrt(1 - System.Math.Pow(minorRadius, 2) / System.Math.Pow(majorRadius, 2));
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
      var h = 3.0 * System.Math.Pow(a - b, 2) / System.Math.Pow(a + b, 2);

      return System.Math.PI * (a + b) * (1.0 + h / (10.0 + System.Math.Sqrt(4.0 - h)));
    }

    /// <summary>Returns an Ellipse from the specified cartesian coordinates. The angle (radians) is derived as starting at a 90 degree angle (i.e. 3 o'clock), so not at the "top" as may be expected.</summary>
    public static Ellipse FromCartesian(double x, double y)
      => new Ellipse(System.Math.Sqrt(x * x + y * y), System.Math.Atan2(y, x));

    #region Overloaded operators
    public static bool operator ==(Ellipse a, Ellipse b)
      => a.Equals(b);
    public static bool operator !=(Ellipse a, Ellipse b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(Ellipse other)
      => m_width == other.m_width && m_height == other.m_height && m_angle == other.m_angle;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Ellipse o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_width, m_height, m_angle);
    public override string? ToString()
      => $"<{GetType().Name}: {m_width}, {m_height}, {Quantity.Angle.FromUnitValue(Quantity.AngleUnit.Radian, m_angle)}>";
    #endregion Object overrides
  }
}
