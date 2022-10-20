namespace Flux
{
  /// <summary>Magnetic flux density unit of tesla.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Magnetic_flux_density"/>
  public record struct MagneticFluxDensity3D
  {
    public const MagneticFluxDensityUnit DefaultUnit = MagneticFluxDensityUnit.Tesla;

    private readonly MagneticFluxDensity m_x;
    private readonly MagneticFluxDensity m_y;
    private readonly MagneticFluxDensity m_z;

    public MagneticFluxDensity3D(MagneticFluxDensity x, MagneticFluxDensity y, MagneticFluxDensity z)
    {
      m_x = x;
      m_y = y;
      m_z = z;
    }

    public MagneticFluxDensity X => m_x;
    public MagneticFluxDensity Y => m_y;
    public MagneticFluxDensity Z => m_z;

    #region Overloaded operators
    public static MagneticFluxDensity3D operator -(MagneticFluxDensity3D v)
      => new(-v.m_x, -v.m_y, -v.m_z);
    public static MagneticFluxDensity3D operator +(MagneticFluxDensity3D a, double b)
      => new(a.m_x + b, a.m_y + b, a.m_z + b);
    public static MagneticFluxDensity3D operator +(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
      => new(a.m_x + b.m_x, a.m_y + b.m_y, a.m_z + b.m_z);
    public static MagneticFluxDensity3D operator /(MagneticFluxDensity3D a, double b)
      => new(a.m_x / b, a.m_y / b, a.m_z / b);
    public static MagneticFluxDensity3D operator /(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
      => new(a.m_x / b.m_x, a.m_y / b.m_y, a.m_z / b.m_z);
    public static MagneticFluxDensity3D operator *(MagneticFluxDensity3D a, double b)
      => new(a.m_x * b, a.m_y * b, a.m_z * b);
    public static MagneticFluxDensity3D operator *(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
      => new(a.m_x * b.m_x, a.m_y * b.m_y, a.m_z * b.m_z);
    public static MagneticFluxDensity3D operator %(MagneticFluxDensity3D a, double b)
      => new(a.m_x % b, a.m_y % b, a.m_z % b);
    public static MagneticFluxDensity3D operator %(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
      => new(a.m_x % b.m_x, a.m_y % b.m_y, a.m_z % b.m_z);
    public static MagneticFluxDensity3D operator -(MagneticFluxDensity3D a, double b)
      => new(a.m_x - b, a.m_y - b, a.m_z - b);
    public static MagneticFluxDensity3D operator -(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
      => new(a.m_x - b.m_x, a.m_y - b.m_y, a.m_z - b.m_z);
    #endregion Overloaded operators
  }
}

//namespace Flux
//{
//  /// <summary>Magnetic flux density unit of tesla.</summary>
//  /// <see cref="https://en.wikipedia.org/wiki/Magnetic_flux_density"/>
//  /// <see cref="https://en.wikipedia.org/wiki/Magnetic_field_density"/>
//  public readonly struct MagneticFluxDensity3D
//    : System.IEquatable<MagneticFluxDensity3D>, IUnitQuantifiable<CartesianCoordinate3R, MagneticFluxDensityUnit>
//  {
//    public const MagneticFluxDensityUnit DefaultUnit = MagneticFluxDensityUnit.Tesla;

//    private readonly CartesianCoordinate3R m_value;

//    public MagneticFluxDensity3D(CartesianCoordinate3R value, MagneticFluxDensityUnit unit = DefaultUnit)
//      => m_value = unit switch
//      {
//        MagneticFluxDensityUnit.Tesla => value,
//        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
//      };

//    #region Overloaded operators
//    public static bool operator ==(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
//      => a.Equals(b);
//    public static bool operator !=(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
//      => !a.Equals(b);

//    public static MagneticFluxDensity3D operator -(MagneticFluxDensity3D v)
//      => new(-v.m_value);
//    public static MagneticFluxDensity3D operator +(MagneticFluxDensity3D a, double b)
//      => new(a.m_value + b);
//    public static MagneticFluxDensity3D operator +(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
//      => new(a.m_value + b.m_value);
//    public static MagneticFluxDensity3D operator /(MagneticFluxDensity3D a, double b)
//      => new(a.m_value / b);
//    public static MagneticFluxDensity3D operator /(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
//      => new(a.m_value / b.m_value);
//    public static MagneticFluxDensity3D operator *(MagneticFluxDensity3D a, double b)
//      => new(a.m_value * b);
//    public static MagneticFluxDensity3D operator *(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
//      => new(a.m_value * b.m_value);
//    public static MagneticFluxDensity3D operator %(MagneticFluxDensity3D a, double b)
//      => new(a.m_value % b);
//    public static MagneticFluxDensity3D operator %(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
//      => new(a.m_value % b.m_value);
//    public static MagneticFluxDensity3D operator -(MagneticFluxDensity3D a, double b)
//      => new(a.m_value - b);
//    public static MagneticFluxDensity3D operator -(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
//      => new(a.m_value - b.m_value);
//    #endregion Overloaded operators

//    #region Implemented interfaces
//    // IEquatable
//    [System.Diagnostics.Contracts.Pure]
//    public bool Equals(MagneticFluxDensity3D other)
//      => m_value == other.m_value;

//    // IQuantifiable<>
//    [System.Diagnostics.Contracts.Pure] public CartesianCoordinate3R Value { get => m_value; init => m_value = value; }
//    // IUnitQuantifiable<>
//    [System.Diagnostics.Contracts.Pure]
//    public string ToUnitString(MagneticFluxDensityUnit unit = DefaultUnit, string? format = null, bool preferUnicode = false, bool useFullName = false)
//      => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
//    [System.Diagnostics.Contracts.Pure]
//    public CartesianCoordinate3R ToUnitValue(MagneticFluxDensityUnit unit = DefaultUnit)
//      => unit switch
//      {
//        MagneticFluxDensityUnit.Tesla => m_value,
//        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
//      };
//    #endregion Implemented interfaces

//    #region Object overrides
//    public override bool Equals(object? obj)
//      => obj is MagneticFluxDensity3D o && Equals(o);
//    public override int GetHashCode()
//      => m_value.GetHashCode();
//    public override string ToString()
//      => $"{GetType().Name} {{ Value = {ToUnitString()} }}";
//    #endregion Object overrides
//  }
//}
