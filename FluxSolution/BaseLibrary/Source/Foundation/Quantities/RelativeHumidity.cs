namespace Flux.Quantity
{
  public enum RelativeHumidityUnit
  {
    Percent,
  }

  /// <summary>Relative humidity is represented as a percentage value, e.g. 34.5 for 34.5%.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Humidity#Relative_humidity"/>
  public struct RelativeHumidity
    : System.IComparable<RelativeHumidity>, System.IEquatable<RelativeHumidity>, IValuedUnit
  {
    private readonly double m_value;

    public RelativeHumidity(double value, RelativeHumidityUnit unit = RelativeHumidityUnit.Percent)
    {
      switch (unit)
      {
        case RelativeHumidityUnit.Percent:
          m_value = value;
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    public double Value
      => m_value;

    public double ToUnitValue(RelativeHumidityUnit unit = RelativeHumidityUnit.Percent)
    {
      switch (unit)
      {
        case RelativeHumidityUnit.Percent:
          return m_value;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Overloaded operators
    public static explicit operator double(RelativeHumidity v)
      => v.m_value;
    public static explicit operator RelativeHumidity(double v)
      => new RelativeHumidity(v);

    public static bool operator <(RelativeHumidity a, RelativeHumidity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(RelativeHumidity a, RelativeHumidity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(RelativeHumidity a, RelativeHumidity b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(RelativeHumidity a, RelativeHumidity b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(RelativeHumidity a, RelativeHumidity b)
      => a.Equals(b);
    public static bool operator !=(RelativeHumidity a, RelativeHumidity b)
      => !a.Equals(b);

    public static RelativeHumidity operator -(RelativeHumidity v)
      => new RelativeHumidity(-v.m_value);
    public static RelativeHumidity operator +(RelativeHumidity a, double b)
      => new RelativeHumidity(a.m_value + b);
    public static RelativeHumidity operator +(RelativeHumidity a, RelativeHumidity b)
      => a + b.m_value;
    public static RelativeHumidity operator /(RelativeHumidity a, double b)
      => new RelativeHumidity(a.m_value / b);
    public static RelativeHumidity operator /(RelativeHumidity a, RelativeHumidity b)
      => a / b.m_value;
    public static RelativeHumidity operator *(RelativeHumidity a, double b)
      => new RelativeHumidity(a.m_value * b);
    public static RelativeHumidity operator *(RelativeHumidity a, RelativeHumidity b)
      => a * b.m_value;
    public static RelativeHumidity operator %(RelativeHumidity a, double b)
      => new RelativeHumidity(a.m_value % b);
    public static RelativeHumidity operator %(RelativeHumidity a, RelativeHumidity b)
      => a % b.m_value;
    public static RelativeHumidity operator -(RelativeHumidity a, double b)
      => new RelativeHumidity(a.m_value - b);
    public static RelativeHumidity operator -(RelativeHumidity a, RelativeHumidity b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(RelativeHumidity other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(RelativeHumidity other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is RelativeHumidity o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value}\u0025>";
    #endregion Object overrides
  }
}