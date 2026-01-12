namespace Flux.Units
{
  /// <summary>
  /// <para>Magnetic flux strength (H), unit of ampere-per-meter.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Magnetic_field"/></para>
  /// </summary>
  public readonly record struct MagneticFluxStrength
    : System.IComparable, System.IComparable<MagneticFluxStrength>, System.IFormattable, ISiUnitValueQuantifiable<double, MagneticFluxStrengthUnit>
  {
    private readonly double m_value;

    public MagneticFluxStrength(double value, MagneticFluxStrengthUnit unit = MagneticFluxStrengthUnit.AmperePerMeter) => m_value = ConvertFromUnit(unit, value);

    public MagneticFluxStrength(MetricPrefix prefix, double amperePerMeter) => m_value = prefix.ConvertPrefix(amperePerMeter, MetricPrefix.Unprefixed);

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

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + MagneticFluxStrengthUnit.AmperePerMeter.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(MagneticFluxStrengthUnit unit, double value)
      => unit switch
      {
        MagneticFluxStrengthUnit.AmperePerMeter => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(MagneticFluxStrengthUnit unit, double value)
      => unit switch
      {
        MagneticFluxStrengthUnit.AmperePerMeter => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, MagneticFluxStrengthUnit from, MagneticFluxStrengthUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(MagneticFluxStrengthUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(MagneticFluxStrengthUnit unit = MagneticFluxStrengthUnit.AmperePerMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(Numbers.IsConsideredPlural(value)) : unit.GetUnitSymbol(false));
    }

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
