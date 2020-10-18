namespace Flux.Geometry
{
  public struct Ellipse
    : System.IEquatable<Ellipse>, System.IFormattable
  {
    public static readonly Ellipse Empty;
    public bool IsEmpty => Equals(Empty);

    /// <summary>The height (Y axis) of the ellipse.</summary>
    public double Height { get; set; }
    /// <summary>The width (X axis) of the ellipse.</summary>
    public double Width { get; set; }

    /// <summary>The angle (in radians) of rotational tilt.</summary>
    public double Angle { get; set; }

    public Ellipse(double height, double width, double angle)
    {
      Height = height;
      Width = width;
      Angle = angle;
    }
    /// <summary>As a circle.</summary>
    public Ellipse(double radius, double angle)
    {
      Height = radius;
      Width = radius;
      Angle = angle;
    }

    public static Ellipse ToEllipse(System.Numerics.Vector2 vector2)
      => new Ellipse(System.Math.Sqrt(vector2.X * vector2.X + vector2.Y * vector2.Y), System.Math.Atan2(vector2.Y, vector2.X));
    public static System.Numerics.Vector2 FromEllipse(Ellipse ellipse)
      => new System.Numerics.Vector2((float)(System.Math.Cos(ellipse.Angle) * ellipse.Width), (float)(System.Math.Sin(ellipse.Angle) * ellipse.Height));

    /// <summary>Creates a elliptical polygon with random vertices from the specified number of segments, width, height and an optional random variance unit interval (toward 0 = least random, toward 1 = most random).
    /// Flux.Media.Geometry.Ellipse.Create(3, 100, 100, 0); // triangle, horizontally pointy
    /// Flux.Media.Geometry.Ellipse.Create(3, 100, 100, Flux.Math.Pi.DivBy6); // triangle, vertically pointy
    /// Flux.Media.Geometry.Ellipse.Create(4, 100, 100, 0); // rectangle, horizontally and vertically pointy
    /// Flux.Media.Geometry.Ellipse.Create(4, 100, 100, Flux.Math.Pi.DivBy4); // rectangle, vertically and horizontally flat
    /// Flux.Media.Geometry.Ellipse.Create(5, 100, 100, 0); // pentagon, horizontally pointy
    /// Flux.Media.Geometry.Ellipse.Create(5, 100, 100, Flux.Math.Pi.DivBy6); // pentagon, vertically pointy
    /// Flux.Media.Geometry.Ellipse.Create(6, 100, 100, 0); // hexagon, vertically flat (or horizontally pointy)
    /// Flux.Media.Geometry.Ellipse.Create(6, 100, 100, Flux.Math.Pi.DivBy6); // hexagon, horizontally flat (or vertically pointy)
    /// Flux.Media.Geometry.Ellipse.Create(8, 100, 100, 0); // octagon, horizontally and vertically pointy
    /// Flux.Media.Geometry.Ellipse.Create(8, 100, 100, Flux.Math.Pi.DivBy8); // octagon, vertically and horizontally flat
    /// </summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector2> Create(double segments, double radiusX, double radiusY, double offsetRadians = 0, double randomnessUnitInterval = 0)
    {
      offsetRadians += -Flux.Maths.PiOver2;

      var segmentRadian = Flux.Maths.PiX2 / segments;

      var rng = new System.Random();

      for (var segment = 0; segment < segments; segment++)
      {
        var angle = offsetRadians + segment * segmentRadian;

        if (randomnessUnitInterval > double.Epsilon)
        {
          angle += rng.NextDouble(segmentRadian * (1 - randomnessUnitInterval), segmentRadian);
        }

        yield return ToCartesian(radiusX, radiusY, angle);
      }
    }

    /// Flux.Media.Geometry.Ellipse.Create(7, 100, 100, Flux.Math.Pi.DivBy7); // heptagon, flat top
    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector2> CreateHeptagon(double width, double height, double offsetRadians = Flux.Maths.PiOver7)
      => Create(7, width, height, offsetRadians);
    /// Flux.Media.Geometry.Ellipse.Create(6, 100, 100, Flux.Math.Pi.DivBy6); // hexagon, flat top
    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector2> CreateHexagon(double width, double height, double offsetRadians = Flux.Maths.PiOver6)
      => Create(6, width, height, offsetRadians);
    /// Flux.Media.Geometry.Ellipse.Create(9, 100, 100, Flux.Math.Pi.DivBy9); // nonagon, flat top
    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector2> CreateNonagon(double width, double height, double offsetRadians = Flux.Maths.PiOver9)
      => Create(9, width, height, offsetRadians);
    /// Flux.Media.Geometry.Ellipse.Create(8, 100, 100, Flux.Math.Pi.DivBy8); // octagon, flat top
    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector2> CreateOctagon(double width, double height, double offsetRadians = Flux.Maths.PiOver8)
      => Create(8, width, height, offsetRadians);
    /// Flux.Media.Geometry.Ellipse.Create(5, 100, 100, Flux.Math.Pi.DivBy5); // pentagon, flat top
    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector2> CreatePentagon(double width, double height, double offsetRadians = Flux.Maths.PiOver5)
      => Create(5, width, height, offsetRadians);
    /// Flux.Media.Geometry.Ellipse.Create(4, 100, 100, Flux.Math.Pi.DivBy4); // square, flat top
    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector2> CreateSquare(double width, double height, double offsetRadians = Flux.Maths.PiOver4)
      => Create(4, width, height, offsetRadians);
    /// Flux.Media.Geometry.Ellipse.Create(3, 100, 100, Flux.Math.Pi.DivBy3); // triangle, flat top
    public static System.Collections.Generic.IEnumerable<System.Numerics.Vector2> CreateTriangle(double width, double height, double offsetRadians = Flux.Maths.PiOver3)
      => Create(3, width, height, offsetRadians);

    /// <summary>Returns the area of an ellipse based on two semi-axes or radii a and b (the order of the arguments do not matter).</summary>
    public static double SurfaceArea(double radiusA, double radiusB) => System.Math.PI * radiusA * radiusB;
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
    /// <summary>Returns a vector from the specified elliptical (circular) properties. The angle (radians) is expected as starting at a 90 degree angle (i.e. 3 o'clock), so not at the "top" as may be expected.</summary>
    public static System.Numerics.Vector2 ToCartesian(double radius, double angle)
      => new System.Numerics.Vector2((float)(System.Math.Cos(angle) * radius), (float)(System.Math.Sin(angle) * radius));
    /// <summary>Returns a vector from the specified elliptical properties. The angle (radians) is expected as starting at a 90 degree angle (i.e. 3 o'clock), so not at the "top" as may be expected.</summary>
    public static System.Numerics.Vector2 ToCartesian(double radiusX, double radiusY, double angle)
      => new System.Numerics.Vector2((float)(System.Math.Cos(angle) * radiusX), (float)(System.Math.Sin(angle) * radiusY));

    // Operators
    public static bool operator ==(Ellipse a, Ellipse b)
      => a.Equals(b);
    public static bool operator !=(Ellipse a, Ellipse b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals(Ellipse other)
      => Angle == other.Angle && Height == other.Height && Width == other.Width;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? provider)
      => $"<{nameof(Ellipse)}: {Width.ToString(format, provider)}, {Height.ToString(format, provider)}, {Angle.ToString(format, provider)}>";

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Ellipse o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Angle, Height, Width);
    public override string? ToString()
      => ToString(default, System.Globalization.CultureInfo.CurrentCulture);
  }
}
