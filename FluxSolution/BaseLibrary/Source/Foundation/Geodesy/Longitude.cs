namespace Flux
{
  /// <summary>Longitude, unit of degree, is a geographic coordinate that specifies the east–west position of a point on the Earth's surface, or the surface of a celestial body. The unit here is defined in the range [-180, +180] in relation to the prime meridian, by convention.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Longitude"/>
  public struct Longitude
    : System.IComparable<Longitude>, System.IEquatable<Longitude>, Quantity.IValuedUnit
  {
    public const double MaxValue = +180;
    public const double MinValue = -180;

    private readonly double m_value;

    public Longitude(double degLongitude)
      => m_value = IsLongitude(degLongitude) ? degLongitude : throw new System.ArgumentOutOfRangeException(nameof(degLongitude));
    public Longitude(Quantity.Angle angle)
      : this(angle.ToUnitValue(Quantity.AngleUnit.Degree)) // Call base to ensure value is between min/max.
    { }

    /// <summary>Computes the theoretical timezone offset, relative prime meridian. This can be used for a rough timezone estimate.</summary>
    public int TheoreticalTimezoneOffset
      => GetTheoreticalTimezoneOffset(m_value);

    /// <summary>Projects the longitude to a mercator X value in the range [-PI, PI].</summary>
    /// https://en.wikipedia.org/wiki/Mercator_projection
    /// https://en.wikipedia.org/wiki/Web_Mercator_projection#Formulas
    public double GetMercatorProjectedX()
      => Radian;

    public double Radian
      => Quantity.Angle.ConvertDegreeToRadian(m_value);

    public double Value
      => m_value;

    #region Static methods
    /// <summary>Returns the theoretical time zone offset, relative prime meridian. There are many places with deviations across all time zones.</summary>
    /// <param name="degLongitude"></param>
    public static int GetTheoreticalTimezoneOffset(double degLongitude)
      => (int)Maths.RoundTo((degLongitude + System.Math.CopySign(7.5, degLongitude)) / 15, FullRoundingBehavior.TowardZero);

    /// <summary>Returns whether the specified longitude (in degrees) is a valid longitude, i.e. [-180, +180].</summary>
    public static bool IsLongitude(double degLongitude)
      => degLongitude >= MinValue && degLongitude <= MaxValue;
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Longitude v)
      => v.m_value;
    public static explicit operator Longitude(double v)
      => new Longitude(v);

    public static bool operator <(Longitude a, Longitude b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Longitude a, Longitude b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Longitude a, Longitude b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Longitude a, Longitude b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Longitude a, Longitude b)
      => a.Equals(b);
    public static bool operator !=(Longitude a, Longitude b)
      => !a.Equals(b);

    public static Longitude operator -(Longitude v)
      => new Longitude(-v.m_value);
    public static Longitude operator +(Longitude a, double b)
      => new Longitude(Maths.Wrap(a.m_value + b, MinValue, MaxValue));
    public static Longitude operator +(Longitude a, Longitude b)
      => a + b.Value;
    public static Longitude operator /(Longitude a, double b)
      => new Longitude(Maths.Wrap(a.m_value / b, MinValue, MaxValue));
    public static Longitude operator /(Longitude a, Longitude b)
      => a / b.Value;
    public static Longitude operator *(Longitude a, double b)
      => new Longitude(Maths.Wrap(a.m_value * b, MinValue, MaxValue));
    public static Longitude operator *(Longitude a, Longitude b)
      => a * b.Value;
    public static Longitude operator %(Longitude a, double b)
      => new Longitude(Maths.Wrap(a.m_value % b, MinValue, MaxValue));
    public static Longitude operator %(Longitude a, Longitude b)
      => a % b.Value;
    public static Longitude operator -(Longitude a, double b)
      => new Longitude(Maths.Wrap(a.m_value - b, MinValue, MaxValue));
    public static Longitude operator -(Longitude a, Longitude b)
      => a - b.Value;
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
      => $"<{GetType().Name}: {m_value}{Quantity.Angle.DegreeSymbol}>";
    #endregion Object overrides
  }
}
