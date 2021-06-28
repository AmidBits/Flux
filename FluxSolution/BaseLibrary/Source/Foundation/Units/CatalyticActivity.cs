namespace Flux.Units
{
  /// <summary>Catalytic activity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Catalysis"/>
  public struct CatalyticActivity
    : System.IComparable<CatalyticActivity>, System.IEquatable<CatalyticActivity>, IStandardizedScalar
  {
    private readonly double m_katal;

    public CatalyticActivity(double katal)
      => m_katal = katal;

    public double Katal
      => m_katal;

    #region Static methods
    public static CatalyticActivity Add(CatalyticActivity left, CatalyticActivity right)
      => new CatalyticActivity(left.m_katal + right.m_katal);
    public static CatalyticActivity Divide(CatalyticActivity left, CatalyticActivity right)
      => new CatalyticActivity(left.m_katal / right.m_katal);
    public static CatalyticActivity Multiply(CatalyticActivity left, CatalyticActivity right)
      => new CatalyticActivity(left.m_katal * right.m_katal);
    public static CatalyticActivity Negate(CatalyticActivity value)
      => new CatalyticActivity(-value.m_katal);
    public static CatalyticActivity Remainder(CatalyticActivity dividend, CatalyticActivity divisor)
      => new CatalyticActivity(dividend.m_katal % divisor.m_katal);
    public static CatalyticActivity Subtract(CatalyticActivity left, CatalyticActivity right)
      => new CatalyticActivity(left.m_katal - right.m_katal);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator double(CatalyticActivity v)
      => v.m_katal;
    public static implicit operator CatalyticActivity(double v)
      => new CatalyticActivity(v);

    public static bool operator <(CatalyticActivity a, CatalyticActivity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(CatalyticActivity a, CatalyticActivity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(CatalyticActivity a, CatalyticActivity b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(CatalyticActivity a, CatalyticActivity b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(CatalyticActivity a, CatalyticActivity b)
      => a.Equals(b);
    public static bool operator !=(CatalyticActivity a, CatalyticActivity b)
      => !a.Equals(b);

    public static CatalyticActivity operator +(CatalyticActivity a, CatalyticActivity b)
      => Add(a, b);
    public static CatalyticActivity operator /(CatalyticActivity a, CatalyticActivity b)
      => Divide(a, b);
    public static CatalyticActivity operator *(CatalyticActivity a, CatalyticActivity b)
      => Multiply(a, b);
    public static CatalyticActivity operator -(CatalyticActivity v)
      => Negate(v);
    public static CatalyticActivity operator %(CatalyticActivity a, CatalyticActivity b)
      => Remainder(a, b);
    public static CatalyticActivity operator -(CatalyticActivity a, CatalyticActivity b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(CatalyticActivity other)
      => m_katal.CompareTo(other.m_katal);

    // IEquatable
    public bool Equals(CatalyticActivity other)
      => m_katal == other.m_katal;

    // IUnitStandardized
    public double GetScalar()
      => m_katal;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is CatalyticActivity o && Equals(o);
    public override int GetHashCode()
      => m_katal.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_katal} kat>";
    #endregion Object overrides
  }
}
