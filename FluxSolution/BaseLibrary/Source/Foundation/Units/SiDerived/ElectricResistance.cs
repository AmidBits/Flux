namespace Flux.Units
{
  /// <summary>Electric resistance.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Electric_resistance"/>
  public struct ElectricResistance
    : System.IComparable<ElectricResistance>, System.IEquatable<ElectricResistance>, IStandardizedScalar
  {
    private readonly double m_ohm;

    public ElectricResistance(double ohm)
      => m_ohm = ohm;

    public double Ohm
      => m_ohm;

    #region Static methods
    /// <summary>Creates a new ElectricResistance instance from the specified voltage and current.</summary>
    /// <param name="voltage"></param>
    /// <param name="current"></param>
    public static ElectricResistance From(Voltage voltage, ElectricCurrent current)
      => new ElectricResistance(voltage.Volt / current.Ampere);
    /// <summary>Converts resistor values as if in parallel configuration.</summary>
    public static ElectricResistance FromParallelResistors(params double[] resistors)
    {
      var sum = 0.0;
      foreach (var resistor in resistors)
        sum += 1 / resistor;
      return (ElectricResistance)(1 / sum);
    }
    /// <summary>Converts resistor values as if in serial configuration.</summary>
    public static ElectricResistance FromSerialResistors(params double[] resistors)
    {
      var sum = 0.0;
      foreach (var resistor in resistors)
        sum += resistor;
      return (ElectricResistance)sum;
    }
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(ElectricResistance v)
      => v.m_ohm;
    public static explicit operator ElectricResistance(double v)
      => new ElectricResistance(v);

    public static bool operator <(ElectricResistance a, ElectricResistance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricResistance a, ElectricResistance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricResistance a, ElectricResistance b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(ElectricResistance a, ElectricResistance b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(ElectricResistance a, ElectricResistance b)
      => a.Equals(b);
    public static bool operator !=(ElectricResistance a, ElectricResistance b)
      => !a.Equals(b);

    public static ElectricResistance operator -(ElectricResistance v)
      => new ElectricResistance(-v.m_ohm);
    public static ElectricResistance operator +(ElectricResistance a, ElectricResistance b)
      => new ElectricResistance(a.m_ohm + b.m_ohm);
    public static ElectricResistance operator /(ElectricResistance a, ElectricResistance b)
      => new ElectricResistance(a.m_ohm / b.m_ohm);
    public static ElectricResistance operator *(ElectricResistance a, ElectricResistance b)
      => new ElectricResistance(a.m_ohm * b.m_ohm);
    public static ElectricResistance operator %(ElectricResistance a, ElectricResistance b)
      => new ElectricResistance(a.m_ohm % b.m_ohm);
    public static ElectricResistance operator -(ElectricResistance a, ElectricResistance b)
      => new ElectricResistance(a.m_ohm - b.m_ohm);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricResistance other)
      => m_ohm.CompareTo(other.m_ohm);

    // IEquatable
    public bool Equals(ElectricResistance other)
      => m_ohm == other.m_ohm;

    // IUnitStandardized
    public double GetScalar()
      => m_ohm;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ElectricResistance o && Equals(o);
    public override int GetHashCode()
      => m_ohm.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_ohm} \u03A9>";
    #endregion Object overrides
  }
}
