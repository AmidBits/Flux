namespace Flux.Quantity
{
  public enum MagneticFluxDensityUnit
  {
    Tesla,
  }

  /// <summary>Luminous flux unit of lumen.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Amount_of_substance"/>
  public struct MagneticFluxDensity
    : System.IComparable<MagneticFluxDensity>, System.IEquatable<MagneticFluxDensity>, IValuedUnit
  {
    private readonly double m_value;

    public MagneticFluxDensity(double value, MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla)
    {
      switch (unit)
      {
        case MagneticFluxDensityUnit.Tesla:
          m_value = value;
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    public double Value
      => m_value;

    public double ToUnitValue(MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla)
    {
      switch (unit)
      {
        case MagneticFluxDensityUnit.Tesla:
          return m_value;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(unit));
      }
    }

    #region Overloaded operators
    public static explicit operator double(MagneticFluxDensity v)
      => v.m_value;
    public static explicit operator MagneticFluxDensity(double v)
      => new MagneticFluxDensity(v);

    public static bool operator <(MagneticFluxDensity a, MagneticFluxDensity b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(MagneticFluxDensity a, MagneticFluxDensity b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(MagneticFluxDensity a, MagneticFluxDensity b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(MagneticFluxDensity a, MagneticFluxDensity b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(MagneticFluxDensity a, MagneticFluxDensity b)
      => a.Equals(b);
    public static bool operator !=(MagneticFluxDensity a, MagneticFluxDensity b)
      => !a.Equals(b);

    public static MagneticFluxDensity operator -(MagneticFluxDensity v)
      => new MagneticFluxDensity(-v.m_value);
    public static MagneticFluxDensity operator +(MagneticFluxDensity a, double b)
      => new MagneticFluxDensity(a.m_value + b);
    public static MagneticFluxDensity operator +(MagneticFluxDensity a, MagneticFluxDensity b)
      => a + b.m_value;
    public static MagneticFluxDensity operator /(MagneticFluxDensity a, double b)
      => new MagneticFluxDensity(a.m_value / b);
    public static MagneticFluxDensity operator /(MagneticFluxDensity a, MagneticFluxDensity b)
      => a / b.m_value;
    public static MagneticFluxDensity operator *(MagneticFluxDensity a, double b)
      => new MagneticFluxDensity(a.m_value * b);
    public static MagneticFluxDensity operator *(MagneticFluxDensity a, MagneticFluxDensity b)
      => a * b.m_value;
    public static MagneticFluxDensity operator %(MagneticFluxDensity a, double b)
      => new MagneticFluxDensity(a.m_value % b);
    public static MagneticFluxDensity operator %(MagneticFluxDensity a, MagneticFluxDensity b)
      => a % b.m_value;
    public static MagneticFluxDensity operator -(MagneticFluxDensity a, double b)
      => new MagneticFluxDensity(a.m_value - b);
    public static MagneticFluxDensity operator -(MagneticFluxDensity a, MagneticFluxDensity b)
      => a - b.m_value;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    public int CompareTo(MagneticFluxDensity other)
      => m_value.CompareTo(other.m_value);

    // IEquatable
    public bool Equals(MagneticFluxDensity other)
      => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is MagneticFluxDensity o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"<{GetType().Name}: {m_value} lm>";
    #endregion Object overrides
  }
}
