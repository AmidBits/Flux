namespace Flux.Units
{
  /// <summary>Frequency is a mutable data type to accomodate changes across multiple consumers.</summary>
  public struct Latitude
    : System.IComparable<Latitude>, System.IEquatable<Latitude>, System.IFormattable
  {
    private readonly double m_degree;

    public Latitude(double degree)
      => m_degree = Maths.Wrap(degree, -90, +90);

    public double Degree
      => m_degree;
    public double Radian
      => Angle.ConvertDegreesToRadians(m_degree);

    public Angle ToAngle()
      => Angle.FromDegree(m_degree);

    #region Static methods
    public static Latitude Add(Latitude left, Latitude right)
      => new Latitude(left.m_degree + right.m_degree);
    public static Latitude Divide(Latitude left, Latitude right)
      => new Latitude(left.m_degree / right.m_degree);
    public static Latitude Multiply(Latitude left, Latitude right)
      => new Latitude(left.m_degree * right.m_degree);
    public static Latitude Negate(Latitude value)
      => new Latitude(-value.m_degree);
    public static Latitude Remainder(Latitude dividend, Latitude divisor)
      => new Latitude(dividend.m_degree % divisor.m_degree);
    public static Latitude Subtract(Latitude left, Latitude right)
      => new Latitude(left.m_degree - right.m_degree);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Latitude v)
      => v.m_degree;
    public static implicit operator Latitude(double v)
      => new Latitude(v);

    public static bool operator <(Latitude a, Latitude b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Latitude a, Latitude b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Latitude a, Latitude b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Latitude a, Latitude b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Latitude a, Latitude b)
      => a.Equals(b);
    public static bool operator !=(Latitude a, Latitude b)
      => !a.Equals(b);

    public static Latitude operator +(Latitude a, Latitude b)
      => Add(a, b);
    public static Latitude operator /(Latitude a, Latitude b)
      => Divide(a, b);
    public static Latitude operator *(Latitude a, Latitude b)
      => Multiply(a, b);
    public static Latitude operator -(Latitude v)
      => Negate(v);
    public static Latitude operator %(Latitude a, Latitude b)
      => Remainder(a, b);
    public static Latitude operator -(Latitude a, Latitude b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Latitude other)
      => m_degree.CompareTo(other.m_degree);

    // IEquatable
    public bool Equals(Latitude other)
      => m_degree == other.m_degree;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(Latitude)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Latitude o && Equals(o);
    public override int GetHashCode()
      => m_degree.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
