namespace Flux.Quantity
{
  public enum PressureUnit
  {
    Pascal,
    Psi,
  }

  /// <summary>Pressure, unit of Pascal. This is an SI derived quantity.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Pressure"/>
#if NET5_0
  public struct Pressure
    : System.IComparable<Pressure>, System.IEquatable<Pressure>, IValuedUnit<double>
#else
  public record struct Pressure
    : System.IComparable<Pressure>, IValuedUnit<double>
#endif
  {
    public static Pressure StandardAtmosphere
      => new(101325);
    public static Pressure StandardStatePressure
      => new(100000);

    private readonly double m_value;

    public Pressure(double value, PressureUnit unit = PressureUnit.Pascal)
      => m_value = unit switch
      {
        PressureUnit.Pascal => value,
        PressureUnit.Psi => value * (8896443230521.0 / 1290320000.0),
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double Value
      => m_value;

    public double ToUnitValue(PressureUnit unit = PressureUnit.Pascal)
      => unit switch
      {
        PressureUnit.Pascal => m_value,
        PressureUnit.Psi => m_value * (1290320000.0 / 8896443230521.0),
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Static methods
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Pressure v)
      => v.m_value;
    public static explicit operator Pressure(double v)
      => new(v);

    public static bool operator <(Pressure a, Pressure b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Pressure a, Pressure b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Pressure a, Pressure b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Pressure a, Pressure b)
      => a.CompareTo(b) >= 0;

#if NET5_0
    public static bool operator ==(Pressure a, Pressure b)
      => a.Equals(b);
    public static bool operator !=(Pressure a, Pressure b)
      => !a.Equals(b);
#endif

    public static Pressure operator -(Pressure v)
      => new(-v.m_value);
    public static Pressure operator +(Pressure a, double b)
      => new(a.m_value + b);
    public static Pressure operator +(Pressure a, Pressure b)
      => a + b.m_value;
    public static Pressure operator /(Pressure a, double b)
      => new(a.m_value / b);
    public static Pressure operator /(Pressure a, Pressure b)
      => a / b.m_value;
    public static Pressure operator *(Pressure a, double b)
      => new(a.m_value * b);
    public static Pressure operator *(Pressure a, Pressure b)
      => a * b.m_value;
    public static Pressure operator %(Pressure a, double b)
      => new(a.m_value % b);
    public static Pressure operator %(Pressure a, Pressure b)
      => a % b.m_value;
    public static Pressure operator -(Pressure a, double b)
      => new(a.m_value - b);
    public static Pressure operator -(Pressure a, Pressure b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(Pressure other)
      => m_value.CompareTo(other.m_value);

#if NET5_0
    // IEquatable
    public bool Equals(Pressure other)
      => m_value == other.m_value;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is Pressure o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
#endif
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_value} Pa }}";
    #endregion Object overrides
  }
}
