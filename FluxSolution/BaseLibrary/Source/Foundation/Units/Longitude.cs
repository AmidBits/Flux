namespace Flux.Units
{
  /// <summary>Longitude.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Longitude"/>
  public struct Longitude
    : System.IComparable<Longitude>, System.IEquatable<Longitude>, IStandardizedScalar
  {
    public const double MaxValue = +180;
    public const double MinValue = -180;

    private readonly double m_degree;

    public Longitude(double degree)
      => m_degree = Maths.Wrap(degree, MinValue, MaxValue);

    public double Degree
      => m_degree;
    public double Radian
      => Angle.ConvertDegreeToRadian(m_degree);

    public Angle ToAngle()
      => Angle.FromDegree(m_degree);

    #region Static methods
    public static Longitude Add(Longitude left, Longitude right)
      => new Longitude(left.m_degree + right.m_degree);
    public static Longitude Divide(Longitude left, Longitude right)
      => new Longitude(left.m_degree / right.m_degree);
    public static Longitude Multiply(Longitude left, Longitude right)
      => new Longitude(left.m_degree * right.m_degree);
    public static Longitude Negate(Longitude value)
      => new Longitude(-value.m_degree);
    public static Longitude Remainder(Longitude dividend, Longitude divisor)
      => new Longitude(dividend.m_degree % divisor.m_degree);
    public static Longitude Subtract(Longitude left, Longitude right)
      => new Longitude(left.m_degree - right.m_degree);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator double(Longitude v)
      => v.m_degree;
    public static implicit operator Longitude(double v)
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
      => m_degree.CompareTo(other.m_degree);

    // IEquatable
    public bool Equals(Longitude other)
      => m_degree == other.m_degree;

    // IUnitStandardized
    public double GetScalar()
      => m_degree;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Longitude o && Equals(o);
    public override int GetHashCode()
      => m_degree.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_degree}\u00B0>";
    #endregion Object overrides
  }
}
