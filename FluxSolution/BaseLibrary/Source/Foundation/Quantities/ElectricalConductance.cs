namespace Flux.Quantity
{
  /// <summary>Electrical conductance unit of Siemens.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Force"/>
  public struct ElectricalConductance
    : System.IComparable<ElectricalConductance>, System.IEquatable<ElectricalConductance>, IValuedUnit
  {
    private readonly double m_value;

    public ElectricalConductance(double siemens)
      => m_value = siemens;

    public double Value
      => m_value;

    #region Overloaded operators
    public static explicit operator double(ElectricalConductance v)
      => v.m_value;
    public static explicit operator ElectricalConductance(double v)
      => new ElectricalConductance(v);

    public static bool operator <(ElectricalConductance a, ElectricalConductance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricalConductance a, ElectricalConductance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricalConductance a, ElectricalConductance b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(ElectricalConductance a, ElectricalConductance b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(ElectricalConductance a, ElectricalConductance b)
      => a.Equals(b);
    public static bool operator !=(ElectricalConductance a, ElectricalConductance b)
      => !a.Equals(b);

    public static ElectricalConductance operator -(ElectricalConductance v)
      => new ElectricalConductance(-v.m_value);
    public static ElectricalConductance operator +(ElectricalConductance a, double b)
      => new ElectricalConductance(a.m_value + b);
    public static ElectricalConductance operator +(ElectricalConductance a, ElectricalConductance b)
      => a + b.m_value;
    public static ElectricalConductance operator /(ElectricalConductance a, double b)
      => new ElectricalConductance(a.m_value / b);
    public static ElectricalConductance operator /(ElectricalConductance a, ElectricalConductance b)
      => a / b.m_value;
    public static ElectricalConductance operator *(ElectricalConductance a, double b)
      => new ElectricalConductance(a.m_value * b);
    public static ElectricalConductance operator *(ElectricalConductance a, ElectricalConductance b)
      => a * b.m_value;
    public static ElectricalConductance operator %(ElectricalConductance a, double b)
      => new ElectricalConductance(a.m_value % b);
    public static ElectricalConductance operator %(ElectricalConductance a, ElectricalConductance b)
      => a % b.m_value;
    public static ElectricalConductance operator -(ElectricalConductance a, double b)
      => new ElectricalConductance(a.m_value - b);
    public static ElectricalConductance operator -(ElectricalConductance a, ElectricalConductance b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricalConductance other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(ElectricalConductance other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ElectricalConductance o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} S>";
    #endregion Object overrides
  }
}
