namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitSymbol(this Quantity.TimeUnit unit)
    {
      switch (unit)
      {
        case Quantity.TimeUnit.Nanosecond:
          return @" ns";
        case Quantity.TimeUnit.Microsecond:
          return " \u00B5s";
        case Quantity.TimeUnit.Millisecond:
          return @" ms";
        case Quantity.TimeUnit.Second:
          return @" s";
        case Quantity.TimeUnit.Minute:
          return @" min";
        case Quantity.TimeUnit.Hour:
          return @" h";
        case Quantity.TimeUnit.Day:
          return @" d";
        case Quantity.TimeUnit.Week:
          return @" week(s)";
        case Quantity.TimeUnit.Fortnight:
          return @" fortnight(s)";
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
  }

  namespace Quantity
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

    /// <summary>Time. SI unit of second. This is a base quantity.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Time"/>
    public struct Time
      : System.IComparable<Time>, System.IEquatable<Time>, IValuedUnit
    {
      /// <see href="https://en.wikipedia.org/wiki/Flick_(time)"></see>
      public static Time Flick
        => new Time(1.0 / 705600000.0);

      private readonly double m_value;

      public Time(double value, TimeUnit unit = TimeUnit.Second)
      {
        switch (unit)
        {
          case TimeUnit.Nanosecond:
            m_value = value / 1000000000;
            break;
          case TimeUnit.Microsecond:
            m_value = value / 1000000;
            break;
          case TimeUnit.Millisecond:
            m_value = value / 1000;
            break;
          case TimeUnit.Second:
            m_value = value;
            break;
          case TimeUnit.Minute:
            m_value = value * 60;
            break;
          case TimeUnit.Hour:
            m_value = value * 3600;
            break;
          case TimeUnit.Day:
            m_value = value * 86400;
            break;
          case TimeUnit.Week:
            m_value = value * 604800;
            break;
          case TimeUnit.Fortnight:
            m_value = value * 1209600;
            break;
          default:
            throw new System.ArgumentOutOfRangeException(nameof(unit));
        }
      }
      /// <summary>Creates a new Time instance from the specified <paramref name="timeSpan"/>.</summary>
      public Time(System.TimeSpan timeSpan)
        : this(timeSpan.TotalSeconds)
      { }

      public double Value
        => m_value;

      public System.TimeSpan ToTimeSpan()
        => System.TimeSpan.FromSeconds(m_value);
      public string ToUnitString(TimeUnit unit = TimeUnit.Second, string? format = null)
        => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
      public double ToUnitValue(TimeUnit unit = TimeUnit.Second)
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
        => $"<{GetType().Name}: {ToUnitString()}>";
      #endregion Object overrides
    }
  }
}
