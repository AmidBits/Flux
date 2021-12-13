namespace Flux
{
  public enum TorqueUnit
  {
    NewtonMeter,
  }

  /// <summary>Torque unit of newton meter.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Torque"/>
  public struct Torque
    : System.IComparable<Torque>, System.IEquatable<Torque>, IValueGeneralizedUnit<double>, IValueDerivedUnitSI<double>
  {
    private readonly double m_value;

    public Torque(double value, TorqueUnit unit = TorqueUnit.NewtonMeter)
      => m_value = unit switch
      {
        TorqueUnit.NewtonMeter => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double DerivedUnitValue
      => m_value;

    public double GeneralUnitValue
      => m_value;

    public double ToUnitValue(TorqueUnit unit = TorqueUnit.NewtonMeter)
      => unit switch
      {
        TorqueUnit.NewtonMeter => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(Torque v)
      => v.m_value;
    public static explicit operator Torque(double v)
      => new(v);

    public static bool operator <(Torque a, Torque b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Torque a, Torque b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Torque a, Torque b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Torque a, Torque b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Torque a, Torque b)
      => a.Equals(b);
    public static bool operator !=(Torque a, Torque b)
      => !a.Equals(b);

    public static Torque operator -(Torque v)
      => new(-v.m_value);
    public static Torque operator +(Torque a, double b)
      => new(a.m_value + b);
    public static Torque operator +(Torque a, Torque b)
      => a + b.m_value;
    public static Torque operator /(Torque a, double b)
      => new(a.m_value / b);
    public static Torque operator /(Torque a, Torque b)
      => a / b.m_value;
    public static Torque operator *(Torque a, double b)
      => new(a.m_value * b);
    public static Torque operator *(Torque a, Torque b)
      => a * b.m_value;
    public static Torque operator %(Torque a, double b)
      => new(a.m_value % b);
    public static Torque operator %(Torque a, Torque b)
      => a % b.m_value;
    public static Torque operator -(Torque a, double b)
      => new(a.m_value - b);
    public static Torque operator -(Torque a, Torque b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Torque other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Torque other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Torque o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} N m }}";
    #endregion Object overrides
  }
}
