namespace Flux
{
  /// <summary>Longitude, unit of degree, is a geographic coordinate that specifies the east–west position of a point on the Earth's surface, or the surface of a celestial body. The unit here is defined in the range [-180, +180] in relation to the prime meridian, by convention. Arithmetic results are wrapped around the range.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Longitude"/>
  public struct Longitude
    : System.IComparable<Longitude>, System.IEquatable<Longitude>, IValueGeneralizedUnit<double>
  {
    public const double MaxValue = +180;
    public const double MinValue = -180;

    private readonly double m_degree;

    public Longitude(double degree)
      => m_degree = IsLongitude(degree) ? Wrap(degree) : throw new System.ArgumentOutOfRangeException(nameof(degree));
    public Longitude(Angle angle)
      : this(angle.ToUnitValue(AngleUnit.Degree)) // Call base to ensure value is between min/max.
    { }

    public double MathCos
      => System.Math.Cos(Radian);
    public double MathSin
      => System.Math.Sin(Radian);
    public double MathTan
      => System.Math.Tan(Radian);

    public double Radian
      => Angle.ConvertDegreeToRadian(m_degree);

    /// <summary>Computes the theoretical timezone offset, relative prime meridian. This can be used for a rough timezone estimate.</summary>
    public int TheoreticalTimezoneOffset
      => GetTheoreticalTimezoneOffset(m_degree);

    public double GeneralUnitValue
      => m_degree;

    /// <summary>Projects the longitude to a mercator X value in the range [-PI, PI].</summary>
    /// https://en.wikipedia.org/wiki/Mercator_projection
    /// https://en.wikipedia.org/wiki/Web_Mercator_projection#Formulas
    public double GetMercatorProjectedX()
      => ToAngle().GeneralUnitValue;

    public Angle ToAngle()
      => new(m_degree, AngleUnit.Degree);

    #region Static methods
    /// <summary>Returns the theoretical time zone offset, relative prime meridian. There are many places with deviations across all time zones.</summary>
    /// <param name="degLongitude"></param>
    public static int GetTheoreticalTimezoneOffset(double degLongitude)
      => (int)Maths.RoundTo((degLongitude + System.Math.CopySign(7.5, degLongitude)) / 15, FullRoundingBehavior.TowardZero);

    /// <summary>Returns whether the specified longitude (in degrees) is a valid longitude, i.e. [-180, +180].</summary>
    public static bool IsLongitude(double degLongitude)
      => degLongitude >= MinValue && degLongitude <= MaxValue;

    public static double Wrap(double degLongitude)
      => Maths.Wrap(degLongitude, MinValue, MaxValue) % MaxValue;
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Longitude v)
      => v.m_degree;
    public static explicit operator Longitude(double v)
      => new(v);

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
      => new(-v.m_degree);
    public static Longitude operator +(Longitude a, double b)
      => new(Wrap(a.m_degree + b));
    public static Longitude operator +(Longitude a, Longitude b)
      => a + b.GeneralUnitValue;
    public static Longitude operator /(Longitude a, double b)
      => new(Wrap(a.m_degree / b));
    public static Longitude operator /(Longitude a, Longitude b)
      => a / b.GeneralUnitValue;
    public static Longitude operator *(Longitude a, double b)
      => new(Wrap(a.m_degree * b));
    public static Longitude operator *(Longitude a, Longitude b)
      => a * b.GeneralUnitValue;
    public static Longitude operator %(Longitude a, double b)
      => new(Wrap(a.m_degree % b));
    public static Longitude operator %(Longitude a, Longitude b)
      => a % b.GeneralUnitValue;
    public static Longitude operator -(Longitude a, double b)
      => new(Wrap(a.m_degree - b));
    public static Longitude operator -(Longitude a, Longitude b)
      => a - b.GeneralUnitValue;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Longitude other)
      => m_degree.CompareTo(other.m_degree);

    // IEquatable
    public bool Equals(Longitude other)
      => m_degree == other.m_degree;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Longitude o && Equals(o);
    public override int GetHashCode()
      => m_degree.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToAngle().ToUnitString(AngleUnit.Degree)} }}";
    #endregion Object overrides
  }
}
