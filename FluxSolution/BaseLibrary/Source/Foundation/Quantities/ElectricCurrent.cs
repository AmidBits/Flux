namespace Flux.Quantity
{
  /// <summary>Electric current. SI unit of ampere. This is a base quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Electric_current"/>
  public struct ElectricCurrent
    : System.IComparable<ElectricCurrent>, System.IEquatable<ElectricCurrent>, IValuedUnit
  {
    private readonly double m_value;

    public ElectricCurrent(double value, ElectricCurrentUnit unit = ElectricCurrentUnit.Ampere)
    {
      switch (unit)
      {
        case ElectricCurrentUnit.Milliampere:
          m_value = value / 1000;
          break;
        case ElectricCurrentUnit.Ampere:
          m_value = value;
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    public double Value
      => m_value;

    public double ToUnitValue(ElectricCurrentUnit unit = ElectricCurrentUnit.Ampere)
    {
      switch (unit)
      {
        case ElectricCurrentUnit.Milliampere:
          return m_value * 1000;
        case ElectricCurrentUnit.Ampere:
          return m_value;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Static methods
    /// <summary>Creates a new ElectricCurrent instance from power and voltage.</summary>
    /// <param name="power"></param>
    /// <param name="voltage"></param>
    public static ElectricCurrent From(Power power, Voltage voltage)
      => new ElectricCurrent(power.Value / voltage.Value);
    /// <summary>Creates a new ElectricCurrent instance from voltage and resistance.</summary>
    /// <param name="voltage"></param>
    /// <param name="resistance"></param>
    public static ElectricCurrent From(Voltage voltage, ElectricResistance resistance)
      => new ElectricCurrent(voltage.Value / resistance.Value);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(ElectricCurrent v)
      => v.m_value;
    public static explicit operator ElectricCurrent(double v)
      => new ElectricCurrent(v);

    public static bool operator <(ElectricCurrent a, ElectricCurrent b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricCurrent a, ElectricCurrent b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricCurrent a, ElectricCurrent b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(ElectricCurrent a, ElectricCurrent b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(ElectricCurrent a, ElectricCurrent b)
      => a.Equals(b);
    public static bool operator !=(ElectricCurrent a, ElectricCurrent b)
      => !a.Equals(b);

    public static ElectricCurrent operator -(ElectricCurrent v)
      => new ElectricCurrent(-v.m_value);
    public static ElectricCurrent operator +(ElectricCurrent a, double b)
      => new ElectricCurrent(a.m_value + b);
    public static ElectricCurrent operator +(ElectricCurrent a, ElectricCurrent b)
      => a + b.m_value;
    public static ElectricCurrent operator /(ElectricCurrent a, double b)
      => new ElectricCurrent(a.m_value / b);
    public static ElectricCurrent operator /(ElectricCurrent a, ElectricCurrent b)
      => a / b.m_value;
    public static ElectricCurrent operator *(ElectricCurrent a, double b)
      => new ElectricCurrent(a.m_value * b);
    public static ElectricCurrent operator *(ElectricCurrent a, ElectricCurrent b)
      => a * b.m_value;
    public static ElectricCurrent operator %(ElectricCurrent a, double b)
      => new ElectricCurrent(a.m_value % b);
    public static ElectricCurrent operator %(ElectricCurrent a, ElectricCurrent b)
      => a % b.m_value;
    public static ElectricCurrent operator -(ElectricCurrent a, double b)
      => new ElectricCurrent(a.m_value - b);
    public static ElectricCurrent operator -(ElectricCurrent a, ElectricCurrent b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricCurrent other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(ElectricCurrent other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ElectricCurrent o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} A>";
    #endregion Object overrides
  }
}
