namespace Flux.Quantity
{
  public enum PressureUnit
  {
    Pascal,
    PSI,
  }

  /// <summary>Pressure, unit of Pascal. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Pressure"/>
  public struct Pressure
    : System.IComparable<Pressure>, System.IEquatable<Pressure>, IValuedSiDerivedUnit
  {
    public static Pressure StandardAtmosphere
      => new Pressure(101325);
    public static Pressure StandardStatePressure
      => new Pressure(100000);

    private readonly double m_value;

    public Pressure(double pascal)
      => m_value = pascal;

    public double Value
      => m_value;

    public double ToUnitValue(PressureUnit unit)
    {
      switch (unit)
      {
        case PressureUnit.Pascal:
          return m_value;
        case PressureUnit.PSI:
          return m_value * (1290320000.0 / 8896443230521.0);
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
      => v.m_value;
    public static explicit operator Pressure(double v)
      => new Pressure(v);

    public static bool operator <(Pressure a, Pressure b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Pressure a, Pressure b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Pressure a, Pressure b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Pressure a, Pressure b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Pressure a, Pressure b)
      => a.Equals(b);
    public static bool operator !=(Pressure a, Pressure b)
      => !a.Equals(b);

    public static Pressure operator -(Pressure v)
      => new Pressure(-v.m_value);
    public static Pressure operator +(Pressure a, double b)
      => new Pressure(a.m_value + b);
    public static Pressure operator +(Pressure a, Pressure b)
      => a + b.m_value;
    public static Pressure operator /(Pressure a, double b)
      => new Pressure(a.m_value / b);
    public static Pressure operator /(Pressure a, Pressure b)
      => a / b.m_value;
    public static Pressure operator *(Pressure a, double b)
      => new Pressure(a.m_value * b);
    public static Pressure operator *(Pressure a, Pressure b)
      => a * b.m_value;
    public static Pressure operator %(Pressure a, double b)
      => new Pressure(a.m_value % b);
    public static Pressure operator %(Pressure a, Pressure b)
      => a % b.m_value;
    public static Pressure operator -(Pressure a, double b)
      => new Pressure(a.m_value - b);
    public static Pressure operator -(Pressure a, Pressure b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Pressure other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(Pressure other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Pressure o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} Pa>";
    #endregion Object overrides
  }
}
