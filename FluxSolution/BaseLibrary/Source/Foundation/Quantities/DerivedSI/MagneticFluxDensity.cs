namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static MagneticFluxDensity Create(this MagneticFluxDensityUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this MagneticFluxDensityUnit unit)
      => unit switch
      {
        MagneticFluxDensityUnit.Tesla => @" T",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum MagneticFluxDensityUnit
  {
    Tesla,
  }

  /// <summary>Magnetic flux density unit of tesla.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Magnetic_flux_density"/>
  public struct MagneticFluxDensity
    : System.IComparable<MagneticFluxDensity>, System.IEquatable<MagneticFluxDensity>, IValueGeneralizedUnit<double>, IValueDerivedUnitSI<double>
  {
    public const MagneticFluxDensityUnit DefaultUnit = MagneticFluxDensityUnit.Tesla;

    private readonly double m_value;

    public MagneticFluxDensity(double value, MagneticFluxDensityUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        MagneticFluxDensityUnit.Tesla => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double DerivedUnitValue
      => m_value;

    public double GeneralUnitValue
      => m_value;

    public string ToUnitString(MagneticFluxDensityUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(MagneticFluxDensityUnit unit = DefaultUnit)
      => unit switch
      {
        MagneticFluxDensityUnit.Tesla => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(MagneticFluxDensity v)
      => v.m_value;
    public static explicit operator MagneticFluxDensity(double v)
      => new(v);

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
      => new(-v.m_value);
    public static MagneticFluxDensity operator +(MagneticFluxDensity a, double b)
      => new(a.m_value + b);
    public static MagneticFluxDensity operator +(MagneticFluxDensity a, MagneticFluxDensity b)
      => a + b.m_value;
    public static MagneticFluxDensity operator /(MagneticFluxDensity a, double b)
      => new(a.m_value / b);
    public static MagneticFluxDensity operator /(MagneticFluxDensity a, MagneticFluxDensity b)
      => a / b.m_value;
    public static MagneticFluxDensity operator *(MagneticFluxDensity a, double b)
      => new(a.m_value * b);
    public static MagneticFluxDensity operator *(MagneticFluxDensity a, MagneticFluxDensity b)
      => a * b.m_value;
    public static MagneticFluxDensity operator %(MagneticFluxDensity a, double b)
      => new(a.m_value % b);
    public static MagneticFluxDensity operator %(MagneticFluxDensity a, MagneticFluxDensity b)
      => a % b.m_value;
    public static MagneticFluxDensity operator -(MagneticFluxDensity a, double b)
      => new(a.m_value - b);
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
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}