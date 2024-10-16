namespace Flux.Quantities
{
  public enum WavelengthUnit
  {
    /// <summary>This is the default unit for <see cref="Wavelength"/>.</summary>
    MeterPerRadian,
  }

  /// <summary>Surface tension, unit of Newton per meter.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Surface_tension"/>
  public readonly record struct Wavelength
    : System.IComparable, System.IComparable<Wavelength>, System.IFormattable, ISiUnitValueQuantifiable<double, WavelengthUnit>
  {
    private readonly double m_value;

    public Wavelength(double value, WavelengthUnit unit = WavelengthUnit.MeterPerRadian) => m_value = ConvertToUnit(unit, value);

    public Wavelength(MetricPrefix prefix, double meterPerRadian) => m_value = prefix.ConvertTo(meterPerRadian, MetricPrefix.Unprefixed);

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Wavelength a, Wavelength b) => a.CompareTo(b) < 0;
    public static bool operator <=(Wavelength a, Wavelength b) => a.CompareTo(b) <= 0;
    public static bool operator >(Wavelength a, Wavelength b) => a.CompareTo(b) > 0;
    public static bool operator >=(Wavelength a, Wavelength b) => a.CompareTo(b) >= 0;

    public static Wavelength operator -(Wavelength v) => new(-v.m_value);
    public static Wavelength operator +(Wavelength a, double b) => new(a.m_value + b);
    public static Wavelength operator +(Wavelength a, Wavelength b) => a + b.m_value;
    public static Wavelength operator /(Wavelength a, double b) => new(a.m_value / b);
    public static Wavelength operator /(Wavelength a, Wavelength b) => a / b.m_value;
    public static Wavelength operator *(Wavelength a, double b) => new(a.m_value * b);
    public static Wavelength operator *(Wavelength a, Wavelength b) => a * b.m_value;
    public static Wavelength operator %(Wavelength a, double b) => new(a.m_value % b);
    public static Wavelength operator %(Wavelength a, Wavelength b) => a % b.m_value;
    public static Wavelength operator -(Wavelength a, double b) => new(a.m_value - b);
    public static Wavelength operator -(Wavelength a, Wavelength b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Wavelength o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Wavelength other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToUnitString(WavelengthUnit.MeterPerRadian, format, formatProvider);

    #region ISiUnitValueQuantifiable<>

    public string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(WavelengthUnit.MeterPerRadian, preferPlural);

    public string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(WavelengthUnit.MeterPerRadian, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(WavelengthUnit unit, double value)
      => unit switch
      {
        WavelengthUnit.MeterPerRadian => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(WavelengthUnit unit, double value)
      => unit switch
      {
        WavelengthUnit.MeterPerRadian => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, WavelengthUnit from, WavelengthUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(WavelengthUnit unit)
      => unit switch
      {
        WavelengthUnit.MeterPerRadian => 1,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(WavelengthUnit unit, bool preferPlural)
      => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(WavelengthUnit unit, bool preferUnicode)
      => unit switch
      {
        WavelengthUnit.MeterPerRadian => "m/rad",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(WavelengthUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(WavelengthUnit unit = WavelengthUnit.MeterPerRadian, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Wavelength.Value"/> property is in <see cref="WavelengthUnit.MeterPerRadian"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
