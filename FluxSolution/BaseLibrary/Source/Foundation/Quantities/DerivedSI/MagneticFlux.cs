namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static MagneticFlux Create(this MagneticFluxUnit unit, double value)
      => new(value, unit);
    public static string GetUnitSymbol(this MagneticFluxUnit unit)
      => unit switch
      {
        MagneticFluxUnit.Weber => @" Wb",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  public enum MagneticFluxUnit
  {
    Weber,
  }

  /// <summary>Magnetic flux unit of weber.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Magnetic_flux"/>
  public struct MagneticFlux
    : System.IComparable<MagneticFlux>, System.IEquatable<MagneticFlux>, IValueGeneralizedUnit<double>, IValueDerivedUnitSI<double>
  {
    public const MagneticFluxUnit DefaultUnit = MagneticFluxUnit.Weber;

    private readonly double m_value;

    public MagneticFlux(double value, MagneticFluxUnit unit = MagneticFluxUnit.Weber)
      => m_value = unit switch
      {
        MagneticFluxUnit.Weber => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double DerivedUnitValue
      => m_value;

    public double GeneralUnitValue
      => m_value;

    public string ToUnitString(MagneticFluxUnit unit = DefaultUnit, string? format = null)
      => $"{(format is null ? ToUnitValue(unit) : string.Format($"{{0:{format}}}", ToUnitValue(unit)))}{unit.GetUnitSymbol()}";
    public double ToUnitValue(MagneticFluxUnit unit = MagneticFluxUnit.Weber)
      => unit switch
      {
        MagneticFluxUnit.Weber => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static explicit operator double(MagneticFlux v)
      => v.m_value;
    public static explicit operator MagneticFlux(double v)
      => new(v);

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
      => new(-v.m_value);
    public static MagneticFlux operator +(MagneticFlux a, double b)
      => new(a.m_value + b);
    public static MagneticFlux operator +(MagneticFlux a, MagneticFlux b)
      => a + b.m_value;
    public static MagneticFlux operator /(MagneticFlux a, double b)
      => new(a.m_value / b);
    public static MagneticFlux operator /(MagneticFlux a, MagneticFlux b)
      => a / b.m_value;
    public static MagneticFlux operator *(MagneticFlux a, double b)
      => new(a.m_value * b);
    public static MagneticFlux operator *(MagneticFlux a, MagneticFlux b)
      => a * b.m_value;
    public static MagneticFlux operator %(MagneticFlux a, double b)
      => new(a.m_value % b);
    public static MagneticFlux operator %(MagneticFlux a, MagneticFlux b)
      => a % b.m_value;
    public static MagneticFlux operator -(MagneticFlux a, double b)
      => new(a.m_value - b);
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
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}