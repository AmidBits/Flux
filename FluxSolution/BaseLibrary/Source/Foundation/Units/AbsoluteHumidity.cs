namespace Flux.Units
{
  /// <summary>Frequency is a mutable data type to accomodate changes across multiple consumers.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Humidity#Absolute_humidity"/>
  public struct AbsoluteHumidity
    : System.IComparable<AbsoluteHumidity>, System.IEquatable<AbsoluteHumidity>
  {
    private readonly double m_gramPerCubicMeter;

    public AbsoluteHumidity(double gramPerCubicMeter)
      => m_gramPerCubicMeter = gramPerCubicMeter;

    public double GramPerCubicMeter
      => m_gramPerCubicMeter;

    #region Static methods
    public static AbsoluteHumidity Add(AbsoluteHumidity left, AbsoluteHumidity right)
      => new AbsoluteHumidity(left.m_gramPerCubicMeter + right.m_gramPerCubicMeter);
    public static AbsoluteHumidity Divide(AbsoluteHumidity left, AbsoluteHumidity right)
      => new AbsoluteHumidity(left.m_gramPerCubicMeter / right.m_gramPerCubicMeter);
    public static AbsoluteHumidity Multiply(AbsoluteHumidity left, AbsoluteHumidity right)
      => new AbsoluteHumidity(left.m_gramPerCubicMeter * right.m_gramPerCubicMeter);
    public static AbsoluteHumidity Negate(AbsoluteHumidity value)
      => new AbsoluteHumidity(-value.m_gramPerCubicMeter);
    public static AbsoluteHumidity Remainder(AbsoluteHumidity dividend, AbsoluteHumidity divisor)
      => new AbsoluteHumidity(dividend.m_gramPerCubicMeter % divisor.m_gramPerCubicMeter);
    public static AbsoluteHumidity Subtract(AbsoluteHumidity left, AbsoluteHumidity right)
      => new AbsoluteHumidity(left.m_gramPerCubicMeter - right.m_gramPerCubicMeter);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(AbsoluteHumidity v)
      => v.m_gramPerCubicMeter;
    public static implicit operator AbsoluteHumidity(double v)
      => new AbsoluteHumidity(v);

    public static bool operator <(AbsoluteHumidity a, AbsoluteHumidity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(AbsoluteHumidity a, AbsoluteHumidity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(AbsoluteHumidity a, AbsoluteHumidity b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(AbsoluteHumidity a, AbsoluteHumidity b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(AbsoluteHumidity a, AbsoluteHumidity b)
      => a.Equals(b);
    public static bool operator !=(AbsoluteHumidity a, AbsoluteHumidity b)
      => !a.Equals(b);

    public static AbsoluteHumidity operator +(AbsoluteHumidity a, AbsoluteHumidity b)
      => Add(a, b);
    public static AbsoluteHumidity operator /(AbsoluteHumidity a, AbsoluteHumidity b)
      => Divide(a, b);
    public static AbsoluteHumidity operator *(AbsoluteHumidity a, AbsoluteHumidity b)
      => Multiply(a, b);
    public static AbsoluteHumidity operator -(AbsoluteHumidity v)
      => Negate(v);
    public static AbsoluteHumidity operator %(AbsoluteHumidity a, AbsoluteHumidity b)
      => Remainder(a, b);
    public static AbsoluteHumidity operator -(AbsoluteHumidity a, AbsoluteHumidity b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AbsoluteHumidity other)
      => m_gramPerCubicMeter.CompareTo(other.m_gramPerCubicMeter);

    // IEquatable
    public bool Equals(AbsoluteHumidity other)
      => m_gramPerCubicMeter == other.m_gramPerCubicMeter;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is AbsoluteHumidity o && Equals(o);
    public override int GetHashCode()
      => m_gramPerCubicMeter.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_gramPerCubicMeter} g/m³>";
    #endregion Object overrides
  }
}
