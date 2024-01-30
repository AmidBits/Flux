namespace Flux
{
  public static partial class Em
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public static string GetUnitString(this Units.MagneticFluxStrengthUnit unit, bool preferUnicode, bool useFullName = false)
#pragma warning restore IDE0060 // Remove unused parameter
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
      AmperePerMeter
    }

    /// <summary>Magnetic flux strength (H), unit of ampere per meter.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Magnetic_field"/>
    public readonly record struct MagneticFluxStrength
      : System.IComparable, System.IComparable<MagneticFluxStrength>, System.IFormattable, IUnitValueQuantifiable<double, MagneticFluxStrengthUnit>
    {
      public const MagneticFluxStrengthUnit DefaultUnit = MagneticFluxStrengthUnit.AmperePerMeter;

      private readonly double m_value;

      public MagneticFluxStrength(double value, MagneticFluxStrengthUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          MagneticFluxStrengthUnit.AmperePerMeter => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public MetricMultiplicative ToMetricMultiplicative()
        => new(GetUnitValue(DefaultUnit), MetricMultiplicativePrefix.One);

      #region Overloaded operators
      public static explicit operator double(MagneticFluxStrength v) => v.m_value;
      public static explicit operator MagneticFluxStrength(double v) => new(v);

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
      public string ToString(string? format, IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

      // IQuantifiable<>
      public string ToValueString(string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => ToUnitValueString(DefaultUnit, format, preferUnicode, useFullName, culture);

      public double Value => m_value;

      //IUnitQuantifiable<>
      public double GetUnitValue(MagneticFluxStrengthUnit unit)
        => unit switch
        {
          MagneticFluxStrengthUnit.AmperePerMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(MagneticFluxStrengthUnit unit, string? format = null, bool preferUnicode = false, bool useFullName = false, System.Globalization.CultureInfo? culture = null)
        => $"{string.Format(culture, $"{{0{(format is null ? string.Empty : $":{format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(preferUnicode, useFullName)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
