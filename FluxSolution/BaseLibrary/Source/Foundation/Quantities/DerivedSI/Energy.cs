namespace Flux
{
  public enum EnergyUnit
  {
    Joule,
    ElectronVolt,
  }

  /// <summary>Energy unit of Joule.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Energy"/>
  public struct Energy
    : System.IComparable<Energy>, System.IEquatable<Energy>, IValueGeneralizedUnit<double>, IValueDerivedUnitSI<double>
  {
    private readonly double m_value;

    public Energy(double value, EnergyUnit unit = EnergyUnit.Joule)
      => m_value = unit switch
      {
        EnergyUnit.Joule => value,
        EnergyUnit.ElectronVolt => value / 1.602176634e-19,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double DerivedUnitValue
      => m_value;

    public double GeneralUnitValue
      => m_value;

    public double ToUnitValue(EnergyUnit unit = EnergyUnit.Joule)
      => unit switch
      {
        EnergyUnit.Joule => m_value,
        EnergyUnit.ElectronVolt => m_value * 1.602176634e-19,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Energy v)
      => v.m_value;
    public static explicit operator Energy(double v)
      => new(v);

    public static bool operator <(Energy a, Energy b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Energy a, Energy b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Energy a, Energy b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Energy a, Energy b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Energy a, Energy b)
      => a.Equals(b);
    public static bool operator !=(Energy a, Energy b)
      => !a.Equals(b);

    public static Energy operator -(Energy v)
      => new(-v.m_value);
    public static Energy operator +(Energy a, double b)
      => new(a.m_value + b);
    public static Energy operator +(Energy a, Energy b)
      => a + b.m_value;
    public static Energy operator /(Energy a, double b)
      => new(a.m_value / b);
    public static Energy operator /(Energy a, Energy b)
      => a / b.m_value;
    public static Energy operator *(Energy a, double b)
      => new(a.m_value * b);
    public static Energy operator *(Energy a, Energy b)
      => a * b.m_value;
    public static Energy operator %(Energy a, double b)
      => new(a.m_value % b);
    public static Energy operator %(Energy a, Energy b)
      => a % b.m_value;
    public static Energy operator -(Energy a, double b)
      => new(a.m_value - b);
    public static Energy operator -(Energy a, Energy b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Energy other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Energy other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Energy o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} J }}";
    #endregion Object overrides
  }
}
