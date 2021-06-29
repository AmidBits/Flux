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
    public static ElectricResistance Add(ElectricResistance left, ElectricResistance right)
      => new ElectricResistance(left.m_ohm + right.m_ohm);
    public static ElectricResistance Divide(ElectricResistance left, ElectricResistance right)
      => new ElectricResistance(left.m_ohm / right.m_ohm);
    public static ElectricResistance FromVI(Voltage v, ElectricCurrent i)
      => new ElectricResistance(v / i);
    /// <summary>Converts resistor values as if in parallel configuration.</summary>
    public static ElectricResistance FromParallelResistors(params double[] resistors)
    {
      var sum = 0.0;
      foreach (var resistor in resistors)
        sum += 1 / resistor;
      return 1 / sum;
    }
    /// <summary>Converts resistor values as if in parallel configuration.</summary>
    public static ElectricResistance FromSerialResistors(params double[] resistors)
    {
      var sum = 0.0;
      foreach (var resistor in resistors)
        sum += resistor;
      return sum;
    }
    public static ElectricResistance Multiply(ElectricResistance left, ElectricResistance right)
      => new ElectricResistance(left.m_ohm * right.m_ohm);
    public static ElectricResistance Negate(ElectricResistance value)
      => new ElectricResistance(-value.m_ohm);
    public static ElectricResistance Remainder(ElectricResistance dividend, ElectricResistance divisor)
      => new ElectricResistance(dividend.m_ohm % divisor.m_ohm);
    public static ElectricResistance Subtract(ElectricResistance left, ElectricResistance right)
      => new ElectricResistance(left.m_ohm - right.m_ohm);
    #endregion Static methods

    #region Overloaded operators
    public static implicit operator double(ElectricResistance v)
      => v.m_ohm;
    public static implicit operator ElectricResistance(double v)
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

    public static ElectricResistance operator +(ElectricResistance a, ElectricResistance b)
      => Add(a, b);
    public static ElectricResistance operator /(ElectricResistance a, ElectricResistance b)
      => Divide(a, b);
    public static ElectricResistance operator *(ElectricResistance a, ElectricResistance b)
      => Multiply(a, b);
    public static ElectricResistance operator -(ElectricResistance v)
      => Negate(v);
    public static ElectricResistance operator %(ElectricResistance a, ElectricResistance b)
      => Remainder(a, b);
    public static ElectricResistance operator -(ElectricResistance a, ElectricResistance b)
      => Subtract(a, b);
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
