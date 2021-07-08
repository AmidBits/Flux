namespace Flux.Units
{
  public enum TimeUnit
  {
    Nanosecond,
    Microsecond,
    Millisecond,
    Second,
    Minute,
    Hour,
    Day,
    Week,
    Fortnight,
  }

  /// <summary>Time.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Time"/>
  public struct Time
    : System.IComparable<Time>, System.IEquatable<Time>, IStandardizedScalar
  {
    private readonly double m_second;

    public Time(double seconds)
      => m_second = seconds;

    public double Second
      => m_second;

    public System.TimeSpan ToTimeSpan()
      => System.TimeSpan.FromSeconds(m_second);
    public double ToUnitValue(TimeUnit unit)
    {
      switch (unit)
      {
        case TimeUnit.Nanosecond:
          return m_second * 1000000000;
        case TimeUnit.Microsecond:
          return m_second * 1000000;
        case TimeUnit.Millisecond:
          return m_second * 1000;
        case TimeUnit.Second:
          return m_second;
        case TimeUnit.Minute:
          return m_second / 60;
        case TimeUnit.Hour:
          return m_second / 3600;
        case TimeUnit.Day:
          return m_second / 86400;
        case TimeUnit.Week:
          return m_second / 604800;
        case TimeUnit.Fortnight:
          return m_second / 1209600;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Static methods
    public static Time Add(Time left, Time right)
      => new Time(left.m_second + right.m_second);
    public static Time Divide(Time left, Time right)
      => new Time(left.m_second / right.m_second);
    public static Time FromTimeSpan(System.TimeSpan timeSpan)
      => new Time(timeSpan.TotalSeconds);
    public static Time FromUnitValue(TimeUnit unit, double value)
    {
      switch (unit)
      {
        case TimeUnit.Nanosecond:
          return new Time(value / 1000000000);
        case TimeUnit.Microsecond:
          return new Time(value / 1000000);
        case TimeUnit.Millisecond:
          return new Time(value / 1000);
        case TimeUnit.Second:
          return new Time(value);
        case TimeUnit.Minute:
          return new Time(value * 60);
        case TimeUnit.Hour:
          return new Time(value * 3600);
        case TimeUnit.Day:
          return new Time(value * 86400);
        case TimeUnit.Week:
          return new Time(value * 604800);
        case TimeUnit.Fortnight:
          return new Time(value * 1209600);
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
    public static Time Multiply(Time left, Time right)
      => new Time(left.m_second * right.m_second);
    public static Time Negate(Time value)
      => new Time(-value.m_second);
    public static Time Remainder(Time dividend, Time divisor)
      => new Time(dividend.m_second % divisor.m_second);
    public static Time Subtract(Time left, Time right)
      => new Time(left.m_second - right.m_second);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Time v)
      => v.m_second;
    public static explicit operator Time(double v)
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
      => m_second.CompareTo(other.m_second);

    // IEquatable
    public bool Equals(Time other)
      => m_second == other.m_second;

    // IUnitStandardized
    public double GetScalar()
      => m_second;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Time o && Equals(o);
    public override int GetHashCode()
      => m_second.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_second} s>";
    #endregion Object overrides
  }
}
