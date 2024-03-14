namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.MagneticFluxDensityUnit unit, bool useFullName = false)
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
      /// <summary>This is the default unit for <see cref="MagneticFluxDensity"/>.</summary>
      Tesla,
      KilogramPerSquareSecond
    }

    /// <summary>Magnetic flux density (B), unit of tesla.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Magnetic_flux_density"/>
    public readonly record struct MagneticFluxDensity
      : System.IComparable, System.IComparable<MagneticFluxDensity>, System.IFormattable, IUnitValueQuantifiable<double, MagneticFluxDensityUnit>
    {
      private readonly double m_value;

      public MagneticFluxDensity(double value, MagneticFluxDensityUnit unit = MagneticFluxDensityUnit.Tesla)
        => m_value = unit switch
        {
          MagneticFluxDensityUnit.Tesla => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      //public MetricMultiplicative ToMetricMultiplicative() => new(GetUnitValue(MagneticFluxDensityUnit.Tesla), MetricMultiplicativePrefix.One);

      #region Overloaded operators

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
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(MagneticFluxDensityUnit.Tesla, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="MagneticFluxDensity.Value"/> property is in <see cref="MagneticFluxDensityUnit.Tesla"/>.</para>
      /// </summary>
      public double Value => m_value;

      //IUnitQuantifiable<>
      public double GetUnitValue(MagneticFluxDensityUnit unit)
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
