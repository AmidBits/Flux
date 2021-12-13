namespace Flux
{
  /// <summary>Azimuth unit of degree. The unit here is defined in the range [0, +360]. Arithmetic results are wrapped around the range.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Azimuth"/>
  public struct Azimuth
    : System.IComparable<Azimuth>, System.IEquatable<Azimuth>, IValueGeneralizedUnit<double>
  {
    public const double MaxValue = 360;
    public const double MinValue = 0;

    private readonly double m_degree;

    public Azimuth(double degree)
      => m_degree = IsAzimuth(degree) ? Wrap(degree) : throw new System.ArgumentOutOfRangeException(nameof(degree));
    public Azimuth(Angle angle)
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

    public double GeneralUnitValue
      => m_degree;

    public Angle ToAngle()
      => new(m_degree, AngleUnit.Degree);

    #region Static methods
    /// <summary>Finding the angle between two bearings.</summary>
    public static double DeltaBearing(double degBearing1, double degBearing2)
      => Flux.Maths.Wrap(degBearing2 - degBearing1, MinValue, MaxValue);

    /// <summary>Returns whether the specified bearing (in degrees) is a valid bearing, i.e. [0, 360).</summary>
    public static bool IsAzimuth(double degBearing)
      => degBearing >= MinValue && degBearing <= MaxValue;

    /// <summary>Returns the bearing needle latched to one of the specified number of positions around the compass. For example, 4 positions will return an index [0, 3] (of four) for the latched bearing.</summary>
    public static int LatchNeedle(double radBearing, int positions)
      => (int)System.Math.Round(Maths.Wrap(radBearing, 0, Maths.PiX2) / (Maths.PiX2 / positions) % positions);

    public static double Wrap(double degBearing)
      => Maths.Wrap(degBearing, MinValue, MaxValue) % MaxValue;
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Azimuth v)
     => v.m_degree;
    public static explicit operator Azimuth(double v)
      => new(v);

    public static bool operator <(Azimuth a, Azimuth b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Azimuth a, Azimuth b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Azimuth a, Azimuth b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Azimuth a, Azimuth b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Azimuth a, Azimuth b)
      => a.Equals(b);
    public static bool operator !=(Azimuth a, Azimuth b)
      => !a.Equals(b);

    public static Azimuth operator -(Azimuth v)
      => new(-v.m_degree);
    public static Azimuth operator +(Azimuth a, double b)
      => new(Wrap(a.m_degree + b));
    public static Azimuth operator +(Azimuth a, Azimuth b)
      => a + b.GeneralUnitValue;
    public static Azimuth operator /(Azimuth a, double b)
      => new(Wrap(a.m_degree / b));
    public static Azimuth operator /(Azimuth a, Azimuth b)
      => a / b.GeneralUnitValue;
    public static Azimuth operator *(Azimuth a, double b)
      => new(Wrap(a.m_degree * b));
    public static Azimuth operator *(Azimuth a, Azimuth b)
      => a * b.GeneralUnitValue;
    public static Azimuth operator %(Azimuth a, double b)
      => new(Wrap(a.m_degree % b));
    public static Azimuth operator %(Azimuth a, Azimuth b)
      => a % b.GeneralUnitValue;
    public static Azimuth operator -(Azimuth a, double b)
      => new(Wrap(a.m_degree - b));
    public static Azimuth operator -(Azimuth a, Azimuth b)
      => a - b.GeneralUnitValue;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Azimuth other)
      => m_degree.CompareTo(other.m_degree);

    // IEquatable
    public bool Equals(Azimuth other)
      => m_degree == other.m_degree;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Azimuth o && Equals(o);
    public override int GetHashCode()
      => m_degree.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {AngleUnit.Degree.Create(m_degree).ToUnitString(AngleUnit.Degree)} }}";
    #endregion Object overrides
  }
}
