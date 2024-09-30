namespace Flux.Quantities
{
  public enum EnergyUnit
  {
    /// <summary>This is the default unit for <see cref="Energy"/>.</summary>
    Joule,
    ElectronVolt,
    Calorie,
    WattHour,
    KilowattHour
  }

  /// <summary>Energy unit of Joule.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Energy"/>
  public readonly record struct Energy
    : System.IComparable, System.IComparable<Energy>, System.IFormattable, ISiPrefixValueQuantifiable<double, EnergyUnit>
  {
    private readonly double m_value;

    public Energy(double value, EnergyUnit unit = EnergyUnit.Joule) => m_value = ConvertFromUnit(unit, value);

    #region Static methods

    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(Energy a, Energy b) => a.CompareTo(b) < 0;
    public static bool operator <=(Energy a, Energy b) => a.CompareTo(b) <= 0;
    public static bool operator >(Energy a, Energy b) => a.CompareTo(b) > 0;
    public static bool operator >=(Energy a, Energy b) => a.CompareTo(b) >= 0;

    public static Energy operator -(Energy v) => new(-v.m_value);
    public static Energy operator +(Energy a, double b) => new(a.m_value + b);
    public static Energy operator +(Energy a, Energy b) => a + b.m_value;
    public static Energy operator /(Energy a, double b) => new(a.m_value / b);
    public static Energy operator /(Energy a, Energy b) => a / b.m_value;
    public static Energy operator *(Energy a, double b) => new(a.m_value * b);
    public static Energy operator *(Energy a, Energy b) => a * b.m_value;
    public static Energy operator %(Energy a, double b) => new(a.m_value % b);
    public static Energy operator %(Energy a, Energy b) => a % b.m_value;
    public static Energy operator -(Energy a, double b) => new(a.m_value - b);
    public static Energy operator -(Energy a, Energy b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Energy o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Energy other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiPrefixString(MetricPrefix.Unprefixed);

    // ISiUnitValueQuantifiable<>
    public string GetSiPrefixName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(EnergyUnit.Joule, preferPlural);

    public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(EnergyUnit.Joule, preferUnicode);

    public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiPrefixString(MetricPrefix prefix, bool fullName = false)
      => GetSiPrefixValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiPrefixName(prefix, true) : GetSiPrefixSymbol(prefix, false));

    #region IQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="Energy.Value"/> property is in <see cref="EnergyUnit.Joule"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IQuantifiable<>

    #region IUnitQuantifiable<>

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

    public static double GetUnitFactor(EnergyUnit unit)
      => unit switch
      {
        EnergyUnit.Joule => 1,

        EnergyUnit.ElectronVolt => 1.602176634e-19,
        EnergyUnit.Calorie => 4.184,
        EnergyUnit.WattHour => 3.6e3,
        EnergyUnit.KilowattHour => 3.6e6,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(EnergyUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(EnergyUnit unit, bool preferUnicode)
      => unit switch
      {
        Quantities.EnergyUnit.Joule => "J",
        Quantities.EnergyUnit.ElectronVolt => "eV",
        Quantities.EnergyUnit.Calorie => preferUnicode ? "\u3388" : "cal",
        Quantities.EnergyUnit.WattHour => "W\u22C5h",
        Quantities.EnergyUnit.KilowattHour => "kW\u22C5h",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(EnergyUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(EnergyUnit unit = EnergyUnit.Joule, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
