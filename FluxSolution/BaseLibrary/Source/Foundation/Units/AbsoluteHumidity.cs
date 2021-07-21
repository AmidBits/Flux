namespace Flux.Units
{
  /// <summary>Absolute humidity unit of grams per cubic meter.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Humidity#Absolute_humidity"/>
  public struct AbsoluteHumidity
    : System.IComparable<AbsoluteHumidity>, System.IEquatable<AbsoluteHumidity>, IValuedUnit
  {
    private readonly double m_value;

    public AbsoluteHumidity(double gramsPerCubicMeter)
      => m_value = gramsPerCubicMeter;

    public double Value
      => m_value;

    #region Overloaded operators
    public static explicit operator double(AbsoluteHumidity v)
      => v.m_value;
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
      => new AbsoluteHumidity(-v.m_value);
    public static AbsoluteHumidity operator +(AbsoluteHumidity a, AbsoluteHumidity b)
      => new AbsoluteHumidity(a.m_value + b.m_value);
    public static AbsoluteHumidity operator /(AbsoluteHumidity a, AbsoluteHumidity b)
      => new AbsoluteHumidity(a.m_value / b.m_value);
    public static AbsoluteHumidity operator *(AbsoluteHumidity a, AbsoluteHumidity b)
      => new AbsoluteHumidity(a.m_value * b.m_value);
    public static AbsoluteHumidity operator %(AbsoluteHumidity a, AbsoluteHumidity b)
      => new AbsoluteHumidity(a.m_value % b.m_value);
    public static AbsoluteHumidity operator -(AbsoluteHumidity a, AbsoluteHumidity b)
      => new AbsoluteHumidity(a.m_value - b.m_value);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(AbsoluteHumidity other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(AbsoluteHumidity other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is AbsoluteHumidity o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} g/m³>";
    #endregion Object overrides
  }
}
