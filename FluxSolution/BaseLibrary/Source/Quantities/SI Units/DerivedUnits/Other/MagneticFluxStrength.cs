namespace Flux.Quantities
{
  public enum MagneticFluxStrengthUnit
  {
    /// <summary>This is the default unit for <see cref="MagneticFluxStrength"/>.</summary>
    AmperePerMeter
  }

  /// <summary>Magnetic flux strength (H), unit of ampere-per-meter.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Magnetic_field"/>
  public readonly record struct MagneticFluxStrength
    : System.IComparable, System.IComparable<MagneticFluxStrength>, System.IFormattable, ISiUnitValueQuantifiable<double, MagneticFluxStrengthUnit>
  {
    private readonly double m_value;

    public MagneticFluxStrength(double value, MagneticFluxStrengthUnit unit = MagneticFluxStrengthUnit.AmperePerMeter) => m_value = ConvertFromUnit(unit, value);

    public MagneticFluxStrength(MetricPrefix prefix, double amperePerMeter) => m_value = prefix.ConvertTo(amperePerMeter, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(MagneticFluxStrength a, MagneticFluxStrength b) => a.CompareTo(b) < 0;
    public static bool operator >(MagneticFluxStrength a, MagneticFluxStrength b) => a.CompareTo(b) > 0;
    public static bool operator <=(MagneticFluxStrength a, MagneticFluxStrength b) => a.CompareTo(b) <= 0;
    public static bool operator >=(MagneticFluxStrength a, MagneticFluxStrength b) => a.CompareTo(b) >= 0;

    public static MagneticFluxStrength operator -(MagneticFluxStrength v) => new(-v.m_value);
    public static MagneticFluxStrength operator *(MagneticFluxStrength a, MagneticFluxStrength b) => new(a.m_value * b.m_value);
    public static MagneticFluxStrength operator /(MagneticFluxStrength a, MagneticFluxStrength b) => new(a.m_value / b.m_value);
    public static MagneticFluxStrength operator %(MagneticFluxStrength a, MagneticFluxStrength b) => new(a.m_value % b.m_value);
    public static MagneticFluxStrength operator +(MagneticFluxStrength a, MagneticFluxStrength b) => new(a.m_value + b.m_value);
    public static MagneticFluxStrength operator -(MagneticFluxStrength a, MagneticFluxStrength b) => new(a.m_value - b.m_value);
    public static MagneticFluxStrength operator *(MagneticFluxStrength a, double b) => new(a.m_value * b);
    public static MagneticFluxStrength operator /(MagneticFluxStrength a, double b) => new(a.m_value / b);
    public static MagneticFluxStrength operator %(MagneticFluxStrength a, double b) => new(a.m_value % b);
    public static MagneticFluxStrength operator +(MagneticFluxStrength a, double b) => new(a.m_value + b);
    public static MagneticFluxStrength operator -(MagneticFluxStrength a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is MagneticFluxStrength o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(MagneticFluxStrength other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(MagneticFluxStrengthUnit.AmperePerMeter, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(MagneticFluxStrengthUnit.AmperePerMeter, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(MagneticFluxStrengthUnit.AmperePerMeter, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(MagneticFluxStrengthUnit unit, double value)
      => unit switch
      {
        MagneticFluxStrengthUnit.AmperePerMeter => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(MagneticFluxStrengthUnit unit, double value)
      => unit switch
      {
        MagneticFluxStrengthUnit.AmperePerMeter => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, MagneticFluxStrengthUnit from, MagneticFluxStrengthUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(MagneticFluxStrengthUnit unit)
      => unit switch
      {
        MagneticFluxStrengthUnit.AmperePerMeter => 1,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(MagneticFluxStrengthUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(MagneticFluxStrengthUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.MagneticFluxStrengthUnit.AmperePerMeter => "A/m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(MagneticFluxStrengthUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(MagneticFluxStrengthUnit unit = MagneticFluxStrengthUnit.AmperePerMeter, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="MagneticFluxStrength.Value"/> property is in <see cref="MagneticFluxStrengthUnit.AmperePerMeter"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
