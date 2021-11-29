namespace Flux.Quantity
{
  public enum ElectricalConductanceUnit
  {
    Siemens,
  }

  /// <summary>Electrical conductance unit of Siemens.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Force"/>
#if NET5_0
  public struct ElectricalConductance
    : System.IComparable<ElectricalConductance>, System.IEquatable<ElectricalConductance>, IValuedUnit<double>
#else
  public record struct ElectricalConductance
    : System.IComparable<ElectricalConductance>, IValuedUnit<double>
#endif
  {
    private readonly double m_value;

    public ElectricalConductance(double value, ElectricalConductanceUnit unit = ElectricalConductanceUnit.Siemens)
      => m_value = unit switch
      {
        ElectricalConductanceUnit.Siemens => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public double ToUnitValue(ElectricalConductanceUnit unit = ElectricalConductanceUnit.Siemens)
      => unit switch
      {
        ElectricalConductanceUnit.Siemens => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(ElectricalConductance v)
      => v.m_value;
    public static explicit operator ElectricalConductance(double v)
      => new(v);

    public static bool operator <(ElectricalConductance a, ElectricalConductance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricalConductance a, ElectricalConductance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricalConductance a, ElectricalConductance b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(ElectricalConductance a, ElectricalConductance b)
      => a.CompareTo(b) >= 0;

#if NET5_0
    public static bool operator ==(ElectricalConductance a, ElectricalConductance b)
      => a.Equals(b);
    public static bool operator !=(ElectricalConductance a, ElectricalConductance b)
      => !a.Equals(b);
#endif

    public static ElectricalConductance operator -(ElectricalConductance v)
      => new(-v.m_value);
    public static ElectricalConductance operator +(ElectricalConductance a, double b)
      => new(a.m_value + b);
    public static ElectricalConductance operator +(ElectricalConductance a, ElectricalConductance b)
      => a + b.m_value;
    public static ElectricalConductance operator /(ElectricalConductance a, double b)
      => new(a.m_value / b);
    public static ElectricalConductance operator /(ElectricalConductance a, ElectricalConductance b)
      => a / b.m_value;
    public static ElectricalConductance operator *(ElectricalConductance a, double b)
      => new(a.m_value * b);
    public static ElectricalConductance operator *(ElectricalConductance a, ElectricalConductance b)
      => a * b.m_value;
    public static ElectricalConductance operator %(ElectricalConductance a, double b)
      => new(a.m_value % b);
    public static ElectricalConductance operator %(ElectricalConductance a, ElectricalConductance b)
      => a % b.m_value;
    public static ElectricalConductance operator -(ElectricalConductance a, double b)
      => new(a.m_value - b);
    public static ElectricalConductance operator -(ElectricalConductance a, ElectricalConductance b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricalConductance other)
      => m_value.CompareTo(other.m_value);

#if NET5_0
    // IEquatable
    public bool Equals(ElectricalConductance other)
      => m_value == other.m_value;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is ElectricalConductance o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
#endif
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} S }}";
    #endregion Object overrides
  }
}
