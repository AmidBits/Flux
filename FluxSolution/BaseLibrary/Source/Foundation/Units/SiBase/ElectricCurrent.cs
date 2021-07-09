namespace Flux.Units
{
  public enum ElectricCurrentUnit
  {
    Milliampere,
    Ampere,
  }

  /// <summary>Electric current.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Electric_current"/>
  public struct ElectricCurrent
    : System.IComparable<ElectricCurrent>, System.IEquatable<ElectricCurrent>, IStandardizedScalar
  {
    private readonly double m_ampere;

    public ElectricCurrent(double ampere)
      => m_ampere = ampere;

    public double Ampere
      => m_ampere;

    public double ToUnitValue(ElectricCurrentUnit unit)
    {
      switch (unit)
      {
        case ElectricCurrentUnit.Milliampere:
          return m_ampere * 1000;
        case ElectricCurrentUnit.Ampere:
          return m_ampere;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Static methods
    /// <summary>Creates a new ElectricCurrent instance from power and voltage.</summary>
    /// <param name="power"></param>
    /// <param name="voltage"></param>
    public static ElectricCurrent From(Power power, Voltage voltage)
      => new ElectricCurrent(power.Watt / voltage.Volt);
    /// <summary>Creates a new ElectricCurrent instance from voltage and resistance.</summary>
    /// <param name="voltage"></param>
    /// <param name="resistance"></param>
    public static ElectricCurrent From(Voltage voltage, ElectricResistance resistance)
      => new ElectricCurrent(voltage.Volt / resistance.Ohm);
    public static ElectricCurrent FromUnitValue(ElectricCurrentUnit unit, double value)
    {
      switch (unit)
      {
        case ElectricCurrentUnit.Milliampere:
          return new ElectricCurrent(value / 1000);
        case ElectricCurrentUnit.Ampere:
          return new ElectricCurrent(value);
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(ElectricCurrent v)
      => v.m_ampere;
    public static explicit operator ElectricCurrent(double v)
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

    public static ElectricCurrent operator -(ElectricCurrent v)
      => new ElectricCurrent(-v.m_ampere);
    public static ElectricCurrent operator +(ElectricCurrent a, ElectricCurrent b)
      => new ElectricCurrent(a.m_ampere + b.m_ampere);
    public static ElectricCurrent operator /(ElectricCurrent a, ElectricCurrent b)
      => new ElectricCurrent(a.m_ampere / b.m_ampere);
    public static ElectricCurrent operator *(ElectricCurrent a, ElectricCurrent b)
      => new ElectricCurrent(a.m_ampere * b.m_ampere);
    public static ElectricCurrent operator %(ElectricCurrent a, ElectricCurrent b)
      => new ElectricCurrent(a.m_ampere % b.m_ampere);
    public static ElectricCurrent operator -(ElectricCurrent a, ElectricCurrent b)
      => new ElectricCurrent(a.m_ampere - b.m_ampere);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricCurrent other)
      => m_ampere.CompareTo(other.m_ampere);

    // IEquatable
    public bool Equals(ElectricCurrent other)
      => m_ampere == other.m_ampere;

    // IUnitStandardized
    public double GetScalar()
      => m_ampere;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ElectricCurrent o && Equals(o);
    public override int GetHashCode()
      => m_ampere.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_ampere} A>";
    #endregion Object overrides
  }
}
