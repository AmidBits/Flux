namespace Flux.Units
{
  /// <summary>Frequency is a mutable data type to accomodate changes across multiple consumers.</summary>
  public struct ElectricCharge
    : System.IComparable<ElectricCharge>, System.IEquatable<ElectricCharge>, System.IFormattable
  {
    private readonly double m_Coulomb;

    public ElectricCharge(double coulomb)
      => m_Coulomb = coulomb;

    public double Coulomb
      => m_Coulomb;

    #region Static methods
    public static ElectricCharge Add(ElectricCharge left, ElectricCharge right)
      => new ElectricCharge(left.m_Coulomb + right.m_Coulomb);
    public static ElectricCharge Divide(ElectricCharge left, ElectricCharge right)
      => new ElectricCharge(left.m_Coulomb / right.m_Coulomb);
    public static ElectricCharge Multiply(ElectricCharge left, ElectricCharge right)
      => new ElectricCharge(left.m_Coulomb * right.m_Coulomb);
    public static ElectricCharge Negate(ElectricCharge value)
      => new ElectricCharge(-value.m_Coulomb);
    public static ElectricCharge Remainder(ElectricCharge dividend, ElectricCharge divisor)
      => new ElectricCharge(dividend.m_Coulomb % divisor.m_Coulomb);
    public static ElectricCharge Subtract(ElectricCharge left, ElectricCharge right)
      => new ElectricCharge(left.m_Coulomb - right.m_Coulomb);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(ElectricCharge v)
      => v.m_Coulomb;
    public static implicit operator ElectricCharge(double v)
      => new ElectricCharge(v);

    public static bool operator <(ElectricCharge a, ElectricCharge b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricCharge a, ElectricCharge b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricCharge a, ElectricCharge b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(ElectricCharge a, ElectricCharge b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(ElectricCharge a, ElectricCharge b)
      => a.Equals(b);
    public static bool operator !=(ElectricCharge a, ElectricCharge b)
      => !a.Equals(b);

    public static ElectricCharge operator +(ElectricCharge a, ElectricCharge b)
      => Add(a, b);
    public static ElectricCharge operator /(ElectricCharge a, ElectricCharge b)
      => Divide(a, b);
    public static ElectricCharge operator *(ElectricCharge a, ElectricCharge b)
      => Multiply(a, b);
    public static ElectricCharge operator -(ElectricCharge v)
      => Negate(v);
    public static ElectricCharge operator %(ElectricCharge a, ElectricCharge b)
      => Remainder(a, b);
    public static ElectricCharge operator -(ElectricCharge a, ElectricCharge b)
      => Subtract(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricCharge other)
      => m_Coulomb.CompareTo(other.m_Coulomb);

    // IEquatable
    public bool Equals(ElectricCharge other)
      => m_Coulomb == other.m_Coulomb;

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Format(formatProvider, format ?? $"<{nameof(ElectricCharge)}: {{0:D3}}>", this);
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ElectricCharge o && Equals(o);
    public override int GetHashCode()
      => m_Coulomb.GetHashCode();
    public override string ToString()
      => ToString(null, null);
    #endregion Object overrides
  }
}
