namespace Flux
{
  /// <summary>Magnetic flux density unit of tesla.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Magnetic_flux_density"/>
  public struct MagneticFluxDensity2D
    : System.IEquatable<MagneticFluxDensity2D>, ISiDerivedUnitQuantifiable<CartesianCoordinate2, MagneticFluxDensityUnit>
  {
    public const MagneticFluxDensityUnit DefaultUnit = MagneticFluxDensityUnit.Tesla;

    private readonly CartesianCoordinate2 m_value;

    public MagneticFluxDensity2D(CartesianCoordinate2 value, MagneticFluxDensityUnit unit = DefaultUnit)
      => m_value = unit switch
      {
        MagneticFluxDensityUnit.Tesla => value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public CartesianCoordinate2 Value
      => m_value;

    public string ToUnitString(MagneticFluxDensityUnit unit = DefaultUnit, string? format = null, bool useFullName = false, bool preferUnicode = false)
      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString()}";
    public CartesianCoordinate2 ToUnitValue(MagneticFluxDensityUnit unit = DefaultUnit)
      => unit switch
      {
        MagneticFluxDensityUnit.Tesla => m_value,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    #region Overloaded operators
    public static bool operator ==(MagneticFluxDensity2D a, MagneticFluxDensity2D b)
      => a.Equals(b);
    public static bool operator !=(MagneticFluxDensity2D a, MagneticFluxDensity2D b)
      => !a.Equals(b);

    public static MagneticFluxDensity2D operator -(MagneticFluxDensity2D v)
      => new(-v.m_value);
    public static MagneticFluxDensity2D operator +(MagneticFluxDensity2D a, double b)
      => new(a.m_value + b);
    public static MagneticFluxDensity2D operator +(MagneticFluxDensity2D a, MagneticFluxDensity2D b)
      => new(a.m_value + b.m_value);
    public static MagneticFluxDensity2D operator /(MagneticFluxDensity2D a, double b)
      => new(a.m_value / b);
    public static MagneticFluxDensity2D operator /(MagneticFluxDensity2D a, MagneticFluxDensity2D b)
      => new(a.m_value / b.m_value);
    public static MagneticFluxDensity2D operator *(MagneticFluxDensity2D a, double b)
      => new(a.m_value * b);
    public static MagneticFluxDensity2D operator *(MagneticFluxDensity2D a, MagneticFluxDensity2D b)
      => new(a.m_value * b.m_value);
    public static MagneticFluxDensity2D operator %(MagneticFluxDensity2D a, double b)
      => new(a.m_value % b);
    public static MagneticFluxDensity2D operator %(MagneticFluxDensity2D a, MagneticFluxDensity2D b)
      => new(a.m_value % b.m_value);
    public static MagneticFluxDensity2D operator -(MagneticFluxDensity2D a, double b)
      => new(a.m_value - b);
    public static MagneticFluxDensity2D operator -(MagneticFluxDensity2D a, MagneticFluxDensity2D b)
      => new(a.m_value - b.m_value);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(MagneticFluxDensity2D other) => m_value == other.m_value;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is MagneticFluxDensity2D o && Equals(o);
    public override int GetHashCode()
      => m_value.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
    #endregion Object overrides
  }
}
