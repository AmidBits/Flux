namespace Flux.Units
{
  /// <summary>Flow.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Flow"/>
  public struct Flow
    : System.IComparable<Flow>, System.IEquatable<Flow>, System.IFormattable
  {
    private readonly double m_cubicMeterPerSecond;

    public Flow(double cubicMeterPerSecond)
      => m_cubicMeterPerSecond = cubicMeterPerSecond;

    public double CubicMeterPerSecond
      => m_cubicMeterPerSecond;

    #region Static methods
    public static Flow Add(Flow left, Flow right)
      => new Flow(left.m_cubicMeterPerSecond + right.m_cubicMeterPerSecond);
    public static Flow Divide(Flow left, Flow right)
      => new Flow(left.m_cubicMeterPerSecond / right.m_cubicMeterPerSecond);
    public static Flow Multiply(Flow left, Flow right)
      => new Flow(left.m_cubicMeterPerSecond * right.m_cubicMeterPerSecond);
    public static Flow Negate(Flow value)
      => new Flow(-value.m_cubicMeterPerSecond);
    public static Flow Remainder(Flow dividend, Flow divisor)
      => new Flow(dividend.m_cubicMeterPerSecond % divisor.m_cubicMeterPerSecond);
    public static Flow Subtract(Flow left, Flow right)
      => new Flow(left.m_cubicMeterPerSecond - right.m_cubicMeterPerSecond);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Flow v)
      => v.m_cubicMeterPerSecond;
    public static implicit operator Flow(double v)
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

    public static Flow operator +(Flow a, Flow b)
      => Add(a, b);
    public static Flow operator /(Flow a, Flow b)
      => Divide(a, b);
    public static Flow operator *(Flow a, Flow b)
      => Multiply(a, b);
    public static Flow operator -(Flow v)
      => Negate(v);
    public static Flow operator %(Flow a, Flow b)
      => Remainder(a, b);
    public static Flow operator -(Flow a, Flow b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Flow other)
      => m_cubicMeterPerSecond.CompareTo(other.m_cubicMeterPerSecond);

    // IEquatable
    public bool Equals(Flow other)
      => m_cubicMeterPerSecond == other.m_cubicMeterPerSecond;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(Flow)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Flow o && Equals(o);
    public override int GetHashCode()
      => m_cubicMeterPerSecond.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
