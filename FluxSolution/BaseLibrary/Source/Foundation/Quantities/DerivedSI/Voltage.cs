namespace Flux
{
  public enum VoltageUnit
  {
    Volt,
  }

  /// <summary>Voltage unit of volt.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Voltage"/>
  public struct Voltage
    : System.IComparable<Voltage>, System.IEquatable<Voltage>, IValueGeneralizedUnit<double>, IValueDerivedUnitSI<double>
  {
    private readonly double m_value;

    public Voltage(double value, VoltageUnit unit = VoltageUnit.Volt)
      => m_value = unit switch
      {
        VoltageUnit.Volt => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double DerivedUnitValue
      => m_value;

    public double GeneralUnitValue
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
      => new(current.GeneralUnitValue * resistance.GeneralUnitValue);
    /// <summary>Creates a new Voltage instance from the specified power and current.</summary>
    /// <param name="power"></param>
    /// <param name="current"></param>
    public static Voltage From(Power power, ElectricCurrent current)
      => new(power.GeneralUnitValue / current.GeneralUnitValue);
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

    public static bool operator ==(Voltage a, Voltage b)
      => a.Equals(b);
    public static bool operator !=(Voltage a, Voltage b)
      => !a.Equals(b);

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

    // IEquatable
    public bool Equals(Voltage other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Voltage o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ {m_value} V }}";
    #endregion Object overrides
  }
}
