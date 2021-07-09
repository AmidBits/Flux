namespace Flux.Units
{
  /// <summary>Frequency is a mutable data type to accomodate changes across multiple consumers.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Humidity#Absolute_humidity"/>
  public struct AbsoluteHumidity
    : System.IComparable<AbsoluteHumidity>, System.IEquatable<AbsoluteHumidity>, IStandardizedScalar
  {
    private readonly double m_gramPerCubicMeter;

    public AbsoluteHumidity(double gramPerCubicMeter)
      => m_gramPerCubicMeter = gramPerCubicMeter;

    public double GramPerCubicMeter
      => m_gramPerCubicMeter;

    #region Overloaded operators
    public static explicit operator double(AbsoluteHumidity v)
      => v.m_gramPerCubicMeter;
    public static explicit operator AbsoluteHumidity(double v)
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

    public static AbsoluteHumidity operator -(AbsoluteHumidity v)
      => new AbsoluteHumidity(-v.m_gramPerCubicMeter);
    public static AbsoluteHumidity operator +(AbsoluteHumidity a, AbsoluteHumidity b)
      => new AbsoluteHumidity(a.m_gramPerCubicMeter + b.m_gramPerCubicMeter);
    public static AbsoluteHumidity operator /(AbsoluteHumidity a, AbsoluteHumidity b)
      => new AbsoluteHumidity(a.m_gramPerCubicMeter / b.m_gramPerCubicMeter);
    public static AbsoluteHumidity operator *(AbsoluteHumidity a, AbsoluteHumidity b)
      => new AbsoluteHumidity(a.m_gramPerCubicMeter * b.m_gramPerCubicMeter);
    public static AbsoluteHumidity operator %(AbsoluteHumidity a, AbsoluteHumidity b)
      => new AbsoluteHumidity(a.m_gramPerCubicMeter % b.m_gramPerCubicMeter);
    public static AbsoluteHumidity operator -(AbsoluteHumidity a, AbsoluteHumidity b)
      => new AbsoluteHumidity(a.m_gramPerCubicMeter - b.m_gramPerCubicMeter);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AbsoluteHumidity other)
      => m_gramPerCubicMeter.CompareTo(other.m_gramPerCubicMeter);

    // IEquatable
    public bool Equals(AbsoluteHumidity other)
      => m_gramPerCubicMeter == other.m_gramPerCubicMeter;

    // IUnitStandardized
    public double GetScalar()
      => m_gramPerCubicMeter;
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
