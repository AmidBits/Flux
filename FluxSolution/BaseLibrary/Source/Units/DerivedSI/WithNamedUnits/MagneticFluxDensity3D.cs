#if NET7_0_OR_GREATER
namespace Flux
{
  namespace Units
  {
    /// <summary>Magnetic flux density unit of tesla.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Magnetic_flux_density"/>
    /// <see cref="https://en.wikipedia.org/wiki/Magnetic_field_density"/>
    public readonly record struct MagneticFluxDensity3D
  : System.IFormattable, IUnitQuantifiable<Geometry.CartesianCoordinate3<double>, MagneticFluxDensityUnit>
    {
      private readonly Geometry.CartesianCoordinate3<double> m_value;

      public MagneticFluxDensity3D(Geometry.CartesianCoordinate3<double> value, MagneticFluxDensityUnit unit = MagneticFluxDensity.DefaultUnit)
        => m_value = unit switch
        {
          MagneticFluxDensityUnit.Tesla => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static MagneticFluxDensity3D operator -(MagneticFluxDensity3D v)
        => new(-v.m_value);
      public static MagneticFluxDensity3D operator +(MagneticFluxDensity3D a, double b)
        => new(a.m_value + b);
      public static MagneticFluxDensity3D operator +(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
        => new(a.m_value + b.m_value);
      public static MagneticFluxDensity3D operator /(MagneticFluxDensity3D a, double b)
        => new(a.m_value / b);
      public static MagneticFluxDensity3D operator /(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
        => new(a.m_value / b.m_value);
      public static MagneticFluxDensity3D operator *(MagneticFluxDensity3D a, double b)
        => new(a.m_value * b);
      public static MagneticFluxDensity3D operator *(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
        => new(a.m_value * b.m_value);
      public static MagneticFluxDensity3D operator %(MagneticFluxDensity3D a, double b)
        => new(a.m_value % b);
      public static MagneticFluxDensity3D operator %(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
        => new(a.m_value % b.m_value);
      public static MagneticFluxDensity3D operator -(MagneticFluxDensity3D a, double b)
        => new(a.m_value - b);
      public static MagneticFluxDensity3D operator -(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
        => new(a.m_value - b.m_value);
      #endregion Overloaded operators

      #region Implemented interfaces

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => ToQuantityString(format, false, false);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(MagneticFluxDensity.DefaultUnit, format, preferUnicode, useFullName);
      public Geometry.CartesianCoordinate3<double> Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(MagneticFluxDensityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{Value.ToString(format, null)} {unit.GetUnitString(preferUnicode, useFullName)}";
      public Geometry.CartesianCoordinate3<double> ToUnitValue(MagneticFluxDensityUnit unit = MagneticFluxDensity.DefaultUnit)
        => unit switch
        {
          MagneticFluxDensityUnit.Tesla => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
#endif
