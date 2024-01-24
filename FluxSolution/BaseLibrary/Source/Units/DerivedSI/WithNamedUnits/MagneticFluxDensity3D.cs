#if NET7_0_OR_GREATER
namespace Flux
{
  namespace Units
  {
    /// <summary>Magnetic flux density unit of tesla.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Magnetic_flux_density"/>
    /// <see href="https://en.wikipedia.org/wiki/Magnetic_field_density"/>
    public readonly record struct MagneticFluxDensity3D
  : System.IFormattable, IUnitValueQuantifiable<System.Numerics.Vector3, MagneticFluxDensityUnit>
    {
      private readonly System.Numerics.Vector3 m_value;

      public MagneticFluxDensity3D(System.Numerics.Vector3 value, MagneticFluxDensityUnit unit = MagneticFluxDensity.DefaultUnit)
        => m_value = unit switch
        {
          MagneticFluxDensityUnit.Tesla => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static MagneticFluxDensity3D operator -(MagneticFluxDensity3D v)
        => new(-v.m_value);
      public static MagneticFluxDensity3D operator +(MagneticFluxDensity3D a, float b)
        => new(a.m_value + new System.Numerics.Vector3(b));
      public static MagneticFluxDensity3D operator +(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
        => new(a.m_value + b.m_value);
      public static MagneticFluxDensity3D operator /(MagneticFluxDensity3D a, float b)
        => new(a.m_value / b);
      public static MagneticFluxDensity3D operator /(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
        => new(a.m_value / b.m_value);
      public static MagneticFluxDensity3D operator *(MagneticFluxDensity3D a, float b)
        => new(a.m_value * b);
      public static MagneticFluxDensity3D operator *(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
        => new(a.m_value * b.m_value);
      public static MagneticFluxDensity3D operator %(MagneticFluxDensity3D a, float b)
        => new(new System.Numerics.Vector3(a.m_value.X % b, a.m_value.Y % b, a.m_value.Z % b));
      public static MagneticFluxDensity3D operator %(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
        => new(new System.Numerics.Vector3(a.m_value.X % b.m_value.X, a.m_value.Y % b.m_value.Y, a.m_value.Z % b.m_value.Z));
      public static MagneticFluxDensity3D operator -(MagneticFluxDensity3D a, float b)
        => new(a.m_value - new System.Numerics.Vector3(b));
      public static MagneticFluxDensity3D operator -(MagneticFluxDensity3D a, MagneticFluxDensity3D b)
        => new(a.m_value - b.m_value);
      #endregion Overloaded operators

      #region Implemented interfaces

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => ToValueString(format, false, false);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(MagneticFluxDensity.DefaultUnit, format, preferUnicode, useFullName, culture);

      public System.Numerics.Vector3 Value => m_value;

      // IUnitQuantifiable<>
      public System.Numerics.Vector3 GetUnitValue(MagneticFluxDensityUnit unit)
        => unit switch
        {
          MagneticFluxDensityUnit.Tesla => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(MagneticFluxDensityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{Value.ToString(format, culture)} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
#endif
