namespace Flux.Units
{
  /// <summary>
  /// <para>Radiation exposure, unit of coulomb per kilogram. This is an SI derived quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Radiation_exposure"/></para>
  /// </summary>
  public readonly record struct RadiationExposure
    : System.IComparable, System.IComparable<RadiationExposure>, System.IFormattable, ISiUnitValueQuantifiable<double, RadiationExposureUnit>
  {
    private readonly double m_value;

    public RadiationExposure(double value, RadiationExposureUnit unit = RadiationExposureUnit.CoulombPerKilogram) => m_value = ConvertFromUnit(unit, value);

    public RadiationExposure(MetricPrefix prefix, double coulombPerKilogram) => m_value = prefix.ConvertTo(coulombPerKilogram, MetricPrefix.Unprefixed);

    #region Overloaded operators

    public static bool operator <(RadiationExposure a, RadiationExposure b) => a.CompareTo(b) < 0;
    public static bool operator >(RadiationExposure a, RadiationExposure b) => a.CompareTo(b) > 0;
    public static bool operator <=(RadiationExposure a, RadiationExposure b) => a.CompareTo(b) <= 0;
    public static bool operator >=(RadiationExposure a, RadiationExposure b) => a.CompareTo(b) >= 0;

    public static RadiationExposure operator -(RadiationExposure v) => new(-v.m_value);
    public static RadiationExposure operator *(RadiationExposure a, RadiationExposure b) => new(a.m_value * b.m_value);
    public static RadiationExposure operator /(RadiationExposure a, RadiationExposure b) => new(a.m_value / b.m_value);
    public static RadiationExposure operator %(RadiationExposure a, RadiationExposure b) => new(a.m_value % b.m_value);
    public static RadiationExposure operator +(RadiationExposure a, RadiationExposure b) => new(a.m_value + b.m_value);
    public static RadiationExposure operator -(RadiationExposure a, RadiationExposure b) => new(a.m_value - b.m_value);
    public static RadiationExposure operator *(RadiationExposure a, double b) => new(a.m_value * b);
    public static RadiationExposure operator /(RadiationExposure a, double b) => new(a.m_value / b);
    public static RadiationExposure operator %(RadiationExposure a, double b) => new(a.m_value % b);
    public static RadiationExposure operator +(RadiationExposure a, double b) => new(a.m_value + b);
    public static RadiationExposure operator -(RadiationExposure a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is RadiationExposure o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(RadiationExposure other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(RadiationExposureUnit.CoulombPerKilogram, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(RadiationExposureUnit.CoulombPerKilogram, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(RadiationExposureUnit.CoulombPerKilogram, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(RadiationExposureUnit unit, double value)
      => unit switch
      {
        RadiationExposureUnit.CoulombPerKilogram => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(RadiationExposureUnit unit, double value)
      => unit switch
      {
        RadiationExposureUnit.CoulombPerKilogram => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, RadiationExposureUnit from, RadiationExposureUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(RadiationExposureUnit unit)
      => unit switch
      {
        RadiationExposureUnit.CoulombPerKilogram => 1,
        RadiationExposureUnit.R�ntgen => 1.0 / 3876.0,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(RadiationExposureUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(RadiationExposureUnit unit, bool preferUnicode)
      => unit switch
      {
        Units.RadiationExposureUnit.CoulombPerKilogram => "C/kg",
        Units.RadiationExposureUnit.R�ntgen => "R",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(RadiationExposureUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(RadiationExposureUnit unit = RadiationExposureUnit.CoulombPerKilogram, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="RadiationExposure.Value"/> property is in <see cref="RadiationExposureUnit.CoulombPerKilogram"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
