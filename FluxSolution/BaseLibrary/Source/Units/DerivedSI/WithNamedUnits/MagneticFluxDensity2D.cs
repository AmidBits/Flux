#if NET7_0_OR_GREATER
namespace Flux
{
  namespace Units
  {
    /// <summary>Magnetic flux density unit of tesla.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Magnetic_flux_density"/>
    public readonly record struct MagneticFluxDensity2D
  : IUnitValueQuantifiable<System.Numerics.Vector2, MagneticFluxDensityUnit>
    {
      private readonly System.Numerics.Vector2 m_value;

      public MagneticFluxDensity2D(System.Numerics.Vector2 value, MagneticFluxDensityUnit unit = MagneticFluxDensity.DefaultUnit)
        => m_value = unit switch
        {
          MagneticFluxDensityUnit.Tesla => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static MagneticFluxDensity2D operator -(MagneticFluxDensity2D v)
        => new(-v.m_value);
      public static MagneticFluxDensity2D operator +(MagneticFluxDensity2D a, float b)
        => new(a.m_value + new System.Numerics.Vector2(b));
      public static MagneticFluxDensity2D operator +(MagneticFluxDensity2D a, MagneticFluxDensity2D b)
        => new(a.m_value + b.m_value);
      public static MagneticFluxDensity2D operator /(MagneticFluxDensity2D a, float b)
        => new(a.m_value / b);
      public static MagneticFluxDensity2D operator /(MagneticFluxDensity2D a, MagneticFluxDensity2D b)
        => new(a.m_value / b.m_value);
      public static MagneticFluxDensity2D operator *(MagneticFluxDensity2D a, float b)
        => new(a.m_value * b);
      public static MagneticFluxDensity2D operator *(MagneticFluxDensity2D a, MagneticFluxDensity2D b)
        => new(a.m_value * b.m_value);
      public static MagneticFluxDensity2D operator %(MagneticFluxDensity2D a, float b)
        => new(new System.Numerics.Vector2(a.m_value.X % b, a.m_value.Y % b));
      public static MagneticFluxDensity2D operator %(MagneticFluxDensity2D a, MagneticFluxDensity2D b)
        => new(new System.Numerics.Vector2(a.m_value.X % b.m_value.X, a.m_value.Y % b.m_value.Y));
      public static MagneticFluxDensity2D operator -(MagneticFluxDensity2D a, float b)
        => new(a.m_value - new System.Numerics.Vector2(b));
      public static MagneticFluxDensity2D operator -(MagneticFluxDensity2D a, MagneticFluxDensity2D b)
        => new(a.m_value - b.m_value);
      #endregion Overloaded operators

      #region Implemented interfaces

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(MagneticFluxDensity.DefaultUnit, format, preferUnicode, useFullName, culture);

      public System.Numerics.Vector2 Value => m_value;

      // IUnitQuantifiable<>
      public System.Numerics.Vector2 GetUnitValue(MagneticFluxDensityUnit unit)
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
