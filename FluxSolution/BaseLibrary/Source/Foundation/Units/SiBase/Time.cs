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

  /// <summary>Time unit of second.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Time"/>
  public struct Time
    : System.IComparable<Time>, System.IEquatable<Time>, IValuedUnit
  {
    /// <see href="https://en.wikipedia.org/wiki/Flick_(time)"></see>
    public static Time Flick
      => new Time(1.0 / 705600000.0);

    private readonly double m_value;

    public Time(double seconds)
      => m_value = seconds;

    public double Value
      => m_value;

    public System.TimeSpan ToTimeSpan()
      => System.TimeSpan.FromSeconds(m_value);
    public double ToUnitValue(TimeUnit unit)
    {
      switch (unit)
      {
        case TimeUnit.Nanosecond:
          return m_value * 1000000000;
        case TimeUnit.Microsecond:
          return m_value * 1000000;
        case TimeUnit.Millisecond:
          return m_value * 1000;
        case TimeUnit.Second:
          return m_value;
        case TimeUnit.Minute:
          return m_value / 60;
        case TimeUnit.Hour:
          return m_value / 3600;
        case TimeUnit.Day:
          return m_value / 86400;
        case TimeUnit.Week:
          return m_value / 604800;
        case TimeUnit.Fortnight:
          return m_value / 1209600;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Static methods
    /// <summary>Creates a new Time instance from the specified <paramref name="timeSpan"/>.</summary>
    public static Time From(System.TimeSpan timeSpan)
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
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Time v)
      => v.m_value;
    public static explicit operator Time(double v)
      => new Time(v);

    public static bool operator <(Time a, Time b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Time a, Time b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Time a, Time b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Time a, Time b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Time a, Time b)
      => a.Equals(b);
    public static bool operator !=(Time a, Time b)
      => !a.Equals(b);

    public static Time operator -(Time v)
      => new Time(-v.m_value);
    public static Time operator +(Time a, double b)
      => new Time(a.m_value + b);
    public static Time operator +(Time a, Time b)
      => a + b.m_value;
    public static Time operator /(Time a, double b)
      => new Time(a.m_value / b);
    public static Time operator /(Time a, Time b)
      => a / b.m_value;
    public static Time operator *(Time a, double b)
      => new Time(a.m_value * b);
    public static Time operator *(Time a, Time b)
      => a * b.m_value;
    public static Time operator %(Time a, double b)
      => new Time(a.m_value % b);
    public static Time operator %(Time a, Time b)
      => a % b.m_value;
    public static Time operator -(Time a, double b)
      => new Time(a.m_value - b);
    public static Time operator -(Time a, Time b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Time other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Time other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Time o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} s>";
    #endregion Object overrides
  }
}
