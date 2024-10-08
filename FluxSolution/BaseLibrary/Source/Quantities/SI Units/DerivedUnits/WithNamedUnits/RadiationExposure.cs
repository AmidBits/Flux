namespace Flux.Quantities
{
  public enum RadiationExposureUnit
  {
    /// <summary>This is the default unit for <see cref="RadiationExposure"/>.</summary>
    CoulombPerKilogram,
    Röntgen
  }

  /// <summary>Force, unit of newton. This is an SI derived quantity.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Radiation_exposure"/>
  public readonly record struct RadiationExposure
    : System.IComparable, System.IComparable<RadiationExposure>, System.IFormattable, ISiUnitValueQuantifiable<double, RadiationExposureUnit>
  {
    private readonly double m_value;

    public RadiationExposure(double value, RadiationExposureUnit unit = RadiationExposureUnit.CoulombPerKilogram) => m_value = ConvertFromUnit(unit, value);

    #region Overloaded operators

    public static bool operator <(RadiationExposure a, RadiationExposure b) => a.CompareTo(b) < 0;
    public static bool operator <=(RadiationExposure a, RadiationExposure b) => a.CompareTo(b) <= 0;
    public static bool operator >(RadiationExposure a, RadiationExposure b) => a.CompareTo(b) > 0;
    public static bool operator >=(RadiationExposure a, RadiationExposure b) => a.CompareTo(b) >= 0;

    public static RadiationExposure operator -(RadiationExposure v) => new(-v.m_value);
    public static RadiationExposure operator +(RadiationExposure a, double b) => new(a.m_value + b);
    public static RadiationExposure operator +(RadiationExposure a, RadiationExposure b) => a + b.m_value;
    public static RadiationExposure operator /(RadiationExposure a, double b) => new(a.m_value / b);
    public static RadiationExposure operator /(RadiationExposure a, RadiationExposure b) => a / b.m_value;
    public static RadiationExposure operator *(RadiationExposure a, double b) => new(a.m_value * b);
    public static RadiationExposure operator *(RadiationExposure a, RadiationExposure b) => a * b.m_value;
    public static RadiationExposure operator %(RadiationExposure a, double b) => new(a.m_value % b);
    public static RadiationExposure operator %(RadiationExposure a, RadiationExposure b) => a % b.m_value;
    public static RadiationExposure operator -(RadiationExposure a, double b) => new(a.m_value - b);
    public static RadiationExposure operator -(RadiationExposure a, RadiationExposure b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is RadiationExposure o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(RadiationExposure other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(RadiationExposureUnit.CoulombPerKilogram, format, formatProvider);

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="RadiationExposure.Value"/> property is in <see cref="RadiationExposureUnit.CoulombPerKilogram"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region ISiUnitValueQuantifiable<>

    public string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(RadiationExposureUnit.CoulombPerKilogram, preferPlural);

    public string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(RadiationExposureUnit.CoulombPerKilogram, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitQuantifiable<>

    private static double ConvertFromUnit(RadiationExposureUnit unit, double value)
      => unit switch
      {
        RadiationExposureUnit.CoulombPerKilogram => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(RadiationExposureUnit unit, double value)
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
        RadiationExposureUnit.Röntgen => 1.0 / 3876.0,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(RadiationExposureUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(RadiationExposureUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.RadiationExposureUnit.CoulombPerKilogram => "C/kg",
        Quantities.RadiationExposureUnit.Röntgen => "R",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(RadiationExposureUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(RadiationExposureUnit unit = RadiationExposureUnit.CoulombPerKilogram, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion IUnitQuantifiable<>

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
