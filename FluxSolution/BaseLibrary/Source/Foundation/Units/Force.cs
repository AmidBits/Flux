namespace Flux.Units
{
  /// <summary>Force.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Force"/>
  public struct Force
    : System.IComparable<Force>, System.IEquatable<Force>, System.IFormattable
  {
    private readonly double m_newton;

    public Force(double newton)
      => m_newton = newton;

    public double Newton
      => m_newton;

    #region Static methods
    public static Force Add(Force left, Force right)
      => new Force(left.m_newton + right.m_newton);
    public static Force Divide(Force left, Force right)
      => new Force(left.m_newton / right.m_newton);
    public static Force Multiply(Force left, Force right)
      => new Force(left.m_newton * right.m_newton);
    public static Force Negate(Force value)
      => new Force(-value.m_newton);
    public static Force Remainder(Force dividend, Force divisor)
      => new Force(dividend.m_newton % divisor.m_newton);
    public static Force Subtract(Force left, Force right)
      => new Force(left.m_newton - right.m_newton);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Force v)
      => v.m_newton;
    public static implicit operator Force(double v)
      => new Force(v);

    public static bool operator <(Force a, Force b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Force a, Force b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Force a, Force b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Force a, Force b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Force a, Force b)
      => a.Equals(b);
    public static bool operator !=(Force a, Force b)
      => !a.Equals(b);

    public static Force operator +(Force a, Force b)
      => Add(a, b);
    public static Force operator /(Force a, Force b)
      => Divide(a, b);
    public static Force operator *(Force a, Force b)
      => Multiply(a, b);
    public static Force operator -(Force v)
      => Negate(v);
    public static Force operator %(Force a, Force b)
      => Remainder(a, b);
    public static Force operator -(Force a, Force b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Force other)
      => m_newton.CompareTo(other.m_newton);

    // IEquatable
    public bool Equals(Force other)
      => m_newton == other.m_newton;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(Force)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Force o && Equals(o);
    public override int GetHashCode()
      => m_newton.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
