namespace Flux.Units
{
  /// <summary>RelativeHumidity is represented as a percentage value, e.g. 34.5 for 34.5%.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Humidity#Relative_humidity"/>
  public struct RelativeHumidity
    : System.IComparable<RelativeHumidity>, System.IEquatable<RelativeHumidity>, IStandardizedScalar
  {
    private readonly double m_percent;

    public RelativeHumidity(double percent)
      => m_percent = percent;

    public double Percent
      => m_percent;

    #region Static methods
    public static RelativeHumidity Add(RelativeHumidity left, RelativeHumidity right)
      => new RelativeHumidity(left.m_percent + right.m_percent);
    public static RelativeHumidity Divide(RelativeHumidity left, RelativeHumidity right)
      => new RelativeHumidity(left.m_percent / right.m_percent);
    public static RelativeHumidity Multiply(RelativeHumidity left, RelativeHumidity right)
      => new RelativeHumidity(left.m_percent * right.m_percent);
    public static RelativeHumidity Negate(RelativeHumidity value)
      => new RelativeHumidity(-value.m_percent);
    public static RelativeHumidity Remainder(RelativeHumidity dividend, RelativeHumidity divisor)
      => new RelativeHumidity(dividend.m_percent % divisor.m_percent);
    public static RelativeHumidity Subtract(RelativeHumidity left, RelativeHumidity right)
      => new RelativeHumidity(left.m_percent - right.m_percent);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(RelativeHumidity v)
      => v.m_percent;
    public static explicit operator RelativeHumidity(double v)
      => new RelativeHumidity(v);

    public static bool operator <(RelativeHumidity a, RelativeHumidity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(RelativeHumidity a, RelativeHumidity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(RelativeHumidity a, RelativeHumidity b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(RelativeHumidity a, RelativeHumidity b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(RelativeHumidity a, RelativeHumidity b)
      => a.Equals(b);
    public static bool operator !=(RelativeHumidity a, RelativeHumidity b)
      => !a.Equals(b);

    public static RelativeHumidity operator +(RelativeHumidity a, RelativeHumidity b)
      => Add(a, b);
    public static RelativeHumidity operator /(RelativeHumidity a, RelativeHumidity b)
      => Divide(a, b);
    public static RelativeHumidity operator *(RelativeHumidity a, RelativeHumidity b)
      => Multiply(a, b);
    public static RelativeHumidity operator -(RelativeHumidity v)
      => Negate(v);
    public static RelativeHumidity operator %(RelativeHumidity a, RelativeHumidity b)
      => Remainder(a, b);
    public static RelativeHumidity operator -(RelativeHumidity a, RelativeHumidity b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(RelativeHumidity other)
      => m_percent.CompareTo(other.m_percent);

    // IEquatable
    public bool Equals(RelativeHumidity other)
      => m_percent == other.m_percent;

    // IUnitStandardized
    public double GetScalar()
      => m_percent;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is RelativeHumidity o && Equals(o);
    public override int GetHashCode()
      => m_percent.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_percent}%>";
    #endregion Object overrides
  }
}
