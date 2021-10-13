namespace Flux.Quantity
{
  /// <summary>Magnetic flux unit of weber.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Magnetic_flux"/>
  public struct MagneticFlux
    : System.IComparable<MagneticFlux>, System.IEquatable<MagneticFlux>, IValuedUnit
  {
    private readonly double m_value;

    public MagneticFlux(double value, MagneticFluxUnit unit = MagneticFluxUnit.Weber)
    {
      switch (unit)
      {
        case MagneticFluxUnit.Weber:
          m_value = value;
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    public double Value
      => m_value;

    public double ToUnitValue(MagneticFluxUnit unit = MagneticFluxUnit.Weber)
    {
      switch (unit)
      {
        case MagneticFluxUnit.Weber:
          return m_value;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Overloaded operators
    public static explicit operator double(MagneticFlux v)
      => v.m_value;
    public static explicit operator MagneticFlux(double v)
      => new MagneticFlux(v);

    public static bool operator <(MagneticFlux a, MagneticFlux b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(MagneticFlux a, MagneticFlux b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(MagneticFlux a, MagneticFlux b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(MagneticFlux a, MagneticFlux b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(MagneticFlux a, MagneticFlux b)
      => a.Equals(b);
    public static bool operator !=(MagneticFlux a, MagneticFlux b)
      => !a.Equals(b);

    public static MagneticFlux operator -(MagneticFlux v)
      => new MagneticFlux(-v.m_value);
    public static MagneticFlux operator +(MagneticFlux a, double b)
      => new MagneticFlux(a.m_value + b);
    public static MagneticFlux operator +(MagneticFlux a, MagneticFlux b)
      => a + b.m_value;
    public static MagneticFlux operator /(MagneticFlux a, double b)
      => new MagneticFlux(a.m_value / b);
    public static MagneticFlux operator /(MagneticFlux a, MagneticFlux b)
      => a / b.m_value;
    public static MagneticFlux operator *(MagneticFlux a, double b)
      => new MagneticFlux(a.m_value * b);
    public static MagneticFlux operator *(MagneticFlux a, MagneticFlux b)
      => a * b.m_value;
    public static MagneticFlux operator %(MagneticFlux a, double b)
      => new MagneticFlux(a.m_value % b);
    public static MagneticFlux operator %(MagneticFlux a, MagneticFlux b)
      => a % b.m_value;
    public static MagneticFlux operator -(MagneticFlux a, double b)
      => new MagneticFlux(a.m_value - b);
    public static MagneticFlux operator -(MagneticFlux a, MagneticFlux b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(MagneticFlux other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(MagneticFlux other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is MagneticFlux o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} lm>";
    #endregion Object overrides
  }
}
