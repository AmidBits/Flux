namespace Flux.Quantity
{
  public enum ElectricResistanceUnit
  {
    Ohm,
  }

  /// <summary>Electric resistance unit of Ohm.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Electric_resistance"/>
  public struct ElectricResistance
    : System.IComparable<ElectricResistance>, System.IEquatable<ElectricResistance>, IValuedUnit
  {
    public static ElectricResistance VonKlitzing
      => new(25812.80745); // 25812.80745;

    private readonly double m_value;

    public ElectricResistance(double value, ElectricResistanceUnit unit = ElectricResistanceUnit.Ohm)
      => m_value = unit switch
      {
        ElectricResistanceUnit.Ohm => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public double ToUnitValue(ElectricResistanceUnit unit = ElectricResistanceUnit.Ohm)
      => unit switch
      {
        ElectricResistanceUnit.Ohm => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Creates a new ElectricResistance instance from the specified voltage and current.</summary>
    /// <param name="voltage"></param>
    /// <param name="current"></param>
    public static ElectricResistance From(Voltage voltage, ElectricCurrent current)
      => new(voltage.Value / current.Value);
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
      => v.m_value;
    public static explicit operator ElectricResistance(double v)
      => new(v);

    public static bool operator <(ElectricResistance a, ElectricResistance b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(ElectricResistance a, ElectricResistance b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(ElectricResistance a, ElectricResistance b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(ElectricResistance a, ElectricResistance b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(ElectricResistance a, ElectricResistance b)
      => a.Equals(b);
    public static bool operator !=(ElectricResistance a, ElectricResistance b)
      => !a.Equals(b);

    public static ElectricResistance operator -(ElectricResistance v)
      => new(-v.m_value);
    public static ElectricResistance operator +(ElectricResistance a, double b)
      => new(a.m_value + b);
    public static ElectricResistance operator +(ElectricResistance a, ElectricResistance b)
      => a + b.m_value;
    public static ElectricResistance operator /(ElectricResistance a, double b)
      => new(a.m_value / b);
    public static ElectricResistance operator /(ElectricResistance a, ElectricResistance b)
      => a / b.m_value;
    public static ElectricResistance operator *(ElectricResistance a, double b)
      => new(a.m_value * b);
    public static ElectricResistance operator *(ElectricResistance a, ElectricResistance b)
      => a * b.m_value;
    public static ElectricResistance operator %(ElectricResistance a, double b)
      => new(a.m_value % b);
    public static ElectricResistance operator %(ElectricResistance a, ElectricResistance b)
      => a % b.m_value;
    public static ElectricResistance operator -(ElectricResistance a, double b)
      => new(a.m_value - b);
    public static ElectricResistance operator -(ElectricResistance a, ElectricResistance b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(ElectricResistance other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(ElectricResistance other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ElectricResistance o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} \u2126>";
    #endregion Object overrides
  }
}
