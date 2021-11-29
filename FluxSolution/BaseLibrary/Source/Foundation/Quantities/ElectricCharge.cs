namespace Flux.Quantity
{
  public enum ElectricChargeUnit
  {
    Coulomb,
  }

  /// <summary>Electric charge unit of Coulomb.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Electric_charge"/>
#if NET5_0
  public struct ElectricCharge
    : System.IComparable<ElectricCharge>, System.IEquatable<ElectricCharge>, IValuedUnit<double>
#else
  public record struct ElectricCharge
    : System.IComparable<ElectricCharge>, IValuedUnit<double>
#endif
  {
    public static ElectricCharge ElementaryCharge
      => new(1.602176634e-19);

    private readonly double m_value;

    public ElectricCharge(double value, ElectricChargeUnit unit = ElectricChargeUnit.Coulomb)
      => m_value = unit switch
      {
        ElectricChargeUnit.Coulomb => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public double ToUnitValue(ElectricChargeUnit unit = ElectricChargeUnit.Coulomb)
      => unit switch
      {
        ElectricChargeUnit.Coulomb => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(ElectricCharge v)
      => v.m_value;
    public static explicit operator ElectricCharge(double v)
      => new(v);

    public static bool operator <(ElectricCharge a, ElectricCharge b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricCharge a, ElectricCharge b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricCharge a, ElectricCharge b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(ElectricCharge a, ElectricCharge b)
      => a.CompareTo(b) >= 0;

#if NET5_0
    public static bool operator ==(ElectricCharge a, ElectricCharge b)
      => a.Equals(b);
    public static bool operator !=(ElectricCharge a, ElectricCharge b)
      => !a.Equals(b);
#endif

    public static ElectricCharge operator -(ElectricCharge v)
      => new(-v.m_value);
    public static ElectricCharge operator +(ElectricCharge a, double b)
      => new(a.m_value + b);
    public static ElectricCharge operator +(ElectricCharge a, ElectricCharge b)
      => a + b.m_value;
    public static ElectricCharge operator /(ElectricCharge a, double b)
      => new(a.m_value / b);
    public static ElectricCharge operator /(ElectricCharge a, ElectricCharge b)
      => a / b.m_value;
    public static ElectricCharge operator *(ElectricCharge a, double b)
      => new(a.m_value * b);
    public static ElectricCharge operator *(ElectricCharge a, ElectricCharge b)
      => a * b.m_value;
    public static ElectricCharge operator %(ElectricCharge a, double b)
      => new(a.m_value % b);
    public static ElectricCharge operator %(ElectricCharge a, ElectricCharge b)
      => a % b.m_value;
    public static ElectricCharge operator -(ElectricCharge a, double b)
      => new(a.m_value - b);
    public static ElectricCharge operator -(ElectricCharge a, ElectricCharge b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricCharge other)
      => m_value.CompareTo(other.m_value);

#if NET5_0
    // IEquatable
    public bool Equals(ElectricCharge other)
      => m_value == other.m_value;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is ElectricCharge o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
#endif
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} C }}";
    #endregion Object overrides
  }
}
