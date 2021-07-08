namespace Flux.Units
{
  /// <summary>Voltage.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Voltage"/>
  public struct Voltage
    : System.IComparable<Voltage>, System.IEquatable<Voltage>, IStandardizedScalar
  {
    private readonly double m_volt;

    public Voltage(double volt)
      => m_volt = volt;

    public double Volt
      => m_volt;

    #region Static methods
    public static Voltage From(ElectricCurrent current, ElectricResistance resistance)
      => new Voltage(current.Ampere * resistance.Ohm);
    public static Voltage From(Power power, ElectricCurrent current)
      => new Voltage(power.Watt / current.Ampere);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Voltage v)
      => v.m_volt;
    public static explicit operator Voltage(double v)
      => new Voltage(v);

    public static bool operator <(Voltage a, Voltage b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Voltage a, Voltage b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Voltage a, Voltage b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Voltage a, Voltage b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Voltage a, Voltage b)
      => a.Equals(b);
    public static bool operator !=(Voltage a, Voltage b)
      => !a.Equals(b);

    public static Voltage operator -(Voltage v)
      => new Voltage(-v.m_volt);
    public static Voltage operator +(Voltage a, Voltage b)
      => new Voltage(a.m_volt + b.m_volt);
    public static Voltage operator /(Voltage a, Voltage b)
      => new Voltage(a.m_volt / b.m_volt);
    public static Voltage operator *(Voltage a, Voltage b)
      => new Voltage(a.m_volt * b.m_volt);
    public static Voltage operator %(Voltage a, Voltage b)
      => new Voltage(a.m_volt % b.m_volt);
    public static Voltage operator -(Voltage a, Voltage b)
      => new Voltage(a.m_volt - b.m_volt);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Voltage other)
      => m_volt.CompareTo(other.m_volt);

    // IEquatable
    public bool Equals(Voltage other)
      => m_volt == other.m_volt;

    // IUnitStandardized
    public double GetScalar()
      => m_volt;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Voltage o && Equals(o);
    public override int GetHashCode()
      => m_volt.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_volt} V>";
    #endregion Object overrides
  }
}
