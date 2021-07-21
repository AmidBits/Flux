namespace Flux.Units
{
  /// <summary>Longitude, unit of degree, is a geographic coordinate that specifies the east–west position of a point on the Earth's surface, or the surface of a celestial body. The unit here is defined in the range [-180, +180] in relation to the prime meridian, by convention.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Longitude"/>
  public struct Longitude
    : System.IComparable<Longitude>, System.IEquatable<Longitude>, IValuedUnit
  {
    public const double MaxValue = +180;
    public const double MinValue = -180;

    private readonly Angle m_value;

    public Longitude(double degree)
      => m_value = Angle.FromUnitValue(AngleUnit.Degree, Maths.Wrap(degree, MinValue, MaxValue));
    public Longitude(Angle angle)
      : this(angle.Degree) // Call base to ensure value is between min/max.
    { }

    public Angle Angle
      => m_value;

    public double Value
      => m_value.Degree;

    public int TheoreticalTimezoneOffset
      => ComputeTheoreticalTimezoneOffset(m_value.Degree);

    #region Static methods
    public static int ComputeTheoreticalTimezoneOffset(double longitude)
      => (int)Maths.RoundToNearest((longitude + System.Math.CopySign(7.5, longitude)) / 15, RoundingBehavior.RoundTowardZero);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Longitude v)
      => v.m_value.Degree;
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

    public static Longitude operator -(Longitude v)
      => new Longitude(-v.m_value);
    public static Longitude operator +(Longitude a, Longitude b)
      => new Longitude(a.m_value + b.m_value);
    public static Longitude operator /(Longitude a, Longitude b)
      => new Longitude(a.m_value / b.m_value);
    public static Longitude operator *(Longitude a, Longitude b)
      => new Longitude(a.m_value * b.m_value);
    public static Longitude operator %(Longitude a, Longitude b)
      => new Longitude(a.m_value % b.m_value);
    public static Longitude operator -(Longitude a, Longitude b)
      => new Longitude(a.m_value - b.m_value);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Longitude other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Longitude other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Longitude o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value.Degree}\u00B0>";
    #endregion Object overrides
  }
}
