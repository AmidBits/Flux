namespace Flux.Units
{
  /// <summary>Flow unit of cubic meters per second.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Flow"/>
  public struct Flow
    : System.IComparable<Flow>, System.IEquatable<Flow>, IStandardizedScalar
  {
    private readonly double m_value;

    public Flow(double cubicMeterPerSecond)
      => m_value = cubicMeterPerSecond;

    public double Value
      => m_value;

    #region Overloaded operators
    public static explicit operator double(Flow v)
      => v.m_value;
    public static explicit operator Flow(double v)
      => new Flow(v);

    public static bool operator <(Flow a, Flow b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Flow a, Flow b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Flow a, Flow b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Flow a, Flow b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Flow a, Flow b)
      => a.Equals(b);
    public static bool operator !=(Flow a, Flow b)
      => !a.Equals(b);

    public static Flow operator -(Flow v)
      => new Flow(-v.m_value);
    public static Flow operator +(Flow a, Flow b)
      => new Flow(a.m_value + b.m_value);
    public static Flow operator /(Flow a, Flow b)
      => new Flow(a.m_value / b.m_value);
    public static Flow operator *(Flow a, Flow b)
      => new Flow(a.m_value * b.m_value);
    public static Flow operator %(Flow a, Flow b)
      => new Flow(a.m_value % b.m_value);
    public static Flow operator -(Flow a, Flow b)
      => new Flow(a.m_value - b.m_value);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Flow other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Flow other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Flow o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} m³/s>";
    #endregion Object overrides
  }
}
