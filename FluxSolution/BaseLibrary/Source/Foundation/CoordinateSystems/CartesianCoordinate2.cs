namespace Flux
{
  /// <summary>Cartesian coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cartesian_coordinate_system"/>
  public struct CartesianCoordinate2
    : System.IEquatable<CartesianCoordinate2>
  {
    private readonly double m_x;
    private readonly double m_y;

    public CartesianCoordinate2(double x, double y)
    {
      m_x = x;
      m_y = y;
    }
    public CartesianCoordinate2(System.ValueTuple<double, double> xy)
      : this(xy.Item1, xy.Item2)
    { }

    public double X
      => m_x;
    public double Y
      => m_y;

    public Quantity.Angle ToRotationAngle()
      => new Quantity.Angle(ConvertToRotationAngle(m_x, m_y));
    public Quantity.Angle ToRotationAngleEx()
      => new Quantity.Angle(ConvertToRotationAngleEx(m_x, m_y));
    public PolarCoordinate ToPolarCoordinate()
      => new PolarCoordinate(ConvertToPolarCoordinate(m_x, m_y));

    #region Static methods
    public static (double radius, double azimuthRad) ConvertToPolarCoordinate(double x, double y)
      => (System.Math.Sqrt(x * x + y * y), System.Math.Atan2(y, x));
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double ConvertToRotationAngle(double x, double y)
      => System.Math.Atan2(y, x) is var atan2 && atan2 < 0 ? Maths.PiX2 + atan2 : atan2;
    /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
    public static double ConvertToRotationAngleEx(double x, double y)
      => Maths.PiX2 - ConvertToRotationAngle(y, -x); // Pass the cartesian vector (x, y) rotated 90 degrees counter-clockwise.
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(CartesianCoordinate2 a, CartesianCoordinate2 b)
      => a.Equals(b);
    public static bool operator !=(CartesianCoordinate2 a, CartesianCoordinate2 b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(CartesianCoordinate2 other)
      => m_x == other.m_x && m_y == other.m_y;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is CartesianCoordinate2 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_x, m_y);
    public override string ToString()
      => $"<{GetType().Name}: {m_x}, {m_y}>";
    #endregion Object overrides
  }
}
