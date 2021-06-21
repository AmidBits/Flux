namespace Flux.Units
{
  /// <summary>Azimuth.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Azimuth"/>
  public struct Azimuth
    : System.IComparable<Azimuth>, System.IEquatable<Azimuth>
  {
    public const double MaxValue = 360;
    public const double MinValue = 0;

    private readonly double m_degree;

    public Azimuth(double degree)
      => m_degree = Maths.Wrap(degree, 0, 360);

    public double Degree
      => m_degree;
    public double Radian
      => Angle.ConvertDegreeToRadian(m_degree);

    public Angle ToAngle()
      => Angle.FromDegree(m_degree);

    #region Static methods
    public static Azimuth Add(Azimuth left, Azimuth right)
      => new Azimuth(left.m_degree + right.m_degree);
    public static Azimuth Divide(Azimuth left, Azimuth right)
      => new Azimuth(left.m_degree / right.m_degree);
    public static Azimuth Multiply(Azimuth left, Azimuth right)
      => new Azimuth(left.m_degree * right.m_degree);
    public static Azimuth Negate(Azimuth value)
      => new Azimuth(-value.m_degree);
    public static Azimuth Remainder(Azimuth dividend, Azimuth divisor)
      => new Azimuth(dividend.m_degree % divisor.m_degree);
    public static Azimuth Subtract(Azimuth left, Azimuth right)
      => new Azimuth(left.m_degree - right.m_degree);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Azimuth v)
     => v.m_degree;
    public static implicit operator Azimuth(double v)
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

    public static Azimuth operator +(Azimuth a, Azimuth b)
      => Add(a, b);
    public static Azimuth operator /(Azimuth a, Azimuth b)
      => Divide(a, b);
    public static Azimuth operator *(Azimuth a, Azimuth b)
      => Multiply(a, b);
    public static Azimuth operator -(Azimuth v)
      => Negate(v);
    public static Azimuth operator %(Azimuth a, Azimuth b)
      => Remainder(a, b);
    public static Azimuth operator -(Azimuth a, Azimuth b)
      => Subtract(a, b);
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
      => $"<{GetType().Name}: {m_degree}\u00B0>";
    #endregion Object overrides
  }
}
