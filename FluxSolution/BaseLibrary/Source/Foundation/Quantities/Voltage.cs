namespace Flux.Quantity
{
  public enum VoltageUnit
  {
    Volt,
  }

  /// <summary>Voltage unit of volt.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Voltage"/>
#if NET5_0
  public struct Voltage
    : System.IComparable<Voltage>, System.IEquatable<Voltage>, IValuedUnit<double>
#else
  public record struct Voltage
    : System.IComparable<Voltage>, IValuedUnit<double>
#endif
  {
    private readonly double m_value;

    public Voltage(double value, VoltageUnit unit = VoltageUnit.Volt)
      => m_value = unit switch
      {
        VoltageUnit.Volt => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public double ToUnitValue(VoltageUnit unit = VoltageUnit.Volt)
      => unit switch
      {
        VoltageUnit.Volt => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    /// <summary>Creates a new Voltage instance from the specified current and resistance.</summary>
    /// <param name="current"></param>
    /// <param name="resistance"></param>
    public static Voltage From(ElectricCurrent current, ElectricResistance resistance)
      => new(current.Value * resistance.Value);
    /// <summary>Creates a new Voltage instance from the specified power and current.</summary>
    /// <param name="power"></param>
    /// <param name="current"></param>
    public static Voltage From(Power power, ElectricCurrent current)
      => new(power.Value / current.Value);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Voltage v)
      => v.m_value;
    public static explicit operator Voltage(double v)
      => new(v);

    public static bool operator <(Voltage a, Voltage b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Voltage a, Voltage b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Voltage a, Voltage b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Voltage a, Voltage b)
      => a.CompareTo(b) >= 0;

#if NET5_0
    public static bool operator ==(Voltage a, Voltage b)
      => a.Equals(b);
    public static bool operator !=(Voltage a, Voltage b)
      => !a.Equals(b);
#endif

    public static Voltage operator -(Voltage v)
      => new(-v.m_value);
    public static Voltage operator +(Voltage a, double b)
      => new(a.m_value + b);
    public static Voltage operator +(Voltage a, Voltage b)
      => a + b.m_value;
    public static Voltage operator /(Voltage a, double b)
      => new(a.m_value / b);
    public static Voltage operator /(Voltage a, Voltage b)
      => a / b.m_value;
    public static Voltage operator *(Voltage a, double b)
      => new(a.m_value * b);
    public static Voltage operator *(Voltage a, Voltage b)
      => a * b.m_value;
    public static Voltage operator %(Voltage a, double b)
      => new(a.m_value % b);
    public static Voltage operator %(Voltage a, Voltage b)
      => a % b.m_value;
    public static Voltage operator -(Voltage a, double b)
      => new(a.m_value - b);
    public static Voltage operator -(Voltage a, Voltage b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Voltage other)
      => m_value.CompareTo(other.m_value);

#if NET5_0
    // IEquatable
    public bool Equals(Voltage other)
      => m_value == other.m_value;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is Voltage o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
#endif
    public override string ToString()
      => $"{GetType().Name} {{ {m_value} V }}";
    #endregion Object overrides
  }
}
