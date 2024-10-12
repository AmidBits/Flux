namespace Flux.Quantities
{
  public enum LuminousIntensityUnit
  {
    Candela,
  }

  /// <summary>
  /// <para>Luminous intensity. SI unit of candela. This is a base quantity.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Luminous_intensity"/></para>
  /// </summary>
  public readonly record struct LuminousIntensity
    : System.IComparable, System.IComparable<LuminousIntensity>, System.IFormattable, ISiUnitValueQuantifiable<double, LuminousIntensityUnit>
  {
    private readonly double m_value;

    public LuminousIntensity(double value, LuminousIntensityUnit unit = LuminousIntensityUnit.Candela) => m_value = ConvertFromUnit(unit, value);

    /// <summary>
    /// <para>Creates a new instance from the specified <see cref="MetricPrefix"/> (metric multiple) of <see cref="AmountOfSubstanceUnit.Mole"/>, e.g. <see cref="MetricPrefix.Mega"/> for megacandelas.</para>
    /// </summary>
    /// <param name="candela"></param>
    /// <param name="prefix"></param>
    public LuminousIntensity(MetricPrefix prefix, double candela) => m_value = prefix.ConvertTo(candela, MetricPrefix.Unprefixed);

    #region Static methods
    #endregion // Static methods

    #region Overloaded operators

    public static bool operator <(LuminousIntensity a, LuminousIntensity b) => a.CompareTo(b) < 0;
    public static bool operator <=(LuminousIntensity a, LuminousIntensity b) => a.CompareTo(b) <= 0;
    public static bool operator >(LuminousIntensity a, LuminousIntensity b) => a.CompareTo(b) > 0;
    public static bool operator >=(LuminousIntensity a, LuminousIntensity b) => a.CompareTo(b) >= 0;

    public static LuminousIntensity operator -(LuminousIntensity v) => new(-v.m_value);
    public static LuminousIntensity operator +(LuminousIntensity a, double b) => new(a.m_value + b);
    public static LuminousIntensity operator +(LuminousIntensity a, LuminousIntensity b) => a + b.m_value;
    public static LuminousIntensity operator /(LuminousIntensity a, double b) => new(a.m_value / b);
    public static LuminousIntensity operator /(LuminousIntensity a, LuminousIntensity b) => a / b.m_value;
    public static LuminousIntensity operator *(LuminousIntensity a, double b) => new(a.m_value * b);
    public static LuminousIntensity operator *(LuminousIntensity a, LuminousIntensity b) => a * b.m_value;
    public static LuminousIntensity operator %(LuminousIntensity a, double b) => new(a.m_value % b);
    public static LuminousIntensity operator %(LuminousIntensity a, LuminousIntensity b) => a % b.m_value;
    public static LuminousIntensity operator -(LuminousIntensity a, double b) => new(a.m_value - b);
    public static LuminousIntensity operator -(LuminousIntensity a, LuminousIntensity b) => a - b.m_value;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is LuminousIntensity o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(LuminousIntensity other) => m_value.CompareTo(other.m_value);

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider) => ToSiUnitString(MetricPrefix.Unprefixed);

    #region ISiUnitValueQuantifiable<>

    public string GetSiUnitName(MetricPrefix prefix, bool preferPlural) => prefix.GetPrefixName() + GetUnitName(LuminousIntensityUnit.Candela, preferPlural);

    public string GetSiUnitSymbol(MetricPrefix prefix, bool preferUnicode) => prefix.GetPrefixSymbol(preferUnicode) + GetUnitSymbol(LuminousIntensityUnit.Candela, preferUnicode);

    public double GetSiUnitValue(MetricPrefix prefix) => MetricPrefix.Unprefixed.ConvertTo(m_value, prefix);

    public string ToSiUnitString(MetricPrefix prefix, bool fullName = false)
      => GetSiUnitValue(prefix).ToSiFormattedString() + UnicodeSpacing.ThinSpace.ToSpacingString() + (fullName ? GetSiUnitName(prefix, true) : GetSiUnitSymbol(prefix, false));

    #endregion // ISiUnitValueQuantifiable<>

    #region IUnitValueQuantifiable<>

    private static double ConvertFromUnit(LuminousIntensityUnit unit, double value)
      => unit switch
      {
        LuminousIntensityUnit.Candela => value,

        _ => GetUnitFactor(unit) * value,
      };

    private static double ConvertToUnit(LuminousIntensityUnit unit, double value)
      => unit switch
      {
        LuminousIntensityUnit.Candela => value,

        _ => value / GetUnitFactor(unit),
      };

    public static double ConvertUnit(double value, LuminousIntensityUnit from, LuminousIntensityUnit to) => ConvertToUnit(to, ConvertFromUnit(from, value));

    public static double GetUnitFactor(LuminousIntensityUnit unit)
      => unit switch
      {
        LuminousIntensityUnit.Candela => 1,

        _ => throw new System.NotImplementedException()
      };

    public string GetUnitName(LuminousIntensityUnit unit, bool preferPlural) => unit.ToString().ConvertUnitNameToPlural(preferPlural && GetUnitValue(unit).IsConsideredPlural());

    public string GetUnitSymbol(LuminousIntensityUnit unit, bool preferUnicode)
      => unit switch
      {
        LuminousIntensityUnit.Candela => preferUnicode ? "\u33C5" : "cd",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };

    public double GetUnitValue(LuminousIntensityUnit unit) => ConvertToUnit(unit, m_value);

    public string ToUnitString(LuminousIntensityUnit unit = LuminousIntensityUnit.Candela, string? format = null, System.IFormatProvider? formatProvider = null, bool fullName = false)
      => GetUnitValue(unit).ToString(format, formatProvider) + UnicodeSpacing.Space.ToSpacingString() + (fullName ? GetUnitName(unit, true) : GetUnitSymbol(unit, false));

    #endregion // IUnitValueQuantifiable<>

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The unit of the <see cref="LuminousIntensity.Value"/> property is in <see cref="LuminousIntensityUnit.Candela"/>.</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
