namespace Flux.Units
{
  /// <summary>Frequency is a mutable data type to accomodate changes across multiple consumers.</summary>
  public struct ElectricCurrent
    : System.IComparable<ElectricCurrent>, System.IEquatable<ElectricCurrent>, System.IFormattable
  {
    private readonly double m_ampere;

    public ElectricCurrent(double ampere)
      => m_ampere = ampere;

    public double Ampere
      => m_ampere;

    #region Static methods
    public static ElectricCurrent Add(ElectricCurrent left, ElectricCurrent right)
      => new ElectricCurrent(left.m_ampere + right.m_ampere);
    public static ElectricCurrent Divide(ElectricCurrent left, ElectricCurrent right)
      => new ElectricCurrent(left.m_ampere / right.m_ampere);
    public static ElectricCurrent Multiply(ElectricCurrent left, ElectricCurrent right)
      => new ElectricCurrent(left.m_ampere * right.m_ampere);
    public static ElectricCurrent Negate(ElectricCurrent value)
      => new ElectricCurrent(-value.m_ampere);
    public static ElectricCurrent Remainder(ElectricCurrent dividend, ElectricCurrent divisor)
      => new ElectricCurrent(dividend.m_ampere % divisor.m_ampere);
    public static ElectricCurrent Subtract(ElectricCurrent left, ElectricCurrent right)
      => new ElectricCurrent(left.m_ampere - right.m_ampere);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(ElectricCurrent v)
      => v.m_ampere;
    public static implicit operator ElectricCurrent(double v)
      => new ElectricCurrent(v);

    public static bool operator <(ElectricCurrent a, ElectricCurrent b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricCurrent a, ElectricCurrent b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricCurrent a, ElectricCurrent b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(ElectricCurrent a, ElectricCurrent b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(ElectricCurrent a, ElectricCurrent b)
      => a.Equals(b);
    public static bool operator !=(ElectricCurrent a, ElectricCurrent b)
      => !a.Equals(b);

    public static ElectricCurrent operator +(ElectricCurrent a, ElectricCurrent b)
      => Add(a, b);
    public static ElectricCurrent operator /(ElectricCurrent a, ElectricCurrent b)
      => Divide(a, b);
    public static ElectricCurrent operator *(ElectricCurrent a, ElectricCurrent b)
      => Multiply(a, b);
    public static ElectricCurrent operator -(ElectricCurrent v)
      => Negate(v);
    public static ElectricCurrent operator %(ElectricCurrent a, ElectricCurrent b)
      => Remainder(a, b);
    public static ElectricCurrent operator -(ElectricCurrent a, ElectricCurrent b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricCurrent other)
      => m_ampere.CompareTo(other.m_ampere);

    // IEquatable
    public bool Equals(ElectricCurrent other)
      => m_ampere == other.m_ampere;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(ElectricCurrent)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ElectricCurrent o && Equals(o);
    public override int GetHashCode()
      => m_ampere.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
