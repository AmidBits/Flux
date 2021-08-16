namespace Flux.Quantity
{
  /// <summary>Force, unit of newton. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Force"/>
  public struct Force
    : System.IComparable<Force>, System.IEquatable<Force>, IValuedUnit
  {
    private readonly double m_value;

    public Force(double newton)
      => m_value = newton;

    public double Value
      => m_value;

    #region Overloaded operators
    public static explicit operator double(Force v)
      => v.m_value;
    public static explicit operator Force(double v)
      => new Force(v);

    public static bool operator <(Force a, Force b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Force a, Force b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Force a, Force b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Force a, Force b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Force a, Force b)
      => a.Equals(b);
    public static bool operator !=(Force a, Force b)
      => !a.Equals(b);

    public static Force operator -(Force v)
      => new Force(-v.m_value);
    public static Force operator +(Force a, double b)
      => new Force(a.m_value + b);
    public static Force operator +(Force a, Force b)
      => a + b.m_value;
    public static Force operator /(Force a, double b)
      => new Force(a.m_value / b);
    public static Force operator /(Force a, Force b)
      => a / b.m_value;
    public static Force operator *(Force a, double b)
      => new Force(a.m_value * b);
    public static Force operator *(Force a, Force b)
      => a * b.m_value;
    public static Force operator %(Force a, double b)
      => new Force(a.m_value % b);
    public static Force operator %(Force a, Force b)
      => a % b.m_value;
    public static Force operator -(Force a, double b)
      => new Force(a.m_value - b);
    public static Force operator -(Force a, Force b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Force other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Force other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Force o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} N>";
    #endregion Object overrides
  }
}
