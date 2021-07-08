namespace Flux.Units
{
  /// <summary>Azimuth.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Azimuth"/>
  public struct Azimuth
    : System.IComparable<Azimuth>, System.IEquatable<Azimuth>, IStandardizedScalar
  {
    public const double MaxValue = 360;
    public const double MinValue = 0;

    public const double North = 0;
    public const double NorthNorthEast = 22.5;
    public const double NorthEast = 45;
    public const double EastNorthEast = 67.5;
    public const double East = 90;
    public const double EastSouthEast = 112.5;
    public const double SouthEast = 135;
    public const double SouthSouthEast = 157.5;
    public const double South = 180;
    public const double SouthSouthWest = 202.5;
    public const double SouthWest = 225;
    public const double WestSouthWest = 247.5;
    public const double West = 270;
    public const double WestNorthWest = 292.5;
    public const double NorthWest = 315;
    public const double NorthNorthWest = 337.5;

    private readonly Angle m_angle;

    public Azimuth(double degree)
      => m_angle = Angle.FromUnitValue(AngleUnit.Degree, Maths.Wrap(degree, MinValue, MaxValue) % MaxValue);
    public Azimuth(Angle angle)
      : this(angle.Degree) // Call base to ensure value is between min/max.
    { }

    public Angle Angle
      => m_angle;

    #region Overloaded operators
    public static explicit operator double(Azimuth v)
     => v.m_angle.Degree;
    public static explicit operator Azimuth(double v)
      => new Azimuth(v);

    public static bool operator <(Azimuth a, Azimuth b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Azimuth a, Azimuth b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Azimuth a, Azimuth b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Azimuth a, Azimuth b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Azimuth a, Azimuth b)
      => a.Equals(b);
    public static bool operator !=(Azimuth a, Azimuth b)
      => !a.Equals(b);

    public static Azimuth operator -(Azimuth v)
      => new Azimuth(-v.Angle);
    public static Azimuth operator +(Azimuth a, Azimuth b)
      => new Azimuth(a.Angle + b.Angle);
    public static Azimuth operator +(Azimuth a, double b)
      => new Azimuth(a.Angle.Degree + b);
    public static Azimuth operator +(double a, Azimuth b)
      => new Azimuth(a + b.Angle.Degree);
    public static Azimuth operator /(Azimuth a, Azimuth b)
      => new Azimuth(a.Angle / b.Angle);
    public static Azimuth operator /(Azimuth a, double b)
      => new Azimuth(a.Angle.Degree / b);
    public static Azimuth operator /(double a, Azimuth b)
      => new Azimuth(a / b.Angle.Degree);
    public static Azimuth operator *(Azimuth a, Azimuth b)
      => new Azimuth(a.Angle * b.Angle);
    public static Azimuth operator *(Azimuth a, double b)
      => new Azimuth(a.Angle.Degree * b);
    public static Azimuth operator *(double a, Azimuth b)
      => new Azimuth(a * b.Angle.Degree);
    public static Azimuth operator %(Azimuth a, Azimuth b)
      => new Azimuth(a.Angle % b.Angle);
    public static Azimuth operator %(Azimuth a, double b)
      => new Azimuth(a.Angle.Degree % b);
    public static Azimuth operator %(double a, Azimuth b)
      => new Azimuth(a % b.Angle.Degree);
    public static Azimuth operator -(Azimuth a, Azimuth b)
      => new Azimuth(a.Angle - b.Angle);
    public static Azimuth operator -(Azimuth a, double b)
      => new Azimuth(a.Angle.Degree - b);
    public static Azimuth operator -(double a, Azimuth b)
      => new Azimuth(a - b.Angle.Degree);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Azimuth other)
      => m_angle.CompareTo(other.m_angle);

    // IEquatable
    public bool Equals(Azimuth other)
      => m_angle == other.m_angle;

    // IUnitStandardized
    public double GetScalar()
      => m_angle.Degree;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Azimuth o && Equals(o);
    public override int GetHashCode()
      => m_angle.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_angle.Degree}\u00B0>";
    #endregion Object overrides
  }
}
