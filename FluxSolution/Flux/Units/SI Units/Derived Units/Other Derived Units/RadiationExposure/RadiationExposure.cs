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

    public RadiationExposure(MetricPrefix prefix, double coulombPerKilogram) => m_value = prefix.ConvertPrefix(coulombPerKilogram, MetricPrefix.Unprefixed);

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

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertPrefix(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null)
      => GetSiUnitValue(prefix).ToSiFormattedString(format, formatProvider) + UnicodeSpacing.ThinSpace.ToSpacingString() + prefix.GetMetricPrefixSymbol() + RadiationExposureUnit.CoulombPerKilogram.GetUnitSymbol();

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(RadiationExposureUnit unit, double value)
      => unit switch
      {
        RadiationExposureUnit.CoulombPerKilogram => value,

        _ => unit.GetUnitFactor() * value,
      };

    public static double ConvertToUnit(RadiationExposureUnit unit, double value)
      => unit switch
      {
        RadiationExposureUnit.CoulombPerKilogram => value,

        _ => value / unit.GetUnitFactor(),
      };

    public static double ConvertUnit(double value, RadiationExposureUnit from, RadiationExposureUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public double GetUnitValue(RadiationExposureUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(RadiationExposureUnit unit = RadiationExposureUnit.CoulombPerKilogram, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing spacing = UnicodeSpacing.Space, bool fullName = false)
    {
      var value = GetUnitValue(unit);

      return value.ToString(format, formatProvider) + spacing.ToSpacingString() + (fullName ? unit.GetUnitName(value.IsConsideredPlural()) : unit.GetUnitSymbol(false));
    }

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
