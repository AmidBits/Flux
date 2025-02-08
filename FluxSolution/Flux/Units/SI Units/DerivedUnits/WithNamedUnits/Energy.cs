namespace Flux.Units
{
  /// <summary>
  /// <para>Energy, unit of Joule.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Energy"/></para>
  /// </summary>
  public readonly record struct Energy
    : System.IComparable, System.IComparable<Energy>, System.IFormattable, ISiUnitValueQuantifiable<double, EnergyUnit>
  {
    private readonly double m_value;

    public Energy(double value, EnergyUnit unit = EnergyUnit.Joule) => m_value = ConvertFromUnit(unit, value);

    public Energy(MetricPrefix prefix, double joule) => m_value = prefix.ConvertTo(joule, MetricPrefix.Unprefixed);

    public Energy(Pressure pressure, Volume volumn) => m_value = pressure.Value * volumn.Value;

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Energy a, Energy b) => a.CompareTo(b) < 0;
    public static bool operator >(Energy a, Energy b) => a.CompareTo(b) > 0;
    public static bool operator <=(Energy a, Energy b) => a.CompareTo(b) <= 0;
    public static bool operator >=(Energy a, Energy b) => a.CompareTo(b) >= 0;

    public static Energy operator -(Energy v) => new(-v.m_value);
    public static Energy operator *(Energy a, Energy b) => new(a.m_value * b.m_value);
    public static Energy operator /(Energy a, Energy b) => new(a.m_value / b.m_value);
    public static Energy operator %(Energy a, Energy b) => new(a.m_value % b.m_value);
    public static Energy operator +(Energy a, Energy b) => new(a.m_value + b.m_value);
    public static Energy operator -(Energy a, Energy b) => new(a.m_value - b.m_value);
    public static Energy operator *(Energy a, double b) => new(a.m_value * b);
    public static Energy operator /(Energy a, double b) => new(a.m_value / b);
    public static Energy operator %(Energy a, double b) => new(a.m_value % b);
    public static Energy operator +(Energy a, double b) => new(a.m_value + b);
    public static Energy operator -(Energy a, double b) => new(a.m_value - b);

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Energy o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Energy other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public static string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetMetricPrefixName() + GetUnitName(EnergyUnit.Joule, preferPlural);

    public static string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetMetricPrefixSymbol(preferUnicode) + GetUnitSymbol(EnergyUnit.Joule, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + Unicode.UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, GetSiUnitValue(prefix).IsConsideredPlural()) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    public static double ConvertFromUnit(EnergyUnit unit, double value)
      => unit switch
      {
        EnergyUnit.Joule => value,

        _ => GetUnitFactor(unit) * value,
      };

    public static double ConvertToUnit(EnergyUnit unit, double value)
      => unit switch
      {
        EnergyUnit.Joule => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, EnergyUnit from, EnergyUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(EnergyUnit unit)
      => unit switch
      {
        EnergyUnit.Joule => 1,

        EnergyUnit.ElectronVolt => ElectricCharge.ElementaryCharge,
        EnergyUnit.Calorie => 4.184,
        EnergyUnit.WattHour => 3.6e3,
        EnergyUnit.KilowattHour => 3.6e6,
        EnergyUnit.BritishThermalUnits => 1055.05585262,

        _ => throw new System.NotImplementedException()
      };

    public static string GetUnitName(EnergyUnit unit, bool preferPlural) => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(EnergyUnit unit, bool preferUnicode)
      => unit switch
      {
        EnergyUnit.Joule => "J",

        EnergyUnit.BritishThermalUnits => "BTU",
        EnergyUnit.ElectronVolt => "eV",
        EnergyUnit.Calorie => preferUnicode ? "\u3388" : "cal",
        EnergyUnit.WattHour => "W\u22C5h",
        EnergyUnit.KilowattHour => "kW\u22C5h",

        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(EnergyUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(EnergyUnit unit = EnergyUnit.Joule, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + Unicode.UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, GetUnitValue(unit).IsConsideredPlural()) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Energy.Value"/> property is in <see cref="EnergyUnit.Joule"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
