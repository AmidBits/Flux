namespace Flux.Units
{
  /// <summary>Electric charge.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Electric_charge"/>
  public struct ElectricCharge
    : System.IComparable<ElectricCharge>, System.IEquatable<ElectricCharge>, IStandardizedScalar
  {
    private readonly double m_coulomb;

    public ElectricCharge(double coulomb)
      => m_coulomb = coulomb;

    public double Coulomb
      => m_coulomb;

    #region Overloaded operators
    public static explicit operator double(ElectricCharge v)
      => v.m_coulomb;
    public static explicit operator ElectricCharge(double v)
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

    public static ElectricCharge operator -(ElectricCharge v)
      => new ElectricCharge(-v.m_coulomb);
    public static ElectricCharge operator +(ElectricCharge a, ElectricCharge b)
      => new ElectricCharge(a.m_coulomb + b.m_coulomb);
    public static ElectricCharge operator /(ElectricCharge a, ElectricCharge b)
      => new ElectricCharge(a.m_coulomb / b.m_coulomb);
    public static ElectricCharge operator *(ElectricCharge a, ElectricCharge b)
      => new ElectricCharge(a.m_coulomb * b.m_coulomb);
    public static ElectricCharge operator %(ElectricCharge a, ElectricCharge b)
      => new ElectricCharge(a.m_coulomb % b.m_coulomb);
    public static ElectricCharge operator -(ElectricCharge a, ElectricCharge b)
      => new ElectricCharge(a.m_coulomb - b.m_coulomb);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricCharge other)
      => m_coulomb.CompareTo(other.m_coulomb);

    // IEquatable
    public bool Equals(ElectricCharge other)
      => m_coulomb == other.m_coulomb;

    // IUnitStandardized
    public double GetScalar()
      => m_coulomb;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ElectricCharge o && Equals(o);
    public override int GetHashCode()
      => m_coulomb.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_coulomb} C>";
    #endregion Object overrides
  }
}
