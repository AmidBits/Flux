namespace Flux.Units
{
  public enum PressureUnit
  {
    Pascal,
    PSI,
  }

  /// <summary>Pressure.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Pressure"/>
  public struct Pressure
    : System.IComparable<Pressure>, System.IEquatable<Pressure>, IStandardizedScalar
  {
    private readonly double m_pascal;

    public Pressure(double pascal)
      => m_pascal = pascal;

    public double Pascal
      => m_pascal;

    public double ToUnitValue(PressureUnit unit)
    {
      switch (unit)
      {
        case PressureUnit.Pascal:
          return m_pascal;
        case PressureUnit.PSI:
          return m_pascal * (1290320000.0 / 8896443230521.0);
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Static methods
    public static Pressure FromUnitValue(PressureUnit unit, double value)
    {
      switch (unit)
      {
        case PressureUnit.Pascal:
          return new Pressure(value);
        case PressureUnit.PSI:
          return new Pressure(value * (8896443230521.0 / 1290320000.0));
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Pressure v)
      => v.m_pascal;
    public static explicit operator Pressure(double v)
      => new Pressure(v);

    public static bool operator <(Pressure a, Pressure b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Pressure a, Pressure b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Pressure a, Pressure b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(Pressure a, Pressure b)
      => a.CompareTo(b) <= 0;

    public static bool operator ==(Pressure a, Pressure b)
      => a.Equals(b);
    public static bool operator !=(Pressure a, Pressure b)
      => !a.Equals(b);

    public static Pressure operator -(Pressure v)
      => new Pressure(-v.m_pascal);
    public static Pressure operator +(Pressure a, Pressure b)
      => new Pressure(a.m_pascal + b.m_pascal);
    public static Pressure operator /(Pressure a, Pressure b)
      => new Pressure(a.m_pascal / b.m_pascal);
    public static Pressure operator *(Pressure a, Pressure b)
      => new Pressure(a.m_pascal * b.m_pascal);
    public static Pressure operator %(Pressure a, Pressure b)
      => new Pressure(a.m_pascal % b.m_pascal);
    public static Pressure operator -(Pressure a, Pressure b)
      => new Pressure(a.m_pascal - b.m_pascal);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Pressure other)
      => m_pascal.CompareTo(other.m_pascal);

    // IEquatable
    public bool Equals(Pressure other)
      => m_pascal == other.m_pascal;

    // IUnitStandardized
    public double GetScalar()
      => m_pascal;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Pressure o && Equals(o);
    public override int GetHashCode()
      => m_pascal.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_pascal} Pa>";
    #endregion Object overrides
  }
}
