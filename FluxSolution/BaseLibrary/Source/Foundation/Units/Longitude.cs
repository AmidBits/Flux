namespace Flux.Units
{
  /// <summary>Longitude is a geographic coordinate that specifies the east–west position of a point on the Earth's surface, or the surface of a celestial body. The unit here is defined in the range [-180, +180] in relation to the prime meridian, by convention.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Longitude"/>
  public struct Longitude
    : System.IComparable<Longitude>, System.IEquatable<Longitude>, IStandardizedScalar
  {
    public const double MaxValue = +180;
    public const double MinValue = -180;

    private readonly Angle m_angle;

    public Longitude(double degree)
      => m_angle = Angle.FromDegree(Maths.Wrap(degree, MinValue, MaxValue));
    public Longitude(Angle angle)
      : this(angle.Degree)
    { }

    public Angle Angle
      => m_angle;

    public int TheoreticalTimezoneOffset
      => ComputeTheoreticalTimezoneOffset(m_angle.Degree);

    #region Static methods
    public static Longitude Add(Longitude left, Longitude right)
      => new Longitude(left.m_angle + right.m_angle);
    public static int ComputeTheoreticalTimezoneOffset(double longitude)
      => (int)Maths.RoundToNearest((longitude + System.Math.CopySign(7.5, longitude)) / 15, RoundingBehavior.RoundTowardZero);
    public static Longitude Divide(Longitude left, Longitude right)
      => new Longitude(left.m_angle / right.m_angle);
    public static Longitude Multiply(Longitude left, Longitude right)
      => new Longitude(left.m_angle * right.m_angle);
    public static Longitude Negate(Longitude value)
      => new Longitude(-value.m_angle);
    public static Longitude Remainder(Longitude dividend, Longitude divisor)
      => new Longitude(dividend.m_angle % divisor.m_angle);
    public static Longitude Subtract(Longitude left, Longitude right)
      => new Longitude(left.m_angle - right.m_angle);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Longitude v)
      => v.m_angle.Degree;
    public static explicit operator Longitude(double v)
      => new Longitude(v);

    public static bool operator <(Longitude a, Longitude b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Longitude a, Longitude b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Longitude a, Longitude b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Longitude a, Longitude b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Longitude a, Longitude b)
      => a.Equals(b);
    public static bool operator !=(Longitude a, Longitude b)
      => !a.Equals(b);

    public static Longitude operator +(Longitude a, Longitude b)
      => Add(a, b);
    public static Longitude operator /(Longitude a, Longitude b)
      => Divide(a, b);
    public static Longitude operator *(Longitude a, Longitude b)
      => Multiply(a, b);
    public static Longitude operator -(Longitude v)
      => Negate(v);
    public static Longitude operator %(Longitude a, Longitude b)
      => Remainder(a, b);
    public static Longitude operator -(Longitude a, Longitude b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Longitude other)
      => m_angle.CompareTo(other.m_angle);

    // IEquatable
    public bool Equals(Longitude other)
      => m_angle == other.m_angle;

    // IUnitStandardized
    public double GetScalar()
      => m_angle.Degree;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Longitude o && Equals(o);
    public override int GetHashCode()
      => m_angle.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_angle.Degree}\u00B0>";
    #endregion Object overrides
  }
}
