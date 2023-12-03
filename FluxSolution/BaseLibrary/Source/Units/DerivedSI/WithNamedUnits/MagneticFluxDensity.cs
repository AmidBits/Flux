namespace Flux
{
  public static partial class Em
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public static string GetUnitString(this Units.MagneticFluxDensityUnit unit, bool preferUnicode, bool useFullName = false)
#pragma warning restore IDE0060 // Remove unused parameter
      => useFullName ? unit.ToString() : unit switch
      {
        Units.MagneticFluxDensityUnit.Tesla => "T",
        Units.MagneticFluxDensityUnit.KilogramPerSquareSecond => "kg/s²",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum MagneticFluxDensityUnit
    {
      /// <summary>Tesla.</summary>
      Tesla,
      KilogramPerSquareSecond
    }

    /// <summary>Magnetic flux density unit of tesla.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Magnetic_flux_density"/>
    public readonly record struct MagneticFluxDensity
      : System.IComparable, System.IComparable<MagneticFluxDensity>, System.IFormattable, IUnitQuantifiable<double, MagneticFluxDensityUnit>
    {
      public const MagneticFluxDensityUnit DefaultUnit = MagneticFluxDensityUnit.Tesla;

      private readonly double m_value;

      public MagneticFluxDensity(double value, MagneticFluxDensityUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          MagneticFluxDensityUnit.Tesla => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };


      public MetricMultiplicative ToMetricMultiplicative()
        => new(ToUnitValue(DefaultUnit), MetricMultiplicativePrefix.One);

      #region Overloaded operators
      public static explicit operator double(MagneticFluxDensity v) => v.m_value;
      public static explicit operator MagneticFluxDensity(double v) => new(v);

      public static bool operator <(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) < 0;
      public static bool operator <=(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) <= 0;
      public static bool operator >(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) > 0;
      public static bool operator >=(MagneticFluxDensity a, MagneticFluxDensity b) => a.CompareTo(b) >= 0;

      public static MagneticFluxDensity operator -(MagneticFluxDensity v) => new(-v.m_value);
      public static MagneticFluxDensity operator +(MagneticFluxDensity a, double b) => new(a.m_value + b);
      public static MagneticFluxDensity operator +(MagneticFluxDensity a, MagneticFluxDensity b) => a + b.m_value;
      public static MagneticFluxDensity operator /(MagneticFluxDensity a, double b) => new(a.m_value / b);
      public static MagneticFluxDensity operator /(MagneticFluxDensity a, MagneticFluxDensity b) => a / b.m_value;
      public static MagneticFluxDensity operator *(MagneticFluxDensity a, double b) => new(a.m_value * b);
      public static MagneticFluxDensity operator *(MagneticFluxDensity a, MagneticFluxDensity b) => a * b.m_value;
      public static MagneticFluxDensity operator %(MagneticFluxDensity a, double b) => new(a.m_value % b);
      public static MagneticFluxDensity operator %(MagneticFluxDensity a, MagneticFluxDensity b) => a % b.m_value;
      public static MagneticFluxDensity operator -(MagneticFluxDensity a, double b) => new(a.m_value - b);
      public static MagneticFluxDensity operator -(MagneticFluxDensity a, MagneticFluxDensity b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is MagneticFluxDensity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(MagneticFluxDensity other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => ToUnitString(DefaultUnit, format, preferUnicode, useFullName);
      public double Value => m_value;

      //IUnitQuantifiable<>
      public string ToUnitString(MagneticFluxDensityUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false)
        => $"{string.Format($"{{0{(format is null ? string.Empty : $":{format}")}}}", ToUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";
      public double ToUnitValue(MagneticFluxDensityUnit unit = DefaultUnit)
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
