namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.MagneticFluxStrengthUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.MagneticFluxStrengthUnit.AmperePerMeter => "A/m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum MagneticFluxStrengthUnit
    {
      /// <summary>This is the default unit for <see cref="MagneticFluxStrength"/>.</summary>
      AmperePerMeter
    }

    /// <summary>Magnetic flux strength (H), unit of ampere per meter.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Magnetic_field"/>
    public readonly record struct MagneticFluxStrength
      : System.IComparable, System.IComparable<MagneticFluxStrength>, System.IFormattable, IUnitValueQuantifiable<double, MagneticFluxStrengthUnit>
    {
      private readonly double m_value;

      public MagneticFluxStrength(double value, MagneticFluxStrengthUnit unit = MagneticFluxStrengthUnit.AmperePerMeter)
        => m_value = unit switch
        {
          MagneticFluxStrengthUnit.AmperePerMeter => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      //public MetricMultiplicative ToMetricMultiplicative() => new(GetUnitValue(MagneticFluxStrengthUnit.AmperePerMeter), MetricMultiplicativePrefix.One);

      #region Overloaded operators

      public static bool operator <(MagneticFluxStrength a, MagneticFluxStrength b) => a.CompareTo(b) < 0;
      public static bool operator <=(MagneticFluxStrength a, MagneticFluxStrength b) => a.CompareTo(b) <= 0;
      public static bool operator >(MagneticFluxStrength a, MagneticFluxStrength b) => a.CompareTo(b) > 0;
      public static bool operator >=(MagneticFluxStrength a, MagneticFluxStrength b) => a.CompareTo(b) >= 0;

      public static MagneticFluxStrength operator -(MagneticFluxStrength v) => new(-v.m_value);
      public static MagneticFluxStrength operator +(MagneticFluxStrength a, double b) => new(a.m_value + b);
      public static MagneticFluxStrength operator +(MagneticFluxStrength a, MagneticFluxStrength b) => a + b.m_value;
      public static MagneticFluxStrength operator /(MagneticFluxStrength a, double b) => new(a.m_value / b);
      public static MagneticFluxStrength operator /(MagneticFluxStrength a, MagneticFluxStrength b) => a / b.m_value;
      public static MagneticFluxStrength operator *(MagneticFluxStrength a, double b) => new(a.m_value * b);
      public static MagneticFluxStrength operator *(MagneticFluxStrength a, MagneticFluxStrength b) => a * b.m_value;
      public static MagneticFluxStrength operator %(MagneticFluxStrength a, double b) => new(a.m_value % b);
      public static MagneticFluxStrength operator %(MagneticFluxStrength a, MagneticFluxStrength b) => a % b.m_value;
      public static MagneticFluxStrength operator -(MagneticFluxStrength a, double b) => new(a.m_value - b);
      public static MagneticFluxStrength operator -(MagneticFluxStrength a, MagneticFluxStrength b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is MagneticFluxStrength o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(MagneticFluxStrength other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(MagneticFluxStrengthUnit.AmperePerMeter, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="MagneticFluxStrength.Value"/> property is in <see cref="MagneticFluxStrengthUnit.AmperePerMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      //IUnitQuantifiable<>
      public double GetUnitValue(MagneticFluxStrengthUnit unit)
        => unit switch
        {
          MagneticFluxStrengthUnit.AmperePerMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(MagneticFluxStrengthUnit unit = MagneticFluxStrengthUnit.AmperePerMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unicodeSpacing = UnicodeSpacing.None, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unicodeSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
