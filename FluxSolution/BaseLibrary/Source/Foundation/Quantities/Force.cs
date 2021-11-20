namespace Flux.Quantity
{
  public enum ForceUnit
  {
    Newton,
  }

  /// <summary>Force, unit of newton. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Force"/>
  public struct Force
    : System.IComparable<Force>, System.IEquatable<Force>, IValuedUnit<double>
  {
    private readonly double m_value;

    public Force(double value, ForceUnit unit = ForceUnit.Newton)
      => m_value = unit switch
      {
        ForceUnit.Newton => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public double ToUnitValue(ForceUnit unit = ForceUnit.Newton)
      => unit switch
      {
        ForceUnit.Newton => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(Force v)
      => v.m_value;
    public static explicit operator Force(double v)
      => new(v);

    public static bool operator <(Force a, Force b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Force a, Force b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Force a, Force b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Force a, Force b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Force a, Force b)
      => a.Equals(b);
    public static bool operator !=(Force a, Force b)
      => !a.Equals(b);

    public static Force operator -(Force v)
      => new(-v.m_value);
    public static Force operator +(Force a, double b)
      => new(a.m_value + b);
    public static Force operator +(Force a, Force b)
      => a + b.m_value;
    public static Force operator /(Force a, double b)
      => new(a.m_value / b);
    public static Force operator /(Force a, Force b)
      => a / b.m_value;
    public static Force operator *(Force a, double b)
      => new(a.m_value * b);
    public static Force operator *(Force a, Force b)
      => a * b.m_value;
    public static Force operator %(Force a, double b)
      => new(a.m_value % b);
    public static Force operator %(Force a, Force b)
      => a % b.m_value;
    public static Force operator -(Force a, double b)
      => new(a.m_value - b);
    public static Force operator -(Force a, Force b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Force other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Force other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Force o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} N }}";
    #endregion Object overrides
  }
}
