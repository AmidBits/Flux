namespace Flux.Quantity
{
  /// <summary>Azimuth unit of degree.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Azimuth"/>
  public struct Bearing
    : System.IComparable<Bearing>, System.IEquatable<Bearing>, IValuedUnit
  {
    //public static Interval Interval
    //  => new Interval(false, true, 0, 360);

    public const double MaxValue = 360;
    public const double MinValue = 0;

    private readonly double m_value;

    public Bearing(double degree)
      => m_value = IsBearing(degree) ? degree : Maths.Wrap(degree, MinValue, MaxValue) % MaxValue;
    public Bearing(Angle angle)
      : this(angle.Degree) // Call base to ensure value is between min/max.
    { }

    public double Radian
      => Angle.ConvertDegreeToRadian(m_value);
    public double Value
      => m_value;

    public Angle ToAngle()
      => new Angle(Radian);

    #region Static methods
    /// <summary>Returns whether the specified bearing (in degrees) is a valid bearing, i.e. [0, 360).</summary>
    public static bool IsBearing(double degBearing)
      => degBearing >= 0 && degBearing < 360;
    /// <summary>Returns the bearing needle latched to one of the specified number of positions around the compass. For example, 4 positions will return an index [0, 3] (of four) for the latched bearing.</summary>
    public static int LatchNeedle(double radBearing, int positions)
      => (int)System.Math.Round(Maths.Wrap(radBearing, 0, Maths.PiX2) / (Maths.PiX2 / positions) % positions);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Bearing v)
     => v.m_value;
    public static explicit operator Bearing(double v)
      => new Bearing(v);

    public static bool operator <(Bearing a, Bearing b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Bearing a, Bearing b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Bearing a, Bearing b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Bearing a, Bearing b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Bearing a, Bearing b)
      => a.Equals(b);
    public static bool operator !=(Bearing a, Bearing b)
      => !a.Equals(b);

    public static Bearing operator -(Bearing v)
      => new Bearing(-v.m_value);
    public static Bearing operator +(Bearing a, double b)
      => new Bearing(a.m_value + b);
    public static Bearing operator +(Bearing a, Bearing b)
      => a + b.Value;
    public static Bearing operator /(Bearing a, double b)
      => new Bearing(a.m_value / b);
    public static Bearing operator /(Bearing a, Bearing b)
      => a / b.Value;
    public static Bearing operator *(Bearing a, double b)
      => new Bearing(a.m_value * b);
    public static Bearing operator *(Bearing a, Bearing b)
      => a * b.Value;
    public static Bearing operator %(Bearing a, double b)
      => new Bearing(a.m_value % b);
    public static Bearing operator %(Bearing a, Bearing b)
      => a % b.Value;
    public static Bearing operator -(Bearing a, double b)
      => new Bearing(a.m_value - b);
    public static Bearing operator -(Bearing a, Bearing b)
      => a - b.Value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Bearing other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Bearing other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Bearing o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value}{Angle.DegreeSymbol}>";
    #endregion Object overrides
  }
}
