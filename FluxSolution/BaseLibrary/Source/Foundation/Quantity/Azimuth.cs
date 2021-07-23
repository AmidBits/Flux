namespace Flux.Quantity
{
  /// <summary>Azimuth unit of degree.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Azimuth"/>
  public struct Azimuth
    : System.IComparable<Azimuth>, System.IEquatable<Azimuth>, IValuedUnit
  {
    public const double MaxValue = 360;
    public const double MinValue = 0;

    private readonly Angle m_value;

    public Azimuth(double degree)
      => m_value = Angle.FromUnitValue(AngleUnit.Degree, Maths.Wrap(degree, MinValue, MaxValue) % MaxValue);
    public Azimuth(Angle angle)
      : this(angle.Degree) // Call base to ensure value is between min/max.
    { }

    public Angle Angle
      => m_value;

    public double Value
      => m_value.Degree;

    #region Overloaded operators
    public static explicit operator double(Azimuth v)
     => v.m_value.Degree;
    public static explicit operator Azimuth(double v)
      => new Azimuth(v);

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
      => new Azimuth(-v.m_value);
    public static Azimuth operator +(Azimuth a, double b)
      => new Azimuth(a.m_value + b);
    public static Azimuth operator +(Azimuth a, Azimuth b)
      => a + b.Value;
    public static Azimuth operator /(Azimuth a, double b)
      => new Azimuth(a.m_value / b);
    public static Azimuth operator /(Azimuth a, Azimuth b)
      => a / b.Value;
    public static Azimuth operator *(Azimuth a, double b)
      => new Azimuth(a.m_value * b);
    public static Azimuth operator *(Azimuth a, Azimuth b)
      => a * b.Value;
    public static Azimuth operator %(Azimuth a, double b)
      => new Azimuth(a.m_value % b);
    public static Azimuth operator %(Azimuth a, Azimuth b)
      => a % b.Value;
    public static Azimuth operator -(Azimuth a, double b)
      => new Azimuth(a.m_value - b);
    public static Azimuth operator -(Azimuth a, Azimuth b)
      => a - b.Value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Azimuth other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Azimuth other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Azimuth o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value.Degree}{Angle.DegreeSymbol}>";
    #endregion Object overrides
  }
}
