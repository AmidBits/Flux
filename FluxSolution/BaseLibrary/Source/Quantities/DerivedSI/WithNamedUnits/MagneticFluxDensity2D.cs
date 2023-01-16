namespace Flux
{
  namespace Quantities
  {
    /// <summary>Magnetic flux density unit of tesla.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Magnetic_flux_density"/>
    public readonly record struct MagneticFluxDensity2D
    : IUnitQuantifiable<Numerics.CartesianCoordinate2<double>, MagneticFluxDensityUnit>
    {
      private readonly Numerics.CartesianCoordinate2<double> m_value;

      public MagneticFluxDensity2D(Numerics.CartesianCoordinate2<double> value, MagneticFluxDensityUnit unit = MagneticFluxDensity.DefaultUnit)
        => m_value = unit switch
        {
          MagneticFluxDensityUnit.Tesla => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
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
      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
        => ToUnitString(MagneticFluxDensity.DefaultUnit, format, preferUnicode, useFullName);

      public Numerics.CartesianCoordinate2<double> Value { get => m_value; init => m_value = value; }

      // IUnitQuantifiable<>
      public string ToUnitString(MagneticFluxDensityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{Value.ToString()} {unit.GetUnitString(preferUnicode, useFullName)}";

      public Numerics.CartesianCoordinate2<double> ToUnitValue(MagneticFluxDensityUnit unit = MagneticFluxDensity.DefaultUnit)
        => unit switch
        {
          MagneticFluxDensityUnit.Tesla => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };
      #endregion Implemented interfaces

      #region Object overrides
      public override string ToString()
        => $"{GetType().Name} {{ {ToQuantityString()} }}";
      #endregion Object overrides
    }
  }
}
