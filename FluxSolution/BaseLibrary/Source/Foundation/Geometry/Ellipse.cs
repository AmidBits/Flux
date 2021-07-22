namespace Flux.Geometry
{
  public struct Ellipse
    : System.IEquatable<Ellipse>
  {
    public static readonly Ellipse Empty;
    public bool IsEmpty => Equals(Empty);

    /// <summary>The width (X axis) of the ellipse.</summary>
    public readonly double Width;
    /// <summary>The height (Y axis) of the ellipse.</summary>
    public readonly double Height;
    /// <summary>The angle (in radians) of rotational tilt.</summary>
    public readonly double Angle;

    public Ellipse(double width, double height, double angle)
    {
      Width = width;
      Height = height;
      Angle = angle;
    }
    public Ellipse(Size2 size, double angle)
      : this(size.Width, size.Height, angle)
    {
    }
    /// <summary>As a circle.</summary>
    public Ellipse(double radius, double angle)
      : this(radius * 2, radius * 2, angle)
    {
    }

    public static Ellipse ToEllipse(System.Numerics.Vector2 vector2)
      => new Ellipse(System.Math.Sqrt(vector2.X * vector2.X + vector2.Y * vector2.Y), System.Math.Atan2(vector2.Y, vector2.X));
    public static System.Numerics.Vector2 FromEllipse(Ellipse ellipse)
      => new System.Numerics.Vector2((float)(System.Math.Cos(ellipse.Angle) * ellipse.Width), (float)(System.Math.Sin(ellipse.Angle) * ellipse.Height));

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
          angle += Flux.Randomization.NumberGenerator.Crypto.NextDouble(0, circularArc * probabilityOfRandom);

        Flux.Quantity.Angle.ConvertRotationAngleToCartesianEx(angle, out var x, out var y);

        yield return resultSelector(x * radiusX, y * radiusY);
      }
    }

    /// <summary>Returns the area of an ellipse based on two semi-axes or radii a and b (the order of the arguments do not matter).</summary>
    public static double SurfaceArea(double radiusA, double radiusB)
      => System.Math.PI * radiusA * radiusB;
    /// <summary>Returns whether the point is inside a potentially tilted ellipse.</summary>
    public static bool SurfaceContains(double pointX, double pointY, double radiusX, double radiusY, double angle)
    {
      var cos = System.Math.Cos(angle);
      var sin = System.Math.Sin(angle);

      var a = cos * pointX + sin * pointY;
      a *= a; // pow(a, 2)
      var b = sin * pointX - cos * pointY;
      b *= b; // pow(b, 2)

      return a / (radiusX * radiusX) + b / (radiusY * radiusY) <= 1;
    }
    /// <summary>Returns the circumference of an ellipse based on the two semi-axis or radii a and b (the order of the arguments do not matter). Uses Ramanujans second approximation.</summary>
    public static double SurfacePerimeter(double radiusA, double radiusB)
    {
      var aMinusB = radiusA - radiusB;
      var aPlusB = radiusA + radiusB;

      var h = 3.0 * (aMinusB * aMinusB) / (aPlusB * aPlusB);

      return System.Math.PI * aPlusB * (1.0 + h / (10.0 + System.Math.Sqrt(4.0 - h)));
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
      => Width == other.Width && Height == other.Height && Angle == other.Angle;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Ellipse o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Width, Height, Angle);
    public override string? ToString()
      => $"<{GetType().Name}: {Width}, {Height}, {Quantity.Angle.FromUnitValue(Quantity.AngleUnit.Radian, Angle)}>";
    #endregion Object overrides
  }
}
