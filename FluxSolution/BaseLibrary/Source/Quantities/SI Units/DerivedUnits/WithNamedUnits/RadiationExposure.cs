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
    : System.IComparable, System.IComparable<RadiationExposure>, System.IFormattable, IUnitValueQuantifiable<double, RadiationExposureUnit>
  {
    private readonly double m_value;

    public RadiationExposure(double value, RadiationExposureUnit unit = RadiationExposureUnit.CoulombPerKilogram)
      => m_value = unit switch
      {
        RadiationExposureUnit.CoulombPerKilogram => value,
        RadiationExposureUnit.Röntgen => value / 3876,
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

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
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => ToUnitValueSymbolString(RadiationExposureUnit.CoulombPerKilogram, format, formatProvider);

    // IQuantifiable<>
    /// <summary>
    /// <para>The unit of the <see cref="RadiationExposure.Value"/> property is in <see cref="RadiationExposureUnit.CoulombPerKilogram"/>.</para>
    /// </summary>
    public double Value => m_value;

    // IUnitQuantifiable<>
    public string GetUnitName(RadiationExposureUnit unit, bool preferPlural)
      => unit.ToString() is var us && preferPlural ? us + GetUnitValue(unit).PluralStringSuffix() : us;

    public string GetUnitSymbol(RadiationExposureUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.RadiationExposureUnit.CoulombPerKilogram => "C/kg",
        Quantities.RadiationExposureUnit.Röntgen => "R",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(RadiationExposureUnit unit)
        => unit switch
        {
          RadiationExposureUnit.CoulombPerKilogram => m_value,
          RadiationExposureUnit.Röntgen => m_value * 3876,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

    public string ToUnitValueNameString(RadiationExposureUnit unit = RadiationExposureUnit.CoulombPerKilogram, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferPlural = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitName(unit, preferPlural));

    public string ToUnitValueSymbolString(RadiationExposureUnit unit = RadiationExposureUnit.CoulombPerKilogram, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false)
      => string.Concat(GetUnitValue(unit).ToString(format, formatProvider), unitSpacing.ToSpacingString(), GetUnitSymbol(unit, preferUnicode));

    #endregion Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
