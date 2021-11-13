namespace Flux.Quantity
{
  public enum CatalyticActivityUnit
  {
    Katal,
  }

  /// <summary>Catalytic activity unit of Katal.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Catalysis"/>
  public struct CatalyticActivity
    : System.IComparable<CatalyticActivity>, System.IEquatable<CatalyticActivity>, IValuedUnit<double>
  {
    private readonly double m_value;

    public CatalyticActivity(double value, CatalyticActivityUnit unit = CatalyticActivityUnit.Katal)
      => m_value = unit switch
      {
        CatalyticActivityUnit.Katal => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public double ToUnitValue(CatalyticActivityUnit unit = CatalyticActivityUnit.Katal)
      => unit switch
      {
        CatalyticActivityUnit.Katal => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(CatalyticActivity v)
      => v.m_value;
    public static explicit operator CatalyticActivity(double v)
      => new(v);

    public static bool operator <(CatalyticActivity a, CatalyticActivity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(CatalyticActivity a, CatalyticActivity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(CatalyticActivity a, CatalyticActivity b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(CatalyticActivity a, CatalyticActivity b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(CatalyticActivity a, CatalyticActivity b)
      => a.Equals(b);
    public static bool operator !=(CatalyticActivity a, CatalyticActivity b)
      => !a.Equals(b);

    public static CatalyticActivity operator -(CatalyticActivity v)
      => new(-v.m_value);
    public static CatalyticActivity operator +(CatalyticActivity a, double b)
      => new(a.m_value + b);
    public static CatalyticActivity operator +(CatalyticActivity a, CatalyticActivity b)
      => a + b.m_value;
    public static CatalyticActivity operator /(CatalyticActivity a, double b)
      => new(a.m_value / b);
    public static CatalyticActivity operator /(CatalyticActivity a, CatalyticActivity b)
      => a / b.m_value;
    public static CatalyticActivity operator *(CatalyticActivity a, double b)
      => new(a.m_value * b);
    public static CatalyticActivity operator *(CatalyticActivity a, CatalyticActivity b)
      => a * b.m_value;
    public static CatalyticActivity operator %(CatalyticActivity a, double b)
      => new(a.m_value % b);
    public static CatalyticActivity operator %(CatalyticActivity a, CatalyticActivity b)
      => a % b.m_value;
    public static CatalyticActivity operator -(CatalyticActivity a, double b)
      => new(a.m_value - b);
    public static CatalyticActivity operator -(CatalyticActivity a, CatalyticActivity b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(CatalyticActivity other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(CatalyticActivity other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is CatalyticActivity o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} kat>";
    #endregion Object overrides
  }
}
