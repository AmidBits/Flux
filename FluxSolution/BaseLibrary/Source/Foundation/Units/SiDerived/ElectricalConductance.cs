namespace Flux.Units
{
  /// <summary>Force.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Force"/>
  public struct ElectricalConductance
    : System.IComparable<ElectricalConductance>, System.IEquatable<ElectricalConductance>, IStandardizedScalar
  {
    private readonly double m_siemens;

    public ElectricalConductance(double siemens)
      => m_siemens = siemens;

    public double Siemens
      => m_siemens;

    #region Overloaded operators
    public static explicit operator double(ElectricalConductance v)
      => v.m_siemens;
    public static explicit operator ElectricalConductance(double v)
      => new ElectricalConductance(v);

    public static bool operator <(ElectricalConductance a, ElectricalConductance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricalConductance a, ElectricalConductance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricalConductance a, ElectricalConductance b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(ElectricalConductance a, ElectricalConductance b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(ElectricalConductance a, ElectricalConductance b)
      => a.Equals(b);
    public static bool operator !=(ElectricalConductance a, ElectricalConductance b)
      => !a.Equals(b);

    public static ElectricalConductance operator -(ElectricalConductance v)
      => new ElectricalConductance(-v.m_siemens);
    public static ElectricalConductance operator +(ElectricalConductance a, ElectricalConductance b)
      => new ElectricalConductance(a.m_siemens + b.m_siemens);
    public static ElectricalConductance operator /(ElectricalConductance a, ElectricalConductance b)
      => new ElectricalConductance(a.m_siemens / b.m_siemens);
    public static ElectricalConductance operator *(ElectricalConductance a, ElectricalConductance b)
      => new ElectricalConductance(a.m_siemens * b.m_siemens);
    public static ElectricalConductance operator %(ElectricalConductance a, ElectricalConductance b)
      => new ElectricalConductance(a.m_siemens % b.m_siemens);
    public static ElectricalConductance operator -(ElectricalConductance a, ElectricalConductance b)
      => new ElectricalConductance(a.m_siemens - b.m_siemens);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricalConductance other)
      => m_siemens.CompareTo(other.m_siemens);

    // IEquatable
    public bool Equals(ElectricalConductance other)
      => m_siemens == other.m_siemens;

    // IUnitStandardized
    public double GetScalar()
      => m_siemens;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ElectricalConductance o && Equals(o);
    public override int GetHashCode()
      => m_siemens.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_siemens} S>";
    #endregion Object overrides
  }
}
