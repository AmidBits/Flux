namespace Flux.Units
{
  /// <summary>Time.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Time"/>
  public struct Time
    : System.IComparable<Time>, System.IEquatable<Time>, System.IFormattable
  {
    private readonly double m_seconds;

    public Time(double seconds)
      => m_seconds = seconds;

    public double Seconds
      => m_seconds;

    public System.TimeSpan ToTimeSpan()
      => System.TimeSpan.FromSeconds(m_seconds);

    #region Static methods
    public static Time Add(Time left, Time right)
      => new Time(left.m_seconds + right.m_seconds);
    public static Time Divide(Time left, Time right)
      => new Time(left.m_seconds / right.m_seconds);
    public static Time FromTimeSpan(System.TimeSpan timeSpan)
      => new Time(timeSpan.TotalSeconds);
    public static Time Multiply(Time left, Time right)
      => new Time(left.m_seconds * right.m_seconds);
    public static Time Negate(Time value)
      => new Time(-value.m_seconds);
    public static Time Remainder(Time dividend, Time divisor)
      => new Time(dividend.m_seconds % divisor.m_seconds);
    public static Time Subtract(Time left, Time right)
      => new Time(left.m_seconds - right.m_seconds);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Time v)
      => v.m_seconds;
    public static implicit operator Time(double v)
      => new Time(v);

    public static bool operator <(Time a, Time b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Time a, Time b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Time a, Time b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Time a, Time b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Time a, Time b)
      => a.Equals(b);
    public static bool operator !=(Time a, Time b)
      => !a.Equals(b);

    public static Time operator +(Time a, Time b)
      => Add(a, b);
    public static Time operator /(Time a, Time b)
      => Divide(a, b);
    public static Time operator *(Time a, Time b)
      => Multiply(a, b);
    public static Time operator -(Time v)
      => Negate(v);
    public static Time operator %(Time a, Time b)
      => Remainder(a, b);
    public static Time operator -(Time a, Time b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Time other)
      => m_seconds.CompareTo(other.m_seconds);

    // IEquatable
    public bool Equals(Time other)
      => m_seconds == other.m_seconds;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(Time)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Time o && Equals(o);
    public override int GetHashCode()
      => m_seconds.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
