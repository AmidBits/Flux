#if NET7_0_OR_GREATER
namespace Flux
{
  namespace Units
  {
    /// <summary>Magnetic flux density unit of tesla.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Magnetic_flux_density"/>
    public readonly record struct MagneticFluxDensity2D
      : System.IFormattable, IUnitValueQuantifiable<System.Numerics.Vector2, MagneticFluxDensityUnit>
    {
      private readonly System.Numerics.Vector2 m_value;

      public MagneticFluxDensity2D(System.Numerics.Vector2 value, MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla)
        => m_value = unit switch
        {
          MagneticFluxDensityUnit.Tesla => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators

      public static MagneticFluxDensity2D operator -(MagneticFluxDensity2D v) => new(-v.m_value);
      public static MagneticFluxDensity2D operator +(MagneticFluxDensity2D a, float b) => new(a.m_value + new System.Numerics.Vector2(b));
      public static MagneticFluxDensity2D operator +(MagneticFluxDensity2D a, MagneticFluxDensity2D b) => new(a.m_value + b.m_value);
      public static MagneticFluxDensity2D operator /(MagneticFluxDensity2D a, float b) => new(a.m_value / b);
      public static MagneticFluxDensity2D operator /(MagneticFluxDensity2D a, MagneticFluxDensity2D b) => new(a.m_value / b.m_value);
      public static MagneticFluxDensity2D operator *(MagneticFluxDensity2D a, float b) => new(a.m_value * b);
      public static MagneticFluxDensity2D operator *(MagneticFluxDensity2D a, MagneticFluxDensity2D b) => new(a.m_value * b.m_value);
      public static MagneticFluxDensity2D operator %(MagneticFluxDensity2D a, float b) => new(new System.Numerics.Vector2(a.m_value.X % b, a.m_value.Y % b));
      public static MagneticFluxDensity2D operator %(MagneticFluxDensity2D a, MagneticFluxDensity2D b) => new(new System.Numerics.Vector2(a.m_value.X % b.m_value.X, a.m_value.Y % b.m_value.Y));
      public static MagneticFluxDensity2D operator -(MagneticFluxDensity2D a, float b) => new(a.m_value - new System.Numerics.Vector2(b));
      public static MagneticFluxDensity2D operator -(MagneticFluxDensity2D a, MagneticFluxDensity2D b) => new(a.m_value - b.m_value);

      #endregion Overloaded operators

      #region Implemented interfaces

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(MagneticFluxDensityUnit.Tesla, format, formatProvider);

      // IQuantifiable<>
      public System.Numerics.Vector2 Value => m_value;

      // IUnitQuantifiable<>
      public System.Numerics.Vector2 GetUnitValue(MagneticFluxDensityUnit unit)
        => unit switch
        {
          MagneticFluxDensityUnit.Tesla => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
#endif
