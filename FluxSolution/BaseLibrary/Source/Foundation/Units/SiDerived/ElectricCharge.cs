namespace Flux.Units
{
  /// <summary>Electric charge unit of Coulomb.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Electric_charge"/>
  public struct ElectricCharge
    : System.IComparable<ElectricCharge>, System.IEquatable<ElectricCharge>, IValuedUnit
  {
    private readonly double m_value;

    public ElectricCharge(double coulomb)
      => m_value = coulomb;

    public double Value
      => m_value;

    #region Overloaded operators
    public static explicit operator double(ElectricCharge v)
      => v.m_value;
    public static explicit operator ElectricCharge(double v)
      => new ElectricCharge(v);

    public static bool operator <(ElectricCharge a, ElectricCharge b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricCharge a, ElectricCharge b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricCharge a, ElectricCharge b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(ElectricCharge a, ElectricCharge b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(ElectricCharge a, ElectricCharge b)
      => a.Equals(b);
    public static bool operator !=(ElectricCharge a, ElectricCharge b)
      => !a.Equals(b);

    public static ElectricCharge operator -(ElectricCharge v)
      => new ElectricCharge(-v.m_value);
    public static ElectricCharge operator +(ElectricCharge a, double b)
      => new ElectricCharge(a.m_value + b);
    public static ElectricCharge operator +(ElectricCharge a, ElectricCharge b)
      => a + b.m_value;
    public static ElectricCharge operator /(ElectricCharge a, double b)
      => new ElectricCharge(a.m_value / b);
    public static ElectricCharge operator /(ElectricCharge a, ElectricCharge b)
      => a / b.m_value;
    public static ElectricCharge operator *(ElectricCharge a, double b)
      => new ElectricCharge(a.m_value * b);
    public static ElectricCharge operator *(ElectricCharge a, ElectricCharge b)
      => a * b.m_value;
    public static ElectricCharge operator %(ElectricCharge a, double b)
      => new ElectricCharge(a.m_value % b);
    public static ElectricCharge operator %(ElectricCharge a, ElectricCharge b)
      => a % b.m_value;
    public static ElectricCharge operator -(ElectricCharge a, double b)
      => new ElectricCharge(a.m_value - b);
    public static ElectricCharge operator -(ElectricCharge a, ElectricCharge b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricCharge other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(ElectricCharge other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ElectricCharge o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} C>";
    #endregion Object overrides
  }
}
