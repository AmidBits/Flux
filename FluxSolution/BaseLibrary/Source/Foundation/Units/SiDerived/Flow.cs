namespace Flux.Units
{
  /// <summary>Flow.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Flow"/>
  public struct Flow
    : System.IComparable<Flow>, System.IEquatable<Flow>, IStandardizedScalar
  {
    private readonly double m_cubicMeterPerSecond;

    public Flow(double cubicMeterPerSecond)
      => m_cubicMeterPerSecond = cubicMeterPerSecond;

    public double CubicMeterPerSecond
      => m_cubicMeterPerSecond;

    #region Overloaded operators
    public static explicit operator double(Flow v)
      => v.m_cubicMeterPerSecond;
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
      => new Flow(-v.m_cubicMeterPerSecond);
    public static Flow operator +(Flow a, Flow b)
      => new Flow(a.m_cubicMeterPerSecond + b.m_cubicMeterPerSecond);
    public static Flow operator /(Flow a, Flow b)
      => new Flow(a.m_cubicMeterPerSecond / b.m_cubicMeterPerSecond);
    public static Flow operator *(Flow a, Flow b)
      => new Flow(a.m_cubicMeterPerSecond * b.m_cubicMeterPerSecond);
    public static Flow operator %(Flow a, Flow b)
      => new Flow(a.m_cubicMeterPerSecond % b.m_cubicMeterPerSecond);
    public static Flow operator -(Flow a, Flow b)
      => new Flow(a.m_cubicMeterPerSecond - b.m_cubicMeterPerSecond);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Flow other)
      => m_cubicMeterPerSecond.CompareTo(other.m_cubicMeterPerSecond);

    // IEquatable
    public bool Equals(Flow other)
      => m_cubicMeterPerSecond == other.m_cubicMeterPerSecond;

    // IUnitStandardized
    public double GetScalar()
      => m_cubicMeterPerSecond;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Flow o && Equals(o);
    public override int GetHashCode()
      => m_cubicMeterPerSecond.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_cubicMeterPerSecond} m�/s>";
    #endregion Object overrides
  }
}